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
using System.Windows.Shapes;
using Npgsql;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public EditEmployee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("select firstname from employees where id_employee = @employee", sqlConnection);
            command.Parameters.AddWithValue("employee", Data.IdEditEmployee);
            firstname_tb.Text = command.ExecuteScalar().ToString();

            command = new NpgsqlCommand("select lastname from employees where id_employee = @employee", sqlConnection);
            command.Parameters.AddWithValue("employee", Data.IdEditEmployee);
            lastname_tb.Text = command.ExecuteScalar().ToString();

            command = new NpgsqlCommand("select password from employees where id_employee = @employee", sqlConnection);
            command.Parameters.AddWithValue("employee", Data.IdEditEmployee);
            pas_tb.Text = command.ExecuteScalar().ToString();

            firstname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));
            lastname_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));
            pas_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (firstname_tb.Text == "")
            {
                MessageBox.Show("Введите имя!");
                firstname_tb.Focus();
            }
            else
            {
                if (lastname_tb.Text == "")
                {
                    MessageBox.Show("Введите фамилию!");
                    lastname_tb.Focus();
                }
                else
                {
                    if (pas_tb.Text == "")
                    {
                        MessageBox.Show("Введите пароль!");
                        pas_tb.Focus();
                    }
                    else
                    {
                        NpgsqlCommand command = new NpgsqlCommand("update employees set firstname = @firstname, lastname = @lastname, password = @pas where id_employee = @employee", sqlConnection);
                        command.Parameters.AddWithValue("firstname", firstname_tb.Text);
                        command.Parameters.AddWithValue("lastname", lastname_tb.Text);
                        command.Parameters.AddWithValue("pas", pas_tb.Text);
                        command.Parameters.AddWithValue("employee", Data.IdEditEmployee);

                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Информация о сотруднике обнавлена!");

                            Data.MainFraimContent = "employees";
                            this.Close();
                            Main main = new Main();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Информация о сотруднике не обнавлена!");
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
