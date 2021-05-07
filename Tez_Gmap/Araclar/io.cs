/* Mehmet DURMAZ 01201529 - Mezuniyet Tezi 1 - 2008
 * Bu alt program istemci tarafından verilen parametreleri saklayan sınıfı içerir.
 */
using Subgurim.Controles;
using System.Collections.Generic; 

namespace Tez_Gmap.Araclar
{
    public class girisler
    {
        public float __x1;
        public float __x2;
        public float __y1;
        public float __y2;
        public int __yil1;
        public int __yil2;
        public float __m1;
        public float __m2;
        public List<GLatLng> polyKoord = new List<GLatLng>();
    }
    public class cikislar
    {
        public double[] _enlem;
        public double[] _boylam;
        public double[] _magnitud;
        public double[] _derinlik;
        public int[] _yil;
        public string[] _tarih;
        public string[] _saat;
        public string[] _yer;
        public List<GLatLng> eqKoord = new List<GLatLng>();
        public List<GLatLng> eqinPKoord = new List<GLatLng>();
        public List<double> magnitudL = new List<double>();
        public List<double> derinlikL = new List<double>();
        public List<int> yilL = new List<int>();
        public List<string> tarihL = new List<string>();
        public List<string> saatL = new List<string>();
        public List<string> yerL = new List<string>();
    }
    public class veri
    {
        public int vLimit;
        public int vAdet;
        public int vBasla;
        public string sorgu;
        public System.Data.DataTable verim;
        public System.Data.DataRow[] dr;
        public System.Data.DataRow[] drlast;    
    }    
}
