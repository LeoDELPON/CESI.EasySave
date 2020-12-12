using System;
using System.Net.Sockets;

namespace CESI.Server.EasySave.Services
{
    public static class PacketHandler
    {
        public static void Handle(byte[] packet, Socket clientSocket)
        {
            ushort packetLength = BitConverter.ToUInt16(packet, 0);
            ushort packetType = BitConverter.ToUInt16(packet, 2);
            //Console.WriteLine("[+] Packet received... Length:{0} | Type: {1}", packetLength, packetType);
            switch (packetType)
            {
                case 2000:
                    Message msg = new Message(packet);
                    Console.WriteLine("[+] {0} ", msg.Text);
                    break;
                default:
                    Console.WriteLine("Pas OP");
                    break;
            }
        }
    }
}
