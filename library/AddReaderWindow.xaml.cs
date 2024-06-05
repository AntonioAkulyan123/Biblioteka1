using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace library
{
    /// <summary>
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        // Строка подключения к базе данных MySQL
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";
        public AddReaderWindow()
        {
            InitializeComponent();
            SetNextReaderTicketNumber(); // Установка следующего номера читательского билета
        }
        // Метод, который устанавливает следующий номер читательского билета
        private void SetNextReaderTicketNumber()
        {
            try
            {
                int lastReaderTicketNumber = GetLastReaderTicketNumber();
                int newReaderTicketNumber = lastReaderTicketNumber + 1;
                ReaderTicketNumberTextBox.Text = newReaderTicketNumber.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении номера читательского билета: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Метод, который получает последний номер читательского билета из базы данных
        private int GetLastReaderTicketNumber()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT MAX(reader_ticket_number) FROM Reader";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                object result = cmd.ExecuteScalar();
                return (result != DBNull.Value && result != null) ? Convert.ToInt32(result) : 0;
            }
        }
        // Обработчик нажатия кнопки добавления читателя
        private void AddReader_Click(object sender, RoutedEventArgs e)
        {
            string readerTicketNumber = ReaderTicketNumberTextBox.Text;
            string name = NameTextBox.Text;
            string address = AddressTextBox.Text;
            string phone = PhoneTextBox.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Reader (reader_ticket_number, name, address, phone) VALUES (@readerTicketNumber, @name, @address, @phone)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@readerTicketNumber", readerTicketNumber);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(phone) ? DBNull.Value : (object)phone);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Читатель успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении читателя: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
