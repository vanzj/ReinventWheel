using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Tool
{
   public class SimpleFactory
    {
        private static string IRacTypeConfigReflection = ConfigurationManager.AppSettings["Read"];
        private static string DllName = IRacTypeConfigReflection.Split(',')[0];
        private static string TypeName = IRacTypeConfigReflection.Split(',')[1];
        private static IAbstractDataReaderHelper reader = null;
        private static readonly object _lock =new  object();
        public static IAbstractDataReaderHelper CreatDataReaderHelper()
        {
            if (reader == null)
            {
                lock (_lock)
                {
                    if (reader == null)
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        if (!string.IsNullOrEmpty(DllName))
                        {
                            assembly = Assembly.Load(DllName);
                        }
                        Type type = assembly.GetType(TypeName);
                         reader = Activator.CreateInstance(type) as IAbstractDataReaderHelper;
                    }
                }
            }
            return reader;
        }
    }
}
