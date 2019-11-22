using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;
using DataReader_EFWheel.Entity;
using MySql.Data.MySqlClient;

namespace DataReader_EFWheel.Tool
{
    public class DataReaderHelper: IAbstractDataReaderHelper
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
                 cmd.CommandText = BulidSql(id, type);
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
                             var propertyName = GetPropertyName(property);
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

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public int INSERT<T>(T t) where T : BaseModel
        {
            if (t == null)
                return 0;
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {

                if (property.IsDefined(typeof(IAttributeCheck), true))
                {
                    var temp = property.GetCustomAttributes(typeof(IAttributeCheck), true)
                        .FirstOrDefault(item => item is IAttributeCheck);
                    if (temp is IAttributeCheck)
                    {
                        IAttributeCheck attribute = temp as IAttributeCheck;
                        if (!attribute.Check(property.GetValue(t)))
                            return 0;
                    }
                }
            }

            Func<T, int> func = (a =>
             {
                 connection.Open(); //open the connection
               
                 string cmd = "INSERT INTO `" + getClassName(type) + "` ";
                 List<string> PropertyList = new List<string>();
                 List<string> ValueList = new List<string>();
                 foreach (var property in type.GetProperties())
                 {
                     if (GetPropertyName(property) == "id")
                     {
                         break;
                     }
                     if (property.GetValue(t) != null)
                     {
                         PropertyList.Add(GetPropertyName(property));
                         ValueList.Add("'" + property.GetValue(t).ToString() + "'");
                     }
                 }
                 var Properties = String.Join(",", PropertyList);
                 var Values = String.Join(",", ValueList);
                 cmd += "(" + Properties + ")" + " VALUES " + "(" + Values + ")";
                 MySqlCommand mysqlcom = new MySqlCommand(cmd, connection);
                 int count = mysqlcom.ExecuteNonQuery();
                 //    INSERT INTO `user` (name, num) VALUES('刘九', '1231564')
                 connection.Close();
                 return count;
             });
            return GenericCache<CallbackHelper>.GetCache().ThreadWithArgeWithRetrun(func).Invoke();
        }


        public int Delete<T>(int id) where T : BaseModel
        {
            if (Read<T>(id).Count > 0)
            {
                Func<T, int> func = (a =>
                {
                    //  DELETE FROM `user` WHERE id = 2
                    connection.Open(); //open the connection
                    Type type = typeof(T);
                    string cmd = "DELETE FROM  `" + getClassName(type) + "` " + " WHERE id = " + id.ToString();
                    MySqlCommand mysqlcom = new MySqlCommand(cmd, connection);
                    int count = mysqlcom.ExecuteNonQuery();
                    connection.Close();
                    return count;
                });
                return GenericCache<CallbackHelper>.GetCache().ThreadWithArgeWithRetrun(func).Invoke();
            }
            return 0;
        }


        public int Update<T>(T t) where T : BaseModel
        {
            if (t == null)
                return 0;
            Type type = typeof(T);
            foreach (var property in type.GetProperties())
            {
        
                if (property.IsDefined(typeof(IAttributeCheck), true))
                {
                    var temp = property.GetCustomAttributes(typeof(IAttributeCheck), true)
                        .FirstOrDefault(item => item is IAttributeCheck);
                    if (temp is IAttributeCheck)
                    {
                        IAttributeCheck attribute = temp as IAttributeCheck;
                        if (!attribute.Check(property.GetValue(t)))
                            return 0;
                    }
                }
            }


            int id = -1;
            foreach (var property in type.GetProperties())
            {
                if (GetPropertyName(property) == "id")
                {
                    if (property.GetValue(t) != null)
                    {
                        int.TryParse(property.GetValue(t).ToString(), out id);
                        break;
                    }
                }
            }

            if (id == -1)
                return 0;
            if (Read<T>(id).Count > 0)
            {

                Func<T, int> func = (a =>
                {
                    connection.Open(); //open the connection

                    string cmd = "UPDATE  `" + getClassName(type) + "` SET ";

                    // UPDATE Person SET Address = 'Zhongshan 23', City = 'Nanjing'

                    List<string> cmdPartList = new List<string>();
                    foreach (var property in type.GetProperties())
                    {
                        if (GetPropertyName(property) == "id")
                        {
                            break;
                        }
                        if (property.GetValue(t) != null)
                        {

                            cmdPartList.Add(GetPropertyName(property) + "=" + "'" + property.GetValue(t).ToString() + "'");
                        }
                    }
                    var cmdparts = String.Join(",", cmdPartList);

                    cmd += cmdparts + " WHERE id = " + id.ToString();
                    MySqlCommand mysqlcom = new MySqlCommand(cmd, connection);
                    int count = mysqlcom.ExecuteNonQuery();
                    //    INSERT INTO `user` (name, num) VALUES('刘九', '1231564')
                    connection.Close();
                    return count;
                });
                return GenericCache<CallbackHelper>.GetCache().ThreadWithArgeWithRetrun(func).Invoke();
            }

            return 0;


        }

        private string BulidSql(int id, Type type)
        {
            return "SELECT *  FROM `" + getClassName(type) + "` WHERE id = " + id.ToString() + ";";
        }


        private string getClassName(Type type)
        {
            var calssname = type.ToString();
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
            return calssname;
        }
        private string GetPropertyName(PropertyInfo property) 
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
            return propertyName;
        }
    }
}
