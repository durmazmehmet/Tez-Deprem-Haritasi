/* Mehmet DURMAZ 01201529 - Mezuniyet Tezi 1 - 2008
 * Bu alt program veri tabanından verilen sorgu ile veri çeker yada işlem yapar
 *  
 * GetData(sorgu, [parametreler]) -> verilen sorgu içindeki @p.. öntakılı parametreler
 * ile verilen [parametreleri eşleştirir ve gelen veriye GetData DataTable olarak
 * yükler.
 * 
 * ExecuteSql(sorgu, [parametreler]) -> verilen sorgu içindeki @p.. öntakılı parametreler
 * ile verilen [parametreleri eşleştirir ve sorgu içerisinde belirtilen işlemi yapar, 
 * dışarıya veri göndermez.
*/
using System.Data;
using System.Data.OleDb;

namespace Tez_Gmap.Araclar
{
    public class db
    {
        public static DataTable GetData(string sql, params object[] values)
        {
            string _constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\deprem.mdb;Persist Security Info=True";
            OleDbConnection _cn = new OleDbConnection(_constr);
            if (_cn.State == ConnectionState.Closed) _cn.Open();
            OleDbCommand _cmd = new OleDbCommand(sql, _cn);
            for (int i = 0; i < values.Length; i++)
            {
                _cmd.Parameters.AddWithValue("@p", values[i]);
            }
            DataTable _result = new DataTable();
            OleDbDataReader _reader = _cmd.ExecuteReader();
            _result.Load(_reader);
            _cn.Close();
            return _result;
        }

        //public static void ExecuteSql(string sql,params object[] values)
        //{
        //    string _constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\deprem.mdb;Persist Security Info=True";
        //    OleDbConnection _cn = new OleDbConnection(_constr);
        //    if (_cn.State == ConnectionState.Closed) _cn.Open();
        //    OleDbCommand _cmd = new OleDbCommand(sql, _cn);
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        _cmd.Parameters.AddWithValue("@p", values[i]);
        //    }
        //    _cmd.ExecuteNonQuery();
        //    _cn.Close();
            
        //}

    }
}
