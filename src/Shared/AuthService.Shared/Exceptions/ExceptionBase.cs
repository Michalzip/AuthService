
namespace AuthService.Shared.Exceptions
{
    /*
        * Base exception class for all further custom exceptions within all modules and integrations
        */
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase(string message) : base(message)
        {
        }

        protected ExceptionBase(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        /*
         * centrally assigned error code for tracing and localization
         */
        public abstract int StatusCode { get; }
    }

}