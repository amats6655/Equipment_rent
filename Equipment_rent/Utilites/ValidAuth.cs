using System.Collections.Generic;
using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Windows.Markup;
using System.Windows;
using Equipment_rent.View;

namespace Equipment_rent.Utilites
{
    class AuthClient
    {
        public static async void AuthClient_Send(string username, string password)
        {
            using TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync("85.175.4.135", 8888);

            // получаем NetworkStream для взаимодействия с сервером
            var stream = tcpClient.GetStream();

            // буфер для входящих данных
            var response = new List<byte>();
            int bytesRead = 10; // для считывания байтов из потока


            byte[] data_log = Encoding.UTF8.GetBytes(username + '\r' + password + '\n');
            await stream.WriteAsync(data_log);
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                // добавляем в буфер
                response.Add((byte)bytesRead);
            }
            var UserId = Encoding.UTF8.GetString(response.ToArray());
            MessageBox.Show($"Слово {username}: {UserId}");
            response.Clear();

            if(UserId != "")
            {
                var w = Application.Current.Windows[0];
                w.Hide();
                Window window = new MainWindow();
                window.ShowDialog();
                w.Show();
            }
        }
    }
}
