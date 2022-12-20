using Equipment_rent.ViewModel;
using System;
using System.IO;
using System.Text;
using System.Windows;


namespace Equipment_rent.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        public AuthView()
        {

            InitializeComponent();
            read_auth();

        }


        private async void read_auth()
        {
            // считываем данные авторизации из файла
            string path = "./Settings/auth.dat";   // путь к файлу

            // чтение из файла
            using (FileStream fstream = File.OpenRead(path))
            {
                // выделяем массив для считывания данных из файла
                byte[] buffer = new byte[fstream.Length];
                // считываем данные
                await fstream.ReadAsync(buffer, 0, buffer.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.UTF8.GetString(buffer);
                fstream.Close();

                tb_username.Text = textFromFile;


            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
