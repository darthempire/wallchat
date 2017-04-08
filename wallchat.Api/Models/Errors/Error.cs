using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wallchat.Api.Models.Errors
{
    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}