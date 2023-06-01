
using AuthService.Modules.Core.ValueObjects.Exceptions;

namespace AuthService.Modules.Core.ValueObjects
{
    public static class AvailableProviders
    {
        public static readonly Provider Facebook = Provider.Create(nameof(Facebook));
        public static readonly Provider Google = Provider.Create(nameof(Google));

        private static readonly HashSet<Provider> All = new()
        {
              Google,Facebook
        };

        public static Provider GetProvider(string name)
        {
            //equal with ignoring capitalization
            var provider = All.SingleOrDefault(q => q.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) ?? throw new UnsupportedProviderException(name);

            return provider;
        }
    }


}