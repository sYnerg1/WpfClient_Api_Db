using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class ContactDTO
    {
        public int PersonContactId { get; set; }
        public int ContactTypeId { get; set; }
        public string Txt { get; set; }
    }
}
