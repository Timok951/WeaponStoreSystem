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
    /// Логика взаимодействия для WeaponTypePage.xaml
    /// </summary>
    public partial class WeaponTypePage : Page
    {
        WeaponTypeTableAdapter weaponType = new WeaponTypeTableAdapter();

        public WeaponTypePage()
        {
            InitializeComponent();
            WeaponTypeGrid.ItemsSource = weaponType.GetData();
            WeaponTypeNameBox.MaxLength = 30;
        }

        private void WeaponTypeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeaponTypeGrid.SelectedItem != null) {
                Object weaponName = (WeaponTypeGrid.SelectedItem as DataRowView).Row[1];
                WeaponTypeNameBox.Text = Convert.ToString(weaponName);
            }
        }

        private void WeaponTypeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WeaponTypeGrid.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (WeaponTypeGrid.SelectedItem != null)
            {
                if (WeaponTypeNameBox.Text.Length > 0)
                {
                    try
                    {
                        object id = (WeaponTypeGrid.SelectedItem as DataRowView).Row[0];
                        weaponType.UpdateWeaponType(WeaponTypeNameBox.Text, Convert.ToInt32(id));

                        WeaponTypeGrid.ItemsSource = weaponType.GetData();
                        WeaponTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Data is already exsisted");
                    }
                }
                else
                {
                    MessageBox.Show("Input weaponname");

                }
            }

            else
            {
                MessageBox.Show("Select Value to edit");
            }
        }



        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (WeaponTypeGrid.SelectedItem != null)
            {
                try
                {
                    object id = (WeaponTypeGrid.SelectedItem as DataRowView).Row[0];
                    weaponType.DeleteWeaponType(Convert.ToInt32(id));
                    WeaponTypeGrid.ItemsSource = weaponType.GetData();
                    WeaponTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch(System.Data.SqlClient.SqlException) 
                {
                    MessageBox.Show("This data is using");
                }
            }
            else
            {
                MessageBox.Show("Select value to delete");
            }

        }

        private void AddWeaponType_Click(object sender, RoutedEventArgs e)
        {
                if (WeaponTypeNameBox.Text.Length > 0)
                {

                    try
                    {
                        weaponType.InsertWeaponType(WeaponTypeNameBox.Text);
                        WeaponTypeGrid.ItemsSource = weaponType.GetData();
                        WeaponTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Data is already exsisted");
                    }
                

            }
            else
            {
                MessageBox.Show("Input value to add");

            }


        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<WeaponTypeModel> forimport = Converter.DesirializeObject<List<WeaponTypeModel>>();

                foreach (var item in forimport)
                {
                    weaponType.InsertWeaponType(item.weapontypename);
                }
            }
            catch
            {
                MessageBox.Show("File Eror");
            }
        }

        private void WeaponTypeNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c) && !char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
