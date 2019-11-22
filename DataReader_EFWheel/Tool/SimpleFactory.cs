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

        public static IAbstractDataReaderHelper CreatDataReaderHelper()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            if (!string.IsNullOrEmpty(DllName))
            {
                assembly = Assembly.Load(DllName);
            }
            Type type = assembly.GetType(TypeName);
            var reader = Activator.CreateInstance(type);
            return reader as  IAbstractDataReaderHelper;
        }
    }
}
