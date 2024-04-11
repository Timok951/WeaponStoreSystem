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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;


namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для WeaponUserPage.xaml
    /// </summary>
    public partial class WeaponUserPage : Page
    {
        WeaponTableAdapter weapon = new WeaponTableAdapter();

        public WeaponUserPage()
        {
            InitializeComponent();
            WeaponGrid.ItemsSource = weapon.GetWeaponData();

        }
    }
}
