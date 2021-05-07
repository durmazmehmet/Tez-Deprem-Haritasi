using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Subgurim.Controles;
using Tez_Gmap.Araclar;
using ZedGraph;
using ZedGraph.Web;

namespace Tez_Gmap
{
    public partial class WebForm1 : Page
    {
        girisler g2 = new girisler();
        cikislar c2 = new cikislar();
        veri v2 = new veri();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["_adimiki"] == null)
                Response.Redirect("Default.aspx");
            else
            {
                string[] _adimiki = Session["_adimiki"].ToString().Split(';');
                g2.__yil1 = int.Parse(_adimiki[1]);
                g2.__yil2 = int.Parse(_adimiki[2]);
                g2.__m1 = (float)double.Parse(_adimiki[3]);
                g2.__m2 = (float)double.Parse(_adimiki[4]);
                GMap2.Height = int.Parse(_adimiki[5]);
                GMap2.Width = int.Parse(_adimiki[6]) - 300;
                ZedGraphWeb2.Height = int.Parse(_adimiki[5]);
                string[] x = _adimiki[0].ToString().Split('x');
                g2.__x1 = (float)Convert.ToDouble((x[0].Replace(".", ",")).Remove(7, x[0].Length - 7));
                g2.__y1 = (float)Convert.ToDouble((x[1].Replace(".", ",")).Remove(7, x[1].Length - 7));
                g2.__x2 = (float)Convert.ToDouble((x[2].Replace(".", ",")).Remove(7, x[2].Length - 7));
                g2.__y2 = (float)Convert.ToDouble((x[3].Replace(".", ",")).Remove(7, x[3].Length - 7));
                string[] bolbeni = new string[]{"\r\n"};
                string[] td_1 = _adimiki[7].ToString().Split(bolbeni, StringSplitOptions.RemoveEmptyEntries);
                float td_lat = new float();
                float td_lng = new float();
                string[] td_tmp = new string[2];
                for (int i = 0; i < td_1.Length; i++)
                {
                    td_tmp = td_1[i].Split(',');                    
                    td_lat = (float)Convert.ToDouble((td_tmp[0].Replace(".", ",")).Remove(7, td_tmp[0].Length - 7));
                    td_lng = (float)Convert.ToDouble((td_tmp[1].Replace(".", ",")).Remove(7, td_tmp[1].Length - 7));
                    g2.polyKoord.Add(new GLatLng(td_lat, td_lng));
                }

            }
            if (!IsPostBack)
            {
                v2.vLimit = 250;
            }
            else
            {
                v2.vLimit = int.Parse(MarkerLimit.SelectedValue);
            }

            //harita kontrolleri yerleşir
            GMap2.addControl(new GControl(GControl.preBuilt.MapTypeControl));//harita tipi düğmeleri
            GMap2.addControl(new GControl(GControl.preBuilt.LargeMapControl));//harita seyahat okları
            GMap2.addControl(new GControl(GControl.preBuilt.GOverviewMapControl));//kaybolmamak için köşeye bir mini-harita
            GMap2.addControl(new GControl(GControl.preBuilt.ScaleControl));//Haritaya ölçek ekler
            //arazi düğmesi ekle
            GMap2.mapType = GMapType.GTypes.Physical;
            GMap2.addMapType(GMapType.GTypes.Physical);
            GMap2.addControl(new GControl(GControl.preBuilt.MapTypeControl));
            //fare imlecini değiştir
            GCustomCursor customCursor = new GCustomCursor(cursor.crosshair, cursor.move);
            GMap2.addCustomCursor(customCursor);

            //Önceki sayfada seçilen bölgeye yakınlaş
            GLatLng gd = new GLatLng((float)g2.__x1, g2.__y1);
            GLatLng kb = new GLatLng((float)g2.__x2, g2.__y2);
            GLatLngBounds latlngbounds = new GLatLngBounds(gd, kb);
            GMap2.GZoom = GMap2.getBoundsZoomLevel(latlngbounds);
            GMap2.setCenter(latlngbounds.getCenter());

