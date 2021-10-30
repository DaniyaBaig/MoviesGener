using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Status = System.Net.HttpStatusCode;

namespace Mechanic.API.Models
{
    public class Response
    {
        public int Code { get; }
        public string Message { get; }
        public DateTime Date { get; set; }
        public object Data { get; }

        public Response(Status code = Status.OK, string message = null, object data = null)
        {
            Code = (int)code;
            Message = message;
            Date = DateTime.UtcNow;
            Data = data;
        }
    }
}
