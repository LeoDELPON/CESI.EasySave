using System;
using System.Net.Sockets;

namespace CESI.Server.EasySave.Services
{
    public static class PacketHandler
    {
        public static void Handle(byte[] packet, Socket clientSocket)
        {
            Message msg = new Message(packet);
            ActionResultMessage(msg.Text);
        }

        public static void ActionResultMessage(string msg)
        {
            switch (msg)
            {
                case "STOP":
                    break;
                case "START":
                    break;
                case "PAUSE":
                    break;
                default:
                    break;
            }
        }

    }
}
