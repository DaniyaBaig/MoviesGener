using Mechanic.ErrorLogging.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.ErrorLogging.Models
{
    public class ErrorLog
    {
        public int ID { get; set; }
        public int ParentErrorLogId { get; set; }
        public DateTime Date { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string MethodName { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public string Server { get; set; }
        public string AdditionalInfo { get; set; }
        public OriginType Origin { get; set; }
    }
}
