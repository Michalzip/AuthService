
namespace AuthService.Modules.Core.ValueObjects
{
    internal static class AvailableProviderNames
    {
        internal const string Facebook = nameof(Facebook);
        internal const string Google = nameof(Google);

        //readonly - ensures that the collection can only be assigned a value once
        //IReadOnlyCollection -  that the collection is read-only, meaning that elements cannot be added or removed from it, but it can be iterated over
        internal static readonly IReadOnlyCollection<string> AllCodes = new List<string>
        {
             Google,Facebook
        };
    }

}