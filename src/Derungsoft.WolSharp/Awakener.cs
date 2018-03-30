using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Derungsoft.WolSharp
{
    public class Awakener : IAwakener
    {
        private readonly IPhysicalAddressParser _physicalAddressParser;

        public Awakener()
        {
            _physicalAddressParser = new DefaultPhysicalAddressParser();
        }

        public Awakener(IPhysicalAddressParser physicalAddressParser)
        {
            _physicalAddressParser = physicalAddressParser;
        }

        public async Task WakeAsync(string physicalAddress, byte[] password = null)
        {
            var address = _physicalAddressParser.Parse(physicalAddress);
            await SendWakeOnLanPacketAsync(address, password);
        }

        public async Task WakeAsync(PhysicalAddress physicalAddress, byte[] password = null)
        {
            if (physicalAddress == null)
            {
                throw new ArgumentNullException(nameof(physicalAddress));
            }

            await SendWakeOnLanPacketAsync(physicalAddress, password);
        }

        public void Wake(string physicalAddress, byte[] password = null)
        {
            var address = _physicalAddressParser.Parse(physicalAddress);
            SendWakeOnLanPacket(address, password);
        }

        public void Wake(PhysicalAddress physicalAddress, byte[] password = null)
        {
            if (physicalAddress == null)
            {
                throw new ArgumentNullException(nameof(physicalAddress));
            }

            SendWakeOnLanPacket(physicalAddress, password);
        }

        private async Task SendWakeOnLanPacketAsync(PhysicalAddress physicalAddress, byte[] password = null)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Broadcast, 7);

            var packet = BuildWakeOnLanPacket(physicalAddress.GetAddressBytes(), password);

            using (var udpClient = new UdpClient())
            {
                await udpClient.SendAsync(packet, packet.Length, ipEndPoint);
            }
        }

        private void SendWakeOnLanPacket(PhysicalAddress physicalAddress, byte[] password = null)
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Broadcast, 7);

            var packet = BuildWakeOnLanPacket(physicalAddress.GetAddressBytes(), password);

            using (var udpClient = new UdpClient())
            {
                udpClient.Send(packet, packet.Length, ipEndPoint);
            }
        }

        private byte[] BuildWakeOnLanPacket(byte[] macAddress, byte[] password = null)
        {
            if (macAddress == null)
            {
                throw new ArgumentNullException(nameof(macAddress));
            }

            if (macAddress.Length != 6)
            {
                throw new ArgumentException();
            }

            password = password ?? new byte[0];

            if (password.Length != 0 && password.Length != 6)
            {
                throw new ArgumentException($"{nameof(password)} must be exactly 6 bytes long");
            }

            var packet = new byte[17 * 6 + password.Length];

            int offset, i;
            for (offset = 0; offset < 6; ++offset)
            {
                packet[offset] = 0xFF;
            }

            for (offset = 6; offset < 17 * 6; offset += 6)
            {
                for (i = 0; i < 6; ++i)
                {
                    packet[i + offset] = macAddress[i];
                }
            }

            if (password.Length <= 0)
            {
                return packet;
            }

            for (offset = 16 * 6 + 6; offset < (17 * 6 + password.Length); offset += 6)
            {
                for (i = 0; i < 6; ++i)
                {
                    packet[i + offset] = password[i];
                }
            }

            return packet;
        }
    }
}
