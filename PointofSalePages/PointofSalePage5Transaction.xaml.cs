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
    /// Interaction logic for PointofSalePage5Transaction.xaml
    /// </summary>
    public partial class PointofSalePage5Transaction : Page
    {
        public PointofSalePage5Transaction()
        {
            InitializeComponent();
        }

        string serialid;

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

            await Task.Run(() => GetInventoryItems());
        }

        //EndLoaded

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void SettingsNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage8Settings.xaml", UriKind.Relative));
        }

        //EndNavigation

        public class DataItem
        {
            public string SerialNoClmn { get; set; }
            public string ItemNameClmn { get; set; }
            public string PCSClmn { get; set; }
            public string RetailPriceClmn { get; set; }
            public string TotalAmountPriceClmn { get; set; }
            public string ViewButtonClmn { get; set; }
        }

        public void GetInventoryItems()
        {
            double totalamount;

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
                    command.CommandText = @"SELECT serial_no, item_name FROM inventory";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string serialno = reader.GetString(0);
                            string itemname = reader.GetString(1);

                            ItemBox.Items.Add(itemname + "|" + serialno);

                        }
                    }

                    command = connection.CreateCommand();
                    command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                    command.ExecuteNonQuery();
                    command.CommandText = @"SELECT serial_no, item_name, pcs, retail_price, total_price FROM shopping_cart;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string serialno = reader.GetString(0);
                            string itemname = reader.GetString(1);
                            string pcs = reader.GetString(2);
                            string retailprice = reader.GetString(3);
                            string totalprice = reader.GetString(4);

                            DataItem item = new DataItem();
                            item.SerialNoClmn = serialno;
                            item.ItemNameClmn = itemname;
                            item.PCSClmn = pcs;
                            item.RetailPriceClmn = retailprice;
                            item.TotalAmountPriceClmn = totalprice;

                            ShoppingCartTable.Items.Add(item);

                        }
                    }

                    command = connection.CreateCommand();
                    command.CommandText = @"SELECT SUM(total_price) FROM shopping_cart WHERE EXISTS(SELECT total_price FROM shopping_cart);";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            if (reader.IsDBNull(0) == true)
                            {
                                totalamount = 0;
                                GrandTotal.Text = String.Format("{0:0.00}", totalamount);
                            }
                            else if (reader.IsDBNull(0) == false)
                            {

                                string stringtotalamount = reader.GetString(0);

                                totalamount = Convert.ToDouble(stringtotalamount);

                                GrandTotal.Text = String.Format("{0:0.00}", totalamount);
                            }

                        }
                    }

                    connection.Close();
                }

            });

        }

        private void AddtoCartBtnClick(object sender, RoutedEventArgs e)
        {

            string serialno = "", itemname = "", baseprice = "", retailprice = "", totalprice = "";
            string item = ItemBox.Text;
            string itemword = item.Split('|')[0];
            string pcs = Pcs.Text;
            int itemexist = 0;

            if (Validation("variable", "Item", item, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Item", item, 1, 200), "Error");

                ItemBox.BorderBrush = Brushes.Red;
            }
            else if (Validation("number", "PCS", pcs, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("number", "Item", pcs, 1, 100), "Error");

                Pcs.BorderBrush = Brushes.Red;
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
                    command.CommandText = @"SELECT serial_no FROM inventory WHERE item_name = $itemword;";
                    command.Parameters.AddWithValue("$itemword", itemword);
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

                    if (itemexist == 1)
                    {

                        command = connection.CreateCommand();
                        command.CommandText = @"SELECT serial_no, item_name, base_price, retail_price FROM inventory WHERE item_name = $itemword;";
                        command.Parameters.AddWithValue("$itemword", itemword);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                serialno = reader.GetString(0);
                                itemname = reader.GetString(1);
                                baseprice = reader.GetString(2);
                                retailprice = reader.GetString(3);
                            }
                        }

                        double doubleretailprice = Convert.ToDouble(retailprice);
                        int integerpcs = (int)Int32.Parse(pcs);

                        double totalamount = doubleretailprice * integerpcs;

                        command = connection.CreateCommand();
                        command.CommandText = @"INSERT INTO shopping_cart (serial_no, item_name, pcs, base_price, retail_price, total_price) VALUES($serialno, $itemname, $pcs, $baseprice, $retailprice, $totalamount);";
                        command.Parameters.AddWithValue("$serialno", serialno);
                        command.Parameters.AddWithValue("$itemname", itemname);
                        command.Parameters.AddWithValue("$pcs", pcs);
                        command.Parameters.AddWithValue("$baseprice", baseprice);
                        command.Parameters.AddWithValue("$retailprice", retailprice);
                        command.Parameters.AddWithValue("$totalamount", totalamount);

                        command.ExecuteNonQuery();

                        ShoppingCartTable.Items.Clear();

                        command = connection.CreateCommand();
                        command.CommandText = @"SELECT serial_no, item_name, pcs, retail_price, total_price FROM shopping_cart;";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                serialno = reader.GetString(0);
                                itemname = reader.GetString(1);
                                pcs = reader.GetString(2);
                                retailprice = reader.GetString(3);
                                totalprice = reader.GetString(4);

                                DataItem item2 = new DataItem();
                                item2.SerialNoClmn = serialno;
                                item2.ItemNameClmn = itemname;
                                item2.PCSClmn = pcs;
                                item2.RetailPriceClmn = retailprice;
                                item2.TotalAmountPriceClmn = totalprice;

                                ShoppingCartTable.Items.Add(item2);

                            }
                        }


                        command = connection.CreateCommand();
                        command.CommandText = @"SELECT SUM(total_price) FROM shopping_cart WHERE EXISTS(SELECT total_price FROM shopping_cart);";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                if (reader.IsDBNull(0) == true)
                                {
                                    totalamount = 0;
                                    GrandTotal.Text = String.Format("{0:0.00}", totalamount);
                                }
                                else if (reader.IsDBNull(0) == false)
                                {

                                    string stringtotalamount = reader.GetString(0);

                                    totalamount = Convert.ToDouble(stringtotalamount);

                                    GrandTotal.Text = String.Format("{0:0.00}", totalamount);

                                }

                            }
                        }


                    }
                    else if (itemexist == 0)
                    {
                        MessageBox.Show("Item does not Exist", "Error");
                    }

                    connection.Close();
                }

                ItemBox.Text = "";
                Pcs.Text = "";

            }

        }

        private void NewTransactionBtnClick(object sender, RoutedEventArgs e)
        {
            NewTransactionDialogue.IsOpen = true;
        }

        private void OkNewTransactionConfirmationBtnClick(object sender, RoutedEventArgs e)
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
                command.CommandText = @"DELETE FROM shopping_cart;";

                command.ExecuteNonQuery();

                connection.Close();
            }

            ShoppingCartTable.Items.Clear();

            NewTransactionDialogue.IsOpen = false;

        }

        private void CancelNewTransactionConfirmationBtnClick(object sender, RoutedEventArgs e)
        {
            NewTransactionDialogue.IsOpen = false;
        }


        private void CheckoutCartClick(object sender, RoutedEventArgs e)
        {
            CheckoutDialogue.IsOpen = true;

        }

        private void OkCheckoutBtnClick(object sender, RoutedEventArgs e)
        {

            string payment = Payment.Text;
            string grandtotal = GrandTotal.Text;

            if (Validation("money", "Firstname", payment, 1, 100) != "Validated")
            {
                MessageBox.Show(Validation("money", "Payment", payment, 1, 100), "Error");

                Payment.BorderBrush = Brushes.Red;
            }
            else
            {

                double doublepayment = Convert.ToDouble(payment);
                double doublegrandtotal = Convert.ToDouble(grandtotal);

                if (doublepayment < doublegrandtotal)
                {

                    MessageBox.Show("Payment should be equal or greater than the Grand Total", "Error");

                    Payment.BorderBrush = Brushes.Red;
                }
                else
                {

                    CheckoutDialogue.IsOpen = false;

                    NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePages9Invoice.xaml", UriKind.Relative));

                    PointofSalePages9Invoice pointofsalepage9invoice = new PointofSalePages9Invoice(payment);

                    this.NavigationService.Navigate(pointofsalepage9invoice);

                }

            }

        }


        private void CancelCheckoutConfirmationBtnClick(object sender, RoutedEventArgs e)
        {
            CheckoutDialogue.IsOpen = false;

        }

        private void DeleteItemBtnClick(object sender, RoutedEventArgs e)
        {
            int currentRowIndex = ShoppingCartTable.Items.IndexOf(ShoppingCartTable.CurrentItem);

            TextBlock x = ShoppingCartTable.Columns[0].GetCellContent(ShoppingCartTable.Items[currentRowIndex]) as TextBlock;

            serialid = x.Text;

            DeleteItemDialogue.IsOpen = true;

        }

        private void OkItemDeleteConfirmationBtnClick(object sender, RoutedEventArgs e)
        {
            double totalamount;

            string id = serialid;

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
                command.CommandText = @"DELETE from shopping_cart where serial_no = $id;";
                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();

                ShoppingCartTable.Items.Clear();

                command.CommandText = @"SELECT serial_no, item_name, pcs, retail_price, total_price FROM shopping_cart;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string serialno = reader.GetString(0);
                        string itemname = reader.GetString(1);
                        string pcs = reader.GetString(2);
                        string retailprice = reader.GetString(3);
                        string totalprice = reader.GetString(4);

                        DataItem item = new DataItem();
                        item.SerialNoClmn = serialno;
                        item.ItemNameClmn = itemname;
                        item.PCSClmn = pcs;
                        item.RetailPriceClmn = retailprice;
                        item.TotalAmountPriceClmn = totalprice;

                        ShoppingCartTable.Items.Add(item);

                    }
                }

                command.CommandText = @"SELECT SUM(total_price) FROM shopping_cart WHERE EXISTS(SELECT total_price FROM shopping_cart);";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(0) == true)
                        {
                            totalamount = 0;
                            GrandTotal.Text = String.Format("{0:0.00}", totalamount);
                        }
                        else if (reader.IsDBNull(0) == false)
                        {

                            string stringtotalamount = reader.GetString(0);

                            totalamount = Convert.ToDouble(stringtotalamount);

                            GrandTotal.Text = String.Format("{0:0.00}", totalamount);
                        }

                    }
                }

                connection.Close();
            }


            DeleteItemDialogue.IsOpen = false;

        }

        private void CancelItemDeleteConfirmationBtnClick(object sender, RoutedEventArgs e)
        {
            DeleteItemDialogue.IsOpen = false;
        }

        //END 
    }
}
