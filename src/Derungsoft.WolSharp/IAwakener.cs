using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Derungsoft.WolSharp
{
    public interface IAwakener
    {
        Task WakeAsync(string physicalAddress, byte[] password = null);
        Task WakeAsync(PhysicalAddress physicalAddress, byte[] password = null);

        void Wake(string physicalAddress, byte[] password = null);
        void Wake(PhysicalAddress physicalAddress, byte[] password = null);
    }
}
