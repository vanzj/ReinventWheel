using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;

namespace DataReader_EFWheel.Entity
{
    [RenameAtrribute("company")]
    public class CompanyModel : BaseModel
    {
        [RenameAtrribute("name")]
        public string Name { get; set; }
        [RenameAtrribute("address")]
        public string Address { get; set; }
        [RenameAtrribute("telePhone")]
        public string TelePhone { get; set; }
        [RenameAtrribute("email")]
        public string Email { get; set; }
    }
}
