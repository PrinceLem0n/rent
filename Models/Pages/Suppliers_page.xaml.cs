using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Rent.Models.Windows;

namespace Rent.Models.Pages
{
    /// <summary>
    /// Логика взаимодействия для Suppliers_page.xaml
    /// </summary>
    public partial class Suppliers_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        public Suppliers_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            Edit.Visibility = Visibility.Hidden;

            //Вывод списка игр

            NpgsqlCommand command = new NpgsqlCommand("select name as Название from suppliers", sqlConnection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;

            Search_tb.Text = "Поиск";
            Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void AddSupplier_btn_Click(object sender, RoutedEventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier();
            addSupplier.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var row = Datagrid.SelectedItem as DataRowView;
            //var cellValue = row["Название"];

            NpgsqlCommand command = new NpgsqlCommand("select id_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Название"].ToString());
            Data.IdEditSupplier = Convert.ToInt32(command.ExecuteScalar());

            EditSupplier editSupplier = new EditSupplier();
            editSupplier.ShowDialog();
        }

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
        }

        private void Search_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_tb.Text != "Поиск")
            {
                NpgsqlCommand command = new NpgsqlCommand($"select name as Название from suppliers where name like '%{Search_tb.Text}%'", sqlConnection);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Datagrid.ItemsSource = dataTable.DefaultView;
            }
        }

        private void Search_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Search_tb.Foreground = Brushes.White;
            if (Search_tb.Text == "Поиск")
            {
                Search_tb.Text = "";
            }
        }

        private void Search_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Search_tb.Text == "")
            {
                Search_tb.Text = "Поиск";
                Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void Datagrid_LostFocus(object sender, RoutedEventArgs e)
        {
            Datagrid.SelectedIndex = -1;
            if (Datagrid.SelectedIndex < 0)
            {
                Edit.Visibility = Visibility.Hidden;
            }
        }
    }
}
