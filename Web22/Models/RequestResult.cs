using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web22.Enums;

namespace Web22.Models
{
    public class RequestResult
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
    }
}
