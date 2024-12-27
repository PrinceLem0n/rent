using Npgsql;
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
using Npgsql;
using System.Configuration;
using System.Data;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public AddEmployee()
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

            Role_cb.Items.Add("Сотрудник");
            Role_cb.Items.Add("Администратор");
            Role_cb.SelectedIndex = 0;
            Role_cb.Foreground = (Brush)(new BrushConverter().ConvertFrom("black"));
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (firstname_tb.Text == "" || firstname_tb.Text == "Имя")
            {
                MessageBox.Show("Введите имя!");
                firstname_tb.Focus();
            }
            else
            {
                if (lastname_tb.Text == "" || lastname_tb.Text == "Фамилия")
                {
                    MessageBox.Show("Введите фамилию!");
                    lastname_tb.Focus();
                }
                else
                {
                    if (log_tb.Text == "" || log_tb.Text == "Логин")
                    {
                        MessageBox.Show("Введите логин!");
                        log_tb.Focus();
                    }
                    else
                    {
                        if (pas_tb.Text == "" || pas_tb.Text == "Пароль")
                        {
                            MessageBox.Show("Введите пароль!");
                            pas_tb.Focus();
                        }
                        else
                        {
                            if (Role_cb.SelectedItem == null)
                            {
                                MessageBox.Show("Выберите роль!");
                                Role_cb.Focus();
                            }
                            else
                            {
                                NpgsqlCommand command = new NpgsqlCommand("select firstname from employees where firstname = @firstname ", sqlConnection);
                                command.Parameters.AddWithValue("firstname", firstname_tb.Text);

                                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                if (table.Rows.Count <= 0)
                                {
                                    command = new NpgsqlCommand("insert into employees (firstname, lastname, login, password, role) values (@firstname, @lastname, @log, @pas, @role)", sqlConnection);

                                    command.Parameters.AddWithValue("firstname", firstname_tb.Text);
                                    command.Parameters.AddWithValue("lastname", lastname_tb.Text);
                                    command.Parameters.AddWithValue("log", log_tb.Text);
                                    command.Parameters.AddWithValue("pas", pas_tb.Text);
                                    command.Parameters.AddWithValue("role", Role_cb.SelectedValue);

                                    if (command.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Сотрудник добавлен!");

                                        Data.MainFraimContent = "employees";
                                        this.Close();
                                        Main main = new Main();
                                        main.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Сотрудник не добавлен!");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Этот логин уже занят!");
                                    log_tb.Focus();
                                }
                            }
                        }
                    }
                }
            }
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
    }
}
