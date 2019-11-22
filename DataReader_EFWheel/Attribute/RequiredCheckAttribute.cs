using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Attribute
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
  public  class RequiredCheckAttribute:System.Attribute, IAttributeCheck
    {
        public bool Check(object t)
        {
            Type type = Type.GetType(t.ToString());
            if (type.IsDefined(typeof(RequiredCheckAttribute), true))
            {

                return t != null;
            }
            return true;
        }
    }
}
