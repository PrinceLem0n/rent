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
    /// Логика взаимодействия для AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public AddSupplier()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            Name_tb.Text = "Название";
            Name_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "Название" || Name_tb.Text == "")
            {
                MessageBox.Show("Введите название!");
                Name_tb.Focus();
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand("insert into Suppliers (Name) values (@name)", sqlConnection);
                command.Parameters.AddWithValue("name", Name_tb.Text);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Поставщик добавлен!");

                    Data.MainFraimContent = "suppliers";
                    this.Close();
                    Main main = new Main();
                    main.Show();
                }
                else
                {
                    MessageBox.Show("Поставщик не добавлен!");
                }
            }
        }

        private void Name_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Name_tb.Foreground = Brushes.White;
            if (Name_tb.Text == "Название")
            {
                Name_tb.Text = "";
            }
        }

        private void Name_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "")
            {
                Name_tb.Text = "Название";
                Name_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }
    }
}
