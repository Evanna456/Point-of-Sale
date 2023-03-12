using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace PointOfSale.PointofSalePages
{
    /// <summary>
    /// Interaction logic for PointofSalePage3AddInventory.xaml
    /// </summary>
    public partial class PointofSalePage3AddInventory : Page
    {
        public PointofSalePage3AddInventory()
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
                if (word.Length != 0 && Regex.IsMatch(word, @"^[a-zA-Z0-9]+$") == true)
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

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void PointofSaleInventoryNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage2Inventory.xaml", UriKind.Relative));
        }

        //EndNavigation

        private void AddItemClick(object sender, RoutedEventArgs e)
        {

            string serialno, itemname, baseprice, retailprice;
            int itemexist = 0;

            serialno = SerialNo.Text;
            itemname = ItemName.Text;
            baseprice = BasePrice.Text;
            retailprice = RetailPrice.Text;

            if (Validation("number", "Serial No", serialno, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("number", "Serial No", serialno, 1, 200), "Error");

                SerialNo.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Item Name", itemname, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Item Name", itemname, 1, 200), "Error");

                ItemName.BorderBrush = Brushes.Red;
            }
            else if (Validation("money", "Base Price", baseprice, 1, 20) != "Validated")
            {
                MessageBox.Show(Validation("money", "Base Price", baseprice, 1, 20), "Error");

                BasePrice.BorderBrush = Brushes.Red;
            }
            else if (Validation("money", "Retail Price", retailprice, 1, 20) != "Validated")
            {
                MessageBox.Show(Validation("money", "Retail Price", retailprice, 1, 20), "Error");

                RetailPrice.BorderBrush = Brushes.Red;
            }
            else
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
                    command.CommandText = @"SELECT serial_no FROM inventory WHERE serial_no = $serialno";
                    command.Parameters.AddWithValue("$serialno", serialno);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0) == true)
                            {
                                itemexist = 0;
                            }
                            else if (reader.IsDBNull(0) == false)
                            {
                                itemexist = 1;
                            }

                        }
                    }

                    if (itemexist == 0)
                    {

                        command = connection.CreateCommand();
                        command.CommandText = @"INSERT INTO inventory (serial_no, item_name, base_price, retail_price) VALUES($serialno, $itemname, $baseprice, $retailprice);";
                        command.Parameters.AddWithValue("$serialno", serialno);
                        command.Parameters.AddWithValue("$itemname", itemname);
                        command.Parameters.AddWithValue("$baseprice", baseprice);
                        command.Parameters.AddWithValue("$retailprice", retailprice);

                        command.ExecuteNonQuery();

                        connection.Close();

                        SerialNo.Text = "";
                        ItemName.Text = "";
                        BasePrice.Text = "";
                        RetailPrice.Text = "";

                        MessageBox.Show("Item Added", "Success");


                    }
                    else if (itemexist == 1)
                    {

                        connection.Close();

                        SerialNo.BorderBrush = Brushes.Red;

                        MessageBox.Show("Item Serial No already Exist", "Error");
                    }
                }
            }
        }

        //END
    }
}
