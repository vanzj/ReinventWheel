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
        [RenameAtrribute("name","用户名")]
        public string Name { get;  set; }
        [RenameAtrribute("num","号码")]
        public string Num { get; set; }
        [RenameAtrribute("email","电子邮箱")]
        public string Email { get; set; }
        [RenameAtrribute("remark","备注")]
        public string Remark { get; set; }
    }
}
