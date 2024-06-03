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
    /// Логика взаимодействия для ReaderWindow.xaml
    /// </summary>
    public partial class ReaderWindow : Window
    {
        private const string connectionString = "server=localhost;user=root;database=library;port=3306;charset=utf8;";
        public ReaderWindow()
        {
            InitializeComponent();
            LoadBooks();
            LoadReaders();
            LoadRegistrations();
        }
        private void LoadBooks()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Book";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    BooksDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
                }
            }
        }

        private void LoadReaders()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Reader";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    ReadersDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных читателей: " + ex.Message);
                }
            }
        }

        private void LoadRegistrations()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Registration";
                    MySqlDataAdapter registrationAdapter = new MySqlDataAdapter(query, conn);
                    DataTable registrationTable = new DataTable();
                    registrationAdapter.Fill(registrationTable);
                    RegistrationDataGrid.ItemsSource = registrationTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке данных регистрации: " + ex.Message);
                }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
