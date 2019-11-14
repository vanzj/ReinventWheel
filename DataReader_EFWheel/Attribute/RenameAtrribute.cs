using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader_EFWheel.Attribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class RenameAtrribute : System.Attribute
    {
        public  string ReName { get;  }

        public RenameAtrribute(string reName="",string des ="")
        {
            ReName = reName;
            Des = des;
        }
        public string Des { get; }

      
    }
}
