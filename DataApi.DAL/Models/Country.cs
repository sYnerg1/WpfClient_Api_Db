﻿using System;
using System.Collections.Generic;

namespace DataApi.DAL.Models
{
    public partial class Country
    {
        public Country()
        {
            Person = new HashSet<Person>();
        }

        public string Code { get; set; }
        public string Txt1 { get; set; }
        public string Txt2 { get; set; }
        public string Txt3 { get; set; }
        public string Txt4 { get; set; }
        public string IntDialCode { get; set; }
        public byte AddrFormatId { get; set; }
        public byte IsVatIncluded { get; set; }
        public byte Active { get; set; }

        public virtual ICollection<Person> Person { get; set; }
    }
}
