using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SimpleSocketServer
{
    class Program
    {
        private static ConcurrentDictionary<string, Socket> _allClientSockets = new ConcurrentDictionary<string, Socket>();

        private static Socket _serverSocket;

        static void Main(string[] args)
        {
            Console.WriteLine("Start Service");

            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);

            Console.WriteLine($"Address:{IPAddress.Any}:{port}");

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(100);

            ListenClientConnect();

            Console.ReadKey();
        }

        private static void ListenClientConnect()
        {
            var socketAsyncEventArgs = new SocketAsyncEventArgs();
            socketAsyncEventArgs.Completed += SocketAsyncEventArgsOnCompleted;

            _serverSocket.AcceptAsync(socketAsyncEventArgs);            
        }

        private static void SocketAsyncEventArgsOnCompleted(object sender, SocketAsyncEventArgs socketAsyncEventArgs)
        {
            switch (socketAsyncEventArgs.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    ProcessAccept(socketAsyncEventArgs);
                    break;
            }
        }

        private static void ProcessAccept(SocketAsyncEventArgs socketAsyncEventArgs)
        {
            var clientSocket = socketAsyncEventArgs.AcceptSocket;

            Console.WriteLine("Accecpt client success,address:" + clientSocket.RemoteEndPoint);

            _allClientSockets.TryAdd(clientSocket.RemoteEndPoint.ToString(), clientSocket);

            SendHelloToClient(socketAsyncEventArgs);

            ReceiveMessage(clientSocket);

            //监听新的链接
            ListenClientConnect();
        }

        private static void SendHelloToClient(SocketAsyncEventArgs socketAsyncEventArgs)
        {
            var clientSocket = socketAsyncEventArgs.AcceptSocket;
            var sendData = Encoding.UTF8.GetBytes($"Service Time: {DateTime.Now} ;Hello World {clientSocket.RemoteEndPoint}");

            clientSocket.BeginSend(sendData, 0, sendData.Length, SocketFlags.None, SendCallback, clientSocket);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket) ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void ReceiveMessage(object socketClient)
        {
            var client = (Socket) socketClient;
            var receiveReult = new byte[1024];

            client.BeginReceive(receiveReult, 0, receiveReult.Length, SocketFlags.None, ReceiveCallback, new Tuple<Socket,byte[]>(client, receiveReult));
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Tuple<Socket, byte[]> asyncState = (Tuple<Socket, byte[]>)ar.AsyncState;
            var clientSocket = asyncState.Item1;

            try
            {
                int rEnd = clientSocket.EndReceive(ar);
                if (rEnd > 0)
                {
                    byte[] data = new byte[rEnd];
                    Array.Copy(asyncState.Item2, 0, data, 0, rEnd);

                    Console.WriteLine($"Current connected clients number:{_allClientSockets.Count};" + Encoding.UTF8.GetString(data));


                    clientSocket.BeginReceive(asyncState.Item2, 0, asyncState.Item2.Length, 0, ReceiveCallback,
                        new Tuple<Socket, byte[]>(clientSocket, asyncState.Item2));
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);

                Socket outSocket;
                _allClientSockets.TryRemove(clientSocket.RemoteEndPoint.ToString(), out outSocket);

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }
    }
}
