using CESI.BS.EasySave.BS;
using System;
using System.Net.Sockets;

namespace CESI.Server.EasySave.Services
{
    public static class PacketHandler
    {
        public delegate void AbortDelegate();
        public delegate bool ResumeDelegate();
        public delegate bool PauseDelegate();
        public static ResumeDelegate OnResumeSent { get; set; } = DefaultOnResumeSent;
        public static PauseDelegate OnPauseSent { get; set; } = DefaultOnPauseSent;
        public static AbortDelegate OnAbortSent { get; set; } = DefaultOnAbortSent;

        public static void Handle(byte[] packet, Socket clientSocket)
        {
            Message msg = new Message(packet);
            ActionResultMessage(msg.Text);
        }
        public static bool DefaultOnResumeSent() {
            return true;
        }
        public static bool DefaultOnPauseSent() {
            return true;
        }
        public static void DefaultOnAbortSent() { }
        public static void ActionResultMessage(string msg)
        {
            switch (msg)
            {
                case "STOP":
                    OnAbortSent();
                    break;
                case "START":
                    OnResumeSent();
                    break;
                case "PAUSE":
                    OnPauseSent();
                    break;
                default:
                    break;
            }
        }

    }
}
