namespace API.Error
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statuscode, string message = null ,string details = null) : base(statuscode, message)
        {
            Details = details;
        }
        public string Details { set; get; }
    }
}
