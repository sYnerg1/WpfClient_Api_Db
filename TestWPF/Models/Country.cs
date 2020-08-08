using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWPF.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Txt1 { get; set; }
        public string Txt2 { get; set; }
        public string Txt3 { get; set; }
        public string Txt4 { get; set; }
        public string IntDialCode { get; set; }
        public byte AddrFormatId { get; set; }
        public byte IsVatIncluded { get; set; }
        public byte Active { get; set; }
    }
}
