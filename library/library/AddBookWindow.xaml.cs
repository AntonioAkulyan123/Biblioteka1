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

            if (!ValidateFields(inventoryNumber, title, author))
                return;

            if (!IsValidInventoryNumber(inventoryNumber))
            {
                ShowErrorMessage("Инвентарный номер должен быть в диапазоне от 1000 до 100000.");
                return;
            }

            try
            {
                AddBookToDatabase(inventoryNumber, title, author);
                MessageBox.Show("Книга успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при добавлении книги: " + ex.Message);
            }
        }
        // Проверяет, что все поля заполнены
        private bool ValidateFields(string inventoryNumber, string title, string author)
        {
            if (string.IsNullOrWhiteSpace(inventoryNumber) || string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                ShowErrorMessage("Пожалуйста, заполните все поля.");
                return false;
            }
            return true;
        }
        // Проверяет, что инвентарный номер находится в допустимом диапазоне
        private bool IsValidInventoryNumber(string inventoryNumber)
        {
            if (!int.TryParse(inventoryNumber, out int number))
                return false;

            return number >= 1000 && number <= 100000;
        }
        // Добавляет книгу в базу данных
        private void AddBookToDatabase(string inventoryNumber, string title, string author)
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
            }
        }
        // Показ сообщения об ошибке
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}