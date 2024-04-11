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
    /// Логика взаимодействия для HumanRolePage.xaml
    /// </summary>
    public partial class HumanRolePage : Page
    {
        HumanRoleTableAdapter humanrole = new HumanRoleTableAdapter();

        public HumanRolePage()
        {
            InitializeComponent();
            HumanGrid.ItemsSource = humanrole.GetData();
            HumanRoleBox.MaxLength = 30;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(HumanRoleBox.Text.Length >0 )
            {
                try
                {
                    humanrole.InsertHumanRole(HumanRoleBox.Text);

                    HumanGrid.ItemsSource = humanrole.GetData();
                    HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
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
            if (HumanRoleBox.Text.Length > 0) {
                if (HumanGrid.SelectedItem != null)
                {
                    try
                    {
                        object id = (HumanGrid.SelectedItem as DataRowView).Row[0];
                        humanrole.UpdateHumanRole(HumanRoleBox.Text, Convert.ToInt32(id));
                        HumanGrid.ItemsSource = humanrole.GetData();
                        HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
                    }

                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("Data is already exsisted");
                    }
                }
                else
                {
                    MessageBox.Show("Choose license to delete");
                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (HumanGrid.SelectedItem != null)
            {
                try
                {
                    object id = (HumanGrid.SelectedItem as DataRowView).Row[0];
                    humanrole.DeleteHumanRole(Convert.ToInt32(id));
                    HumanGrid.ItemsSource = humanrole.GetData();
                    HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
                }
                catch (System.Data.SqlClient.SqlException) {
                    MessageBox.Show("Data is using");
                }


            }
        }

        private void HumanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HumanGrid.SelectedItem != null)
            {
                object cityname = (HumanGrid.SelectedItem as DataRowView).Row[1];
                HumanRoleBox.Text = (Convert.ToString(cityname));

            }
        }

        private void HumanGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HumanGrid.Columns[0].Visibility = Visibility.Collapsed;

        }

        private void HumanRoleBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
