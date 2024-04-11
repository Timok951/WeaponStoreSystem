using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для WindowUser.xaml
    /// </summary>
    public partial class WindowUser : Window
    {
        public WindowUser()
        {
            InitializeComponent();
            InitializeComponent();
            List<string> tables = new List<string> { "Weapon Type", "Weapon", "Ammo Type", "Ammo" };
            TablechangeCombobox.ItemsSource = tables;
        }

        private void TablechangeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = TablechangeCombobox.Items[TablechangeCombobox.SelectedIndex] as string;

            switch (selected)
            {

                case "Weapon Type":
                    WeaponTypeUserPage weapontypepage = new WeaponTypeUserPage();
                    TablesFrame.Content = weapontypepage;
                    break;
                case "Weapon":
                    WeaponUserPage weaponPage = new WeaponUserPage();
                    TablesFrame.Content = weaponPage;
                    break;
                case "Ammo Type":
                    AmmoTypeUserPage ammoTypePage = new AmmoTypeUserPage();
                    TablesFrame.Content = new AmmoTypeUserPage();
                    break;
                case "Ammo":
                    AmmoUserPage ammoPage = new AmmoUserPage();
                    TablesFrame.Content = ammoPage;
                    break;
               

            }
        }
    }
}
