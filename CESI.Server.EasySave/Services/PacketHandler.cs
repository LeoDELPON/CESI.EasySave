using System;
using System.Net.Sockets;

namespace CESI.Server.EasySave.Services
{
    public static class PacketHandler
    {
        public static void Handle(byte[] packet, Socket clientSocket)
        {
            ushort packetType = BitConverter.ToUInt16(packet, 2);

            switch (packetType)
            {
                case 2000:
                    Message msg = new Message(packet);
                    Console.WriteLine(msg.Text);
                    break;
                default:
                    Console.WriteLine("Pas OP");
                    break;
            }
        }
    }
}
