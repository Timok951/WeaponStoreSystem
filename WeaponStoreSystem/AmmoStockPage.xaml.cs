using System;
using System.Collections.Generic;
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
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для AmmoStockPage.xaml
    /// </summary>
    public partial class AmmoStockPage : Page
    {
        WarehouseTableAdapter warehouse = new WarehouseTableAdapter();
        AmmoStockTableAdapter ammoStock = new AmmoStockTableAdapter();
        AmmoTableAdapter ammo  = new AmmoTableAdapter();

        public AmmoStockPage()
        {
            InitializeComponent();

            AmmoStockGrid.ItemsSource = ammoStock.GetDataAmmoStock();
            AmmoStockWarheuseCombobox.ItemsSource = warehouse.GetData();
            AmmoStockWarheuseCombobox.DisplayMemberPath = "WarehouseAdress";

            AmmoStockAmmoNameCombobobx.ItemsSource = ammo.GetData();
            AmmoStockAmmoNameCombobobx.DisplayMemberPath = "AmmoName";

            AmmoStockNameBox.MaxLength = 30;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (AmmoStockWarheuseCombobox.SelectedItem != null && AmmoStockAmmoNameCombobobx != null && AmmoStockNameBox.Text.Length >0 )
            {
                try
                {
                    Convert.ToInt32(AmmoStockNameBox.Text);
                }
                catch {
                    MessageBox.Show("Input number");
                }
                try
                {
                    var warehouseid = (AmmoStockWarheuseCombobox.SelectedItem as DataRowView).Row[0];
                    var ammoid = (AmmoStockAmmoNameCombobobx.SelectedItem as DataRowView).Row[0];

                    ammoStock.InsertWeaponStock(Convert.ToInt32(ammoid), Convert.ToInt32(AmmoStockNameBox.Text), Convert.ToInt32(warehouseid));


                    AmmoStockGrid.ItemsSource = ammoStock.GetDataAmmoStock();
                    AmmoStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[3].Visibility = Visibility.Collapsed;

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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (AmmoStockWarheuseCombobox.SelectedItem != null && AmmoStockAmmoNameCombobobx != null && AmmoStockGrid.SelectedItem != null &&  AmmoStockNameBox.Text.Length > 0)
            {
                try
                {
                    Convert.ToInt32(AmmoStockNameBox.Text);
                }
                catch
                {
                    MessageBox.Show("Input number");
                }
                try
                {
                    var id = (AmmoStockGrid.SelectedItem as DataRowView).Row[0];
                    var warehouseid = (AmmoStockWarheuseCombobox.SelectedItem as DataRowView).Row[0];
                    var ammoid = (AmmoStockAmmoNameCombobobx.SelectedItem as DataRowView).Row[0];
                    ammoStock.UpdateWeaponStock(Convert.ToInt32(ammoid), Convert.ToInt32(AmmoStockNameBox.Text), Convert.ToInt32(warehouseid), Convert.ToInt32(id));

                    AmmoStockGrid.ItemsSource = ammoStock.GetDataAmmoStock();
                    AmmoStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[3].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is already exist");

                }

            }
            else
            {
                MessageBox.Show("Input all data in tables");
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AmmoStockGrid.SelectedItem != null)
            {
                try
                {
                    var id = (AmmoStockGrid.SelectedItem as DataRowView).Row[0];
                    ammoStock.DeleteWeaponStock(Convert.ToInt32(id));
                    AmmoStockGrid.ItemsSource = ammoStock.GetDataAmmoStock();
                    AmmoStockGrid.Columns[0].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[1].Visibility = Visibility.Collapsed;
                    AmmoStockGrid.Columns[3].Visibility = Visibility.Collapsed;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is using");
                }

            }
            else
            {
                MessageBox.Show("Choose data to delete");
            }
        }

        private void AmmoStockGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AmmoStockGrid.SelectedItem != null)
            {
                var weaponstockamount = (AmmoStockGrid.SelectedItem as DataRowView).Row[2];
                AmmoStockNameBox.Text = weaponstockamount.ToString();
            }
        }

        private void AmmoStockGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AmmoStockGrid.Columns[0].Visibility = Visibility.Collapsed;
            AmmoStockGrid.Columns[1].Visibility = Visibility.Collapsed;
            AmmoStockGrid.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void AmmoStockNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true; // отменить ввод символа, если это не цифра
                    break;
                }
            }
        }
    }
}
