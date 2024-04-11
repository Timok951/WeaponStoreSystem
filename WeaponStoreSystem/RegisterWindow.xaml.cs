using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Shapes;
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;
using static WeaponStoreSystem.WeaponStoreDataSet;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        HumanRoleTableAdapter humanrole = new HumanRoleTableAdapter();
        HumanTableAdapter human = new HumanTableAdapter();
        HumanAccountTableAdapter humanaccount = new HumanAccountTableAdapter();
        LicenseTypeTableAdapter licensetype = new LicenseTypeTableAdapter();
        LicenseTableAdapter license = new LicenseTableAdapter();



        public RegisterWindow()
        {
            InitializeComponent();
            HumanRoleCombobox.ItemsSource = humanrole.GetData();
            HumanRoleCombobox.DisplayMemberPath = "RoleName";
            LicenseTypeCombobox.ItemsSource = licensetype.GetData();
            LicenseTypeCombobox.DisplayMemberPath = "LicenseType";

            HumanAccountPasswordBox.MaxLength = 30;
            HumanAccountNickBox.MaxLength = 30;
            HumanPhoneNumber.MaxLength = 12;
            HumanNameBox.MaxLength = 30;
            HumanSurnameBox.MaxLength = 30;
            HumanSecondNameBox.MaxLength = 30;
            HumanLicense.MaxLength = 5;

            HumanAccountNickBox.IsEnabled = false;
            HumanAccountPasswordBox.IsEnabled = false;
            HumanRoleCombobox.IsEnabled = false;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool register = true;

            string phonenumber = "";
            var role = (HumanRoleCombobox.SelectedItem as DataRowView).Row[0];
            object licenseid = null;
            if (LicenseTypeCombobox.SelectedItem != null && HumanLicense.Text.Length >0) {
                try
                {
                    licenseid = (LicenseTypeCombobox.SelectedItem as DataRowView).Row[0];
                    try
                    {
                        Convert.ToInt32(HumanLicense.Text);
                    }
                    catch
                    {
                        MessageBox.Show("License must be a unique number");
                        register = false;

                    }
                    int humanlicense = Convert.ToInt32(HumanLicense.Text);

                    if (register == true)
                    {
                        license.InsertLicense(humanlicense, Convert.ToInt32(licenseid));

                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Input unique number");
                    register = false;
                }
            }
            else
            {
                MessageBox.Show("Input all data");
                register = false;

            }




            object idhuman = humanaccount.ScalarQuery();
            //MessageBox.Show(Convert.ToString(idhuman));
            if (HumanAccountNickBox.Text.Length > 0 && HumanAccountPasswordBox.Text.Length > 0 && HumanRoleCombobox.SelectedItem != null && register == true)
            {
                try
                {
                    humanaccount.InsertHumanAccount(HumanAccountNickBox.Text, md5.hashPassword(HumanAccountPasswordBox.Text), Convert.ToInt32(role));

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Nick must be unique");
                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }


                object licenselastid = license.ScalarQuery();
            if(HumanPhoneNumber.Text.Length == 12 && HumanPhoneNumber.Text.Contains("+"))
            {
                phonenumber = HumanPhoneNumber.Text;
            }

            else
            {
                MessageBox.Show("Insert only unique phonenumber");
                humanaccount.DeleteHumanAccount(Convert.ToInt32(idhuman));
                license.DeleteLicense(Convert.ToInt32(licenselastid));
                
            }
            
            if (HumanNameBox.Text.Length > 0 && HumanNameBox.Text.Length > 0 && HumanSecondNameBox.Text.Length > 0)
            {
                try
                {
                    human.InsertHuman(HumanNameBox.Text, HumanSurnameBox.Text, HumanSecondNameBox.Text, phonenumber, Convert.ToInt32(idhuman), Convert.ToInt32(licenselastid));

                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("Error");
                    humanaccount.DeleteHumanAccount(Convert.ToInt32(idhuman));
                   license.DeleteLicense(Convert.ToInt32(licenselastid));
                }
            }
            else
            {
                MessageBox.Show("Input all data");
                humanaccount.DeleteHumanAccount(Convert.ToInt32(idhuman));
                license.DeleteLicense(Convert.ToInt32(licenselastid));
            }


        }

        private void HumanNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length ==12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
            

            
        }

        private void HumanSurnameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length == 12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
        }

        private void HumanSecondNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length == 12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
        }

        private void HumanPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length == 12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
        }

        private void HumanLicense_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length == 12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
        }

        private void LicenseTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HumanNameBox.Text != null || HumanSurnameBox.Text != null || HumanSecondNameBox.Text != null || HumanPhoneNumber.Text.Contains("+") || HumanPhoneNumber.Text.Length == 12 || HumanLicense.Text.Length == 5 || LicenseTypeCombobox.SelectedItem != null)
            {
                HumanAccountNickBox.IsEnabled = true;
                HumanAccountPasswordBox.IsEnabled = true;
                HumanRoleCombobox.IsEnabled = true;
            }
        }

        private void HumanLicense_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    break;
                }
            }
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


        private void HumanNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void HumanSurnameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void HumanSecondNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
