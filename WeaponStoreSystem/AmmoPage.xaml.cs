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
    /// Логика взаимодействия для AmmoPage.xaml
    /// </summary>
    public partial class AmmoPage : Page
    {
        AmmoTableAdapter ammo = new AmmoTableAdapter();
        AmmoTypeTableAdapter ammotype = new AmmoTypeTableAdapter();

        public AmmoPage()
        {
            InitializeComponent();

            AmmoGrid.ItemsSource = ammo.GetAmmoData();
            AmmoTypeCombobox.ItemsSource = ammotype.GetData();
            AmmoTypeCombobox.DisplayMemberPath = "AmmoType";
            AmmoPrice.MaxLength = 18;
            AmmoNameBox.MaxLength = 30;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(AmmoNameBox.Text.Length != 0) {
                if (AmmoTypeCombobox.SelectedItem != null)
                {
                    try
                    {
                        try
                        {
                            var id = (AmmoTypeCombobox.SelectedItem as DataRowView).Row[0];

                            if (Convert.ToDecimal(AmmoPrice.Text) > 0)
                            {
                                ammo.InsertAmmo(Convert.ToInt32(id), AmmoNameBox.Text, Convert.ToDecimal(AmmoPrice.Text));
                            }

                            else
                            {
                                MessageBox.Show("Price should be more than 0");
                            }
                        }

                        catch
                        {
                            MessageBox.Show("Price must be number");
                        }


                        AmmoGrid.ItemsSource = ammo.GetAmmoData();
                        AmmoTypeCombobox.ItemsSource = ammotype.GetData();
                        AmmoGrid.Columns[0].Visibility = Visibility.Collapsed;
                        AmmoGrid.Columns[1].Visibility = Visibility.Collapsed;
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("This data is already exist");
                    }
                }
                else
                {
                    MessageBox.Show("Choose ammotype");

                }

            }

            else
            {
                MessageBox.Show("Input ammoname");

            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if ( AmmoGrid.SelectedItem != null)
            {
                if (AmmoTypeCombobox.SelectedItem != null ) {
                    if (Convert.ToDecimal(AmmoPrice.Text) > 0)
                    {
                        try
                        {
                            var id = (AmmoTypeCombobox.SelectedItem as DataRowView).Row[0];
                            var weaponid = (AmmoGrid.SelectedItem as DataRowView).Row[0];

                            ammo.UpdateAmmo(Convert.ToInt32(id), AmmoNameBox.Text, Convert.ToDecimal(AmmoPrice.Text), Convert.ToInt32(weaponid));

                            AmmoGrid.ItemsSource = ammo.GetAmmoData();
                            AmmoTypeCombobox.ItemsSource = ammotype.GetData();
                            AmmoGrid.Columns[0].Visibility = Visibility.Collapsed;
                            AmmoGrid.Columns[1].Visibility = Visibility.Collapsed;
                        }
                        catch (System.Data.SqlClient.SqlException)
                        {
                            MessageBox.Show("This data is already exist");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Price must be more than 0");

                    }
                }

                else
                {
                    MessageBox.Show("Input ammotype");
                }
            }

            else
            {
                MessageBox.Show("Choose weapon");
            }


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (AmmoGrid.SelectedItem != null)
            {
                try
                {
                    var id = (AmmoGrid.SelectedItem as DataRowView).Row[0];
                    ammo.DeleteAmmo(Convert.ToInt32(id));
                    AmmoGrid.ItemsSource = ammo.GetAmmoData();
                    AmmoTypeCombobox.ItemsSource = ammotype.GetData();
                    AmmoGrid.Columns[0].Visibility = Visibility.Collapsed;
                    AmmoGrid.Columns[1].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is using");

                }
            }
            else
            {
                MessageBox.Show("Select value to delete");
            }
        }

        private void AmmoGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (AmmoGrid.SelectedItem != null)
            {
                
                var ammoname = (AmmoGrid.SelectedItem as DataRowView).Row[2];
                var ammoprice = (AmmoGrid.SelectedItem as DataRowView).Row[3];
                AmmoNameBox.Text = Convert.ToString(ammoname);
                AmmoPrice.Text = Convert.ToString(ammoprice);

            }


        }

        private void AmmoGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AmmoGrid.Columns[0].Visibility = Visibility.Collapsed;
            AmmoGrid.Columns[1].Visibility = Visibility.Collapsed;
        }

        private void AmmoPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void AmmoNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
