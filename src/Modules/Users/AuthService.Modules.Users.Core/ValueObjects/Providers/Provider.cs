using AuthService.Shared.Modules.ValueObjects.Common;
using AuthService.Modules.Core.ValueObjects.Exceptions;


namespace AuthService.Modules.Core.ValueObjects
{
    public sealed class Provider : ValueObject
    {
        public string Name { get; }

        internal Provider(string name)
        {
            var nameSupported = IsNameSupported(name);

            if (!nameSupported && name != null) throw new UnsupportedProviderException(name);
            //if name isset in the set add name to Name property
            Name = name;
        }

        //check if name is available form ProviderNames
        private static bool IsNameSupported(string key)
            => AvailableProviderNames.AllCodes.Contains(key, StringComparer.InvariantCultureIgnoreCase);

        internal static Provider Create(string name)
            => new(name);
    }
}