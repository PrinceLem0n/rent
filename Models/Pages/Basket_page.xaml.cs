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
    /// Логика взаимодействия для Basket_page.xaml
    /// </summary>
    public partial class Basket_page : Page
    {
        private NpgsqlConnection sqlConnection = null;

        public Basket_page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Select id_client from Clients where Login = @log", sqlConnection);
            command.Parameters.AddWithValue("log", Data.Login);
            int client = Convert.ToInt32(command.ExecuteScalar());
            Data.Id_client = client;

            command = new NpgsqlCommand("select equipments.name as Название, suppliers.name as Поставщик, baskets.count as Количество, equipments.price * baskets.count as Суммарно from baskets inner join equipments on baskets.id_equipment = equipments.id_equipment inner join suppliers ON suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and baskets.status = 'Редактирование'", sqlConnection);
            command.Parameters.AddWithValue("client", Data.Id_client);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            Datagrid.ItemsSource = dataTable.DefaultView;

            Btn_grid.Visibility = Visibility.Hidden;

            Search_tb.Text = "Поиск";
            Search_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void Datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Btn_grid.Visibility = Visibility.Visible;
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var row = Datagrid.SelectedItem as DataRowView;
            //var cellValue = row["Название"];
            //Data.NamePage = "Library";

            NpgsqlCommand command = new NpgsqlCommand("select id_supplier from suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            int supplier = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_equipment from equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Название"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            int equipment = Convert.ToInt32(command.ExecuteScalar());

            //SqlCommand command = new SqlCommand("select Library.Name as 'Название', Library.Category as 'Категория', Library.Memory as 'Место' from Library, Categories where Library.Category = Categories.Id and Library.Login like @log", sqlConnection);
            //NpgsqlCommand command = new NpgsqlCommand("select Name as 'Название' from Library where Library.Login like @log", sqlConnection);
            command = new NpgsqlCommand("select id_basket from baskets where id_client = @client and id_equipment = @equipment and status = 'Редактирование'", sqlConnection);
            command.Parameters.AddWithValue("client", Data.Id_client);
            command.Parameters.AddWithValue("equipment", equipment);

            Data.NameDeleteEquipment = Convert.ToInt32(command.ExecuteScalar());

            DeleteEquipment deleteEquipment = new DeleteEquipment();
            deleteEquipment.ShowDialog();

            //command.ExecuteNonQuery();

            //NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //Data.NameDeleteGame = dataTable.DefaultView[Datagrid.SelectedIndex]["Название"].ToString();
            //Data.CategoryDeleteGame = Convert.ToInt32(dataTable.DefaultView[Datagrid.SelectedIndex]["Категория"]);
            //Data.MemoryDeleteGame = (float)Convert.ToDouble(dataTable.DefaultView[Datagrid.SelectedIndex]["Место"]);

            //DeleteGame deleteGame = new DeleteGame();
            //deleteGame.ShowDialog();
        }

        private void Search_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search_tb.Text != "Поиск")
            {
                NpgsqlCommand command = new NpgsqlCommand($"select equipments.name as Название, suppliers.name as Поставщик, baskets.count as Количество, equipments.price * baskets.count as Суммарно from baskets inner join equipments on baskets.id_equipment = equipments.id_equipment inner join suppliers ON suppliers.id_supplier = equipments.id_supplier where baskets.id_client = @client and baskets.status = 'Редактирование' and equipments.name like '%{Search_tb.Text}%'", sqlConnection);
                command.Parameters.AddWithValue("client", Data.Id_client);

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

        private void Enter_btn_Click(object sender, RoutedEventArgs e)
        {
            var row = Datagrid.SelectedItem as DataRowView;
            //var cellValue = row["Название"];
            //Data.NamePage = "Library";

            NpgsqlCommand command = new NpgsqlCommand("select id_supplier from suppliers where name = @name", sqlConnection);
            command.Parameters.AddWithValue("name", row["Поставщик"].ToString());
            int supplier = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("select id_equipment from equipments where name = @name and id_supplier = @supplier", sqlConnection);
            command.Parameters.AddWithValue("name", row["Название"].ToString());
            command.Parameters.AddWithValue("supplier", supplier);
            int equipment = Convert.ToInt32(command.ExecuteScalar());

            //SqlCommand command = new SqlCommand("select Library.Name as 'Название', Library.Category as 'Категория', Library.Memory as 'Место' from Library, Categories where Library.Category = Categories.Id and Library.Login like @log", sqlConnection);
            //NpgsqlCommand command = new NpgsqlCommand("select Name as 'Название' from Library where Library.Login like @log", sqlConnection);
            command = new NpgsqlCommand("select id_basket from baskets where id_client = @client and id_equipment = @equipment and status = 'Редактирование'", sqlConnection);
            command.Parameters.AddWithValue("client", Data.Id_client);
            command.Parameters.AddWithValue("equipment", equipment);

            int id_busket = Convert.ToInt32(command.ExecuteScalar());

            Data.IdEnterBasket = Convert.ToInt32(command.ExecuteScalar());

            command = new NpgsqlCommand("insert into bids (id_basket) values (@basket)", sqlConnection);
            command.Parameters.AddWithValue("basket", id_busket);

            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Заявка подана!");
                command = new NpgsqlCommand("update baskets set status = 'Отправлено' where id_basket = @basket", sqlConnection);
                command.Parameters.AddWithValue("basket", id_busket);

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные корзины обновлены!");
                    Data.MainFraimContent = "basket";
                    Main main = new Main();
                    main.Show();
                }
            }
            else
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
    }
}
