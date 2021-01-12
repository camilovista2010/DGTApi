using System;
using System.Collections.Generic;
using System.Text;

namespace DGT.Models.Base
{
    public class Error
    {
        public string msnError { get; set; } 

        public enum Level
        {
            Low,
            Medium,
            High
        }

        public int statusCode { get; set; }

    }
}
