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
    /// Логика взаимодействия для Bills_page.xaml
    /// </summary>
    public partial class Bills_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        public Bills_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            Btn_grid.Visibility = Visibility.Hidden;
            Not_paid_rb.IsChecked = true;

            Search_tb.Text = "Поиск";
            Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            int client;
            int supplier;
            int equipment;
            int basket;
            int bid;

            var row = Datagrid.SelectedItem as DataRowView;

            NpgsqlCommand command = new NpgsqlCommand("select id_client from clients where lastname = @lastname", sqlConnection);
            command.Parameters.AddWithValue("lastname", row["Клиент"].ToString());
            client = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select ID_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            supplier = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_equipment from Equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Товар"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            equipment = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_basket from baskets where id_client = @client and id_equipment = @equipment", sqlConnection);
            command.Parameters.AddWithValue("client", client);
            command.Parameters.AddWithValue("equipment", equipment);
            basket = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_bids from bids where id_basket = @basket", sqlConnection);
            command.Parameters.AddWithValue("basket", basket);
            bid = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("delete from bills where id_bids = @bid", sqlConnection);
            command.Parameters.AddWithValue("bid", bid);

            command.ExecuteNonQuery();
            MessageBox.Show("Счёт отменён!");
            Data.MainFraimContent = "bills";
            Main main = new Main();
            main.Show();
        }

        private void Enter_btn_Click(object sender, RoutedEventArgs e)
        {
            int client;
            int supplier;
            int equipment;
            int basket;
            int bid;

            var row = Datagrid.SelectedItem as DataRowView;

            NpgsqlCommand command = new NpgsqlCommand("select id_client from clients where lastname = @lastname", sqlConnection);
            command.Parameters.AddWithValue("lastname", row["Клиент"].ToString());
            client = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select ID_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            supplier = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_equipment from Equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Товар"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            equipment = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_basket from baskets where id_client = @client and id_equipment = @equipment", sqlConnection);
            command.Parameters.AddWithValue("client", client);
            command.Parameters.AddWithValue("equipment", equipment);
            basket = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_bids from bids where id_basket = @basket", sqlConnection);
            command.Parameters.AddWithValue("basket", basket);
            bid = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("update bills set status = 'Оплачено' where id_bids = @bid", sqlConnection);
            command.Parameters.AddWithValue("bid", bid);
            
            command.ExecuteNonQuery();
            MessageBox.Show("Счёт оплачен!");
            Data.MainFraimContent = "bills";
            Main main = new Main();
            main.Show();
        }

        private void Search_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_tb.Text != "Поиск")
            {
                NpgsqlCommand command = null;

                if (Not_paid_rb.IsChecked == true)
                {
                    command = new NpgsqlCommand($"select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, bills.summa as Сумма from bills inner join bids on bids.id_bids = bills.id_bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bills.status = 'Не оплачен' and clients.lastname like '%{Search_tb.Text}%'", sqlConnection);
                }
                else
                {
                    command = new NpgsqlCommand($"select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, bills.summa as Сумма from bills inner join bids on bids.id_bids = bills.id_bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bills.status = 'Оплачено' and clients.lastname like '%{Search_tb.Text}%'", sqlConnection);
                }
                
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

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid.SelectedIndex != -1)
            {
                if (Not_paid_rb.IsChecked == true)
                {
                    Btn_grid.Visibility = Visibility.Visible;
                }
            }
        }

        private void Not_paid_rb_Checked(object sender, RoutedEventArgs e)
        {
            Not_paid_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#00a816"));
            Not_paid_rb.Foreground = Brushes.White;
            Paid_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#121212"));
            Paid_rb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));

            Btn_grid.Visibility = Visibility.Hidden;

            NpgsqlCommand command = new NpgsqlCommand("select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, bills.summa as Сумма from bills inner join bids on bids.id_bids = bills.id_bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bills.status = 'Не оплачен'", sqlConnection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;
        }

        private void Paid_rb_Checked(object sender, RoutedEventArgs e)
        {
            Paid_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#00a816"));
            Paid_rb.Foreground = Brushes.White;
            Not_paid_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#121212"));
            Not_paid_rb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));

            Btn_grid.Visibility = Visibility.Hidden;

            NpgsqlCommand command = new NpgsqlCommand("select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, bills.summa as Сумма from bills inner join bids on bids.id_bids = bills.id_bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bills.status = 'Оплачено'", sqlConnection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
