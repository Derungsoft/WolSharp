using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Derungsoft.WolSharp
{
    public class DefaultPhysicalAddressParser : IPhysicalAddressParser
    {
        private readonly char[] _supportedSeparators =
        {
            ':',
            ' ',
            '-',
            '|',
            '.'
        };
        
        public bool TryParse(string s, out PhysicalAddress result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public PhysicalAddress Parse(string macAddress)
        {
            var validationResult = Validate(macAddress);

            if (validationResult.HasErrors)
            {
                throw new FormatException($"Validation for {nameof(macAddress)} failed: {string.Join(",", validationResult.ValidationErrors)}");
            }

            macAddress = StripPhysicalAddress(macAddress);

            return PhysicalAddress.Parse(macAddress.ToUpper());
        }

        private string StripPhysicalAddress(string macAddress)
        {
            return new string(macAddress.Where(p => _supportedSeparators.All(s => s != p)).ToArray());
        }

        private MacAddressValidationResult Validate(string physicalAddress)
        {
            var validationErrorMessages = new List<string>();

            if (physicalAddress == null)
            {
                validationErrorMessages.Add($"{nameof(physicalAddress)} cannot be null");
            }

            if (physicalAddress == string.Empty)
            {
                validationErrorMessages.Add($"{nameof(physicalAddress)} cannot be empty");
            }

            physicalAddress = StripPhysicalAddress(physicalAddress);

            if (physicalAddress.Length != 12)
            {
                validationErrorMessages.Add($"{nameof(physicalAddress)} must be exactly 6 bytes long");
            }

            var regex = new Regex("^[a-fA-F0-9]{12}$");
            if (!regex.IsMatch(physicalAddress) && physicalAddress.Length == 12)
            {
                validationErrorMessages.Add($"{nameof(physicalAddress)} has an invalid format. " +
                                            "Only hexadecimal characters allowed. Supported " +
                                            $"separators are: {Environment.NewLine + string.Join(Environment.NewLine, _supportedSeparators.Select(s => $"'{s}'"))}");
            }

            return new MacAddressValidationResult
            {
                HasErrors = validationErrorMessages.Any(),
                ValidationErrors = validationErrorMessages
            };
        }
    }
}
