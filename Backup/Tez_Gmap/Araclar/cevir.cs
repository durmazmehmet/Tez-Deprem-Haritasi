using System;
using System.Data;

namespace Tez_Gmap.Araclar
{
    public class cevir
    {     
        public static double c_enlem(DataRow dr) { return Convert.ToDouble(dr["ENLEM"].ToString()); }
        public static double c_boylam(DataRow dr) { return Convert.ToDouble(dr["BOYLAM"].ToString()); }
        public static int c_yil(DataRow dr) { return Convert.ToInt32(dr["YIL"].ToString()); }
        public static double c_magnitud(DataRow dr) { return Convert.ToDouble(dr["MAGNITUD"].ToString()); }
        public static double c_derinlik(DataRow dr) { return Convert.ToDouble(dr["DERINLIK"].ToString()); }
        public static string c_tarih(DataRow dr) { return Convert.ToString(dr["TARIH"].ToString()); }
        public static string c_saat(DataRow dr) { return Convert.ToString(dr["SAAT"].ToString()); }
        public static string c_yer(DataRow dr) { return Convert.ToString(dr["YER"].ToString()); }
    }
}