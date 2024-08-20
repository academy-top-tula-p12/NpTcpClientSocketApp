using System.Net.Sockets;
using System.Text;

string host = "tula.top-academy.ru";
int port = 80;

using Socket clientTcpSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);

try
{
    await clientTcpSocket.ConnectAsync(host, port);
    Console.WriteLine("Connection!");
    Console.WriteLine($"Local address: {clientTcpSocket.LocalEndPoint}");
    Console.WriteLine($"Remote address: {clientTcpSocket.RemoteEndPoint}");

    string getMessage = $"GET /events HTTP/1.1\r\nHost: {host}\r\nConnection: close\r\n\r\n";
    byte[] getBuffer = Encoding.UTF8.GetBytes(getMessage);

    int bytesCount = await clientTcpSocket.SendAsync(getBuffer);
    Console.WriteLine($"Send to host {host} {bytesCount} bytes");

    byte[] responseBuffer = new byte[512];
    StringBuilder responseText = new();

    do
    {
        bytesCount = await clientTcpSocket.ReceiveAsync(responseBuffer);
        responseText.Append(Encoding.UTF8.GetString(responseBuffer, 0, bytesCount));
    } while(bytesCount > 0);

    Console.WriteLine(responseText);

}
catch (Exception ex)
{
    Console.WriteLine($"Not connection. {ex.Message}");
}
finally
{
    await clientTcpSocket.DisconnectAsync(true);
}