using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using Npgsql;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        private NpgsqlConnection sqlConnection = null;

        bool vis = true;

        public Reg()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            firstname_tb.Text = "Имя";
            firstname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            lastname_tb.Text = "Фамилия";
            lastname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            log_tb.Text = "Логин";
            log_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            pas_tb.Text = "Пароль";
            pas_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            open_eye.Visibility = Visibility.Hidden;
        }

        private void Reg_btn_Click(object sender, RoutedEventArgs e)
        {
            if (firstname_tb.Text == "Имя" || lastname_tb.Text == "Фамилия" || log_tb.Text == "Логин" || pas_tb.Text == "Пароль")
            {
                MessageBox.Show("Введите данные!");
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand($"select Firstname from Clients where Login like @log", sqlConnection);
                command.Parameters.AddWithValue("log", log_tb.Text);

                DataTable dataTable = new DataTable();

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

                adapter.SelectCommand = command;
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count <= 0)
                {
                    MessageBox.Show("Такого пользователя нет");
                    NpgsqlCommand command1 = new NpgsqlCommand("Insert into Clients (Firstname, Lastname, Login, Password) values (@firstname, @lastname, @log, @pas)", sqlConnection);
                    command1.Parameters.AddWithValue("firstname", firstname_tb.Text);
                    command1.Parameters.AddWithValue("lastname", lastname_tb.Text);
                    command1.Parameters.AddWithValue("log", log_tb.Text);
                    MessageBox.Show($"{firstname_tb.Text}");
                    MessageBox.Show($"{lastname_tb.Text}");
                    MessageBox.Show($"{log_tb.Text}");
                    if (vis)
                    {
                        command1.Parameters.AddWithValue("pas", pas_tb.Text);
                        MessageBox.Show($"{pas_tb.Text}");
                    }
                    else
                    {
                        command1.Parameters.AddWithValue("pas", pas);
                        MessageBox.Show($"{pas}");
                    }

                    if (command1.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не добавлен!");
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                }
            }
        }

        private void log_lb_MouseEnter(object sender, MouseEventArgs e)
        {
            Log_lb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#008a12"));
        }

        private void Log_lb_MouseLeave(object sender, MouseEventArgs e)
        {
            Log_lb.Foreground = Brushes.White;
        }

        private void Log_lb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Log log = new Log();
            log.Show();
        }

        private void firstname_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            firstname_tb.Foreground = Brushes.White;
            if (firstname_tb.Text == "Имя")
            {
                firstname_tb.Text = "";
            }
        }

        private void firstname_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (firstname_tb.Text == "")
            {
                firstname_tb.Text = "Имя";
                firstname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void lastname_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            lastname_tb.Foreground = Brushes.White;
            if (lastname_tb.Text == "Фамилия")
            {
                lastname_tb.Text = "";
            }
        }

        private void lastname_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lastname_tb.Text == "")
            {
                lastname_tb.Text = "Фамилия";
                lastname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void log_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            log_tb.Foreground = Brushes.White;
            if (log_tb.Text == "Логин")
            {
                log_tb.Text = "";
            }
        }

        private void log_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (log_tb.Text == "")
            {
                log_tb.Text = "Логин";
                log_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void pas_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            pas_tb.Foreground = Brushes.White;
            if (pas_tb.Text == "Пароль")
            {
                pas_tb.Text = "";
            }
        }

        private void pas_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pas_tb.Text == "")
            {
                pas_tb.Text = "Пароль";
                pas_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        string pas;
        private void open_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vis = !vis;
            if (pas != "Пароль")
            {
                pas_tb.Text = pas;
            }
            open_eye.Visibility = Visibility.Hidden;
            close_eye.Visibility = Visibility.Visible;
            if (pas_tb.IsFocused == true)
            {
                pas_tb.SelectionStart = pas_tb.Text.Length;
            }
        }

        private void close_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vis = !vis;
            pas = pas_tb.Text;
            string hide_pas = "";

            if (pas != "Пароль")
            {
                for (int i = 0; i < pas.Length; i++)
                {
                    hide_pas += "●";
                }
                pas_tb.Text = hide_pas;
            }
            open_eye.Visibility = Visibility.Visible;
            close_eye.Visibility = Visibility.Hidden;
            if (pas_tb.IsFocused == true)
            {
                pas_tb.SelectionStart = pas_tb.Text.Length;
            }
        }

        private void pas_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!vis)
            {
                if (pas_tb.Text.Length > pas.Length)
                {
                    pas += pas_tb.Text.Substring(pas.Length, 1);
                    pas_tb.Text = pas_tb.Text.Substring(0, pas.Length - 1) + "●";
                    pas_tb.SelectionStart = pas_tb.Text.Length;
                }
                else
                {
                    if (pas_tb.Text.Length < pas.Length)
                    {
                        pas = pas.Remove(pas.Length - 1, 1);
                    }
                }
            }
        }
    }
}
