using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для WeaponStockPage.xaml
    /// </summary>
    public partial class WeaponStockPage : Page
    {
        WarehouseTableAdapter warehouse = new WarehouseTableAdapter();
        WeaponStockTableAdapter waeponstock = new WeaponStockTableAdapter();
        WeaponTableAdapter weapon = new WeaponTableAdapter();

        public WeaponStockPage()
        {
            InitializeComponent();
            WeaponStockGrid.ItemsSource = waeponstock.GetWeponStockData();
            WeaponStcokWarehouseCombobox.ItemsSource = warehouse.GetData();
            WeaponStcokWarehouseCombobox.DisplayMemberPath = "WarehouseAdress";

            WeaponWeaponNameStcokCombobox.ItemsSource = weapon.GetData();
            WeaponWeaponNameStcokCombobox.DisplayMemberPath = "WeaponName";

            WeaponStockNameBox.MaxLength = 30;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (WeaponStcokWarehouseCombobox.SelectedItem != null && WeaponWeaponNameStcokCombobox != null && WeaponStockNameBox.Text.Length < 0) {
                try
                {
                    Convert.ToInt32(WeaponStockNameBox.Text);
                }
                catch
                {
                    MessageBox.Show("Input number");
                }
                try
                {
                    var warehouseid = (WeaponStcokWarehouseCombobox.SelectedItem as DataRowView).Row[0];
                    var weaponid = (WeaponWeaponNameStcokCombobox.SelectedItem as DataRowView).Row[0];

                    waeponstock.InsertWeaponStock(Convert.ToInt32(weaponid), Convert.ToInt32(WeaponStockNameBox.Text), Convert.ToInt32(warehouseid));


                    WeaponStockGrid.ItemsSource = waeponstock.GetWeponStockData();
                    WeaponStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WeaponStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                    WeaponStockGrid.Columns[3].Visibility = Visibility.Collapsed;

                }

                catch (System.Data.SqlClient.SqlException) {

                    MessageBox.Show("This data is already exist");

                }


            }
            else
            {
                MessageBox.Show("Input all data");
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (WeaponStcokWarehouseCombobox.SelectedItem != null && WeaponWeaponNameStcokCombobox != null && WeaponStockNameBox.Text.Length < 0)
            {
                try
                {
                    Convert.ToInt32(WeaponStockNameBox.Text);
                }
                catch
                {
                    MessageBox.Show("Input number");
                }
                try
                {
                    var id = (WeaponStockGrid.SelectedItem as DataRowView).Row[0];
                    var warehouseid = (WeaponStcokWarehouseCombobox.SelectedItem as DataRowView).Row[0];
                    var weaponid = (WeaponWeaponNameStcokCombobox.SelectedItem as DataRowView).Row[0];
                    waeponstock.UpdateWeaponStock(Convert.ToInt32(weaponid), Convert.ToInt32(WeaponStockNameBox.Text), Convert.ToInt32(warehouseid), Convert.ToInt32(id));

                    WeaponStockGrid.ItemsSource = waeponstock.GetWeponStockData();
                    WeaponStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WeaponStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                    WeaponStockGrid.Columns[3].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is already exist");

                }

            }
            else
            {
                MessageBox.Show("Input all data");
            }

            
        }

        private void WeaponStockGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeaponStockGrid.SelectedItem != null)
            {
                var weaponstockamount = (WeaponStockGrid.SelectedItem as DataRowView).Row[2];
                WeaponStockNameBox.Text = weaponstockamount.ToString();
            }
        }

        private void WeaponStockGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WeaponStockGrid.Columns[0].Visibility = Visibility.Collapsed;
            WeaponStockGrid.Columns[1].Visibility = Visibility.Collapsed;
            WeaponStockGrid.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (WeaponStockGrid.SelectedItem != null)
            {
                var id = (WeaponStockGrid.SelectedItem as DataRowView).Row[0];
                waeponstock.DeleteWeaponStock(Convert.ToInt32(id));

                WeaponStockGrid.ItemsSource = waeponstock.GetWeponStockData();
                WeaponStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                WeaponStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                WeaponStockGrid.Columns[3].Visibility = Visibility.Collapsed;

            }
        }
    }
}
