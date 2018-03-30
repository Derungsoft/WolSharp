using System.Collections.Generic;

namespace Derungsoft.WolSharp
{
    internal class MacAddressValidationResult
    {
        public bool HasErrors { get; set; }

        public IReadOnlyCollection<string> ValidationErrors { get; set; }
    }
}
