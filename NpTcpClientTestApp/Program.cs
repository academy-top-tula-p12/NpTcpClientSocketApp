using System.Net;
using System.Net.Sockets;

Socket clientTcpSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);

try
{
    await clientTcpSocket.ConnectAsync(IPAddress.Loopback, 5000);
    Console.WriteLine($"We connect to server {clientTcpSocket.RemoteEndPoint}");
}
catch(Exception ex)
{
    Console.WriteLine($"{ex.Message}");
}