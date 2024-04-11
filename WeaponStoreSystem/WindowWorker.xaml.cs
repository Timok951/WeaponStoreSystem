using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для WindowWorker.xaml
    /// </summary>
    public partial class WindowWorker : Window
    {
        QueriesTableAdapter adapter = new QueriesTableAdapter();

        public WindowWorker()
        {

            InitializeComponent();
            List<string> tables = new List<string> { "City", "Weapon Type", "Weapon", "Warehouse", "WeaponStock", "Ammo Type", "Ammo", "Ammo Stock", "License Type", "License", "Human Role", "Human account", "Human", "Tip", "Orders" };
            TablechangeCombobox.ItemsSource = tables;

        }

        private void TablechangeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = TablechangeCombobox.Items[TablechangeCombobox.SelectedIndex] as string;

                switch (selected)
                {
                    case "City":
                        CityPage citypage = new CityPage();
                        TablesFrame.Content = citypage;
                        
                        //MessageBox.Show("a");
                    break;
                case "Weapon Type":
                        WeaponTypePage weapontypepage = new WeaponTypePage();
                        TablesFrame.Content = weapontypepage;
                    break;
                case "Weapon":
                        WeaponPage weaponPage = new WeaponPage();
                        TablesFrame.Content = weaponPage;  
                    break;

                case "Warehouse":
                    WarehousePage wearehousepage = new WarehousePage();
                    TablesFrame.Content = wearehousepage;
                    break;
                case "WeaponStock":
                    WeaponStockPage weaponstockpage = new WeaponStockPage();
                    TablesFrame.Content = new WeaponStockPage();
                        break;
                case "Ammo Type":
                        AmmoTypePage ammoTypePage = new AmmoTypePage();
                    TablesFrame.Content = new AmmoTypePage();
                    break;
                case "Ammo":
                    AmmoPage ammoPage = new AmmoPage();
                    TablesFrame.Content = ammoPage;
                    break;
                case "Ammo Stock":
                    AmmoStockPage ammoStockPage = new AmmoStockPage();
                    TablesFrame.Content = ammoStockPage;
                    break;
                case "License Type":
                    LicenseTypePage licenseTypePage = new LicenseTypePage();
                    TablesFrame.Content= licenseTypePage;
                    break;
                case "License":
                    LicensePage licensePage = new LicensePage();
                    TablesFrame.Content = licensePage;
                    break;
                case "Human Role":
                    HumanRolePage humanRolePage = new HumanRolePage(); 
                    TablesFrame.Content = humanRolePage;
                    break;
                case "Human account":
                    HumanAccountPage humanAccountPage = new HumanAccountPage(); 
                    TablesFrame.Content = humanAccountPage;
                    break;
                case "Human":
                    HumanPage humanPage = new HumanPage();
                    TablesFrame.Content = humanPage;
                    break;
                case "Tip":
                    TipPage tipPage = new TipPage();
                    TablesFrame.Content = tipPage;
                    break;
                case "Orders":
                    Orders ordersPage = new Orders();
                    TablesFrame.Content = ordersPage;
                    break;

            }

        }

        private void RegisetrButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
        }

        private void MakeBuckup_Click(object sender, RoutedEventArgs e)
        {
            
            adapter.BackupWeaponStore();

        }

        private void MakeBuckup_Copy_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order.Show();
        }
    }
}
