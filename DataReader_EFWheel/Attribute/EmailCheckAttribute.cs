using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Attribute
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]

    public class EmailCheckAttribute : System.Attribute, IAttributeCheck
    {
        public bool Check(object t)
        {
            string email = t.ToString();
            Regex regex = new Regex(@"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$");
            return regex.Match(email).Success;
        }
    }
}
