using Equipment_rent.Model;
using Equipment_rent.View;
using Equipment_rent.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Equipment_rent.Utilites
{
    class AuthClient
    {
        private static readonly string sal = "KompASminE";
        public static async void AuthClient_Send(string username, string password)
        {
            List<string> ErrorMessages = new List<string>() { "Неверный логин", "Неверный пароль", "Авторизация прошла успешно", "При авторизации что-то пошло не так" };
            int mode = 0;
            int numberOfIteration = 99;
            var hashFunc = new Crypt();
            byte[] salt_srv = Encoding.UTF8.GetBytes(sal);


            var hashedPass = hashFunc.HashDataWithRounds(Encoding.UTF8.GetBytes(password), salt_srv, numberOfIteration);
            using TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("85.175.4.135", 8888);

            // получаем NetworkStream для взаимодействия с сервером
            var stream = tcpClient.GetStream();

            // буфер для входящих данных
            var response = new List<byte>();
            int bytesRead = 10; // для считывания байтов из потока

            byte[] data_log = Encoding.UTF8.GetBytes(mode.ToString() + '\r' + username + '\r' + hashedPass + '\n');
            await stream.WriteAsync(data_log);

            int Status = 10;
            string UserId;
            while ((bytesRead = stream.ReadByte()) != '\n')
            {

                // добавляем в буфер
                response.Add((byte)bytesRead);
                if (bytesRead == '\r')
                {
                    Status = int.Parse(Encoding.UTF8.GetString(response.ToArray()));
                    response.Clear();
                }
            }
            UserId = Encoding.UTF8.GetString(response.ToArray());

            if (Status == 2 && UserId != null)
            {
                NavigationVM.AuthUser = DataWorker.GetAuthUserById(Guid.Parse(UserId));
                var w = Application.Current.Windows[0];
                w.Hide();
                Window window = new MainWindow();

                window.ShowDialog();
                w.Close();
            }
            response.Clear();
            AuthVM.Message = ErrorMessages[Status];
        }

        public static async void ChangePassword(string username, string lastpassword, string password)
        {
            List<string> ErrorMessages = new List<string>() { "Неверный старый пароль", "Пароль успешно изменен", "Кажется что-то пошло не так" };
            int mode = 1;
            int numberOfIteration = 99;
            var hashFunc = new Crypt();
            byte[] salt_srv = Encoding.UTF8.GetBytes(sal);

            var hashedLastPass = hashFunc.HashDataWithRounds(Encoding.UTF8.GetBytes(lastpassword), salt_srv, numberOfIteration);
            var hashedNewPass = hashFunc.HashDataWithRounds(Encoding.UTF8.GetBytes(password), salt_srv, numberOfIteration);
            using TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("85.175.4.135", 8888);

            var stream = tcpClient.GetStream();

            var response = new List<byte>();
            int bytesRead = 10;

            byte[] data_change = Encoding.UTF8.GetBytes(mode.ToString() + '\r' + username + '\r' + hashedNewPass + '\r' + hashedLastPass + '\n');
            await stream.WriteAsync(data_change);

            int Status = 10;
            string UserId;
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                response.Add((byte)bytesRead);
                if (bytesRead == '\r')
                {
                    Status = int.Parse(Encoding.UTF8.GetString(response.ToArray()));
                    response.Clear();
                }
            }
            UserId = Encoding.UTF8.GetString(response.ToArray());
            response.Clear();
            ProfileVM.Message = ErrorMessages[Status];
        }

        public static async void CreateNewUser(string username, string password)
        {
            int mode = 2;
            int numberOfIteration = 99;
            var hashFunc = new Crypt();
            byte[] salt_srv = Encoding.UTF8.GetBytes(sal);

            var hashedPass = hashFunc.HashDataWithRounds(Encoding.UTF8.GetBytes(password), salt_srv, numberOfIteration);
            using TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("85.175.4.135", 8888);

            var stream = tcpClient.GetStream();

            var response = new List<byte>();
            int bytesRead = 10;
            byte[] data_change = Encoding.UTF8.GetBytes(mode.ToString() + '\r' + username + '\r' + hashedPass + '\n');
            await stream.WriteAsync(data_change);
            int Status = 10;
            string UserId;
            while ((bytesRead = stream.ReadByte()) != '\n')
            {
                response.Add((byte)bytesRead);
                if (bytesRead == '\r')
                {
                    Status = int.Parse(Encoding.UTF8.GetString(response.ToArray()));
                    response.Clear();
                }
            }
            UserId = Encoding.UTF8.GetString(response.ToArray());
            if (Status == 0)
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
            if (Status == 1)
            {
                AddStaffVM.UserId = UserId;
                MessageBox.Show("Пользователь успешно создан");
            }
            //statuses
        }
    }
}