            if (Session["v2verim"] == null)
            {
                v2.sorgu = "SELECT * FROM eqData ";
                v2.sorgu += "WHERE ( (eqData.YIL <= @p1 AND eqData.YIL >= @p2) ";
                v2.sorgu += "AND (eqData.MAGNITUD <= @p3 AND eqData.MAGNITUD >= @p4 ) ";
                v2.sorgu += "AND (eqData.ENLEM <= @p5 AND eqData.ENLEM >= @p6 ) ";
                v2.sorgu += "AND (eqData.BOYLAM <= @p7 AND eqData.BOYLAM >= @p8 ) )";
                v2.sorgu += "ORDER BY eqData.MAGNITUD";
                v2.verim = db.GetData(v2.sorgu, g2.__yil2, g2.__yil1, g2.__m2, g2.__m1, g2.__x2, g2.__x1, g2.__y2, g2.__y1);
                v2.vAdet = v2.verim.Rows.Count;
                v2.dr = new DataRow[v2.vAdet];
                v2.verim.Rows.CopyTo(v2.dr, 0);

                //enlem ve boylam verilerini al
                c2._enlem = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_enlem));
                c2._boylam = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_boylam));

                //GLatLng sınıfı olarak koordinat takımları oluştur
                for (int i = 0; i < v2.vAdet; i++)
                {
                    c2.eqKoord.Add(new GLatLng((float)c2._enlem[i], (float)c2._boylam[i]));
                }
                //Poligon içindeki koordinat takımlarını elde et
                c2.eqinPKoord.AddRange(KPoligon(g2.polyKoord, c2.eqKoord));

                //Poligon dışında olan koordinatlara ait veri satırlarını silinecek veritablosunda diye mimle
                for (int i = 0; i < v2.vAdet; i++)
                {
                    GLatLng tmplatlng = c2.eqinPKoord.Find(o => o == c2.eqKoord[i]);
                    if (tmplatlng == null)
                        v2.verim.Rows[i].Delete();
                }                
                //veritablosunda mimlenen satırları sil
                v2.verim.AcceptChanges();
                //süzülmüş veritablosunu verisatırlarına kopyala
                v2.vAdet = v2.verim.Rows.Count;
                v2.dr = new DataRow[v2.vAdet];
                v2.verim.Rows.CopyTo(v2.dr, 0);
                //Yukarıdaki yoğun işlemi birdaha yapmamak için:
                Session["v2verim"] = v2.verim;
                Session["poligon"] = c2.eqinPKoord;
                v2.verim.Dispose();
                
            }
            else
            {
                v2.verim = (DataTable)Session["v2verim"];
                v2.vAdet = v2.verim.Rows.Count;
                v2.dr = new DataRow[v2.vAdet];
                v2.verim.Rows.CopyTo(v2.dr, 0);
                v2.verim.Dispose();
            }
            //Önceki sayfada seçilen alanı belirt
            GPolygon polygon = new GPolygon(g2.polyKoord, Color.Red, 1, 0.5, Color.Transparent, 0);
            GMap2.Add(polygon);

            if (v2.vAdet <= 0)
                uyarici1.Text = "Ciddi hata var!";
            else
            {
                //verileri dizeylere yükle
                c2._enlem = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_enlem));
                c2._boylam = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_boylam));
                c2._yil = Array.ConvertAll(v2.dr, new Converter<DataRow, Int32>(cevir.c_yil));
                c2._magnitud = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_magnitud));

                uyarici1.Text = g2.__yil1 + " - " + g2.__yil2 + " yılları arasında ";
                uyarici1.Text += g2.__m1 + " - " + g2.__m2 + " büyüklükleri arasında gerçekleşen " + v2.vAdet.ToString() + " adet deprem seçildi.";
                if (v2.vAdet > v2.vLimit)
                {
                    uyarici1.Text += " Haritada sadece en yüksek büyüklükten itibaren son " + v2.vLimit.ToString() + " deprem gösteriliyor. Harita üzerindeki depremlere tıklayarak ayrıntılı bilgi alabilirsiniz...";
                    v2.vBasla = v2.vAdet - v2.vLimit;
                }

                for (int i = v2.vBasla; i < c2._magnitud.Length - 1; i++)
                {
                    GIcon eq_icon = new GIcon();
                    eq_icon.shadowSize = new GSize(0, 0);
                    if (c2._magnitud[i] >= (double)0 && c2._magnitud[i] <= (double)3.9)
                    {
                        eq_icon.image = "images/iki.png";
                        eq_icon.iconSize = new GSize(6, 6);
                        eq_icon.iconAnchor = new GPoint(3, 3);
                        eq_icon.infoWindowAnchor = new GPoint(4, 1);

                    }
                    if (c2._magnitud[i] >= (double)4 && c2._magnitud[i] <= (double)5.9)
                    {
                        eq_icon.iconAnchor = new GPoint(5, 5);
                        eq_icon.infoWindowAnchor = new GPoint(9, 1);
                        eq_icon.image = "images/dort.png";
                        eq_icon.iconSize = new GSize(10, 10);
                    }
                    if ((double)6 <= c2._magnitud[i] && (double)8 >= c2._magnitud[i])
                    {
                        eq_icon.iconAnchor = new GPoint(5, 5);
                        eq_icon.infoWindowAnchor = new GPoint(9, 1);
                        eq_icon.image = "images/alti.png";
                        eq_icon.iconSize = new GSize(10, 10);
                    }
                    GMarkerOptions eq = new GMarkerOptions();
                    eq.icon = eq_icon;
                    GMarker eq_isrt = new GMarker(new GLatLng((float)c2._enlem[i], (float)c2._boylam[i]), eq);
                    GMap2.addGMarker(eq_isrt);
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////

                LQayarla.Text += "(Büyüklük Aralığı :" + c2._magnitud[0].ToString() + " - " + c2._magnitud[c2._magnitud.Length - 1].ToString() + ")";

                double kesme;
                if (Session["kesme"] != null)
                {
                    kesme = Convert.ToDouble(Session["kesme"]);
                    if (kesme < c2._magnitud[0]) kesme = c2._magnitud[0];
                    LQayarla2.Text = "En son girilen kesme büyüklüğü: " + kesme.ToString();
                }
                else
                    kesme = c2._magnitud[0];

                //Kumulatif grafik için absis değerleri
                int _mCAsize = Convert.ToInt32((c2._magnitud[c2._magnitud.Length - 1] - c2._magnitud[0]) / 0.1) + 1;
                List<double> _mCopy = new List<double>(_mCAsize);
                for (int i = 0; i < _mCAsize; i++)
                    _mCopy.Add(Math.Round((c2._magnitud[0] + (i * 0.1)), 1));
                _mCopy.Reverse();

                // büyüklük vektörünü içinden seçebilmek için listeye kaydet
                List<double> _magCopy = new List<double>(c2._magnitud.Length);
                _magCopy.AddRange(c2._magnitud);

                // yığınsal toplamı hesaplar:
                // bu işlemi üretilen _magCopy listesinin içinde denk gelen _mCopy'nin elemanlarının
                // adetini bulur ve bu adetleri kümülatif olarak kumulatif listesine ekler
                List<double> kumulatif = new List<double>();
                kumulatif.Add((_magCopy.FindAll(delegate(double p) { return p == _mCopy[0]; })).Count);
                for (int i = 1; i < _mCopy.Count; i++)
                    kumulatif.Add(_magCopy.FindAll(p => p.Equals(_mCopy[i])).Count + kumulatif[i - 1]);

                //grafik gösterimi listelerin dizeylere kopyalanması
                //ve a ve b kat sayılarının hesabı
                double[] cohLog = new double[3]; //kat sayıların saklanacağı dizey (logaritmik)
                double[] _mCopyA = new double[_mCopy.Count]; //deprem büyüklüklerinin saklanacağı dizey
                double[] _kumA = new double[_mCopy.Count]; //kümulatif toplamların saklanacağı dizey
                double[] _kumLog = new double[_mCopy.Count]; //kümulatif toplamların log değerlerinin saklanacağı dizey

                _mCopy.CopyTo(_mCopyA, 0);
                kumulatif.CopyTo(_kumA, 0);

                for (int i = 0; i < _kumA.Length; i++)
                    _kumLog[i] = Math.Log10(_kumA[i]);

                Session["graf_x"] = _mCopyA;
                Session["graf_y"] = _kumA;

                if (Session["kesme"] != null)
                {
                    // a ve b kat sayılarının hesabı
                    //kumulatif değerlerin hesaplanmı işlemini EKK için kısıtlanmış değerleri için yapar
                    int _mCAsizeLQ = Convert.ToInt32((c2._magnitud[c2._magnitud.Length - 1] - kesme) / 0.1) + 1;
                    List<double> _mCopyLQ = new List<double>(_mCAsizeLQ);
                    for (int i = 0; i < _mCAsizeLQ; i++)
                        _mCopyLQ.Add(Math.Round((kesme + (i * 0.1)), 1));
                    _mCopyLQ.Reverse();

                    List<double> kumulatifLQ = new List<double>();
                    kumulatifLQ.Add((_magCopy.FindAll(p => p == _mCopyLQ[0])).Count);
                    for (int i = 1; i < _mCopyLQ.Count; i++)
                        kumulatifLQ.Add(_magCopy.FindAll(p => p.Equals(_mCopyLQ[i])).Count + kumulatifLQ[i - 1]);

                    //Listeleri dizeylere bir kopyasını oluştur.
                    double[] _mCopyALQ = new double[_mCopyLQ.Count];
                    double[] _kumALQ = new double[_mCopyLQ.Count];
                    double[] _kumLogLQ = new double[_mCopyLQ.Count];

                    _mCopyLQ.CopyTo(_mCopyALQ, 0);
                    kumulatifLQ.CopyTo(_kumALQ, 0);

                    for (int i = 0; i < _kumALQ.Length; i++)
                        _kumLogLQ[i] = Math.Log10(_kumALQ[i]);

                    leastsquares.buildlinearleastsquares(ref _mCopyALQ, ref _kumLogLQ, _mCopyALQ.Length, ref cohLog[0], ref cohLog[1]);
                    cohLog[2] = _mCopyALQ[_mCopyALQ.Length - 1];
                }
                else
                {
                    leastsquares.buildlinearleastsquares(ref _mCopyA, ref _kumLog, _mCopyA.Length, ref cohLog[0], ref cohLog[1]);
                    cohLog[2] = _mCopyA[_mCopyA.Length - 1]; //en küçük büyüklük değerini parametre olarak gönder
                }
                // Katsayıların hesaplanmadığı takdirde işlemi kes hata mesajı göster
                if (double.IsNaN(cohLog[0]) || double.IsNaN(cohLog[1]))
                {
                    uyarici1.Text = "Verdiğiniz kesme büyüklüğü ile hesaplamada bir hata oluştu!!, sıfırlayıp tekrar deneyiniz...(Öneri: Sadece alt limiti değiştirmeyi deneyiniz";
                    ZedGraphWeb2.Visible = false;
                    mmaxLabel.Visible = false;
                    kisitHesap.Visible = false;
                    magx1.Visible = false;
                }
                else
                {
                    Session["graf_cohLog"] = cohLog;

                    ArrayList tabloson = new ArrayList();
                    double yilfark = Convert.ToDouble(g2.__yil2 - g2.__yil1 + 1);
                    double lognn;
                    double tc;
                    double[] pmt = new double[] { -1, -10, -20, -30, -50, -75, -100 };
                    string[] pmts = new string[7];
                    double mmaxLog = Math.Round((-1 * (cohLog[0] / cohLog[1])), 1);
                    for (double i = 4.0; i < mmaxLog; i = Math.Round((i + 0.5), 1))
                    {
                        //Log10N(M) = a + bM - log10(yilfarki)
                        lognn = (cohLog[0] - (-1 * cohLog[1] * i) - Math.Log10(yilfark));
                        lognn = Math.Pow(10, lognn);    //N = 10^^log10N                        
                        tc = ((double)1 / lognn);   //Tc = 1/N
                        for (int j = 0; j < pmts.Length; j++)
                            pmts[j] = "%" + (Math.Round((100 * (1 - Math.Exp((pmt[j] / tc)))), 1)).ToString();
                        tabloson.Add(new tabloyaBindir(i, Math.Round(tc, 1), pmts[0], pmts[1], pmts[2], pmts[3], pmts[4], pmts[5], pmts[6]));
                    }

                    //Log10N(M) = a + bMmax - log10(yilfarki)
                    lognn = (cohLog[0] - (-1 * cohLog[1] * mmaxLog) - Math.Log10(yilfark));
                    lognn = Math.Pow(10, lognn);    //N = 10^^log10N                    
                    tc = ((double)1 / lognn);   //Tc = 1/N
                    for (int j = 0; j < pmts.Length; j++)
                        pmts[j] = "%" + Math.Round((100 * (1 - Math.Exp((pmt[j] / tc)))), 1).ToString();
                    tabloson.Add(new tabloyaBindir(mmaxLog, Math.Round(tc, 1), pmts[0], pmts[1], pmts[2], pmts[3], pmts[4], pmts[5], pmts[6]));
                    //Tabloya yükle
                    DSonuclar.DataSource = tabloson;
                    DSonuclar.DataBind();
                    ZedGraphWeb2.RenderGraph += new ZedGraph.Web.ZedGraphWebControlEventHandler(OnRenderGraph2);
                }
            }
        }

        private void OnRenderGraph2(ZedGraphWeb zgw, Graphics g, MasterPane masterPane)
        {
            string[] _adimiki = Session["_adimiki"].ToString().Split(';');
            string[] x = _adimiki[0].ToString().Split('x');
            g2.__x1 = (float)Convert.ToDouble((x[0].Replace(".", ",")).Remove(7, x[0].Length - 7));
            g2.__y1 = (float)Convert.ToDouble((x[1].Replace(".", ",")).Remove(7, x[1].Length - 7));
            g2.__x2 = (float)Convert.ToDouble((x[2].Replace(".", ",")).Remove(7, x[2].Length - 7));
            g2.__y2 = (float)Convert.ToDouble((x[3].Replace(".", ",")).Remove(7, x[3].Length - 7));
            g2.__yil1 = int.Parse(_adimiki[1]);
            g2.__yil2 = int.Parse(_adimiki[2]);
            double yilfark = Convert.ToDouble(g2.__yil2 - g2.__yil1 + 1);

            GraphPane myPane = masterPane[0];
            myPane.Title.Text = "logN(M) = f(M) Grafiği";
            myPane.XAxis.Title.Text = "Deprem Büyüklüğü M";
            myPane.YAxis.Title.Text = "Yığınsal Deprem Sayısı logN(M)";

            double[] xx = (double[])Session["graf_x"];
            double[] yy = (double[])Session["graf_y"];
            double[] cohLog = (double[])Session["graf_cohLog"];
            double mmaxLog = -1 * (cohLog[0] / cohLog[1]);

            // Legand için hazırlık
            string Logsa = cohLog[0].ToString();
            string LogsaFix = (cohLog[0] - Math.Log10(yilfark)).ToString();
            string Logsb = (-1 * cohLog[1]).ToString();
            string Smmax = mmaxLog.ToString();
            
            Logsa = Logsa.Remove(4, Logsa.Length - 4);
            LogsaFix = LogsaFix.Remove(4, LogsaFix.Length - 4);
            Logsb = Logsb.Remove(4, Logsb.Length - 4);
            Smmax = Smmax.Remove(3, Smmax.Length - 3);

            mmaxLabel.Text = "Aşağıdaki tablo 1 yıla indirgenmiş LogN(M) = " + LogsaFix + "-" + Logsb + " M bağıntısı ile seçilen bölgede (KuzeyBatı: " + g2.__x1 + " , " + g2.__y1 + " - GüneyDoğu: " + g2.__x2 + " , " + g2.__y2 + " ) olabilecek çeşitli büyüklüklerdeki depremlerin yıllık periyotları ve 1, 10, 20, 30, 50, 75 ve 100 yılda gerçekleşme olasılıklarını gösterir: (Mmax: " + Smmax + ")";

            // Grafiklere eklenecek Listeler
            // Kümulatif Toplam gösterimi için elemanlar
            PointPairList nmnf = new PointPairList();
            for (int i = 0; i < xx.Length; i++)
                nmnf.Add(xx[i], yy[i]);

            // Polinom oturtma gösterimi için elemanlar
            PointPairList Logpfit = new PointPairList();
            Logpfit.Add(mmaxLog, 0);
            Logpfit.Add(cohLog[2], (cohLog[0] + (cohLog[1] * cohLog[2])));

            // Kümulatif Toplamların Grafiği
            LineItem myCurve = myPane.AddCurve(g2.__yil1.ToString() + " - " + g2.__yil2.ToString()
                + " Yılları Arası", nmnf, Color.Blue, SymbolType.Circle);
            myCurve.Line.IsVisible = false;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MinorGrid.IsVisible = true;
            myPane.YAxis.Type = AxisType.Log;

            // Polinom oturtma (log kaynaklı, lineer gösterim)
            myCurve = myPane.AddCurve("logN(M) = " + Logsa + " - " + Logsb + "M", Logpfit, Color.Red,
                SymbolType.None);
            myCurve.IsY2Axis = true;
            myCurve.Line.Width = 2;
            myPane.Y2Axis.Type = AxisType.Linear;
            myPane.Y2Axis.IsVisible = false;

            masterPane.AxisChange(g);
        }

        protected void kisitHesap_Click(object sender, EventArgs e)
        {
            double kesme = Convert.ToDouble(magx1.Text);
            Session["kesme"] = kesme;            
            Response.Redirect("adim2form.aspx");
        }


        protected void RWsifirla_Click(object sender, EventArgs e)
        {
            Session["kesme"] = null;
            Response.Redirect("adim2form.aspx");
        }

        protected void basadon_Click(object sender, EventArgs e)
        {
            Session.RemoveAll(); ;
            Response.Redirect("Default.aspx");
        }

        protected string GMap2_MarkerClick(object s, GAjaxServerEventArgs e)
        {
            double xc1 = Math.Round(e.point.lat, 4);
            double yc1 = Math.Round(e.point.lng, 4);

            string[] _adimiki = Session["_adimiki"].ToString().Split(';');
            g2.__yil1 = int.Parse(_adimiki[1]);
            g2.__yil2 = int.Parse(_adimiki[2]);
            g2.__m1 = (float)double.Parse(_adimiki[3]);
            g2.__m2 = (float)double.Parse(_adimiki[4]);

            if (g2.__yil1 > g2.__yil2)
            {
                int _tempYil; _tempYil = g2.__yil1; g2.__yil1 = g2.__yil2; g2.__yil2 = _tempYil;
            }
            if (g2.__m1 > g2.__m2)
            {
                float _tempM; _tempM = g2.__m1; g2.__m1 = g2.__m2; g2.__m2 = _tempM;
            }

            v2.sorgu = "SELECT eqData.ENLEM, eqData.BOYLAM, eqData.YIL, eqData.MAGNITUD, ";
            v2.sorgu += "eqData.DERINLIK, FORMAT([eqData]![GUN],'00') & \".\" & FORMAT([eqData]![AY],'00') & \".\" & [eqData]![YIL]";
            v2.sorgu += "AS TARIH, FORMAT([eqData]![SAAT],'00') & \":\" & FORMAT([eqData]![DAKIKA],'00') AS SAAT, eqData.YER FROM eqData ";
            v2.sorgu += "WHERE ( (eqData.YIL <= @p1 AND eqData.YIL >= @p2) ";
            v2.sorgu += "AND (eqData.MAGNITUD <= @p3 AND eqData.MAGNITUD >= @p4 ) ";
            v2.sorgu += "AND (eqData.ENLEM = @p5 AND eqData.BOYLAM = @p6) )";
            v2.verim = db.GetData(v2.sorgu, g2.__yil2, g2.__yil1, g2.__m2, g2.__m1, xc1, yc1);

            StringBuilder fb = new StringBuilder();
            fb.Append("<div align=\"left\">");
            fb.Append("<b>İşaretçi Kordinatları</b><br/>");
            fb.AppendFormat("Enlem: <i>{0}</i>,  Boylam: <i>{1}</i>", xc1.ToString(), yc1.ToString());
            fb.Append("</div>");
            fb.Append("<div align=\"left\">");

            if (v2.verim.Rows.Count > 0)
            {
                v2.dr = new DataRow[v2.verim.Rows.Count];
                v2.verim.Rows.CopyTo(v2.dr, 0);
                c2._magnitud = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_magnitud));
                c2._derinlik = Array.ConvertAll(v2.dr, new Converter<DataRow, Double>(cevir.c_derinlik));
                c2._tarih = Array.ConvertAll(v2.dr, new Converter<DataRow, String>(cevir.c_tarih));
                c2._saat = Array.ConvertAll(v2.dr, new Converter<DataRow, String>(cevir.c_saat));
                c2._yer = Array.ConvertAll(v2.dr, new Converter<DataRow, String>(cevir.c_yer));                
                for (int i = 0; i < v2.verim.Rows.Count; i++)
                {
                    fb.AppendFormat("<b>Deprem Merkez Üssü Bilgileri [{0}]</b>", (i + 1).ToString());
                    fb.AppendFormat("<br>Tarih: <b>{0}</b> Saat: <b>{1}</b><br/>", c2._tarih[i], c2._saat[i]);
                    fb.AppendFormat("Büyüklük: <i><b>{0}</b></i> Derinlik: <i><b>{1}km</b></i><br/>Yer: {2}<br/>", c2._magnitud[i].ToString(), c2._derinlik[i].ToString(), c2._yer[i]);
                }


            }
            else
            {
                fb.Append("Tıklanan noktada verilen kısıtlara göre<br/>gösterilecek deprem kaydı bulunamadı.<br/>Biraz yakınlaştırıp tekrar deneyebilirsin...");
            }

            if (Session["yerbilgi"] != null)
            {
                if (Session["yerbilgi"].ToString().Contains("1"))
                {
                    inverseGeocodingManager igeoManager = new inverseGeocodingManager(e.point, "es");
                    igeoManager.language = "tr";
                    inverseGeocoding iGeos = new inverseGeocoding();
                    iGeos = igeoManager.inverseGeoCodeRequest();                    
                    geoName geo;

                    if (iGeos.geonames.Count > 0)
                    {
                        geo = iGeos.geonames[0];
                        fb.AppendFormat("<br/><b>İşaretçiye En Yakın Yer Bilgileri [{0}]</b>", iGeos.geonames.Count.ToString());
                        fb.AppendFormat("<br />{0}", geo.name);
                        fb.AppendFormat(" ,{0} ({1})", geo.countryName, geo.countryCode);
                        fb.AppendFormat("<br />Rakım: <i>{0}m</i>", geo.initialPointElevation > -9000 ? geo.initialPointElevation.ToString() : "...");
                    }
                    else
                    {
                        fb.Append("<br/><i>Bu noktaya ait yer bilgisi bulunamadı...<br/>(muhtemelen açık denizdeyiz)</i>");
                    }
                }
            }
            GInfoWindow window = new GInfoWindow(e.point, fb.ToString(), true);
            return window.ToString(e.map);
        }

        protected void yerbilgial_CheckedChanged(object sender, EventArgs e)
        {
            if (yerbilgial.Checked) Session["yerbilgi"] = "1";
            else Session["yerbilgi"] = "0";

        }
        public List<GLatLng> KPoligon(List<GLatLng> PoliKoords, List<GLatLng> Koords)
        {
            List<GLatLng> suzulenKoord = new List<GLatLng>();
            PoliKoords.Add(PoliKoords[0]);

            int x = 0;
            int valor = 0;
            foreach (GLatLng Koord in Koords)
            {
                x = 0;
                valor = 0;
                while (x + 1 < PoliKoords.Count)
                {
                    if (IsOnRigth(PoliKoords[x], PoliKoords[x + 1], Koord))
                        valor++;
                    x++;
                }

                //Eğer nokta poligon içinde ise valor tek sayıdır
                if ((valor % 2) != 0)
                    suzulenKoord.Add(Koord);
            }
            return suzulenKoord;
        }
        private Boolean IsOnRigth(GLatLng PolyPointA, GLatLng PolyPointB, GLatLng point)
        {
            double M = 0;
            double LngInFunction = 0;
            //Eğer nokta iki enlem arasında ise
            if ((PolyPointA.lat >= point.lat && PolyPointB.lat <= point.lat) || (PolyPointB.lat >= point.lat && PolyPointA.lat <= point.lat))
            {
                M = (PolyPointA.lat - PolyPointB.lat) / (PolyPointA.lng - PolyPointB.lng);
                LngInFunction = ((point.lat - PolyPointA.lat) / M) + PolyPointA.lng;
                if (LngInFunction <= point.lng)
                    return true;
            }
            return false;
        }
    }
}