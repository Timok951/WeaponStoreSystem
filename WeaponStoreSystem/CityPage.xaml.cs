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
    /// Логика взаимодействия для CityPage.xaml
    /// </summary>
    public partial class CityPage : Page
    {
        CityTableAdapter city = new CityTableAdapter();

        public CityPage()
        {
            InitializeComponent();

            CityGrid.ItemsSource = city.GetData();
            CityNameBox.MaxLength = 30;
        }

        private void CityGrid_Loaded(object sender, RoutedEventArgs e)
        {
            CityGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            if (CityNameBox.Text.Length > 0)
            {
                try
                {
                    city.InsertCity(CityNameBox.Text);
                    CityGrid.ItemsSource = city.GetData();
                    CityGrid.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is already exsisted");
                }
            }
            else
            {
                MessageBox.Show("Input City Name");
            }

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CityGrid.SelectedItem != null)
            {
                if (CityNameBox.Text.Length > 0)
                {
                    try
                    {
                        object id = (CityGrid.SelectedItem as DataRowView).Row[0];
                        city.UpdateCity(CityNameBox.Text, Convert.ToInt32(id));
                        CityGrid.ItemsSource = city.GetData();
                        CityGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Data is already exsisted");
                    }
                }
                else
                {
                    MessageBox.Show("Input City Name");
                }

            }

            else
            {
                MessageBox.Show("Select value to edit");
            }
        }
        private void CityGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityGrid.SelectedItem != null) {
                object cityname = (CityGrid.SelectedItem as DataRowView).Row[1];
                CityNameBox.Text = (Convert.ToString(cityname));

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(CityGrid.SelectedItem != null)
            {
                try
                {
                    object id = (CityGrid.SelectedItem as DataRowView).Row[0];
                    city.DeleteCity(Convert.ToInt32(id));
                    CityGrid.ItemsSource = city.GetData();
                    CityGrid.Columns[0].Visibility = Visibility.Collapsed;
                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is being used");

                }
            }

            else
            {
                MessageBox.Show("Select value to delete");
            }

        }

        private void CityNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
