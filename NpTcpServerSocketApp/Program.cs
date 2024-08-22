using System.Net;
using System.Net.Sockets;

IPEndPoint ipPoint = new(IPAddress.Any, 5000);
using Socket socketTcpListener = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream,
                                            ProtocolType.Tcp);

socketTcpListener.Bind(ipPoint);
Console.WriteLine($"Server binding with Endpoint");

socketTcpListener.Listen(10);
Console.WriteLine($"Server waiting clients");

using Socket socketTcpClient = await socketTcpListener.AcceptAsync();
Console.WriteLine($"Accept client with Endpoint {socketTcpClient.RemoteEndPoint}");

