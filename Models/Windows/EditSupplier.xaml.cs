using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        private NpgsqlConnection sqlConnection = null;

        string old_name;

        public EditSupplier()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("select name from suppliers where id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("supplier", Data.IdEditSupplier);

            old_name = command.ExecuteScalar().ToString();

            Name_tb.Text = old_name;
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "")
            {
                MessageBox.Show("Введите название!");
                Name_tb.Focus();
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand("select name from suppliers where Name like @name", sqlConnection);
                command.Parameters.AddWithValue("name", Name_tb.Text);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count <= 0)
                {
                    command = new NpgsqlCommand("update Suppliers set Name = @name where Name like @old_name", sqlConnection);
                    command.Parameters.AddWithValue("name", Name_tb.Text);
                    command.Parameters.AddWithValue("old_name", old_name);

                    if (command.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Информация об поставщике обнавлена!");

                        Data.MainFraimContent = "suppliers";
                        this.Close();
                        Main main = new Main();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show("Информация об поставщике не обнавлена!");
                    }
                }
            }
        }
    }
}
