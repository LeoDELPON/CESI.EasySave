using CESI.BS.EasySave.BS;
using CESI.BS.EasySave.BS.Observers;
using CESI.BS.EasySave.DTO;
using CESI.Server.EasySave.DTO;
using CESI.Server.EasySave.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

namespace CESI.Server.EasySave.Networking
{
    public sealed class ServerSocket : IObserver
    {
        private Socket _socket;
        private Socket _clientSocket;
        private DTOHostMachine _host;
        private const int PORT = 9999;
        private byte[] _buffer = new byte[1024];
        private string _dataSent = "SB";
        private static readonly Lazy<ServerSocket> lazy = new Lazy<ServerSocket>(() => new ServerSocket());
        public static ServerSocket Instance { get { return lazy.Value; } }
        private ServerSocket()
        {
            DataHandler.Instance.SubscribeServer(this);
            _host = new DTOHostMachine();
            _host = HostInfoBuilder.GetHostIpAndName();
            Console.WriteLine("[+] Initializing the socket");
            _socket = new Socket(
                _host.ipAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
        }

        public void StartConnection(int backlog)
        {
            BindInfo();
            Listen(backlog);
            AcceptConnection();
        }

        public void BindInfo()
        {
            try
            {
                Console.WriteLine("[+] Binding the IP to the port {0}", PORT);
                Console.WriteLine("[+] Server adress {0}", _host.ipAddress);
                _socket.Bind(new IPEndPoint(_host.ipAddress, PORT));
            }
            catch (Exception e)
            {
                Console.WriteLine("[+] An error occured while trying to bind the IP and the address :", e);
            }
        }

        public void Listen(int backlog)
        {
            Console.WriteLine("[+] This server will listen to {0} client(s)", backlog);
            try
            {
                _socket.Listen(backlog);
            } catch(ObjectDisposedException e)
            {
                Console.WriteLine("[-] The connection has been closed : {0}", e);
            }
        }

        public void AcceptConnection()
        {
            Console.WriteLine("[+] Beginning accepting connections from clients");
            _socket.BeginAccept(AcceptedCallBack, null);
        }

        private void AcceptedCallBack(IAsyncResult result)
        {
            Socket clientSocket = _socket.EndAccept(result);
            _clientSocket = clientSocket;
            AcceptConnection();
            clientSocket.BeginReceive(
                _buffer,
                0,
                _buffer.Length,
                SocketFlags.None,
                ReceiveCallBack,
                clientSocket);
            AcceptConnection();
            
        }

        private void ReceiveCallBack(IAsyncResult _clientSocketResult)
        {
            Socket clientSocket = (Socket)_clientSocketResult.AsyncState;
            try
            {
                int bufferSize = clientSocket.EndReceive(_clientSocketResult);
                byte[] packet = new byte[bufferSize];
                Array.Copy(_buffer, packet, packet.Length);
                PacketHandler.Handle(packet, clientSocket);
            }
            catch (SocketException e)
            {
                Console.WriteLine("[-] Connexion had to be closed because the host ended the connection : {0}", e);
                Environment.Exit(0);
            }
            _buffer = new byte[1024];
            clientSocket.BeginReceive(
                _buffer,
                0,
                _buffer.Length,
                SocketFlags.None,
                ReceiveCallBack,
                clientSocket);
        }

        public void SendLogData(Socket s)
        {
            string data = _dataSent;
            Message msg = new Message(data);
            //SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            //e.RemoteEndPoint = s.RemoteEndPoint;
            //e.SetBuffer(msg.finalBuffer, 0, data.Length);
            s.SendTo(msg.finalBuffer, 0, msg.finalBuffer.Length, SocketFlags.None, s.RemoteEndPoint);
        }

        public bool ReactDataLogServ(DTODataServer dto)
        {
            _dataSent = JsonSerializer.Serialize(dto);
            if(_clientSocket != null)
            {
                SendLogData(_clientSocket);
                return true;
            } else
            {
                Console.WriteLine("[-] No client connected");
                return false;
            }
        }
    }
}