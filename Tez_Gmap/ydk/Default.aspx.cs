using System;
using System.Data;
using System.Text;
using System.Web.UI;
using Subgurim.Controles;
using Tez_Gmap.Araclar;
using System.Collections.Generic;

namespace Tez_Gmap
{
    public partial class _Default : Page
    {
        girisler g1 = new girisler();
        cikislar c1 = new cikislar();
        veri v1 = new veri();

        protected void Page_Load(object sender, EventArgs e)
        {
            //tracedata.Style.Add("Width", "0.1px");
            //tracedata.Style.Add("height", "0.1px");
            maxmin.Style.Add("Width", "0.1px");
            maxmin.Style.Add("height", "0.1px");
            int hZoom = 6;
            if (Session["ScreenResolution"] == null)
            {
                //İstemci ekran çözünürlüğünü tesbit et
                Response.Redirect("detectscreen.aspx");
            }
            else
            {
                //istemci ekran çözünürlüne göre harita büyüklüğü ve yakınlaşmayı  ayarla
                string[] cozun = Session["ScreenResolution"].ToString().Split('x');
                int hWidth = int.Parse(cozun[0]);
                int hHeight = int.Parse(cozun[1]);
                //ekran çözünürlüğüne göre haritayı boyutlandır
                GMap1.Width = hWidth - 50;
                GMap1.Height = (int)(hHeight * 0.50);
                uyarici1.Width = hHeight;


            }
            //harita açılışta Türkiye'ye odaklanır ((enlem, boylam), yakınlaşma)
            GMap1.setCenter(new GLatLng(38.92, 35.26), hZoom);
            //harita kontrolleri yerleşir
            GMap1.addControl(new GControl(GControl.preBuilt.MapTypeControl));//harita tipi düğmeleri
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl));//harita seyahat okları
            GMap1.addControl(new GControl(GControl.preBuilt.GOverviewMapControl));//kaybolmamak için köşeye bir mini-harita
            GMap1.addControl(new GControl(GControl.preBuilt.ScaleControl));//Haritaya ölçek ekler
            //arazi düğmesi ekle
            GMap1.mapType = GMapType.GTypes.Physical;
            GMap1.addMapType(GMapType.GTypes.Physical);
            GMap1.addControl(new GControl(GControl.preBuilt.MapTypeControl));
            //fare imlecini değiştir
            GCustomCursor customCursor = new GCustomCursor(cursor.crosshair, cursor.move);
            GMap1.addCustomCursor(customCursor);

