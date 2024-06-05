using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для LibraryManagerWindow.xaml
    /// </summary>
    public partial class LibraryManagerWindow : Window
    {
        // Строка подключения к базе данных MySQL
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";

        // Адаптеры и таблицы данных для книг, читателей и регистраций
        private MySqlDataAdapter booksAdapter;
        private DataTable booksTable;

        private MySqlDataAdapter readersAdapter;
        private DataTable readersTable;

        private MySqlDataAdapter registrationAdapter;
        private DataTable registrationTable;

        public LibraryManagerWindow()
        {
            InitializeComponent();
            InitializeAdaptersAndTables();
            LoadBooks();
            LoadReaders();
            LoadRegistrations();
        }
        // Инициализация адаптеров и таблиц данных
        private void InitializeAdaptersAndTables()
        {
            booksAdapter = new MySqlDataAdapter("SELECT * FROM Book", connectionString);
            booksTable = new DataTable();

            readersAdapter = new MySqlDataAdapter("SELECT * FROM Reader", connectionString);
            readersTable = new DataTable();

            registrationAdapter = new MySqlDataAdapter("SELECT * FROM Registration", connectionString);
            registrationTable = new DataTable();
        }
        // Загрузка данных книг из базы данных
        private void LoadBooks()
        {
            try
            {
                booksTable.Clear();
                booksAdapter.Fill(booksTable);
                BooksDataGrid.ItemsSource = booksTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных книг: " + ex.Message);
            }
        }
        // Загрузка данных читателей из базы данных
        private void LoadReaders()
        {
            try
            {
                readersTable.Clear();
                readersAdapter.Fill(readersTable);
                ReadersDataGrid.ItemsSource = readersTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных читателей: " + ex.Message);
            }
        }
        // Загрузка данных регистраций из базы данных
        private void LoadRegistrations()
        {
            try
            {
                registrationTable.Clear();
                registrationAdapter.Fill(registrationTable);
                RegistrationDataGrid.ItemsSource = registrationTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных регистрации: " + ex.Message);
            }
        }
        // Обработчик события нажатия кнопки "Добавить книгу"
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            if (addBookWindow.ShowDialog() == true)
            {
                DataRow newRow = booksTable.NewRow();
                newRow["inventory_number"] = addBookWindow.InventoryNumberTextBox.Text;
                newRow["title"] = addBookWindow.TitleTextBox.Text;
                newRow["author"] = addBookWindow.AuthorTextBox.Text;
                booksTable.Rows.Add(newRow);
                booksAdapter.Update(booksTable);
            }
        }
        // Обработчик события нажатия кнопки "Добавить читателя"
        private void AddReader_Click(object sender, RoutedEventArgs e)
        {
            AddReaderWindow addReaderWindow = new AddReaderWindow();
            if (addReaderWindow.ShowDialog() == true)
            {
                DataRow newRow = readersTable.NewRow();
                newRow["reader_ticket_number"] = addReaderWindow.ReaderTicketNumberTextBox.Text;
                newRow["name"] = addReaderWindow.NameTextBox.Text;
                newRow["address"] = addReaderWindow.AddressTextBox.Text;
                readersTable.Rows.Add(newRow);
                readersAdapter.Update(readersTable);
            }
        }
        // Обработчик события нажатия кнопки "Сохранить изменения"
        private void SaveBooksChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommandBuilder booksBuilder = new MySqlCommandBuilder(booksAdapter);
                booksAdapter.Update(booksTable);

                MySqlCommandBuilder readersBuilder = new MySqlCommandBuilder(readersAdapter);
                readersAdapter.Update(readersTable);

                MySqlCommandBuilder registrationBuilder = new MySqlCommandBuilder(registrationAdapter);
                registrationAdapter.Update(registrationTable);

                MessageBox.Show("Изменения успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
            }
        }
        // Обработчик события нажатия кнопки "Выход"
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}