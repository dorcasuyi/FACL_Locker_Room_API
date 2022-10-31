using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FACL_Locker_Room_Core.DTOs
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }

        public static ResponseDto<T> Fail(string message, int statusCode = (int)HttpStatusCode.BadRequest)
        {
            return new ResponseDto<T> { Status = false, Message = message, StatusCode = statusCode };
        }

        public static ResponseDto<T> Success(string message, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new ResponseDto<T> { Status = true, Message = message, Data = data, StatusCode = statusCode };
        }
    }

}