            //İstemciden sorguyu oluşturacak kısıtlar geldiyse
            if (Session["_suzgec"] == null)
            {
                iPanel1.Visible = true;
                iPanel2.Visible = false;
                iSecAktif.Disabled = true;
                iSecPasif.Disabled = true;
                yerbilgial.Enabled = false;
                maxminValidator.Enabled = false;
            }
            else
            {
                iPanel1.Visible = false;
                iPanel2.Visible = true;
                iSecAktif.Disabled = false;
                iSecPasif.Disabled = false;
                iSecPasif.Checked = true;
                yerbilgial.Enabled = true;
                maxminValidator.Enabled = true;
                v1.vLimit = 0;

                string[] __suz = Session["_suzgec"].ToString().Split(';');
                g1.__yil1 = int.Parse(__suz[0]);
                g1.__yil2 = int.Parse(__suz[1]);
                g1.__m1 = (float)(double.Parse(__suz[2]));
                g1.__m2 = (float)(double.Parse(__suz[3]));
                v1.vLimit = int.Parse(__suz[4]);

                if (g1.__yil1 > g1.__yil2)
                {
                    int _tempYil = g1.__yil1;
                    g1.__yil1 = g1.__yil2;
                    g1.__yil2 = _tempYil;
                }
                if (g1.__m1 > g1.__m2)
                {
                    float _tempM = g1.__m1;
                    g1.__m1 = g1.__m2;
                    g1.__m2 = _tempM;
                }

                //Çeşitli büyüklükteki depremler için işaretçi oluşturma ve
                //İşaretçileri harita üzerine veritabanından çekilen
                //enlem ve boylam bilgilerine göre haritaya işleme

                v1.sorgu = "SELECT FORMAT([eqData]![GUN],'00') & \".\" & FORMAT([eqData]![AY],'00') & \".\" & [eqData]![YIL]";
                v1.sorgu += "AS TARIH, FORMAT([eqData]![SAAT],'00') & \":\" & FORMAT([eqData]![DAKIKA],'00') AS SAAT, ";
                v1.sorgu += "eqData.ENLEM, eqData.BOYLAM, eqData.YIL, eqData.DERINLIK, eqData.MAGNITUD FROM eqData ";
                v1.sorgu += "WHERE ( (eqData.YIL <= @p1 AND eqData.YIL >= @p2) ";
                v1.sorgu += "AND (eqData.MAGNITUD <= @p3 AND eqData.MAGNITUD >= @p4 ) ";
                v1.sorgu += "AND (eqData.ENLEM <= @p5 AND eqData.ENLEM >= @p6 ) ";
                v1.sorgu += "AND (eqData.BOYLAM <= @p7 AND eqData.BOYLAM >= @p8 ) )";
                v1.sorgu += "ORDER BY eqData.MAGNITUD";

                v1.verim = db.GetData(v1.sorgu, g1.__yil2, g1.__yil1, g1.__m2, g1.__m1, (double)42.35, (double)35.49, (double)44.51, (double)25);
                v1.vAdet = v1.verim.Rows.Count;

                uyarici1.Text = g1.__yil1 + " - " + g1.__yil2 + " yılları arasında ";
                uyarici1.Text += g1.__m1 + " - " + g1.__m2 + " büyüklükleri arasında gerçekleşen " + v1.vAdet.ToString() + " adet deprem seçildi.";
                if (v1.vAdet > v1.vLimit)
                {
                    uyarici1.Text += " Haritada sadece en yüksek büyüklükten itibaren son " + v1.vLimit.ToString() + " deprem gösteriliyor.";
                    v1.vBasla = v1.vAdet - v1.vLimit;
                }

                DataRow[] dr = new DataRow[v1.vAdet];
                v1.verim.Rows.CopyTo(dr, 0);
                v1.verim.Dispose();
                c1._enlem = Array.ConvertAll(dr, new Converter<DataRow, Double>(cevir.c_enlem));
                c1._boylam = Array.ConvertAll(dr, new Converter<DataRow, Double>(cevir.c_boylam));
                c1._magnitud = Array.ConvertAll(dr, new Converter<DataRow, Double>(cevir.c_magnitud));
                
                for (int i = 0; i < v1.vAdet; i++)
                {
                    c1.eqKoord.Add(new GLatLng((float)c1._enlem[i], (float)c1._boylam[i]));
                }

                for (int i = v1.vBasla; i < c1._magnitud.Length - 1; i++)
                {
                    GIcon eq_icon = new GIcon();
                    eq_icon.shadowSize = new GSize(0, 0);
                    if (c1._magnitud[i] >= (double)0 && c1._magnitud[i] <= (double)3.9)
                    {
                        eq_icon.image = "images/iki.png";
                        eq_icon.iconSize = new GSize(6, 6);
                        eq_icon.iconAnchor = new GPoint(3, 3);
                        eq_icon.infoWindowAnchor = new GPoint(4, 1);
                    
                    }
                    if (c1._magnitud[i] >= (double)4 && c1._magnitud[i] <= (double)5.9)
                    {
                        eq_icon.iconAnchor = new GPoint(5, 5);
                        eq_icon.infoWindowAnchor = new GPoint(9, 1);
                        eq_icon.image = "images/dort.png";
                        eq_icon.iconSize = new GSize(10, 10);
                    }
                    if ((double)6 <= c1._magnitud[i] && (double)8 >= c1._magnitud[i])
                    {
                        eq_icon.iconAnchor = new GPoint(5, 5);
                        eq_icon.infoWindowAnchor = new GPoint(9, 1);
                        eq_icon.image = "images/alti.png";
                        eq_icon.iconSize = new GSize(10, 10);
                    }
                    GMarkerOptions eq = new GMarkerOptions();
                    eq.icon = eq_icon;
                    GMarker eq_isrt = new GMarker(c1.eqKoord[i], eq);
                    GMap1.addGMarker(eq_isrt);
                }
                c1.eqKoord.Clear();
            }

