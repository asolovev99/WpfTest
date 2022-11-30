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
using System.IO;

namespace WpfTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string request = "SELECT TOP 4 ProductName, ProductCount * Price AS TotalSum" +
            "FROM Products" +
            "ORDER BY TotalSum DESC, ProductName asc, ProductCount* Price" +
            "OFFSET 2 ROWS" +
            "FETCH NEXT 3 ROWS ONLY"+
            "WHERE NOT Manufacturer = 'Samsung' and Manufacturer NOT IN('Samsung', 'Xiaomi', 'Huawei')" +
            "and Price NOT BETWEEN 20000 AND 40000 and ProductName LIKE 'iPhone [^1-6]%'";

            string update = "UPDATE Products" +
            "SET Manufacturer = 'Apple Inc.'" +
            "FROM" +
            "(SELECT TOP 2 FROM Products WHERE Manufacturer = 'Apple') AS Selected" +
            "WHERE Products.Id = Selected.Id";

            string insertImage = "INSERT INTO adventureworks.dbo.myimages values (1, (SELECT * FROM OPENROWSET(BULK N'C:\img\1.png', SINGLE_BLOB) as T1))";

            SqlDataReader reader = new SqlCommand().ExecuteReader();


            ImageSource PictureID;
            using (var stream = new MemoryStream((byte)reader["PictureID"]))
            {
                PictureID = BitmapFrame.Create(stream,
                                        BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }

            MessageBoxResult result = MessageBox.Show("text", "nazv", MessageBoxButton.YesNoCancel);
        }
    }
}
