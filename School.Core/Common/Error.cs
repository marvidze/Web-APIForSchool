using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Common
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public Error(string code, string message, int statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
