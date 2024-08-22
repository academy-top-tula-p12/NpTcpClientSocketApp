using System.Data;
using System.Net.Sockets;
using System.Text;

string host = "tula.top-academy.ru";
int port = 80;

string responseText = await SocketSendAsync(host, port);

Console.WriteLine(responseText);

async Task<Socket> SocketConnectAsync(string host, int port)
{
    Socket clientTcpSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
    
    try
    {
        await clientTcpSocket.ConnectAsync(host, port);
        return clientTcpSocket;
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        clientTcpSocket.Close();
    }

    return null;
    
}

async Task<string> SocketSendAsync(string host, int port)
{
    using Socket? clientTcpSocket = await SocketConnectAsync(host, port);

    using NetworkStream stream = new NetworkStream(clientTcpSocket);

    if (clientTcpSocket is null)
        return $"Not connect with {host}:{port}";

    string getMessage = $"GET /events HTTP/1.1\r\nHost: {host}\r\nConnection: close\r\n\r\n";
    byte[] getBuffer = Encoding.UTF8.GetBytes(getMessage);

    //int bytesCount = await clientTcpSocket.SendAsync(getBuffer);
    await stream.WriteAsync(getBuffer, 0, getBuffer.Length);

    byte[] responseBuffer = new byte[1024];
    StringBuilder responseText = new();
    int bytesCountStream;

    do
    {
        bytesCountStream = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
        responseText.Append(Encoding.UTF8.GetString(responseBuffer));
    } while (bytesCountStream > 0);

    //do
    //{
    //    bytesCount = await clientTcpSocket.ReceiveAsync(responseBuffer);
    //    responseText.Append(Encoding.UTF8.GetString(responseBuffer, 0, bytesCount));
    //} while (bytesCount > 0);

    return responseText.ToString();
}