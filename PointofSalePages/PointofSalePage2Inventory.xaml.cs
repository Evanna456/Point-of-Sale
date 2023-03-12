using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace PointOfSale.PointofSalePages
{
    /// <summary>
    /// Interaction logic for PointofSalePage2Inventory.xaml
    /// </summary>
    public partial class PointofSalePage2Inventory : Page
    {
        public PointofSalePage2Inventory()
        {
            InitializeComponent();
        }

        //Utilities

        private Storyboard myStoryboard;
        string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        string tempPath = System.IO.Path.GetTempPath();
        int optionmenuvisible = 0;
        protected string passwordDB = "steamsql456sahgdg23t432jj";
        protected string passwordLIC = "steamlicense523645sasawekk";

        public void CheckDataDirectory()
        {

            if (Directory.Exists(baseDirectory + "/data") == false)
            {
                Directory.CreateDirectory(baseDirectory + "/data");
            }

        }

        public void CheckDatabaseFile()
        {
            CheckDataDirectory();

            if (File.Exists(baseDirectory + "/data/pos.db") == false)
            {
                byte[] database = Properties.Resources.pos;


                File.WriteAllBytes(baseDirectory + "/data/pos.db", database);

            }
            else if (File.Exists(baseDirectory + "/data/lic.db") == false)
            {
                byte[] license = Properties.Resources.lic;

                File.WriteAllBytes(baseDirectory + "/data/lic.db", license);
            }

        }

        //UtilitiesEnd

        //OptionsMenu

        private void PageClick(object sender, RoutedEventArgs e)
        {
            OptionMenu.Visibility = Visibility.Hidden;
            optionmenuvisible = 0;
        }

        private void OptionMenuBtnClick(object sender, RoutedEventArgs e)
        {
            CheckOptionMenu();
        }

        private void AboutBtnClick(object sender, RoutedEventArgs e)
        {
            CheckOptionMenu();

            AboutWindow aboutwindow = new AboutWindow();

            aboutwindow.Show();
        }

        private void HelpFileBtnClick(object sender, RoutedEventArgs e)
        {
            CheckOptionMenu();

            CheckDataDirectory();

            HelpFile();
        }

        private void ExitAppBtnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }

        public void CheckOptionMenu()
        {
            if (optionmenuvisible == 0)
            {
                OptionMenu.Visibility = Visibility.Visible;
                optionmenuvisible = 1;
            }
            else if (optionmenuvisible == 1)
            {
                OptionMenu.Visibility = Visibility.Hidden;
                optionmenuvisible = 0;
            }

        }

        public void HelpFile()
        {

            CheckDataDirectory();

            byte[] helpfile = Properties.Resources.POSHelp;

            if (File.Exists(baseDirectory + "/data/POSHelp.chm") == false || File.Exists(baseDirectory + "/data/POSHelp.chm") == true)
            {
                File.Delete(System.IO.Path.Combine(baseDirectory + "/", "POSHelp.chm"));

                File.WriteAllBytes(baseDirectory + "/data/POSHelp.chm", helpfile);

                System.Diagnostics.Process.Start(baseDirectory + "/data/POSHelp.chm");
            }
            else
            {
                MessageBox.Show("Try Reinstalling the Application", "Error");
            }

        }

        //OptionsMenuEnd

        //Validation

        public string Validation(string type, string name, string word, int min, int max)
        {
            string result = "";

            if (type == "variable")
            {
                if (word.Length != 0 && word.Length >= min && word.Length <= max)
                {
                    result = "Validated";

                    return result;
                }
                else if (word.Length != 0 && word.Length < min || word.Length > max)
                {
                    result = name + " characters should be at least " + min + " and lesser than or equal to " + max;

                    return result;
                }
                else if (word.Length == 0)
                {
                    result = name + " Field is Empty"; ;

                    return result;
                }
            }
            else if (type == "number")
            {
                if (Regex.IsMatch(word, @"^[0-9]*$") == true)
                {
                    if (word.Length != 0 && word.Length >= min && word.Length <= max)
                    {
                        result = "Validated";

                        return result;
                    }
                    else if (word.Length != 0 && word.Length != 0 && word.Length < min || word.Length > max)
                    {
                        result = name + " characters should be at least " + min + " and lesser than or equal to " + max;

                        return result;
                    }
                    else if (word.Length == 0)
                    {
                        result = name + " Field is Empty";

                        return result;
                    }

                }
                else if (word.Length != 0 && Regex.IsMatch(word, @"^[0-9]*$") == false)
                {
                    result = name + " is not a number";

                    return result;
                }
                else if (word.Length == 0)
                {
                    result = name + " Field is Empty";

                    return result;
                }
            }
            else if (type == "alphanum")
            {
                if (word.Length != 0 && Regex.IsMatch(word, @"^[a-zA-Z0-9_]*$") == true)
                {
                    if (word.Length != 0 && word.Length >= min && word.Length <= max)
                    {
                        result = "Validated";

                        return result;
                    }
                    else if (word.Length != 0 && word.Length < min || word.Length > max)
                    {
                        result = name + " characters should be at least " + min + " and lesser than or equal to " + max;

                        return result;
                    }
                }
                else if (word.Length != 0 && Regex.IsMatch(word, @"^[a-zA-Z0-9]+$") == false)
                {
                    result = name + " Should only contain letters and numbers";

                    return result;
                }
                else if (word.Length == 0)
                {
                    result = name + " Field is Empty";

                    return result;
                }
            }
            else if (type == "alphanum2")
            {
                if (word.Length != 0 && Regex.IsMatch(word, @"^[-\w\s]+$") == true)
                {
                    if (word.Length != 0 && word.Length >= min && word.Length <= max)
                    {
                        result = "Validated";

                        return result;
                    }
                    else if (word.Length != 0 && word.Length < min || word.Length > max)
                    {
                        result = name + " characters should be at least " + min + " and lesser than or equal to " + max;

                        return result;
                    }
                }
                else if (word.Length != 0 && Regex.IsMatch(word, @"^[-\w\s]+$") == false)
                {
                    result = name + " Should only contain letters, numbers and spaces";

                    return result;
                }
                else if (word.Length == 0)
                {
                    result = name + " Field is Empty";

                    return result;
                }
            }
            else if (type == "money")
            {
                if (word.Length != 0 && Regex.IsMatch(word, @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$") == true)
                {
                    if (word.Length != 0 && word.Length >= min && word.Length <= max)
                    {
                        result = "Validated";

                        return result;
                    }
                    else if (word.Length != 0 && word.Length < min || word.Length > max)
                    {
                        result = name + " characters should be at least " + min + " and lesser than or equal to " + max;

                        return result;
                    }
                }
                else if (word.Length != 0 && Regex.IsMatch(word, @"^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$") == false)
                {
                    result = name + " Invalid";

                    return result;
                }
                else if (word.Length == 0)
                {
                    result = name + " Field is Empty";

                    return result;
                }
            }

            return result;

        }

        //ValidationEnd

        //Loaded

        private async void Load(object sender, RoutedEventArgs e)
        {

            await Task.Run(() => GetInventoryTable());
        }

        //EndLoaded

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void AddInventoryNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage3AddInventory.xaml", UriKind.Relative));
        }

        //EndNavigation

        public class DataItem
        {
            public string SerialNoClmn { get; set; }
            public string ItemNameClmn { get; set; }
            public string BasePriceClmn { get; set; }
            public string RetailPriceClmn { get; set; }
            public string ViewButtonClmn { get; set; }
        }

        public void GetInventoryTable()
        {
            this.Dispatcher.Invoke(() =>
            {

                using (var connection = new SqliteConnection("Data Source=" + baseDirectory + "/data/pos.db"))
                {

                    connection.ConnectionString =
        new SqliteConnectionStringBuilder(connection.ConnectionString)
        { Password = passwordDB }
            .ToString();

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"SELECT serial_no, item_name, base_price, retail_price FROM inventory";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string serialno = reader.GetString(0);
                            string itemname = reader.GetString(1);
                            string baseprice = reader.GetString(2);
                            string retailprice = reader.GetString(3);

                            DataItem item = new DataItem();
                            item.SerialNoClmn = serialno;
                            item.ItemNameClmn = itemname;
                            item.BasePriceClmn = baseprice;
                            item.RetailPriceClmn = retailprice;

                            InventoryTable.Items.Add(item);

                        }
                    }

                    connection.Close();
                }

            });

        }

        private void SearchItemBtnClick(object sender, RoutedEventArgs e)
        {
            string searchItem;

            searchItem = SearchItemBox.Text;

            if (Validation("alphanum2", "Search Item", searchItem, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("alphanum2", "Search Item", searchItem, 1, 200), "Error");

                SearchItemBox.BorderBrush = Brushes.Red;
            }
            else
            {

                InventoryTable.Items.Clear();

                using (var connection = new SqliteConnection("Data Source=" + baseDirectory + "/data/pos.db"))
                {

                    connection.ConnectionString =
        new SqliteConnectionStringBuilder(connection.ConnectionString)
        { Password = passwordDB }
            .ToString();

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"SELECT serial_no, item_name, base_price, retail_price FROM inventory where item_name like '%" + searchItem + "%' or serial_no like '%" + searchItem + "%';";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string serialno = reader.GetString(0);
                            string itemname = reader.GetString(1);
                            string baseprice = reader.GetString(2);
                            string retailprice = reader.GetString(3);

                            DataItem item = new DataItem();
                            item.SerialNoClmn = serialno;
                            item.ItemNameClmn = itemname;
                            item.BasePriceClmn = baseprice;
                            item.RetailPriceClmn = retailprice;

                            InventoryTable.Items.Add(item);

                        }
                    }

                    connection.Close();
                }

            }

        }

        private void ViewItemClick(object sender, RoutedEventArgs e)
        {
            int currentRowIndex = InventoryTable.Items.IndexOf(InventoryTable.CurrentItem);

            TextBlock x = InventoryTable.Columns[0].GetCellContent(InventoryTable.Items[currentRowIndex]) as TextBlock;

            string id = x.Text;

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage4EditInventory.xaml", UriKind.Relative));

            PointofSalePage4EditInventory pointofsalepage4editinventory = new PointofSalePage4EditInventory(id);

            this.NavigationService.Navigate(pointofsalepage4editinventory);

        }

        private void ResetInventoryTableClick(object sender, RoutedEventArgs e)
        {
            SearchItemBox.Text = "";

            InventoryTable.Items.Clear();

            GetInventoryTable();
        }

        //END
    }
}
