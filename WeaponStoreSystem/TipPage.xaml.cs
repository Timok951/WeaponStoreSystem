using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для TipPage.xaml
    /// </summary>
    public partial class TipPage : Page
    {
        HumanTableAdapter human = new HumanTableAdapter();
        TipTableAdapter tip = new TipTableAdapter();
        OrdersTableAdapter orders = new OrdersTableAdapter();


        public TipPage()
        {
            InitializeComponent();

            TipGrid.ItemsSource = tip.GetTipData();

            Clientcombobox.ItemsSource = human.GetHumanAdmin();
            Clientcombobox.DisplayMemberPath = "HumanName";

            WorkerCombobox.ItemsSource = human.GetHumanUser();
            WorkerCombobox.DisplayMemberPath = "HumanName";


        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (WorkerCombobox.SelectedItem != null && Clientcombobox.SelectedItem != null && DataPick.SelectedDate != null )
            {
                try
                {
                     object client = (Clientcombobox.SelectedItem as DataRowView).Row[0];
                    object worker = (WorkerCombobox.SelectedItem as DataRowView).Row[0];
                    var date = DataPick.SelectedDate;

                    tip.InsertTip(Convert.ToInt32(client), Convert.ToInt32(worker), Convert.ToString(date));

                    TipGrid.ItemsSource = tip.GetTipData();
                    TipGrid.Columns[0].Visibility = Visibility.Collapsed;
                    TipGrid.Columns[1].Visibility = Visibility.Collapsed;
                    TipGrid.Columns[2].Visibility = Visibility.Collapsed;

                }

                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This data is alredy exsists");

                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (WorkerCombobox.SelectedItem != null && Clientcombobox.SelectedItem != null && DataPick.SelectedDate != null)
            {
                if (TipGrid.SelectedItem != null)
                {
                    try
                    {
                        object id = (TipGrid.SelectedItem as DataRowView).Row[0];
                        object client = (Clientcombobox.SelectedItem as DataRowView).Row[0];
                        object worker = (WorkerCombobox.SelectedItem as DataRowView).Row[0];
                        var date = DataPick.SelectedDate;

                        tip.UpdateTip(Convert.ToInt32(client), Convert.ToInt32(worker), Convert.ToString(date), Convert.ToInt32(id));

                        TipGrid.ItemsSource = tip.GetTipData();
                        TipGrid.Columns[0].Visibility = Visibility.Collapsed;
                        TipGrid.Columns[1].Visibility = Visibility.Collapsed;
                        TipGrid.Columns[2].Visibility = Visibility.Collapsed;

                    }

                    catch (System.Data.SqlClient.SqlException)
                    {
                        MessageBox.Show("This data is alredy exsists");

                    }
                }
                else
                {
                    MessageBox.Show("Choose tip to edit");
                }
            }
            else
            {
                MessageBox.Show("Input all data");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TipGrid.SelectedItem != null)
            {
                try
                {
                    object id = (TipGrid.SelectedItem as DataRowView).Row[0];
                    tip.DeleteTip(Convert.ToInt32(id)); 
                    TipGrid.ItemsSource = tip.GetTipData();
                    TipGrid.Columns[0].Visibility = Visibility.Collapsed;
                    TipGrid.Columns[1].Visibility = Visibility.Collapsed;
                    TipGrid.Columns[2].Visibility = Visibility.Collapsed;
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    MessageBox.Show("This tip is using");

                }
            }
            else
            {
                MessageBox.Show("Choose tip to delete");
            }
        }

        private void TipGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TipGrid_Loaded(object sender, RoutedEventArgs e)
        {
            TipGrid.Columns[0].Visibility = Visibility.Collapsed;
            TipGrid.Columns[1].Visibility = Visibility.Collapsed;
            TipGrid.Columns[2].Visibility = Visibility.Collapsed;



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TipGrid.SelectedItem != null)
            {
                object tipid = (TipGrid.SelectedItem as DataRowView).Row[0];
                object tiphumanid = (TipGrid.SelectedItem as DataRowView).Row[1];
                object workerid = (TipGrid.SelectedItem as DataRowView).Row[2];

                object weaponname = orders.GetWeaponName(Convert.ToInt32(tipid));
                object ammoname = orders.GetAmmoName(Convert.ToInt32(tipid));
                object ammoprice = orders.GetAmmoPrice(Convert.ToInt32(tipid));
                object weaponprice = orders.GetWeponPrice(Convert.ToInt32(tipid));
                object worker = human.GetWorkerName(Convert.ToInt32(workerid));
                object client = human.GetWorkerName(Convert.ToInt32(tiphumanid));
                object weaponammount = orders.OrderGetWeaponamount(Convert.ToInt32(tipid));
                object ammoamount = orders.OrdersGetAmmoamount(Convert.ToInt32(tiphumanid));

                int sumweapon = Convert.ToInt32(weaponammount) * Convert.ToInt32(weaponprice);

                int sumammo = Convert.ToInt32(ammoamount) * Convert.ToInt32(ammoprice);


                string username = Environment.UserName;
                string path = $"C:\\Users\\{username}\\Downloads\\tip.txt";

                int waspayed = sumammo + sumweapon;

                string tiptext = $"Weaponstore\n" +
                $"{Convert.ToInt32(tipid)}" +
                $"\n<{weaponname}> <{sumweapon}> \n" +
                 $"<{ammoname}> <{sumammo}> \n" +
                $"Total = {sumammo + sumweapon} \n" +
                $"Was payed = {waspayed} \n" +
                $"Change: {waspayed - (sumammo + sumweapon)}\n" +
                $"Worker {worker} Client {client}";


                if (File.Exists(path))
                {
                    File.WriteAllText(path, tiptext);
                }

                else
                {
                    File.Create(path);
                }

            }
            else
            {
                MessageBox.Show("Chosse tip to save him");
            }
        }
        }
    }

