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
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;
using static MaterialDesignThemes.Wpf.Theme;

namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HumanAccountTableAdapter accountadapter = new HumanAccountTableAdapter();

        public MainWindow()
        {
            InitializeComponent();
            PasswordTextBox.PasswordChar = '*';
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = accountadapter.GetData().Rows;

           

            for (int i = 0; i < allLogins.Count; i++)
            {

                if (allLogins[i][1].ToString() == LoginTextbox.Text
                    && allLogins[i][2].ToString() == md5.hashPassword(PasswordTextBox.Password))
                {
                    int roleID = (int)allLogins[i][3];

                    switch (roleID)
                    {
                        case 1:
                            WindowWorker admin = new WindowWorker();
                            admin.Show();
                            
                            break;
                        case 2:
                            WindowUser user = new WindowUser();
                            user.Show();
                            
                            break;
                    }
                }
            }
        }



    }
}
