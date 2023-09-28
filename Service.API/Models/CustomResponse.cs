namespace Service.API.Models
{
    public class CustomResponse
    {
       
        public string Message { get; set; } // Not exist customer with ID = 3
        public int StatusCode { get; set; } // 500
        public string Details { get; set; } // internal Server error

        public CustomResponse() { }
        public CustomResponse(string message, int statusCode, string details)
        {
            Message = message;
            StatusCode = statusCode;
            Details = details;
        }

        public override string ToString()
        {
            return $" Message: {Message}\n StatusCode: {StatusCode}\n Details: {Details}\n";
        }

    }
}
