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
using System.Windows.Shapes;
using WeaponStoreSystem.WeaponStoreDataSetTableAdapters;
using System;
using System.IO;


namespace WeaponStoreSystem
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        WeaponTableAdapter weapon = new WeaponTableAdapter();
        AmmoTableAdapter ammo = new AmmoTableAdapter();

        TipTableAdapter tip = new TipTableAdapter();
        HumanTableAdapter human = new HumanTableAdapter();
        OrdersTableAdapter orders = new OrdersTableAdapter();



        public Order()
        {
            InitializeComponent();

            WorkerCombobox.ItemsSource = human.GetHumanAdmin();
            WorkerCombobox.DisplayMemberPath = "HumanName";

            Clientcombobox.ItemsSource = human.GetHumanUser(); 
            Clientcombobox.DisplayMemberPath = "HumanName";



            AmmoTypeCombobobox.ItemsSource = ammo.GetAmmoData();
            AmmoTypeCombobobox.DisplayMemberPath = "AmmoName";

            WeaponTypeCombobobox.ItemsSource = weapon.GetWeaponData();
            WeaponTypeCombobobox.DisplayMemberPath = "WeaponName";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Clientcombobox.SelectedItem != null || WorkerCombobox.SelectedItem != null)
            {
                object weponid;
                object ammoid;
                object ammoname = null;
                object weaponname = null;
                int ammoamount;
                int weaponamount;
                object priceweapon;
                object priceammo;
                try
                {

                    if (WeaponTypeCombobobox.SelectedItem != null)
                    {

                        weponid = (WeaponTypeCombobobox.SelectedItem as DataRowView).Row[0];
                        weaponname = (WeaponTypeCombobobox.SelectedItem as DataRowView).Row[2];


                        //  MessageBox.Show(Convert.ToString(weponid) );
                        //MessageBox.Show(Convert.ToString(weaponname));



                    }

                    else
                    {
                        weponid = null;

                    }

                    if (AmmoTypeCombobobox.SelectedItem != null)
                    {
                        ammoid = (AmmoTypeCombobobox.SelectedItem as DataRowView).Row[0];
                        ammoname = (AmmoTypeCombobobox.SelectedItem as DataRowView).Row[2];




                    }

                    else { ammoid = null; }
                    try
                    {
                        var date = DateTime.Today;
                        var dateshort = date.ToString("yyyy-MM-dd");



                        // MessageBox.Show(Convert.ToString(dateshort) );

                        int sumweapon;
                        int sumammo;
                        object client = (Clientcombobox.SelectedItem as DataRowView).Row[1];

                        object clientid = (Clientcombobox.SelectedItem as DataRowView).Row[0];

                        object worker = (WorkerCombobox.SelectedItem as DataRowView).Row[1];

                        object workerid = (WorkerCombobox.SelectedItem as DataRowView).Row[0];

                        ammoamount = Convert.ToInt32(AmmountofAmmoBox.Text);
                        weaponamount = Convert.ToInt32(AmmountofWeaponBox.Text);

                        var tipid = tip.ScalarQuery();

                        orders.InsertOrders(Convert.ToInt32(ammoid), ammoamount, Convert.ToInt32(weponid), weaponamount, Convert.ToInt32(tipid));


                        try
                        {
                            priceweapon = (WeaponTypeCombobobox.SelectedItem as DataRowView).Row[3];
                            sumweapon = Convert.ToInt32(priceweapon) * weaponamount;

                        }

                        catch {
                            sumweapon = 0;
                        }

                        try
                        {
                            priceammo = (AmmoTypeCombobobox.SelectedItem as DataRowView).Row[3];

                            sumammo = Convert.ToInt32(priceammo) * weaponamount;

                        }

                        catch
                        {
                            sumammo = 0;
                        }
                        int waspayed = Convert.ToInt32(SummBox.Text);

                        if (waspayed < (sumammo + sumweapon))
                        {
                            MessageBox.Show("Not enough money");
                        }
                        else
                        {

                            string username = Environment.UserName;
                            string path = $"C:\\Users\\{username}\\Downloads\\tip.txt";


                            string tiptext = $"Weaponstore" +
                            $"{Convert.ToInt32(tipid)}" +
                            $"\n<{weaponname}> <{sumweapon}> \n" +
                                $"<{ammoname}> <{sumammo}> \n" +
                                $"Total = {sumammo + sumweapon} \n" +
                                $"Was payed = {waspayed} \n" +
                                $"Change: {waspayed - (sumammo + sumweapon)}\n" +
                                $"Worker {worker} Client {client}";
                            //MessageBox.Show($"{path}");


                            if (File.Exists(path))
                            {
                                File.WriteAllText(path, tiptext);
                                tip.InsertTip(Convert.ToInt32(workerid), Convert.ToInt32(clientid), Convert.ToString(dateshort));
                            }

                            else
                            {
                                File.Create(path);
                            }
                        }

                        }


                    catch (Exception en)
                    {
                        MessageBox.Show(en.Message);
                    }

                }

                catch (System.Data.SqlClient.SqlException)
                {

                    MessageBox.Show("This data is alredy exsists");

                }
            
            }

        }

        private void SummBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void AmmountofAmmoBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void AmmountofWeaponBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
}
