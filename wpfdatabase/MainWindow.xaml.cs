using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace wpfdatabase
{
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("Data Source=LAB25-18;Initial Catalog=wpfdatabasenew;User ID=sa;Password=aptech;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }


        public void LoadData()
        {
            SqlCommand getdata = new SqlCommand("Select * from students", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader dataReader = getdata.ExecuteReader();
            dt.Load(dataReader);
            dataGrid.ItemsSource = dt.DefaultView;
            con.Close();
        }

        private void Clear_data()
        {
            name.Clear();
            email.Clear();
            age.Clear();
            cellNo.Clear();
            city.Clear();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear_data();
        }
    }
}
