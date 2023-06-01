using Microsoft.Extensions.Caching.Memory;

namespace AuthService.Shared.Storage
{
    internal class RequestStorage : IRequestStorage
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session;

        public RequestStorage(IMemoryCache cache, IHttpContextAccessor httpContextAccessor)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public void SetCache<T>(string key, T value, TimeSpan? duration = null)
            => _cache.Set(key, value, duration ?? TimeSpan.FromMinutes(5));

        public T GetCache<T>(string key) => _cache.Get<T>(key);
 
        public void SetSession(string key, string value)
           =>  _session.SetString(key, value);

        public string GetSession(string key) => _session.GetString(key);
    }
}