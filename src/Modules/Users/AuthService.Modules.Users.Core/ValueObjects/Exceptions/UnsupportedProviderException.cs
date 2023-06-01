
using AuthService.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;

namespace AuthService.Modules.Core.ValueObjects.Exceptions
{
    public class UnsupportedProviderException : ExceptionBase
    {
        public UnsupportedProviderException(string name) : base($"An provider with this name: '{name}' is not supported.")
        {
        }
        public override int StatusCode => StatusCodes.Status404NotFound;
    }
}