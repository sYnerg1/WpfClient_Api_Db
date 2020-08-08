using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class PagedPersonsDTO
    {
        public IEnumerable<PersonDTO> Persons { get; set; }
        public int Page { get; set; }
    }
}
