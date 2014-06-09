using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectSpark
{
    public class ServerConnection
    {
        private Socket _clientSocket;
        private byte[] _buffer;

        public ServerConnection()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _buffer = new byte[1024];
        }

        public void Connect()
        {
            ConnectionLoop();
        }

        public void SendMessageToServer(string message)
        {
            if (_clientSocket.Connected)
            {
                byte[] _sendBuffer = Encoding.ASCII.GetBytes(message);
                _clientSocket.Send(_sendBuffer);
            }
        }

        public bool IsConnected()
        {
            return _clientSocket.Connected;
        }

        private void ConnectionLoop()
        {
            int attempts = 0;
            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;
                    _clientSocket.Connect(IPAddress.Loopback, 100);
                    MessageBox.Show("Verbonden!!! " + attempts + " pogingen.");
                    
                    _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _clientSocket);
                }
                catch (SocketException)
                {

                }
            }

        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket currentSocket = (Socket)ar.AsyncState;
            int received = currentSocket.EndReceive(ar);
            byte[] dataBuffer = new byte[received];
            Array.Copy(_buffer, dataBuffer, received);
            MessageBox.Show("Message Reveived: " + Encoding.ASCII.GetString(dataBuffer));
            string[] message = Encoding.ASCII.GetString(dataBuffer).Split('|');
            if (message[0].Equals('T'))
            {
                MessageBox.Show("Message Reveived: " + Encoding.ASCII.GetString(dataBuffer));
            }
            _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), _clientSocket);
        }

    }

}
