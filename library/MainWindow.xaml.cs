using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace library
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Строка подключения к базе данных MySQL
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";
        public MainWindow()
        {
            InitializeComponent();
        }
        // Обработчик события нажатия кнопки "Войти"
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (ValidateUser(username, password, out string role))
            {
                MessageBox.Show("Авторизация успешна!");

                // Открытие соответствующего окна в зависимости от роли
                switch (role)
                {
                    case "Зав. библиотекой":
                        new LibraryManagerWindow().Show();
                        break;
                    case "Читатели":
                        new ReaderWindow().Show();
                        break;
                    case "Библиотекари":
                        new LibrarianWindow().Show();
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя.");
                        return;
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Неверное имя пользователя или пароль.");
            }
        }
        // Метод для проверки подлинности пользователя
        private bool ValidateUser(string username, string password, out string role)
        {
            role = string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT u.role_id, r.role_name FROM User u JOIN Role r ON u.role_id = r.role_id WHERE u.username = @username AND u.password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            role = reader["role_name"].ToString();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
                }
            }

            return false; // Возвращение false при неудачной аутентификации
        }
    }
}