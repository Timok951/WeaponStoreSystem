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
    /// Логика взаимодействия для LicensePage.xaml
    /// </summary>
    /// 

    public partial class LicensePage : Page
    {
        LicenseTableAdapter license = new LicenseTableAdapter();
        LicenseTypeTableAdapter licensetype = new LicenseTypeTableAdapter();

        public LicensePage()
        {
            InitializeComponent();
            LicenseNumber.MaxLength = 5;

            LicenseGrid.ItemsSource = license.GetLicenseData();
            LicenseTypeCombobox.ItemsSource = licensetype.GetData();
            LicenseTypeCombobox.DisplayMemberPath = "LicenseType";
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (LicenseTypeCombobox.SelectedItem != null && LicenseNumber.Text.Length > 0)
            {
                try
                {
                    var typeid = (LicenseTypeCombobox.SelectedItem as DataRowView).Row[0];
                    try
                    {
                        int number = Convert.ToInt32(LicenseNumber.Text);
                        license.InsertLicense(Convert.ToInt32(typeid), number);

                    }
                    catch 
                    {
                        MessageBox.Show("Not right number");
                    }

                    LicenseGrid.ItemsSource = license.GetLicenseData();
                    LicenseGrid.Columns[0].Visibility = Visibility.Collapsed;
                    LicenseGrid.Columns[2].Visibility = Visibility.Collapsed;

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is already exsisted");
                }

            }
            else
            {
                MessageBox.Show("Input all data");
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            if (LicenseTypeCombobox.SelectedItem != null && LicenseGrid.SelectedItem != null && LicenseNumber.Text.Length > 0 && LicenseNumber.Text.Length > 0)
            {
                try
                {
                    var typeid = (LicenseTypeCombobox.SelectedItem as DataRowView).Row[0];
                    var id = (LicenseGrid.SelectedItem as DataRowView).Row[0];


                    try
                    {
                        int number = Convert.ToInt32(LicenseNumber.Text);
                        license.UpdateLicense(number, Convert.ToInt32(typeid), Convert.ToInt32(id));
                    }
                    catch
                    {
                        MessageBox.Show("Not number");
                    }

                    LicenseGrid.ItemsSource = license.GetLicenseData();
                    LicenseGrid.Columns[0].Visibility = Visibility.Collapsed;
                    LicenseGrid.Columns[2].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is already exsisted");
                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }

            

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (LicenseGrid.SelectedItem != null)
            {
                try
                {
                    var id = (LicenseGrid.SelectedItem as DataRowView).Row[0];

                    license.DeleteLicense(Convert.ToInt32(id));
                    LicenseGrid.ItemsSource = license.GetLicenseData();
                    LicenseGrid.Columns[0].Visibility = Visibility.Collapsed;
                    LicenseGrid.Columns[2].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is using");

                }
            }

            }


        private void LicenseGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (LicenseGrid.SelectedItem != null)
            {
                var number = (LicenseGrid.SelectedItem as DataRowView).Row[1];
                LicenseNumber.Text = Convert.ToString(number);

            }
        }

        private void LicenseGrid_Loaded(object sender, RoutedEventArgs e)
        {
            LicenseGrid.Columns[0].Visibility = Visibility.Collapsed;
            LicenseGrid.Columns[2].Visibility = Visibility.Collapsed;

        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Licensemodel> forImport = Converter.DesirializeObject<List<Licensemodel>>();
                foreach (var licensemodel in forImport)
                {
                    license.InsertLicense(licensemodel.licensenumber, licensemodel.licensetypeid);
                }

                LicenseGrid.ItemsSource = null;
                LicenseGrid.ItemsSource = license.GetLicenseData();
            }
            catch
            {
                MessageBox.Show("Error data has not been imported");
            }
        }

        private void LicenseNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
