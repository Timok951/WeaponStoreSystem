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
    /// Логика взаимодействия для WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        WarehouseTableAdapter warehouse = new WarehouseTableAdapter();
        CityTableAdapter city = new CityTableAdapter();
        public WarehousePage()
        {

            InitializeComponent();
            WarehouseGrid.ItemsSource = warehouse.GetWarehouseData();
            WarehouseCombobox.ItemsSource = city.GetData();
            WarehouseCombobox.DisplayMemberPath = "CityName";
            WarehouseNameBox.MaxLines = 30;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (WarehouseCombobox.SelectedItem != null && WarehouseNameBox.Text.Length > 0)
            {
                try
                {
                    var id = (WarehouseCombobox.SelectedItem as DataRowView).Row[0];
                    warehouse.InsertWarehouse( WarehouseNameBox.Text, Convert.ToInt32(id));

                    WarehouseGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WarehouseGrid.Columns[2].Visibility = Visibility.Collapsed;
                    WarehouseGrid.ItemsSource = warehouse.GetWarehouseData();
                    WarehouseCombobox.ItemsSource = city.GetData();

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
            if (WarehouseCombobox.SelectedItem != null && WarehouseGrid.SelectedItem != null && WarehouseNameBox.Text.Length > 0)
            {
                try
                {
                    var id = (WarehouseCombobox.SelectedItem as DataRowView).Row[0];
                    var warehouseid  = (WarehouseGrid.SelectedItem as DataRowView).Row[0];
                    warehouse.UpdateWarehouse(WarehouseNameBox.Text,Convert.ToInt32(id),Convert.ToInt32(warehouseid));

                    WarehouseGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WarehouseGrid.Columns[2].Visibility = Visibility.Collapsed;
                    WarehouseGrid.ItemsSource = warehouse.GetWarehouseData();
                    WarehouseCombobox.ItemsSource = city.GetData();
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is already exist");

                }
            }
            else
            {
                MessageBox.Show("Input all data and chose row to update");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (WarehouseGrid.SelectedItem != null)
                {
                    var id = (WarehouseGrid.SelectedItem as DataRowView).Row[0];
                    warehouse.DeleteWarehouse(Convert.ToInt32(id));
                    WarehouseGrid.ItemsSource = warehouse.GetWarehouseData();

                }
                else
                {
                    MessageBox.Show("Choose row to delete");
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Data is using");
            }

        }

        private void WarehouseGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WarehouseGrid.Columns[0].Visibility = Visibility.Collapsed;
            WarehouseGrid.Columns[2].Visibility = Visibility.Collapsed;

        }

        private void WarehouseGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WarehouseGrid.SelectedItem != null)
            {
                var waarehouseadress = (WarehouseGrid.SelectedItem as DataRowView).Row[1];
                WarehouseNameBox.Text = Convert.ToString(waarehouseadress);

            }
        }
    }
}
