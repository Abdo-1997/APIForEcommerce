using System;

namespace API.Error
{
    public class ApiResponse
    {

        public ApiResponse(int statuscode , string message = null)
        {
            StatusCode = statuscode;
            Message = message ??GetDefaultMessageForStatusCode(statuscode);
        }

       

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "A Bad Request ,You Have Made ",
                401 => "You Are Not Authorized",
                404 => "Resource Not Found ",
                500 => "server encountered an unexpected condition that prevented it from fulfilling the request",
                _ => null
            };
        }
    }
}
