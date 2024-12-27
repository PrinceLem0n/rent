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
    /// Логика взаимодействия для Bids_page.xaml
    /// </summary>
    public partial class Bids_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        int employee;

        public Bids_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            Btn_grid.Visibility = Visibility.Hidden;
            Consideration_rb.IsChecked = true;
            Bill_btn.Visibility = Visibility.Hidden;

            NpgsqlCommand command = null;

            if (Data.Role == "Клиент")
            {
                Reject_btn.Visibility = Visibility.Hidden;
                Enter_btn.Visibility = Visibility.Hidden;

                //command = new NpgsqlCommand("select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого, bids.status as Статус from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client ", sqlConnection);
                //command.Parameters.AddWithValue("client", Data.Id_client);
                //MessageBox.Show(Data.Id_client.ToString());

            }
            else
            {
                Delete_btn.Visibility = Visibility.Hidden;

                command = new NpgsqlCommand("select ID_employee from employees where login = @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);
                employee = Convert.ToInt32(command.ExecuteScalar());

                //command = new NpgsqlCommand("select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Рассмотрение' ", sqlConnection);
            }

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //Datagrid.ItemsSource = dataTable.DefaultView;

            Search_tb.Text = "Поиск";
            Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void Reject_btn_Click(object sender, RoutedEventArgs e)
        {
            int client;
            int supplier;
            int equipment;
            int basket;

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

            command = new NpgsqlCommand("update bids set status = 'Отклонено', ID_employee = @employee where id_basket = @basket", sqlConnection);
            command.Parameters.AddWithValue("basket", basket);
            command.Parameters.AddWithValue("employee", employee);
            command.ExecuteNonQuery();
            MessageBox.Show("Заявка отклонена!");
            Data.MainFraimContent = "bids";
            Main main = new Main();
            main.Show();
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            int supplier;
            int equipment;
            int basket;

            var row = Datagrid.SelectedItem as DataRowView;

            NpgsqlCommand command = new NpgsqlCommand("select ID_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            supplier = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_equipment from Equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Товар"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            equipment = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_basket from baskets where id_client = @client and id_equipment = @equipment", sqlConnection);
            command.Parameters.AddWithValue("client", Data.Id_client);
            command.Parameters.AddWithValue("equipment", equipment);
            basket = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("delete from bids where id_basket = @basket", sqlConnection);
            command.Parameters.AddWithValue("basket", basket);
            command.ExecuteNonQuery();
            MessageBox.Show("Заявка отменена!");
            Data.MainFraimContent = "bids";
            Main main = new Main();
            main.Show();
        }

        private void Enter_btn_Click(object sender, RoutedEventArgs e)
        {
            int client;
            int supplier;
            int equipment;
            int basket;

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

            command = new NpgsqlCommand("update bids set status = 'Одобрено', ID_employee = @employee where id_basket = @basket", sqlConnection);
            command.Parameters.AddWithValue("basket", basket);
            command.Parameters.AddWithValue("employee", employee);
            command.ExecuteNonQuery();
            MessageBox.Show("Заявка одобрена!");
            Data.MainFraimContent = "bids";
            Main main = new Main();
            main.Show();
        }

        private void Search_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_tb.Text != "Поиск")
            {
                NpgsqlCommand command = null;
                if (Data.Role == "Клиент")
                {
                    Reject_btn.Visibility = Visibility.Hidden;
                    Enter_btn.Visibility = Visibility.Hidden;
                    Bill_btn.Visibility = Visibility.Hidden;

                    if (Consideration_rb.IsChecked == true)
                    {
                        command = new NpgsqlCommand($"select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and bids.status = 'Рассмотрение' and equipments.name like '%{Search_tb.Text}%'", sqlConnection);
                        command.Parameters.AddWithValue("client", Data.Id_client);
                    }
                    else
                    {
                        command = new NpgsqlCommand($"select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and bids.status = 'Одобрено' and equipments.name like '%{Search_tb.Text}%'", sqlConnection);
                        command.Parameters.AddWithValue("client", Data.Id_client);
                    }

                    //command = new NpgsqlCommand($"select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого, bids.status as Статус from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and equipments.name like '%{Search_tb.Text}%'", sqlConnection);
                    //command.Parameters.AddWithValue("client", Data.Id_client);
                    //MessageBox.Show(Data.Id_client.ToString());

                }
                else
                {
                    Delete_btn.Visibility = Visibility.Hidden;

                    if (Consideration_rb.IsChecked == true)
                    {
                        command = new NpgsqlCommand($"select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Рассмотрение' and clients.lastname like '%{Search_tb.Text}%'", sqlConnection);
                    }
                    else
                    {
                        command = new NpgsqlCommand($"select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Одобрено' and clients.lastname like '%{Search_tb.Text}%'", sqlConnection);
                    }

                    //command = new NpgsqlCommand($"select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Рассмотрение' and clients.lastname lke '%{Search_tb.Text}%'", sqlConnection);
                }
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                //Datagrid.ItemsSource = null;
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
                if (Consideration_rb.IsChecked == true)
                {
                    Btn_grid.Visibility = Visibility.Visible;
                }
                else
                {
                    if (Data.Role != "Клиент")
                    {
                        Bill_btn.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Consideration_rb_Checked(object sender, RoutedEventArgs e)
        {
            Consideration_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#00a816"));
            Consideration_rb.Foreground = Brushes.White;
            Approved_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#121212"));
            Approved_rb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));

            Bill_btn.Visibility = Visibility.Hidden;
            Btn_grid.Visibility = Visibility.Hidden;

            NpgsqlCommand command = null;

            if (Data.Role == "Клиент")
            {
                command = new NpgsqlCommand("select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and bids.status = 'Рассмотрение'", sqlConnection);
                command.Parameters.AddWithValue("client", Data.Id_client);
                //MessageBox.Show(Data.Id_client.ToString());

            }
            else
            {
                command = new NpgsqlCommand("select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Рассмотрение' ", sqlConnection);
            }

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;
        }

        private void Approved_rb_Checked(object sender, RoutedEventArgs e)
        {
            Approved_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#00a816"));
            Approved_rb.Foreground = Brushes.White;
            Consideration_rb.Background = (Brush)(new BrushConverter().ConvertFrom("#121212"));
            Consideration_rb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));

            Bill_btn.Visibility = Visibility.Hidden;
            Btn_grid.Visibility = Visibility.Hidden;

            NpgsqlCommand command = null;

            if (Data.Role == "Клиент")
            {
                command = new NpgsqlCommand("select equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from Bids inner join baskets on Bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and bids.status = 'Одобрено'", sqlConnection);
                command.Parameters.AddWithValue("client", Data.Id_client);
                //MessageBox.Show(Data.Id_client.ToString());

            }
            else
            {
                command = new NpgsqlCommand("select clients.lastname as Клиент, equipments.name as Товар, suppliers.name as Поставщик, baskets.count as Количество, baskets.count * equipments.price as Итого from bids inner join baskets on bids.id_basket = baskets.id_basket inner join equipments ON equipments.id_equipment = baskets.id_equipment inner join suppliers on suppliers.id_supplier = equipments.id_supplier inner join clients on clients.id_client = baskets.id_client where bids.status = 'Одобрено' ", sqlConnection);
            }

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;
        }

        private void Bill_btn_Click(object sender, RoutedEventArgs e)
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

            command = new NpgsqlCommand("select * from bills where id_bids = @bid", sqlConnection);
            command.Parameters.AddWithValue("bid", bid);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                float summa = Convert.ToInt32(row["Итого"]) * 20 / 100;

                //MessageBox.Show($"Сумма: {summa}");

                command = new NpgsqlCommand("insert into Bills (id_bids, summa) values (@bid, @sum)", sqlConnection);
                command.Parameters.AddWithValue("bid", bid);
                command.Parameters.AddWithValue("sum", summa);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Счёт выставлен!");
                }
                else
                {
                    MessageBox.Show("Счёт не выставлен!");
                }
            }
            else
            {
                MessageBox.Show("Счёт уже выставлен!");
            }
        }
    }
}
