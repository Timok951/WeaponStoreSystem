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
    /// Логика взаимодействия для WeaponPage.xaml
    /// </summary>
    public partial class WeaponPage : Page
    {

        WeaponTableAdapter weapon = new WeaponTableAdapter();
        WeaponTypeTableAdapter weaponType = new WeaponTypeTableAdapter();
        public WeaponPage()
        {
            InitializeComponent();
            WeaponGrid.ItemsSource = weapon.GetWeaponData();
            WeaponCombobox.ItemsSource = weaponType.GetData();
            WeaponCombobox.DisplayMemberPath = "WeaponTypeName";
            WeaponPrice.MaxLength = 18;
            WeaponNameBox.MaxLength = 30;

        }

        private void WeaponGrid_Loaded(object sender, RoutedEventArgs e)
        {
            WeaponGrid.Columns[0].Visibility = Visibility.Collapsed;
            WeaponGrid.Columns[1].Visibility = Visibility.Collapsed;

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(WeaponNameBox.Text.Length < 0)
            { 
            if (WeaponCombobox.SelectedItem != null)
            {
                try
                {
                    try
                    {
                        var id = (WeaponCombobox.SelectedItem as DataRowView).Row[0];
                        if (Convert.ToDecimal(WeaponPrice.Text) > 0)
                        {
                            weapon.InsertWeapon(Convert.ToInt32(id), WeaponNameBox.Text, Convert.ToDecimal(WeaponPrice.Text));

                        }

                        else
                        {
                            MessageBox.Show("Price should be more than 0");
                        }

                    }

                    catch
                    {
                        MessageBox.Show("Price should be number");
                    }


                    WeaponGrid.ItemsSource = weapon.GetWeaponData();
                    WeaponCombobox.ItemsSource = weaponType.GetData();
                    WeaponGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WeaponGrid.Columns[1].Visibility = Visibility.Collapsed;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is already exist");
                }

            }
                else
                {
                    MessageBox.Show("Input weapon name");
                }
            }
            else
            {
                MessageBox.Show("Input Choose weapon type");
            }


        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            if (WeaponNameBox.Text.Length < 0)
            {
                if (WeaponCombobox.SelectedItem != null)
                {
                    try
                    {
                        try
                        {
                            Convert.ToDecimal(WeaponPrice.Text);
                            var id = (WeaponCombobox.SelectedItem as DataRowView).Row[0];
                            var weaponid = (WeaponGrid.SelectedItem as DataRowView).Row[0];
                            if (Convert.ToDecimal(WeaponPrice.Text) > 0)
                            {
                                weapon.UpdateWeapon(Convert.ToInt32(id), WeaponNameBox.Text, Convert.ToDecimal(WeaponPrice.Text), Convert.ToInt32(weaponid));

                            }

                            else
                            {
                                MessageBox.Show("Price should be more than 0");
                            }

                        }

                        catch
                        {
                            MessageBox.Show("Price should be number");
                        }


                        WeaponGrid.ItemsSource = weapon.GetWeaponData();
                        WeaponCombobox.ItemsSource = weaponType.GetData();
                        WeaponGrid.Columns[0].Visibility = Visibility.Collapsed;
                        WeaponGrid.Columns[1].Visibility = Visibility.Collapsed;
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("This data is already exist");
                    }

                }
                else
                {
                    MessageBox.Show("Input weapon name");
                }

            }
            else
            {
                MessageBox.Show("Choose weapon type");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WeaponGrid.SelectedItem != null)
                {
                    var id = (WeaponGrid.SelectedItem as DataRowView).Row[0];
                    weapon.DeleteWeapon(Convert.ToInt32(id));
                    WeaponGrid.ItemsSource = weapon.GetWeaponData();
                    WeaponCombobox.ItemsSource = weaponType.GetData();
                    WeaponGrid.Columns[0].Visibility = Visibility.Collapsed;
                    WeaponGrid.Columns[1].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Select data to delete");
                }
            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Data is using");
            }
        }


        private void WeaponGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (WeaponGrid.SelectedItem != null)
            {
                var weaponname = (WeaponGrid.SelectedItem as DataRowView).Row[2];
                var weaponprice = (WeaponGrid.SelectedItem as DataRowView).Row[3];
                WeaponNameBox.Text = Convert.ToString(weaponname);
                WeaponPrice.Text = Convert.ToString(weaponprice);

            }
        }

        private void WeaponPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void WeaponNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

