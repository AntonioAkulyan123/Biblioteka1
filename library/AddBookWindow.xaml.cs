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
    /// Логика взаимодействия для AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        // Строка подключения к базе данных MySQL
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";
        public AddBookWindow()
        {
            InitializeComponent();
        }
        // Обработчик нажатия кнопки добавления книги
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            string inventoryNumber = InventoryNumberTextBox.Text;
            string title = TitleTextBox.Text;
            string author = AuthorTextBox.Text;

            // Проверка допустимости инвентарного номера
            if (!IsValidInventoryNumber(inventoryNumber))
            {
                MessageBox.Show("Инвентарный номер должен быть в диапазоне от 1000 до 100000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Проверка заполненности обязательных полей
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Book (inventory_number, title, author) VALUES (@inventoryNumber, @title, @author)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@inventoryNumber", inventoryNumber);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Книга успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении книги: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Проверка допустимости инвентарного номера
        private bool IsValidInventoryNumber(string inventoryNumber)
        {
            if (!int.TryParse(inventoryNumber, out int number))
                return false;

            return number >= 1000 && number <= 100000;
        }
    }
}