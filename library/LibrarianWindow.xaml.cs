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
    /// Логика взаимодействия для LibrarianWindow.xaml
    /// </summary>
    public partial class LibrarianWindow : Window
    {
        // Строка подключения к базе данных MySQL
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";

        private MySqlDataAdapter booksAdapter;
        private DataTable booksTable;

        private MySqlDataAdapter readersAdapter;
        private DataTable readersTable;

        private MySqlDataAdapter registrationAdapter;
        private DataTable registrationTable;
        public LibrarianWindow()
        {
            InitializeComponent();
            InitializeAdaptersAndTables();
            LoadBooks();
            LoadReaders();
        }
        // Инициализация адаптеров и таблиц
        private void InitializeAdaptersAndTables()
        {
            booksAdapter = new MySqlDataAdapter("SELECT * FROM Book", connectionString);
            booksTable = new DataTable();

            readersAdapter = new MySqlDataAdapter("SELECT * FROM Reader", connectionString);
            readersTable = new DataTable();
        }
        // Загрузка данных о книгах из базы данных
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
        // Загрузка данных о читателях из базы данных
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
        // Обработчик нажатия кнопки добавления книги
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
        // Обработчик нажатия кнопки добавления читателя
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
        // Обработчик нажатия кнопки сохранения изменений
        private void SaveBooksChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlCommandBuilder booksBuilder = new MySqlCommandBuilder(booksAdapter);
                booksAdapter.Update(booksTable);

                MySqlCommandBuilder readersBuilder = new MySqlCommandBuilder(readersAdapter);
                readersAdapter.Update(readersTable);

                MessageBox.Show("Изменения успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
            }
        }
        // Обработчик нажатия кнопки выхода
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
