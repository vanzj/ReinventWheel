using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;

namespace DataReader_EFWheel.Entity
{
    [RenameAtrribute("company","公司")]
    public class CompanyModel : BaseModel
    {
        [RequiredCheck]
        [RenameAtrribute("name", "公司名")]
        public string Name { get; set; }
        [RequiredCheck]
        [RenameAtrribute("address", "地址")]
        public string Address { get; set; }
        [RenameAtrribute("telePhone", "电话号码")]
        public string TelePhone { get; set; }
        [RenameAtrribute("email", "电子邮箱")]
        public string Email { get; set; }
     
    }
}
