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
using System.Numerics;

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

        private Boolean IsValid()
        {
            if (name.Text == String.Empty)
            {
                MessageBox.Show("Name Cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (email.Text == String.Empty)
            {
                MessageBox.Show("Email Cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (age.Text == String.Empty)
            {
                MessageBox.Show("Age Cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (cellNo.Text == String.Empty)
            {
                MessageBox.Show("CellNo Cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (city.Text == String.Empty)
            {
                MessageBox.Show("City Cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Clear_data()
        {
            SID.Clear();
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

        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                SqlCommand addstd = new SqlCommand("insert into students values(@fname,@email,@age,@cellno,@city)", con);

                con.Open();

                addstd.CommandType = CommandType.Text;

                addstd.Parameters.AddWithValue("@fname", name.Text);
                addstd.Parameters.AddWithValue("@email", email.Text);
                addstd.Parameters.AddWithValue("@age", age.Text);
                addstd.Parameters.AddWithValue("@cellno", cellNo.Text);
                addstd.Parameters.AddWithValue("@city", city.Text);

                addstd.ExecuteNonQuery();
                con.Close();
                LoadData();
                Clear_data();
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SID.Text != string.Empty)
            {
                SqlCommand deletestd = new SqlCommand("Delete from students where id= @SID", con);
                con.Open();
                deletestd.CommandType = CommandType.Text;
                deletestd.Parameters.AddWithValue("@SID", SID.Text);
                deletestd.ExecuteNonQuery();
                con.Close();
                LoadData();
                Clear_data();
                MessageBox.Show("Student data has been Delete", "Deleted Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Student Id required For data Delete", "Can't Delete Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
