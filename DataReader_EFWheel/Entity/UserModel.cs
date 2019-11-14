using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;

namespace DataReader_EFWheel.Entity
{
    [RenameAtrribute("user","用户")]
   public class UserModel:BaseModel
    {
        [RenameAtrribute("name",)]
        public string Name { get;  set; }
        [RenameAtrribute("num")]
        public string Num { get; set; }
        [RenameAtrribute("email")]
        public string Email { get; set; }
        [RenameAtrribute("remark")]
        public string Remark { get; set; }
    }
}
