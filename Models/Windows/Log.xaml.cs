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
using System.Configuration;
using Npgsql;
using System.Data;
using System.Windows.Markup;
using static System.Net.Mime.MediaTypeNames;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для Log.xaml
    /// </summary>
    public partial class Log : Window
    {
        private NpgsqlConnection sqlConnection = null;

        bool vis = true;

        public Log()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            Log_tb.Text = "Логин";
            Log_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            Pas_tb.Text = "Пароль";
            Pas_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            open_eye.Visibility = Visibility.Hidden;
        }

        private void Enter_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Log_tb.Text == "" || Pas_tb.Text == "")
            {
                MessageBox.Show("Все поля должны быть заполнены!");
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand("select firstname from Clients where login = @log and password = @pas", sqlConnection);

                command.Parameters.AddWithValue("log", Log_tb.Text);
                if (vis)
                {
                    command.Parameters.AddWithValue("pas", Pas_tb.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("pas", pas);
                }

                DataTable dataTable = new DataTable();

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);

                adapter.Fill(dataTable);

                if (dataTable.Rows.Count != 0)
                {
                    MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!");
                    Data.Login = Log_tb.Text;
                    Data.Role = "Клиент";
                    this.Hide();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    command = new NpgsqlCommand("select firstname from Employees where login = @log and password = @pas", sqlConnection);

                    command.Parameters.AddWithValue("log", Log_tb.Text);
                    if (vis)
                    {
                        command.Parameters.AddWithValue("pas", Pas_tb.Text);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("pas", pas);
                    }

                    adapter = new NpgsqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        MessageBox.Show($"Добро пожаловать, {command.ExecuteScalar()}!");
                        Data.Login = Log_tb.Text;
                        this.Hide();
                        Main main = new Main();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неправильный логин или пароль!");
                    }
                }
            }
        }

        private void Log_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Log_tb.Foreground = Brushes.White;
            if (Log_tb.Text == "Логин")
            {
                Log_tb.Text = "";
            }
        }

        private void Log_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Log_tb.Text == "")
            {
                Log_tb.Text = "Логин";
                Log_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void Pas_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Pas_tb.Foreground = Brushes.White;
            if (Pas_tb.Text == "Пароль")
            {
                Pas_tb.Text = "";
            }
        }

        private void Pas_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pas_tb.Text == "")
            {
                Pas_tb.Text = "Пароль";
                Pas_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }
        string pas;
        private void open_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vis = !vis;
            if (pas != "Пароль")
            {
                //if (Pas_tb.Text.Length > pas.Length)
                //{
                //    for (int i = pas.Length; i < Pas_tb.Text.Length; i++)
                //    {
                //        pas += Pas_tb.Text[i];
                //    }
                //}
                Pas_tb.Text = pas;
            }
            open_eye.Visibility = Visibility.Hidden;
            close_eye.Visibility = Visibility.Visible;
            if (Pas_tb.IsFocused ==  true)
            {
                Pas_tb.SelectionStart = Pas_tb.Text.Length;
            }
        }

        private void close_eye_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            vis = !vis;
            pas = Pas_tb.Text;
            string hide_pas = "";

            if (pas != "Пароль")
            {
                for (int i = 0; i < pas.Length; i++)
                {
                    hide_pas += "●";
                }
                Pas_tb.Text = hide_pas;
            }
            open_eye.Visibility = Visibility.Visible;
            close_eye.Visibility = Visibility.Hidden;
            if (Pas_tb.IsFocused == true)
            {
                Pas_tb.SelectionStart = Pas_tb.Text.Length;
            }
        }

        private void Pas_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!vis)
            {
                if (Pas_tb.Text.Length > pas.Length)
                {
                    pas += Pas_tb.Text.Substring(pas.Length, 1);
                    Pas_tb.Text = Pas_tb.Text.Substring(0, pas.Length - 1) + "●";
                    Pas_tb.SelectionStart = Pas_tb.Text.Length;
                }
                else
                {
                    if (Pas_tb.Text.Length < pas.Length)
                    {
                        pas = pas.Remove(pas.Length - 1, 1);
                    }
                }
            }
        }

        private void Reg_lb_MouseEnter(object sender, MouseEventArgs e)
        {
            Reg_lb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#008a12"));
        }

        private void Reg_lb_MouseLeave(object sender, MouseEventArgs e)
        {
            Reg_lb.Foreground = Brushes.White;
        }

        private void Reg_lb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            Reg reg = new Reg();
            reg.Show();
        }
    }
}
