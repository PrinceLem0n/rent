using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Логика взаимодействия для DeleteEquipment.xaml
    /// </summary>
    public partial class DeleteEquipment : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public DeleteEquipment()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlCommand command = new NpgsqlCommand("delete from baskets where id_client = @client and id_equipment = @equipment and status = 'Редактирование'", sqlConnection);
            command.Parameters.AddWithValue("client", Data.Id_client);
            command.Parameters.AddWithValue("equipment", Data.NameDeleteEquipment);
            //command.Parameters.AddWithValue("client", 2);
            //command.Parameters.AddWithValue("equipment", 1);

            //MessageBox.Show(Data.Id_client.ToString());
            //MessageBox.Show(Data.NameDeleteEquipment.ToString());

            string str = command.ExecuteNonQuery().ToString();

            MessageBox.Show($"Строк задействовано: {str}");
            MessageBox.Show("Товар удалён!");
            Data.MainFraimContent = "basket";
            this.Close();
            Main main = new Main();
            main.Show();
        }
    }
}
