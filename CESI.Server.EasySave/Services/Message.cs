namespace CESI.Server.EasySave.Services
{
    public class Message : PacketStructure
    {
        private string _message;
        public Message(string message) : base((ushort)(message.Length))
        {
            Text = message;
        }

        public Message(byte[] packet) : base(packet)
        {

        }

        public string Text
        {
            get { return ReadString(0, finalBuffer.Length); }
            set
            {
                _message = value;
                WriteString(value, 0);
            }
        }
    }
}