            //alan seçme için istemci tarafında konacak
            //işaretçinin seçenekleri
            GMarkerOptions iSecenek = new GMarkerOptions();
            iSecenek.draggable = false;
            GMarker isrt = new GMarker();
            GIcon isrt_icon = new GIcon();
            isrt_icon.shadowSize = new GSize(0, 0);
            isrt_icon.iconAnchor = new GPoint(10, 10);
            isrt_icon.infoWindowAnchor = new GPoint(9, 1);
            isrt_icon.image = "images/push_pin.png";
            isrt_icon.iconSize = new GSize(20, 20);
            isrt_icon.shadowSize = new GSize(0, 0);
            isrt.options = iSecenek;
            iSecenek.icon = isrt_icon;
            isrt.javascript_GLatLng = "point";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("function(overlay, point) {");
            sb.Append("var benisay = dataparse(document.getElementById(\"tracedata\").value);");
            sb.Append("if(overlay){");
            sb.Append("}");
            sb.Append("else{");
            sb.Append("if (document.getElementById(\"iSecAktif\").checked) {");
            sb.Append(isrt.ToString(GMap1.GMap_Id));
            sb.Append("addpoint(point);");
            sb.Append("}}");
            sb.Append("}");
            GListener alici = new GListener(GMap1.GMap_Id, GListener.Event.click, sb.ToString());
            GMap1.addListener(alici);
        }

        protected void dataekle_Click(object sender, EventArgs e)
        {
            Session["_suzgec"] = yil1.Text + ";" + yil2.Text + ";" + magnitud1.Text + ";" + magnitud2.Text + ";" + MarkerLimit.SelectedValue.ToString();
            Response.Redirect("Default.aspx");
        }

        protected void Adim1_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void sifirla_Click(object sender, EventArgs e)
        {
            Session["_suzgec"] = null;
            Response.Redirect("Default.aspx");
        }

        protected void Adim2_Click(object sender, EventArgs e)
        {
            if (maxmin.Value != null)
            {
                string[] __suz = Session["_suzgec"].ToString().Split(';');
                g1.__yil1 = int.Parse(__suz[0]); g1.__yil2 = int.Parse(__suz[1]);
                g1.__m1 = (float)(double.Parse(__suz[2])); g1.__m2 = (float)(double.Parse(__suz[3]));
                if (g1.__yil1 > g1.__yil2)
                {
                    int _tempYil; _tempYil = g1.__yil1; g1.__yil1 = g1.__yil2; g1.__yil2 = _tempYil;
                }
                if (g1.__m1 > g1.__m2)
                {
                    float _tempM; _tempM = g1.__m1; g1.__m1 = g1.__m2; g1.__m2 = _tempM;
                }
                int hHeight = (int)GMap1.Height.Value;
                int wWidth = (int)GMap1.Width.Value;
                v1.vLimit = int.Parse(__suz[4]);
                ViewState.Clear();
                Session.Clear();
                Session["_adimiki"] = maxmin.Value.ToString() + ";" + g1.__yil1 + ";" + g1.__yil2 + ";" + g1.__m1
                    + ";" + g1.__m2 + ";" + hHeight + ";" + wWidth + ";" + v1.vLimit + ";" + tracedata.Value.ToString();
                Response.Redirect("adim2form.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void yerbilgial_CheckedChanged(object sender, EventArgs e)
        {
            if (yerbilgial.Checked) Session["yerbilgi"] = "1";
            else Session["yerbilgi"] = "0";
        }

        protected string GMap1_MarkerClick(object s, GAjaxServerEventArgs e)
        {
            double xc1 = Math.Round(e.point.lat, 4);
            double yc1 = Math.Round(e.point.lng, 4);

            string[] __suz = Session["_suzgec"].ToString().Split(';');
            g1.__yil1 = int.Parse(__suz[0]);
            g1.__yil2 = int.Parse(__suz[1]);
            g1.__m1 = (float)(double.Parse(__suz[2]));
            g1.__m2 = (float)(double.Parse(__suz[3]));

            if (g1.__yil1 > g1.__yil2)
            {
                int _tempYil; _tempYil = g1.__yil1; g1.__yil1 = g1.__yil2; g1.__yil2 = _tempYil;
            }
            if (g1.__m1 > g1.__m2)
            {
                float _tempM; _tempM = g1.__m1; g1.__m1 = g1.__m2; g1.__m2 = _tempM;
            }

            v1.sorgu = "SELECT eqData.ENLEM, eqData.BOYLAM, eqData.YIL, eqData.MAGNITUD, ";
            v1.sorgu += "eqData.DERINLIK, FORMAT([eqData]![GUN],'00') & \".\" & FORMAT([eqData]![AY],'00') & \".\" & [eqData]![YIL]";
            v1.sorgu += "AS TARIH, FORMAT([eqData]![SAAT],'00') & \":\" & FORMAT([eqData]![DAKIKA],'00') AS SAAT, eqData.YER FROM eqData ";
            v1.sorgu += "WHERE ( (eqData.YIL <= @p1 AND eqData.YIL >= @p2) ";
            v1.sorgu += "AND (eqData.MAGNITUD <= @p3 AND eqData.MAGNITUD >= @p4 ) ";
            v1.sorgu += "AND (eqData.ENLEM = @p5 AND eqData.BOYLAM = @p6) )";
            v1.verim = db.GetData(v1.sorgu, g1.__yil2, g1.__yil1, g1.__m2, g1.__m1, xc1, yc1);

            StringBuilder fb = new StringBuilder();
            fb.Append("<div align=\"left\">");
            fb.AppendFormat("Enlem: <i>{0}</i>,  Boylam: <i>{1}</i>", xc1, yc1);
            fb.Append("</div>");
            fb.Append("<div align=\"left\">");

            v1.dr = new DataRow[v1.verim.Rows.Count];
            v1.verim.Rows.CopyTo(v1.dr, 0);

            if (v1.verim.Rows.Count > 0)
            {
                c1._magnitud = Array.ConvertAll(v1.dr, new Converter<DataRow, Double>(cevir.c_magnitud));
                c1._derinlik = Array.ConvertAll(v1.dr, new Converter<DataRow, Double>(cevir.c_derinlik));
                c1._tarih = Array.ConvertAll(v1.dr, new Converter<DataRow, String>(cevir.c_tarih));
                c1._saat = Array.ConvertAll(v1.dr, new Converter<DataRow, String>(cevir.c_saat));
                c1._yer = Array.ConvertAll(v1.dr, new Converter<DataRow, String>(cevir.c_yer));
                for (int i = 0; i < v1.verim.Rows.Count; i++)
                {
                    fb.AppendFormat("<b>Deprem Merkez Üssü Bilgileri [{0}]</b>", (i + 1).ToString());
                    fb.AppendFormat("<br/>Tarih: <b>{0}</b> Saat: <b>{1}</b><br/>", c1._tarih[i], c1._saat[i]);
                    fb.AppendFormat("Büyüklük: <i><b>{0}</b></i> Derinlik: <i><b>{1}km</b></i><br/>Yer: {2}<br/>", c1._magnitud[i].ToString(), c1._derinlik[i].ToString(), c1._yer[i]);
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
                        fb.AppendFormat("<br/><b>İşaretçiye En Yakın Yer Bilgileri [{0}]</b>", iGeos.geonames.Count.ToString());
                        for (int i = 0; i < iGeos.geonames.Count; i++)
                        {
                            geo = iGeos.geonames[i];
                            fb.AppendFormat("<br />{0}", geo.name);
                            fb.AppendFormat(" ,{0} ({1})", geo.countryName, geo.countryCode);
                            fb.AppendFormat("<br />Rakım: <i>{0}m</i><br/>", geo.initialPointElevation > -9000 ? geo.initialPointElevation.ToString() : "...");
                        }
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
    }
}