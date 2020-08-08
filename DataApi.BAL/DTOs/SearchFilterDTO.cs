using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class SearchFilterDTO
    {
        public int Page { get; set; }

        public string SearchText { get; set; }
    }
}
