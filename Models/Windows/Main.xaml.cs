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
using Rent.Models.Pages;

namespace Rent.Models.Windows
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private NpgsqlConnection sqlConnection = null;

        bool visible;
        string btn;

        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();

            if (Data.MainFraimContent == "main")
            {
                Frame.Content = new Main_page();
                btn = "main";
                Main_btn.IsEnabled = false;
            }
            else
            {
                if (Data.MainFraimContent == "basket")
                {
                    Frame.Content = new Basket_page();
                    btn = "basket";
                    Basket_btn.IsEnabled = false;
                }
                else
                {
                    if (Data.MainFraimContent == "bids")
                    {
                        Frame.Content = new Bids_page();
                        btn = "bids";
                        Bids_btn.IsEnabled = false;
                    }
                    else
                    {
                        if (Data.MainFraimContent == "bills")
                        {
                            Frame.Content = new Bills_page();
                            btn = "bills";
                            Bills_btn.IsEnabled = false;
                        }
                        else
                        {
                            if (Data.MainFraimContent == "suppliers")
                            {
                                Frame.Content = new Suppliers_page();
                                btn = "suppliers";
                                Suppliers_btn.IsEnabled = false;
                            }
                            else
                            {
                                if (Data.MainFraimContent == "employees")
                                {
                                    Frame.Content = new Employees_page();
                                    btn = "employees";
                                    Employees_btn.IsEnabled = false;
                                }
                                else
                                {
                                    if (Data.MainFraimContent == "profile")
                                    {
                                        Frame.Content = new Profile_page();
                                        btn = "profile";
                                        Profile_btn.IsEnabled = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (Data.Role == "Клиент")
            {
                Suppliers_btn.Height = 0;
                Employees_btn.Height = 0;
                Bills_btn.Height = 0;
            }
            else
            {
                NpgsqlCommand command = new NpgsqlCommand("select Role From Employees where Login like @log", sqlConnection);
                command.Parameters.AddWithValue("log", Data.Login);

                if (command.ExecuteScalar().ToString() == "Сотрудник")
                {
                    Data.Role = "Сотрудник";
                    Basket_btn.Height = 0;
                    Suppliers_btn.Height = 0;
                    Employees_btn.Height = 0;
                    Profile_btn.Height = 0;
                }
                else
                {
                    Data.Role = "Администратор";
                    Basket_btn.Height = 0;
                }
            }
        }

        private async void Menu_img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (!visible)
            {
                int n = 150;
                while (n > 0)
                {
                    await Task.Delay(1);
                    if (n / 10 == 1)
                    {
                        n -= 2;
                        Menu_column.Width = new GridLength(n);
                    }
                    else
                    {
                        if (n < 10)
                        {
                            n -= 1;
                            Menu_column.Width = new GridLength(n);
                        }
                        else
                        {
                            n -= n / 10;
                            Menu_column.Width = new GridLength(n);
                        }
                    }
                }
                visible = !visible;
            }
            else
            {
                int w = 150;
                int n = 0;
                while (n < 150)
                {
                    await Task.Delay(1);
                    if (w / 10 == 1)
                    {
                        n += 2;
                        Menu_column.Width = new GridLength(n);
                    }
                    else
                    {
                        if (w < 10)
                        {
                            n += 1;
                            Menu_column.Width = new GridLength(n);
                        }
                        else
                        {
                            n += w / 10;
                            Menu_column.Width = new GridLength(n);
                        }
                    }
                    w -= w / 10;
                    Menu_column.Width = new GridLength(n);
                }
                visible = !visible;
            }
        }

        private void Close_img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Main_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "main")
            {
                Frame.Content = new Main_page();
                btn = "main";
                Main_btn.IsEnabled = false;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Basket_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "basket")
            {
                Frame.Content = new Basket_page();
                btn = "basket";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = false;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Bids_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "bids")
            {
                Frame.Content = new Bids_page();
                btn = "bids";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = false;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Bills_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "bills")
            {
                Frame.Content = new Bills_page();
                btn = "bills";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = false;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Suppliers_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "suppliers")
            {
                Frame.Content = new Suppliers_page();
                btn = "suppliers";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = false;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Employees_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "employees")
            {
                Frame.Content = new Employees_page();
                btn = "employees";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = false;
                Profile_btn.IsEnabled = true;
            }
        }

        private void Profile_btn_Click(object sender, RoutedEventArgs e)
        {
            if (btn != "profile")
            {
                Frame.Content = new Profile_page();
                btn = "profile";
                Main_btn.IsEnabled = true;
                Basket_btn.IsEnabled = true;
                Bids_btn.IsEnabled = true;
                Bills_btn.IsEnabled = true;
                Suppliers_btn.IsEnabled = true;
                Employees_btn.IsEnabled = true;
                Profile_btn.IsEnabled = false;
            }
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Data.Role = "";
            this.Hide();
            Log log = new Log();
            log.Show();
        }
    }
}
