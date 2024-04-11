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
    /// Логика взаимодействия для LicenseTypePage.xaml
    /// </summary>
    public partial class LicenseTypePage : Page
    {
        LicenseTypeTableAdapter licensetype = new LicenseTypeTableAdapter();

        public LicenseTypePage()
        {
            InitializeComponent();

            LicenseTypeGrid.ItemsSource = licensetype.GetData();
            LicenseTypeBox.MaxLength = 30;
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            if(LicenseTypeBox.Text.Length > 0)
            {
                try
                {
                    licensetype.InsertLicenseType(LicenseTypeBox.Text);

                    LicenseTypeGrid.ItemsSource = licensetype.GetData();
                    LicenseTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is already exsisted");

                }
            }
            else
            {
                MessageBox.Show("Input license type");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            if (LicenseTypeBox.Text.Length > 0)
            {
                if (LicenseTypeGrid.SelectedItem != null)
                {
                    try
                    {
                        object id = (LicenseTypeGrid.SelectedItem as DataRowView).Row[0];
                        licensetype.UpdateLicenseType(LicenseTypeBox.Text, Convert.ToInt32(id));

                        LicenseTypeGrid.ItemsSource = licensetype.GetData();
                        LicenseTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Data is already exsisted");
                    }
                }
            }
            else
            {
                MessageBox.Show("Input license type");
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LicenseTypeGrid.SelectedItem != null)
            {
                try
                {
                    object id = (LicenseTypeGrid.SelectedItem as DataRowView).Row[0];
                    licensetype.DeleteLicenseType(Convert.ToInt32(id));

                    LicenseTypeGrid.ItemsSource = licensetype.GetData();
                    LicenseTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is being used");

                }
            }
        }

        private void LicenseTypeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LicenseTypeGrid.SelectedItem != null)
            {
                object licensetype = (LicenseTypeGrid.SelectedItem as DataRowView).Row[1];
                LicenseTypeBox.Text = (Convert.ToString(licensetype));

            }
            else
            {
                MessageBox.Show("Choose license to delete");
            }
        }

        private void LicenseTypeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LicenseTypeGrid.Columns[0].Visibility = Visibility.Collapsed;

        }
    }
}
