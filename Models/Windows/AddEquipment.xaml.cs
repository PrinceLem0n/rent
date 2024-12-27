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
    /// Логика взаимодействия для AddEquipment.xaml
    /// </summary>
    public partial class AddEquipment : Window
    {
        private NpgsqlConnection sqlConnection = null;

        public AddEquipment()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            NpgsqlCommand command = new NpgsqlCommand("Select Name as Название from Suppliers", sqlConnection);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            Supplier_cb.DisplayMemberPath = "Название";
            //Category_cb.value

            Supplier_cb.ItemsSource = table.DefaultView;

            Name_tb.Text = "Название";
            Name_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            Count_tb.Text = "Количество";
            Count_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
            Price_tb.Text = "Цена";
            Price_tb.Foreground = (Brush)(new BrushConverter().ConvertFrom("#6b6b6b"));
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void Add_btn_Click(object sender, RoutedEventArgs e)
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
                            NpgsqlCommand command = new NpgsqlCommand("select Name from Equipments where Name = @name and ID_supplier = @supplier", sqlConnection);
                            command.Parameters.AddWithValue("name", Name_tb.Text);
                            command.Parameters.AddWithValue("supplier", Supplier_cb.SelectedIndex + 1);

                            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count <= 0)
                            {
                                //command = new NpgsqlCommand($"select Id from Categories where Id = {Supplier_cb.SelectedIndex + 1}", sqlConnection);
                                //int Category_id = Convert.ToInt32(command.ExecuteScalar());

                                command = new NpgsqlCommand("insert into Equipments (ID_supplier, Name, Count, Price) values (@supplier, @name, @count, @price)", sqlConnection);

                                command.Parameters.AddWithValue("supplier", Supplier_cb.SelectedIndex + 1);
                                command.Parameters.AddWithValue("name", Name_tb.Text);
                                command.Parameters.AddWithValue("count", Convert.ToInt32(Count_tb.Text));
                                command.Parameters.AddWithValue("price", Convert.ToInt32(Price_tb.Text));

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Товар добавлен!");

                                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                                    Data.MainFraimContent = "main";
                                    this.Close();
                                    Main main = new Main();
                                    main.Show();

                                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                }
                                else
                                {
                                    MessageBox.Show("Товар не добавлен!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Игра с таким названием уже существует!");
                                Name_tb.Focus();
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

        private void Count_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            Count_tb.Foreground = Brushes.White;
            if (Count_tb.Text == "Количество")
            {
                Count_tb.Text = "";
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
