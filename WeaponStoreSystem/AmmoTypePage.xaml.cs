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
    /// Логика взаимодействия для AmmoTypePage.xaml
    /// </summary>
    public partial class AmmoTypePage : Page
    {
        AmmoTypeTableAdapter ammotype = new AmmoTypeTableAdapter();

        public AmmoTypePage()
        {
            InitializeComponent();

            AmmoTypeGrid.ItemsSource = ammotype.GetData();
            AmmoTypeBox.MaxLength = 30;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (AmmoTypeBox.Text.Length != 0 )
                {


                    ammotype.InsertAmmoType(AmmoTypeBox.Text);
                    AmmoTypeGrid.ItemsSource = ammotype.GetData();
                    AmmoTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Input ammo type");
                }

            }
            catch (System.Data.SqlClient.SqlException) {
                MessageBox.Show("Data is already exsisted");


            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

            if (AmmoTypeGrid.SelectedItem != null)
            {
                try {
                    if (AmmoTypeBox.Text.Length != 0)
                    {
                        object id = (AmmoTypeGrid.SelectedItem as DataRowView).Row[0];
                        ammotype.UpdateAmmoType(AmmoTypeBox.Text, Convert.ToInt32(id));

                        AmmoTypeGrid.ItemsSource = ammotype.GetData();
                        AmmoTypeGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Input ammo type");
                    }
                }
                catch (System.Data.SqlClient.SqlException) {
                    MessageBox.Show("Data is already exsisted");

                }

            }
            else
            {
                MessageBox.Show("Select value to edit");
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (AmmoTypeGrid.SelectedItem != null)
                {
                    object id = (AmmoTypeGrid.SelectedItem as DataRowView).Row[0];
                    ammotype.DeleteAmmoType(Convert.ToInt32(id));
                    AmmoTypeGrid.ItemsSource = ammotype.GetData();
                    AmmoTypeGrid.Columns[0].Visibility = Visibility.Collapsed;

                }
                else
                {
                    MessageBox.Show("ChooseAmmoType");
                }

            }
            catch (System.Data.SqlClient.SqlException)
            {
                MessageBox.Show("Data is Using");

            }
        }



        private void AmmoTypeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AmmoTypeGrid.SelectedItem != null)
            {
                object ammotypename = (AmmoTypeGrid.SelectedItem as DataRowView).Row[1];
                AmmoTypeBox.Text = ammotypename.ToString();

            }
        }

        private void AmmoTypeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            AmmoTypeGrid.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void AmmoTypeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
