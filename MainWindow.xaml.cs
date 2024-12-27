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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using Rent.Models.Windows;

namespace Rent
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NpgsqlConnection sqlConnection = null;

        bool vis = true;
        string text;
        //string hide_text;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sqlConnection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

            sqlConnection.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = "Евгений";
            NpgsqlCommand command = new NpgsqlCommand("select id_client from Clients where firstname like @name", sqlConnection);

            command.Parameters.AddWithValue("name", name);

            DataTable dataTable = new DataTable();

            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count != 0)
            {
                MessageBox.Show($"Всё работает!");
            }
            else
            {
                MessageBox.Show("Всё не работает!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Log log = new Log();
            log.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string hide_text = "";
            if (vis)
            {
                text = tb.Text;
                for (int i = 0; i < text.Length; i++)
                {
                    hide_text += "●";
                }
                tb.Text = hide_text;
            }
            else
            {
                //if (tb.Text.Length > text.Length)
                //{
                //    for (int i = text.Length; i < tb.Text.Length; i++)
                //    {
                //        text += tb.Text[i];
                //    }
                //}
                //text = text + tb.Text[text.Length - 1];
                tb.Text = text;
                //MessageBox.Show(text);
            }
            vis = !vis;
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!vis)
            {
                if (tb.Text.Length > text.Length)
                {
                    //for (int i = text.Length; i < tb.Text.Length; i++)
                    //{
                    //    text += tb.Text[i];
                    //    //MessageBox.Show(text);
                    //}
                    text += tb.Text.Substring(text.Length, 1);
                    //int len = tb.Text.Length - 1;
                    //string t = tb.Text.Substring(0, text.Length - 1) + "●";
                    tb.Text = tb.Text.Substring(0, text.Length - 1) + "●";
                    tb.SelectionStart = tb.Text.Length;
                    //MessageBox.Show(text);
                }
                else
                {
                    if (tb.Text.Length < text.Length)
                    {
                        text = text.Remove(text.Length - 1, 1);
                    }
                }
            }
        }
    }
}
