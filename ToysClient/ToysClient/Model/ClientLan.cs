using System;
using System.Text;
using System.Net.Sockets;
using System.Windows;

namespace ToysClient.Model
{
	public class ClientLan
	{
		private string address;
		private int port;
		private TcpClient client;

		public ClientLan(string address, int port)
		{
			this.address = address;
			this.port = port;
		}

		public string SendRequest(string message)
		{
			NetworkStream stream = null;
			try
			{
				client = new TcpClient(address, port);
				Byte[] data = Encoding.UTF8.GetBytes(message);
				stream = client.GetStream();

				stream.Write(data, 0, data.Length);
				Byte[] readingData = new Byte[256];
				String responseData = String.Empty;
				StringBuilder completeMessage = new StringBuilder();
				int numberOfBytesRead = 0;
				do
				{
					numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
					completeMessage.AppendFormat("{0}", 
						Encoding.UTF8.GetString(readingData, 0, numberOfBytesRead));
				}
				while (stream.DataAvailable);
				return completeMessage.ToString();
			}
			catch { MessageBox.Show("Ошибка подключения к серверу"); }
			finally
			{
				stream?.Close();
				client?.Close();
			}
			return "";
		}
	}
}
