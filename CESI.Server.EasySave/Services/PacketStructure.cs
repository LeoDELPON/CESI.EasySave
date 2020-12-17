using System;
using System.Text;

namespace CESI.Server.EasySave.Services
{
    public abstract class PacketStructure
    {
        private readonly byte[] _buffer;
        public byte[] finalBuffer { get { return _buffer; } }
        public PacketStructure(ushort length)
        {
            _buffer = new byte[length];
        }

        public PacketStructure(byte[] packet)
        {
            _buffer = packet;
        }

        public void WriteString(string message, int offset)
        {
            byte[] tempBuff = new byte[message.Length];
            tempBuff = Encoding.ASCII.GetBytes(message);

            //Array.Copy est index-based et Buffer.BlockCopy est byte-based (c'est utilisé pour des sections critiques de perf) mais c'est 
            // quoi Marshal.Copy ?

            Buffer.BlockCopy(tempBuff, 0, _buffer, offset, message.Length);
        }

        public string ReadString(int offset, int count)
        {
            return Encoding.ASCII.GetString(_buffer, offset, count);
        }
    }
}
