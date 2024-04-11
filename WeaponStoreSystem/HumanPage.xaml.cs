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
    /// Логика взаимодействия для HumanPage.xaml
    /// </summary>
    /// 

    public partial class HumanPage : Page
    {
        HumanTableAdapter human = new HumanTableAdapter();
        LicenseTableAdapter license = new LicenseTableAdapter();
        HumanAccountTableAdapter humanAccount = new HumanAccountTableAdapter();

        public HumanPage()
        {
            InitializeComponent();
            HumanGrid.ItemsSource = human.GetHumanData();

            HumanLicenseBox.ItemsSource = license.GetData();
            HumanLicenseBox.DisplayMemberPath = "LicenseNumber";

            HumanAccountBox.ItemsSource = humanAccount.GetData();
            HumanAccountBox.DisplayMemberPath = "AccountLogin";

            HumanNumberBox.MaxLength = 12;
            HumanNameBox.MaxLength = 30;
            HumanSurnameBox.MaxLength = 30;
            HumanSecondNameBox.MaxLength = 30;

        }

        private void HumanGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (HumanGrid.SelectedItem != null)
            {
                object name = (HumanGrid.SelectedItem as DataRowView).Row[1];
                object surname = (HumanGrid.SelectedItem as DataRowView).Row[2];
                object secondname = (HumanGrid.SelectedItem as DataRowView).Row[3];
                object telphone = (HumanGrid.SelectedItem as DataRowView).Row[4];


                HumanNameBox.Text = Convert.ToString(name);
                HumanSurnameBox.Text = Convert.ToString(surname);
                HumanSecondNameBox.Text = Convert.ToString(secondname);
                HumanNumberBox.Text = Convert.ToString(telphone);



            }

        }

        private void HumanGrid_Loaded(object sender, RoutedEventArgs e)
        {
            HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
            HumanGrid.Columns[5].Visibility = Visibility.Collapsed;
            HumanGrid.Columns[6].Visibility = Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (HumanAccountBox.SelectedItem != null && HumanLicenseBox.SelectedItem != null && HumanNameBox.Text.Length > 0 && HumanSurnameBox.Text.Length > 0 && HumanSecondNameBox.Text.Length >0 && HumanNumberBox.Text.Length > 0)
            {


                try
                {
                    object humanlicense = (HumanAccountBox.SelectedItem as DataRowView).Row[0];
                    object humanaacount = (HumanAccountBox.SelectedItem as DataRowView).Row[0];
                    if (HumanNumberBox.Text.Length == 12 && HumanNumberBox.Text.Contains("+") )
                    {
                        human.InsertHuman(HumanNameBox.Text, HumanSurnameBox.Text, HumanSecondNameBox.Text, HumanNumberBox.Text,Convert.ToInt32(humanaacount), Convert.ToInt32(humanlicense) );
                        
                        HumanGrid.ItemsSource = human.GetHumanData();
                        HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
                        HumanGrid.Columns[5].Visibility = Visibility.Collapsed;
                        HumanGrid.Columns[6].Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        MessageBox.Show("Input valid phone number");
                    }

                }
          

                catch (System.Data.SqlClient.SqlException) {
                    MessageBox.Show("Data is already exsists");
                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (HumanGrid.SelectedItem != null && HumanAccountBox.SelectedItem != null && HumanLicenseBox.SelectedItem != null && HumanNameBox.Text.Length > 0 && HumanSurnameBox.Text.Length > 0 && HumanSecondNameBox.Text.Length > 0 && HumanNumberBox.Text.Length > 0)
            {
                try
                {
                    object id = (HumanGrid.SelectedItem as DataRowView).Row[0];
                    object humanlicense = (HumanAccountBox.SelectedItem as DataRowView).Row[0];
                    object humanaacount = (HumanAccountBox.SelectedItem as DataRowView).Row[0];
                    if (HumanNumberBox.Text.Length == 12 && HumanNumberBox.Text.Contains("+"))
                    {
                        human.UpdateHuman(HumanNameBox.Text, HumanSurnameBox.Text, HumanSecondNameBox.Text, HumanNumberBox.Text, Convert.ToInt32(humanaacount), Convert.ToInt32(humanlicense), Convert.ToInt32(id));
                        HumanGrid.ItemsSource = human.GetHumanData();
                        HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
                        HumanGrid.Columns[5].Visibility = Visibility.Collapsed;
                        HumanGrid.Columns[6].Visibility = Visibility.Collapsed;
                    }

                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is already exsists");
                }
            }
            else
            {
                MessageBox.Show("Input all data and choose grid");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (HumanGrid.SelectedItem != null)
            {
                try
                {
                    object id = (HumanGrid.SelectedItem as DataRowView).Row[0];
                    human.DeleteHuman(Convert.ToInt32(id));
                    HumanGrid.ItemsSource = human.GetHumanData();
                    HumanGrid.Columns[0].Visibility = Visibility.Collapsed;
                    HumanGrid.Columns[5].Visibility = Visibility.Collapsed;
                    HumanGrid.Columns[6].Visibility = Visibility.Collapsed;

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Data is uing");

                }



            }
        }

        private void HumanNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if ( !char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }


        private void HumanSurnameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }

        private void HumanSecondNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                {
                    e.Handled = true;
                    break;
                }
            }
        }
    }
}
