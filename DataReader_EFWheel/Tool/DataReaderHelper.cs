using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;
using DataReader_EFWheel.Entity;
using MySql.Data.MySqlClient;

namespace DataReader_EFWheel.Tool
{
    public class DataReaderHelper
    {
        private MySqlCommand cmd = null;
        private MySqlDataReader reader = null;
        private MySqlConnection connection = null;
        public DataReaderHelper()
        {
            try
            {
                string constr = ConfigurationManager.AppSettings["DBInfo"];
                connection = new MySqlConnection(constr);//use Sqlconnection to connect the data source
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<T> Read<T>(int id) where T : BaseModel
        {

            return GenericCache<CallbackHelper>.GetCache().ThreadWithReturn(() =>
             {
                 connection.Open(); //open the connection
                 cmd = connection.CreateCommand();

                 Type type = typeof(T);
         
                 cmd.CommandText = BulidSql(id,type);
                 MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 mda.Fill(dt);
                 connection.Close();
                 List<T> tlist = new List<T>();

                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     T t = Activator.CreateInstance<T>();
                     for (int j = 0; j < dt.Columns.Count; j++)
                     {
                         foreach (var property in type.GetProperties())
                         {
                             string propertyName = property.Name.ToString();

                             if (property.IsDefined(typeof(RenameAtrribute), true))
                             {
                                 var temp = property.GetCustomAttributes(typeof(RenameAtrribute), true)
                                     .FirstOrDefault(item => item is RenameAtrribute);
                                 if (temp is RenameAtrribute)
                                 {
                                     RenameAtrribute attribute = temp as RenameAtrribute;
                                     propertyName = attribute.ReName;
                                 }

                             }
                             if (dt.Columns[j].ColumnName == propertyName)
                             {
                                 if (dt.Rows[i][j] != DBNull.Value)
                                 {
                                     property.SetValue(t, dt.Rows[i][j]);
                                 }
                                 else
                                 {
                                     property.SetValue(t, null);
                                 }
                                 break;
                             }
                         }
                     }
                     tlist.Add(t);
                 }
                 return tlist;
             }).Invoke();

        }

        private static string BulidSql(int id,Type type)
        {
            var calssname = "";
  
            calssname = type.ToString();
            if (type.IsDefined(typeof(RenameAtrribute), true))
            {
                var temp = type.GetCustomAttributes(typeof(RenameAtrribute), true)
                    .FirstOrDefault(item => item is RenameAtrribute);
                if (temp is RenameAtrribute)
                {
                    RenameAtrribute attribute = temp as RenameAtrribute;
                    calssname = attribute.ReName;
                }
            }
            return "SELECT *  FROM `" + calssname + "` WHERE id = " + id.ToString() + ";";
        }
    }
}
