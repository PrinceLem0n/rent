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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Rent.Models.Windows;

namespace Rent.Models.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main_page.xaml
    /// </summary>
    public partial class Main_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        //DataTable equipTable = new DataTable();

        public Main_page()
        {
            InitializeComponent();
        }

        //string role;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            //Проверка роли пользователя

            NpgsqlCommand command = new NpgsqlCommand("Select id_client from Clients where Login = @log", sqlConnection);
            command.Parameters.AddWithValue("log", Data.Login);
            Data.Id_client = Convert.ToInt32(command.ExecuteScalar());

            if (Data.Role == "Клиент" || Data.Role == "Сотрудник")
            {
                //role = "user";
                AddEquipment_btn.Visibility = Visibility.Hidden;
                AddInBasket_btn.Visibility = Visibility.Hidden;
            }
            else
            {
                //role = "admin";
                AddInBasket_btn.Visibility = Visibility.Hidden;
            }

            EditDeleteGameGrid.Visibility = Visibility.Hidden;
            Count_tb.Visibility = Visibility.Hidden;

            //Вывод списка игр

            command = new NpgsqlCommand("select equipments.name as Название, suppliers.name as Поставщик, equipments.count as Количество, equipments.price as Цена from equipments inner join suppliers on equipments.id_supplier = suppliers.id_supplier where equipments.count > 0", sqlConnection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            //adapter.Fill(equipTable);
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;

            Search_tb.Text = "Поиск";
            Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void AddEquipment_btn_Click(object sender, RoutedEventArgs e)
        {
            AddEquipment addEquipment = new AddEquipment();
            addEquipment.ShowDialog();
        }

        private void AddInBasket_btn_Click(object sender, RoutedEventArgs e)
        {

            var row = Datagrid.SelectedItem as DataRowView;
            var cellValue = row["Название"];
            MessageBox.Show(cellValue.ToString());

            NpgsqlCommand command = new NpgsqlCommand("Select id_client from Clients where Login = @log", sqlConnection);
            command.Parameters.AddWithValue("log", Data.Login);
            int client = Convert.ToInt32(command.ExecuteScalar());
            //Data.Id_client = client;
            //int client = Convert.ToInt32(row["Название"]);

            command = new NpgsqlCommand("select id_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            int supplier = Convert.ToInt32(command.ExecuteScalar());
            //int supplier = Convert.ToInt32(row["Поставщик"]);

            command = new NpgsqlCommand("select id_equipment from Equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Название"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            int equipment = Convert.ToInt32(command.ExecuteScalar());
            //int equipment = Convert.ToInt32(row["Поставщик"]);

            string status = "Редактирование";

            command = new NpgsqlCommand("Select * from Baskets where id_client = @client and id_equipment = @equipment and status = @status", sqlConnection);
            //command = new NpgsqlCommand("Select * from Baskets where id_client = @client and id_equipment = @equipment and id_supplier = @supplier and status = @status", sqlConnection);

            command.Parameters.AddWithValue("client", client);
            command.Parameters.AddWithValue("equipment", equipment);
            //command.Parameters.AddWithValue("supplier", supplier);
            command.Parameters.AddWithValue("status", status);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count <= 0)
            {
                //MessageBox.Show($"{client}");
                //MessageBox.Show($"{equipment}");
                //MessageBox.Show($"{supplier}");
                //MessageBox.Show($"{Convert.ToInt32(Count_tb.Text)}");
                //MessageBox.Show("Редактирование");
                command = new NpgsqlCommand("insert into Baskets (id_client, id_equipment, count, status) values (@client, @equipment, @count, @status)", sqlConnection);
                //command = new NpgsqlCommand("insert into Baskets (id_client, id_equipment, id_supplier, count, status) values (@client, @equipment, @supplier, @count, @status)", sqlConnection);
                command.Parameters.AddWithValue("client", client);
                command.Parameters.AddWithValue("equipment", equipment);
                //command.Parameters.AddWithValue("supplier", supplier);
                command.Parameters.AddWithValue("count", Convert.ToInt32(Count_tb.Text));
                command.Parameters.AddWithValue("status", status);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Товар добавлен в вашу корзину!");
                }
                else
                {
                    MessageBox.Show("Товар не добавлен в вашу корзину!");
                }
            }
            else
            {
                MessageBox.Show("товар уже находится в вашей корзине!");
            }




            //SqlCommand command = new SqlCommand("select Games.Name as 'Название', Games.Category as 'Категория', Games.Memory as 'Место' from Games, Categories where Games.Category = Categories.Id", sqlConnection);
            //NpgsqlCommand command = new NpgsqlCommand("select Name as 'Название', Category as 'Категория', Memory as 'Место' from Games", sqlConnection);

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //textbox.Text = dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString();

            //MessageBox.Show(Datagrid.SelectedIndex.ToString());
            //MessageBox.Show(dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString());

            //command = new NpgsqlCommand("Select Name from Library where Name like @name and Login like @log", sqlConnection);
            //command.Parameters.AddWithValue("name", dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString());
            //command.Parameters.AddWithValue("log", Data.Login);

            //NpgsqlDataAdapter adapter2 = new NpgsqlDataAdapter(command);
            //DataTable dataTable2 = new DataTable();
            //adapter2.Fill(dataTable2);

            //MessageBox.Show(dataTable2.Rows.Count.ToString());
            //MessageBox.Show(command.ExecuteNonQuery().ToString());

            //if (dataTable2.Rows.Count <= 0)
            //{
            //    command = new NpgsqlCommand("insert into Library (Name, Category, Memory, Login) values (@name, @category, @memory, @log)", sqlConnection);
            //    command.Parameters.AddWithValue("name", dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString());
            //    command.Parameters.AddWithValue("category", Convert.ToInt32(dataTable.DefaultView[Datagrid.SelectedIndex]["Категория"]));
            //    command.Parameters.AddWithValue("memory", (float)Convert.ToDouble(dataTable.DefaultView[Datagrid.SelectedIndex]["Место"]));
            //    command.Parameters.AddWithValue("log", Data.Login);

            //    if (command.ExecuteNonQuery() == 1)
            //    {
            //        MessageBox.Show("Игра добавлена в вашу библиотеку!");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Игра не добавлена в вашу библиотеку!");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Игра уже находится в вашей библиотеке!");
            //}
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var row = Datagrid.SelectedItem as DataRowView;
            //var cellValue = row["Название"];

            NpgsqlCommand command = new NpgsqlCommand("select id_supplier from Suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            int supplier = Convert.ToInt32(command.ExecuteScalar());
            //int supplier = Convert.ToInt32(row["Поставщик"]);

            command = new NpgsqlCommand("select id_equipment from Equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Название"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            int equipment = Convert.ToInt32(command.ExecuteScalar());

            //NpgsqlCommand command = new NpgsqlCommand("select Games.Name as 'Название', Games.Category as 'Категория', Games.Memory as 'Место' from Games, Categories where Games.Category = Categories.Id", sqlConnection);
            //command.ExecuteNonQuery();

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            Data.SupplierEditEquipment = supplier;
            Data.NameEditEquipment = equipment;
            Data.CountEditEquipment = Convert.ToInt32(row["Количество"]);
            Data.PriceEditEquipment = Convert.ToInt32(row["Цена"]);

            //MessageBox.Show($"Поставщик: {Data.SupplierEditEquipment}");
            //MessageBox.Show($"Товар: {Data.NameEditEquipment}");
            //MessageBox.Show($"Количество: {Data.CountEditEquipment}");
            //MessageBox.Show($"Цена: {Data.PriceEditEquipment}");

            //Data.SupplierEditEquipment = 1;
            //Data.NameEditEquipment = dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString();
            //Data.CountEditEquipment = Convert.ToInt32(dataTable.DefaultView[Datagrid.SelectedIndex]["Категория"]);
            //Data.PriceEditEquipment = Convert.ToInt32(dataTable.DefaultView[Datagrid.SelectedIndex]["Место"]);

            EditEquipment editEquipment = new EditEquipment();
            editEquipment.ShowDialog();
            //EditGame editGame = new EditGame();
            //editGame.ShowDialog();
        }

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Data.Role == "Клиент")
            {
                AddInBasket_btn.Visibility = Visibility.Visible;
                Count_tb.Visibility = Visibility.Visible;
                Count_tb.Text = "1";
            }
            else
            {
                if (Data.Role == "Администратор")
                {
                    EditDeleteGameGrid.Visibility = Visibility.Visible;
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //Data.NamePage = "main";

            //NpgsqlCommand command = new NpgsqlCommand("select Games.Name as 'Название', Games.Category as 'Категория', Games.Memory as 'Место' from Games, Categories where Games.Category = Categories.Id", sqlConnection);
            //command.ExecuteNonQuery();

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //Data.NameDeleteGame = dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString();
            ////Data.CategoryDeleteGame = Convert.ToInt32(dataTable.DefaultView[Datagrid.SelectedIndex]["Категория"]);
            ////Data.MemoryDeleteGame = (float)Convert.ToDouble(dataTable.DefaultView[Datagrid.SelectedIndex]["Место"]);

            ////DeleteGame deleteGame = new DeleteGame();
            ////deleteGame.ShowDialog();
        }

        private void Search_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_tb.Text != "Поиск")
            {
                NpgsqlCommand command = new NpgsqlCommand($"select equipments.name as Название, suppliers.name as Поставщик, equipments.count as Количество, equipments.price as Цена from equipments inner join suppliers on equipments.id_supplier = suppliers.id_supplier where equipments.count > 0 and equipments.name like '%{Search_tb.Text}%'", sqlConnection);

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
            AddInBasket_btn.Visibility = Visibility.Hidden;
            Count_tb.Visibility = Visibility.Hidden;
        }

        private void Search_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Search_tb.Text == "")
            {
                Search_tb.Text = "Поиск";
                Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
        }

        private void Count_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            var row = Datagrid.SelectedItem as DataRowView;
            //var cellValue = row["Количество"];
            string count = row["Количество"].ToString();
            int max = Convert.ToInt32(count);
            //int max = Convert.ToInt32(equipTable.DefaultView[Datagrid.SelectedIndex]["Количество"].ToString());
            if (Convert.ToInt32(Count_tb.Text) < 0)
            {
                Count_tb.Text = "1";
            }
            else
            {
                if (Convert.ToInt32(Count_tb.Text) > max)
                {
                    Count_tb.Text = max.ToString();
                }
            }
            try
            {
                Convert.ToInt32(Count_tb.Text);

                if (Convert.ToInt32(Count_tb.Text) < 0)
                {
                    Count_tb.Text = "1";
                }
                else
                {
                    if (Convert.ToInt32(Count_tb.Text) > max)
                    {
                        Count_tb.Text = max.ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Введите число!");
            }
        }

        private void Datagrid_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (!Search_tb.IsFocused)
            //{
            //    Datagrid.SelectedIndex = -1;
            //    if (Datagrid.SelectedIndex < 0)
            //    {
            //        Edit.Visibility = Visibility.Hidden;
            //    }
            //}
        }
    }
}
