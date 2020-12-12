using CESI.Server.EasySave.DTO;
using CESI.Server.EasySave.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CESI.Server.EasySave.Networking
{
    public sealed class ServerSocket
    {
        private Socket _socket;
        private DTOHostMachine _host;
        private const int PORT = 9999;
        private byte[] _buffer = new byte[1024];
        private static readonly Lazy<ServerSocket> lazy = new Lazy<ServerSocket>(() => new ServerSocket());
        public static ServerSocket Instance { get { return lazy.Value; } }
        private ServerSocket()
        {
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
            _socket.Listen(backlog);
        }

        public void AcceptConnection()
        {
            Console.WriteLine("[+] Beginning accepting connections from clients");
            _socket.BeginAccept(AcceptedCallBack, null);
        }

        private void AcceptedCallBack(IAsyncResult result)
        {
            Socket clientSocket = _socket.EndAccept(result);
            AcceptConnection();
            clientSocket.BeginReceive(
                _buffer,
                0,
                _buffer.Length,
                SocketFlags.None,
                ReceiveCallBack,
                clientSocket);
            AcceptConnection();
            Console.WriteLine(clientSocket.RemoteEndPoint);
            while (true)
            {
                SendMockData(clientSocket);
                Thread.Sleep(1000);
            }
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
                Console.WriteLine("[-] Connexion had to be closed because the host ended the connection");
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

        public void SendMockData(Socket s)
        {
            string dataMock = "C > Python";
            byte[] data = Encoding.ASCII.GetBytes(dataMock);
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.RemoteEndPoint = s.RemoteEndPoint;
            e.SetBuffer(data, 0, dataMock.Length);
            s.SendToAsync(e);
        }
    }
}