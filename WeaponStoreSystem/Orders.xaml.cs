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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        OrdersTableAdapter orders = new OrdersTableAdapter();

        TipTableAdapter tipTable = new TipTableAdapter();

        WeaponTableAdapter weapon = new WeaponTableAdapter();

        AmmoTableAdapter ammo = new AmmoTableAdapter();

        public Orders()
        {
            InitializeComponent();

            OrdersGrid.ItemsSource = orders.GetOrdersDate();
            AmmoTypeCombobobox.ItemsSource = ammo.GetData();
            AmmoTypeCombobobox.DisplayMemberPath = "AmmoName";

            WeaponTypeCombobobox.ItemsSource = weapon.GetData();
            WeaponTypeCombobobox.DisplayMemberPath = "WeaponName";

            TipDateCombobobox.ItemsSource = tipTable.GetData();
            TipDateCombobobox.DisplayMemberPath = "TipDate";

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (TipDateCombobobox.SelectedItem != null)
            {
                object weponid;
                object ammoid;
                int ammoamount;
                int weaponamount;
                try
                {

                    if (WeaponTypeCombobobox.SelectedItem != null)
                    {
                        weponid = (WeaponTypeCombobobox.SelectedItem as DataRowView).Row[0];
                    }

                    else
                    {
                        weponid = null;

                    }

                    if (AmmoTypeCombobobox.SelectedItem != null)
                    {
                        ammoid = (AmmoTypeCombobobox.SelectedItem as DataRowView).Row[0];
                    }

                    else { ammoid = null; }
                    try
                    {



                        object dateid = (TipDateCombobobox.SelectedItem as DataRowView).Row[0];
                        ammoamount = Convert.ToInt32(AmmountofAmmoBox.Text);
                        weaponamount = Convert.ToInt32(AmmountofWeaponBox.Text);
                        orders.InsertOrders(Convert.ToInt32(ammoid), ammoamount, Convert.ToInt32(weponid), weaponamount, Convert.ToInt32(dateid));

                        OrdersGrid.ItemsSource = orders.GetOrdersDate();
                        OrdersGrid.Columns[0].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[1].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[3].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[5].Visibility = Visibility.Collapsed;


                    }
                    catch
                    {
                        MessageBox.Show("Input numbers");

                    }

                }

                catch (System.Data.SqlClient.SqlException)
                {

                    MessageBox.Show("This data is alredy exsists");

                }

            }
            else {
                MessageBox.Show("Choose order date from tip to create order");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (TipDateCombobobox.SelectedItem != null && OrdersGrid.SelectedItem != null)
            {
                object weponid;
                object ammoid;
                int ammoamount;
                int weaponamount;

                object id = (OrdersGrid.SelectedItem as DataRowView).Row[0];
                try
                {

                    if (WeaponTypeCombobobox.SelectedItem != null)
                    {
                        weponid = (WeaponTypeCombobobox.SelectedItem as DataRowView).Row[0];
                    }

                    else
                    {
                        weponid = null;

                    }

                    if (AmmoTypeCombobobox.SelectedItem != null)
                    {
                        ammoid = (AmmoTypeCombobobox.SelectedItem as DataRowView).Row[0];
                    }

                    else { ammoid = null; }
                    try
                    {

                        object dateid = (TipDateCombobobox.SelectedItem as DataRowView).Row[0];
                        ammoamount = Convert.ToInt32(AmmountofAmmoBox.Text);
                        weaponamount = Convert.ToInt32(AmmountofWeaponBox.Text);
                        orders.UpdateOrders(Convert.ToInt32(ammoid), ammoamount, Convert.ToInt32(weponid), weaponamount, Convert.ToInt32(dateid), Convert.ToInt32(id));

                        OrdersGrid.ItemsSource = orders.GetOrdersDate();
                        OrdersGrid.Columns[0].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[1].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[3].Visibility = Visibility.Collapsed;
                        OrdersGrid.Columns[5].Visibility = Visibility.Collapsed;


                    }
                    catch
                    {
                        MessageBox.Show("Input numbers");

                    }

                }

                catch (System.Data.SqlClient.SqlException)
                {

                    MessageBox.Show("This data is alredy exsists");

                }
            }
            else
            {
                MessageBox.Show("Choose order date from tip to create order");
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem != null)
            {
                try
                {
                    object id = (OrdersGrid.SelectedItem as DataRowView).Row[0];

                    orders.DeleteOrders(Convert.ToInt32(id));
                    OrdersGrid.ItemsSource = orders.GetOrdersDate();
                    OrdersGrid.Columns[0].Visibility = Visibility.Collapsed;
                    OrdersGrid.Columns[1].Visibility = Visibility.Collapsed;
                    OrdersGrid.Columns[3].Visibility = Visibility.Collapsed;
                    OrdersGrid.Columns[5].Visibility = Visibility.Collapsed;
                }
                catch(System.Data.SqlClient.SqlException) {
                    MessageBox.Show("This data is using");
                }

            }
            else
            {
                MessageBox.Show("Choose order to delete");
            }
        }

        private void OrdersCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersGrid.SelectedItem != null)
            {
                object ammoamount = (OrdersGrid.SelectedItem as DataRowView).Row[2];
                object weaponammount = (OrdersGrid.SelectedItem as DataRowView).Row[4];

                AmmountofAmmoBox.Text = Convert.ToString(ammoamount);
                AmmountofWeaponBox.Text = Convert.ToString(weaponammount);



            }
        }

        private void OrdersCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            OrdersGrid.Columns[0].Visibility = Visibility.Collapsed;
            OrdersGrid.Columns[1].Visibility = Visibility.Collapsed;
            OrdersGrid.Columns[3].Visibility = Visibility.Collapsed;
            OrdersGrid.Columns[5].Visibility = Visibility.Collapsed;

        }

        private void AmmoTypeCombobobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AmmoTypeCombobobox.SelectedItem != null)
            {
                AmmountofAmmoBox.IsEnabled = true;
            }

            else
            {
                AmmountofAmmoBox.IsEnabled = false;

            }

        }

        private void WeaponTypeCombobobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeaponTypeCombobobox.SelectedItem != null)
            {
                AmmountofWeaponBox.IsEnabled = true;
            }

            else
            {
                AmmountofWeaponBox.IsEnabled = false;

            }
        }

        private void AmmountofAmmoBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) )
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void AmmountofWeaponBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
