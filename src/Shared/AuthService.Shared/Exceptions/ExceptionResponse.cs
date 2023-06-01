namespace AuthService.Shared.Exceptions
{
    public class ExceptionResponse
    {
        public string Message { get; set; }

        public int StatusCode { get; set; }

        public ExceptionResponse(ExceptionBase exception)
        {
            Message = exception.Message;
            StatusCode = exception.StatusCode;
        }
    }
}

