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
    /// Логика взаимодействия для HumanAccountPage.xaml
    /// </summary>
    public partial class HumanAccountPage : Page
    {
        HumanRoleTableAdapter humanrole = new HumanRoleTableAdapter();
        HumanAccountTableAdapter humanaccount = new HumanAccountTableAdapter();

        public HumanAccountPage()
        {
            InitializeComponent();

            HumanAccountGrid.ItemsSource = humanaccount.GetHumanAccountData();
            HumanRoleCombobox.ItemsSource = humanrole.GetData();
            HumanRoleCombobox.DisplayMemberPath = "RoleName";
            HumanAccountPasswordBox.MaxLength = 30;
            HumanAccountNickBox.MaxLength = 30;



        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (HumanRoleCombobox.SelectedItem != null && HumanAccountNickBox.Text.Length > 0)
            {
                try
                {
                    if (HumanAccountPasswordBox.Text.Length > 5)
                    {


                        var roleid = (HumanRoleCombobox.SelectedItem as DataRowView).Row[0];
                        humanaccount.InsertHumanAccount(HumanAccountNickBox.Text, md5.hashPassword(HumanAccountPasswordBox.Text), Convert.ToInt32(roleid));

                        HumanAccountGrid.ItemsSource = humanaccount.GetHumanAccountData();
                        HumanAccountGrid.Columns[0].Visibility = Visibility.Collapsed;
                        HumanAccountGrid.Columns[3].Visibility = Visibility.Collapsed;
                    }

                    else {
                        MessageBox.Show("Password must be more than 5 sumbols");
                    }
                }
                catch (System.Data.SqlClient.SqlException) {
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
            if (HumanRoleCombobox.SelectedItem != null && HumanAccountNickBox.Text.Length > 0)
            {
                try
                {
                    if (HumanAccountPasswordBox.Text.Length > 5)
                    {
                        var roleid = (HumanRoleCombobox.SelectedItem as DataRowView).Row[0];
                        var id = (HumanAccountGrid.SelectedItem as DataRowView).Row[0];

                        humanaccount.UpdateHuamnAccount(HumanAccountNickBox.Text, md5.hashPassword(HumanAccountPasswordBox.Text), Convert.ToInt32(roleid), Convert.ToInt32(id));

                        HumanAccountGrid.ItemsSource = humanaccount.GetHumanAccountData();
                        HumanAccountGrid.Columns[0].Visibility = Visibility.Collapsed;
                        HumanAccountGrid.Columns[3].Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Password must be more than 5 sumbols");

                    }
                }
                
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is already exist");

                }

            }
            else
            {
                MessageBox.Show("This data is already exist");
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (HumanAccountGrid.SelectedItem != null)
            {
                try
                {
                    var id = (HumanAccountGrid.SelectedItem as DataRowView).Row[0];
                    humanaccount.DeleteHumanAccount(Convert.ToInt32(id));

                    HumanAccountGrid.ItemsSource = humanaccount.GetHumanAccountData();
                    HumanAccountGrid.Columns[0].Visibility = Visibility.Collapsed;
                    HumanAccountGrid.Columns[3].Visibility = Visibility.Collapsed;

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is using");
                }

            }
            else
            {
                MessageBox.Show("Select data to delete");
            }
            
        }

        private void HumanAccountGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HumanAccountGrid.SelectedItem != null)
            {
                var humanname = (HumanAccountGrid.SelectedItem as DataRowView).Row[1];
                HumanAccountNickBox.Text = Convert.ToString(humanname);
            }
        }

        private void HumanAccountGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HumanAccountGrid.Columns[0].Visibility = Visibility.Collapsed;
            HumanAccountGrid.Columns[3].Visibility = Visibility.Collapsed;
        }

        private void HumanAccountNickBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
