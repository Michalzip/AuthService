
using AuthService.Shared.Auth;
using AuthService.Shared.Storage;

namespace AuthService.Modules.Core.Services
{
    public class UserRequestStorage : IUserRequestStorage
    {
        private readonly IRequestStorage _requestStorage;

        public UserRequestStorage(IRequestStorage requestStorage)
        {
            _requestStorage = requestStorage;
        }

        public void SetToken(Guid commandId, JsonWebToken jwt)
        => _requestStorage.SetCache(GetKey(commandId), jwt);

        public JsonWebToken GetToken(Guid commandId)
            => _requestStorage.GetCache<JsonWebToken>(GetKey(commandId));

        //convert guid to string 
        private static string GetKey(Guid commandId) => $"jwt:{commandId:N}";

    }
}