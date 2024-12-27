using System;
using System.Collections.Generic;
using System.Configuration;
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
using Npgsql;
using Rent.Models.Windows;

namespace Rent.Models.Pages
{
    /// <summary>
    /// Логика взаимодействия для Profile_page.xaml
    /// </summary>
    public partial class Profile_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        public Profile_page()
        {
            InitializeComponent();
        }

        string pas;
        string hide_pas;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            if (Data.Role == "Клиент")
            {
                NpgsqlCommand command = new NpgsqlCommand("select Firstname from Clients where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                firstname_tb.Text = command.ExecuteScalar().ToString();

                command = new NpgsqlCommand("select lastname from Clients where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                lastname_tb.Text = command.ExecuteScalar().ToString();

                command = new NpgsqlCommand("select password from Clients where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                pas = command.ExecuteScalar().ToString();
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand("select Firstname from employees where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                firstname_tb.Text = command.ExecuteScalar().ToString();

                command = new NpgsqlCommand("select lastname from employees where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                lastname_tb.Text = command.ExecuteScalar().ToString();

                command = new NpgsqlCommand("select password from employees where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                pas = command.ExecuteScalar().ToString();
            }

            for (int i = 0; i <= pas.Length; i++)
            {
                hide_pas += "●";
            }

            pas_tb.Text = hide_pas;
            close_eye.Visibility = Visibility.Hidden;
        }

        private void open_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pas_tb.Text = pas;
            open_eye.Visibility = Visibility.Hidden;
            close_eye.Visibility = Visibility.Visible;
        }

        private void close_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pas_tb.Text = hide_pas;
            open_eye.Visibility = Visibility.Visible;
            close_eye.Visibility = Visibility.Hidden;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditProfile editProfile = new EditProfile();
            editProfile.ShowDialog();
        }
    }
}
