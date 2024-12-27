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
using System.Windows.Shapes;
using Npgsql;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditEquipment.xaml
    /// </summary>
    public partial class EditEquipment : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public EditEquipment()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Select Name from equipments where id_equipment = @equipment and id_supplier = @supplier", sqlConnection);

            command.Parameters.AddWithValue("equipment", Data.NameEditEquipment);
            command.Parameters.AddWithValue("supplier", Data.SupplierEditEquipment);

            Name_tb.Text = command.ExecuteScalar().ToString();
            Count_tb.Text = Data.CountEditEquipment.ToString();
            Price_tb.Text = Data.PriceEditEquipment.ToString();

            command = new NpgsqlCommand("Select Name as Название from Suppliers", sqlConnection);

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Supplier_cb.DisplayMemberPath = "Название";

            Supplier_cb.ItemsSource = table.DefaultView;

            Supplier_cb.SelectedIndex = Data.SupplierEditEquipment - 1;

            Name_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));
            Count_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));
            Price_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("white"));

            //Supplier_cb.Background = new SolidColorBrush(Colors.Pink);
            //foreach (var item in Supplier_cb.Items)
            //{
            //    if (item is ComboBoxItem comboBoxItem)
            //    {
            //        comboBoxItem.Background = new SolidColorBrush(Colors.Pink);
            //    }
            //}
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Name_tb.Text == "")
            {
                MessageBox.Show("Введите название товара!");
                Name_tb.Focus();
            }
            else
            {
                if (Count_tb.Text == "")
                {
                    MessageBox.Show("Введите количество товара!");
                    Count_tb.Focus();
                }
                else
                {
                    if (Price_tb.Text == "")
                    {
                        MessageBox.Show("Введите цену товара!");
                        Price_tb.Focus();
                    }
                    else
                    {
                        if (Supplier_cb.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите поставщика!");
                            Supplier_cb.Focus();
                        }
                        else
                        {
                            NpgsqlCommand command = new NpgsqlCommand("select Name from Equipments where Name = @name and ID_supplier = @supplier and Count = @count and Price = @price", sqlConnection);
                            command.Parameters.AddWithValue("name", Name_tb.Text);
                            command.Parameters.AddWithValue("supplier", Supplier_cb.SelectedIndex + 1);
                            command.Parameters.AddWithValue("count", Convert.ToInt32(Count_tb.Text));
                            command.Parameters.AddWithValue("price", Convert.ToInt32(Price_tb.Text));
                            //command.Parameters.AddWithValue("old_name", Data.NameEditEquipment);

                            DataTable dataTable = new DataTable();

                            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);

                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count <= 0)
                            {
                                //command = new NpgsqlCommand($"select Id from Categories where Id = {Category_cb.SelectedIndex + 1}", sqlConnection);
                                //int Category_id = Convert.ToInt32(command.ExecuteScalar());

                                command = new NpgsqlCommand("select name from Equipments where id_equipment = @equipment and ID_supplier = @supplier", sqlConnection);
                                command.Parameters.AddWithValue("equipment", Data.NameEditEquipment);
                                command.Parameters.AddWithValue("supplier", Data.SupplierEditEquipment);

                                string old_name = command.ExecuteScalar().ToString();

                                command = new NpgsqlCommand("update Equipments Set ID_supplier = @supplier, Name = @name, Count = @count, Price = @price where Name = @old_name and ID_supplier = @old_supplier", sqlConnection);
                                command.Parameters.AddWithValue("supplier", Supplier_cb.SelectedIndex + 1);
                                command.Parameters.AddWithValue("name", Name_tb.Text);
                                command.Parameters.AddWithValue("count", Convert.ToInt32(Count_tb.Text));
                                command.Parameters.AddWithValue("price", Convert.ToInt32(Price_tb.Text));
                                command.Parameters.AddWithValue("old_name", old_name);
                                command.Parameters.AddWithValue("old_supplier", Data.SupplierEditEquipment);

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    //command = new NpgsqlCommand("update baskets Set ID_supplier = @supplier where id_equipment = @equipment and ID_supplier = @old_supplier and status = 'Редактирование'", sqlConnection);
                                    //command.Parameters.AddWithValue("supplier", Supplier_cb.SelectedIndex + 1);
                                    //command.Parameters.AddWithValue("equipment", Data.NameEditEquipment);
                                    //command.Parameters.AddWithValue("old_supplier", Data.SupplierEditEquipment);

                                    //adapter = new NpgsqlDataAdapter(command);
                                    //adapter.Fill(dataTable);

                                    MessageBox.Show("Данныые о товаре обновлены!");
                                    Data.MainFraimContent = "main";
                                    this.Close();
                                    Main main = new Main();
                                    main.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Данныые о товаре не обновлены!");
                                }
                            }
                        }
                    }
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

        private void Count_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Count_tb.Text == "")
            {
                Count_tb.Text = "Количество";
                Count_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
            else
            {
                try
                {
                    Convert.ToInt32(Count_tb.Text);
                }
                catch
                {
                    MessageBox.Show("Введите число!");
                }
            }
        }

        private void Count_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Count_tb.Foreground = Brushes.White;
            if (Count_tb.Text == "Количество")
            {
                Count_tb.Text = "";
            }
        }

        private void Price_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Price_tb.Text == "")
            {
                Price_tb.Text = "Цена";
                Price_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            }
            else
            {
                try
                {
                    Convert.ToInt32(Price_tb.Text);
                }
                catch
                {
                    MessageBox.Show("Введите число!");
                }
            }
        }

        private void Price_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Price_tb.Foreground = Brushes.White;
            if (Price_tb.Text == "Цена")
            {
                Price_tb.Text = "";
            }
        }
    }
}
