using System.Net.NetworkInformation;

namespace Derungsoft.WolSharp
{
    public interface IPhysicalAddressParser
    {
        bool TryParse(string s, out PhysicalAddress result);
        PhysicalAddress Parse(string macAddress);
    }
}
