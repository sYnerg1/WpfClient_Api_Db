﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataApi.BAL.DTOs
{
    public class GreetingDTO
    {
        public int Id { get; set; }
        public string Txt1 { get; set; }
        public string Txt2 { get; set; }
        public string Txt3 { get; set; }
        public string Txt4 { get; set; }
        public string TxtLetter1 { get; set; }
        public string TxtLetter2 { get; set; }
        public string TxtLetter3 { get; set; }
        public string TxtLetter4 { get; set; }
        public byte Active { get; set; }
    }
}
