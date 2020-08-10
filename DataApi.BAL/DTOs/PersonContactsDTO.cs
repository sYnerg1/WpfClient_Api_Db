using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class PersonContactsDTO
    {
        public int PersonId { get; set; }
        public Dictionary<int,string> Contacts { get; set; }
    }
}
