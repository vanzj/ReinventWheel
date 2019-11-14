using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReader_EFWheel.Attribute;

namespace DataReader_EFWheel.Entity
{
   public class BaseModel
   {
        [RenameAtrribute("id")]
       public int Id { get; set; }

   }
}
