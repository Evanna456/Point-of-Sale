using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Windows.Documents;

namespace PointOfSale.PointofSalePages
{
    /// <summary>
    /// Interaction logic for PointofSalePages9Invoice.xaml
    /// </summary>
    public partial class PointofSalePages9Invoice : Page
    {

        string payment;

        public PointofSalePages9Invoice(string payment2)
        {
            payment = payment2;

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

            byte[] helpfile = Properties.Resources.POSHelp;

            if (File.Exists(baseDirectory + "/data/POSHelp.chm") == false)
            {
                File.WriteAllBytes(baseDirectory + "/data/POSHelp.chm", helpfile);

                System.Diagnostics.Process.Start(baseDirectory + "/data/POSHelp.chm");
            }
            else if (File.Exists(baseDirectory + "/data/POSHelp.chm") == true)
            {

                System.Diagnostics.Process.Start(baseDirectory + "/data/POSHelp.chm");

            }
            else
            {

                MessageBox.Show("Try Reinstalling the Application", "Error");
            }

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

        //OptionsMenuEnd

        //Loaded

        private async void Load(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => GetShoppingCartItems());
        }

        //EndLoaded

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void BackNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage5Transaction.xaml", UriKind.Relative));
        }

        //NavigationEnd



        public class DataItem
        {
            public string ItemNameClmn { get; set; }
            public string PCSClmn { get; set; }
            public string RetailPriceClmn { get; set; }
            public string TotalAmountPriceClmn { get; set; }
            public string ViewButtonClmn { get; set; }
        }

        public void GetShoppingCartItems()
        {
            this.Dispatcher.Invoke(() =>
            {

                double totalamount;
                string firstname = "", lastname = "", storename = "", address = "", contactno = "", vat = "", cashier = "";

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
                    command.CommandText = @"SELECT firstname, lastname,store_name, address, contact_no FROM users where ROWID = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            firstname = reader.GetString(0);
                            lastname = reader.GetString(1);
                            storename = reader.GetString(2);
                            address = reader.GetString(3);
                            contactno = reader.GetString(4);

                        }
                    }

                    Storename.Text = storename;
                    Address.Text = "Address: " + address;
                    ContactNo.Text = "Contact No. " + contactno;


                    DateTime localDate = DateTime.Now;
                    var culture = new CultureInfo("en-US");
                    string timestamp = localDate.ToString(culture);
                    TimeStamp.Text = "Timestamp: " + timestamp;

                    cashier = firstname + " " + lastname;

                    Cashier.Text = cashier;

                    command = connection.CreateCommand();
                    command.CommandText = @"SELECT vat FROM application_settings where ROWID = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vat = reader.GetString(0);

                        }
                    }

                    command = connection.CreateCommand();
                    command.CommandText = @"SELECT item_name, pcs, retail_price, total_price FROM shopping_cart;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemname = reader.GetString(0);
                            string pcs = reader.GetString(1);
                            string retailprice = reader.GetString(2);
                            string totalprice = reader.GetString(3);

                            DataItem item = new DataItem();
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
                                GrandTotal.Text = " Grand Total: " + totalamount;
                            }
                            else if (reader.IsDBNull(0) == false)
                            {
                                string vat2 = "1." + vat;
                                double doublevat2 = Convert.ToDouble(vat2);

                                string stringtotalamount = reader.GetString(0);

                                stringtotalamount = String.Format("{0:0.00}", stringtotalamount);

                                totalamount = Convert.ToDouble(stringtotalamount);

                                double doublepayment = Convert.ToDouble(payment);

                                double subtotalamount = totalamount / doublevat2;

                                double vatsubtotalamount = totalamount - subtotalamount;

                                double change = Math.Abs(totalamount - doublepayment);

                                SubTotal.Text = " Sub Total: " + String.Format("{0:0.00}", subtotalamount) + " php";

                                VatSubTotal.Text = " Plus " + vat + "% Vat: " + String.Format("{0:0.00}", vatsubtotalamount) + " php";

                                GrandTotal.Text = " TOTAL AMOUNT TO BE PAID : " + String.Format("{0:0.00}", totalamount) + " php";

                                Payment.Text = " Payment : " + payment + " php";

                                Change.Text = " Change: " + String.Format("{0:0.00}", change) + " php";
                            }

                        }
                    }


                    connection.Close();
                }

            });

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

            NewTransactionDialogue.IsOpen = false;

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage5Transaction.xaml", UriKind.Relative));

        }

        private void CancelNewTransactionConfirmationBtnClick(object sender, RoutedEventArgs e)
        {
            NewTransactionDialogue.IsOpen = false;
        }

        private void PrintBtnClick(object sender, RoutedEventArgs e)
        {

            PrintDocumentDialogue.IsOpen = true;
        }

        private void A4PrintConfirmationBtnClick(object sender, RoutedEventArgs e)
        {

            PrintDialog printDlg = new PrintDialog();

            PrintDocumentDialogue.IsOpen = false;

            Nullable<Boolean> print = printDlg.ShowDialog();

            if (print == true)
            {
                printDlg.PrintVisual(Document1, "Printing.");
            }

        }

        private void SmallPrintConfirmationBtnClick(object sender, RoutedEventArgs e)
        {

            double totalamount;
            string firstname = "", lastname = "", storename = "", address = "", contactno = "", vat = "", cashier = "", timestamp = "";

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
                command.CommandText = @"SELECT firstname, lastname,store_name, address, contact_no FROM users where ROWID = 1;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        firstname = reader.GetString(0);
                        lastname = reader.GetString(1);
                        storename = reader.GetString(2);
                        address = reader.GetString(3);
                        contactno = reader.GetString(4);

                    }
                }

                DateTime localDate = DateTime.Now;
                var culture = new CultureInfo("en-US");
                timestamp = localDate.ToString(culture);

                cashier = firstname + " " + lastname;

                command = connection.CreateCommand();
                command.CommandText = @"SELECT vat FROM application_settings where ROWID = 1;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vat = reader.GetString(0);

                    }
                }

                PrintDialog printDlg = new PrintDialog();
                FlowDocument doc = new FlowDocument();
                Section sec = new Section();
                Paragraph p1 = new Paragraph();
                Bold bld = new Bold();
                bld.Inlines.Add(new Run("Receipt"));
                p1.Inlines.Add(storename + "\n");
                p1.Inlines.Add("Address: " + address + "\n");
                p1.Inlines.Add("Contact No. " + contactno + "\n");
                p1.Inlines.Add("    Timestamp: " + "\n");
                p1.Inlines.Add(timestamp + "\n");
                p1.Inlines.Add("===============" + "\n");
                p1.Inlines.Add("    Item Description" + "\n");
                p1.Inlines.Add("===============" + "\n");

                command = connection.CreateCommand();
                command.CommandText = @"SELECT item_name, pcs, retail_price, total_price FROM shopping_cart;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string itemname = reader.GetString(0);
                        string pcs = reader.GetString(1);
                        string retailprice = reader.GetString(2);
                        string totalprice = reader.GetString(3);

                        p1.Inlines.Add(pcs + " PCS" + "\n");
                        p1.Inlines.Add(itemname + "\n");
                        p1.Inlines.Add("Retail Price: " + retailprice + " php" + "\n");
                        p1.Inlines.Add("Total Amount: " + totalprice + " php" + "\n");
                        p1.Inlines.Add(" " + "\n");
                    }
                }

                p1.Inlines.Add("===============" + "\n");

                command.CommandText = @"SELECT SUM(total_price) FROM shopping_cart WHERE EXISTS(SELECT total_price FROM shopping_cart);";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        if (reader.IsDBNull(0) == true)
                        {
                            totalamount = 0;

                            p1.Inlines.Add("" + "\n");

                            p1.Inlines.Add("===============" + "\n");

                        }
                        else if (reader.IsDBNull(0) == false)
                        {
                            string vat2 = "1." + vat;

                            double doublevat2 = Convert.ToDouble(vat2);

                            string stringtotalamount = reader.GetString(0);

                            stringtotalamount = String.Format("{0:0.00}", stringtotalamount);

                            totalamount = Convert.ToDouble(stringtotalamount);

                            double doublepayment = Convert.ToDouble(payment);

                            double subtotalamount = totalamount / doublevat2;

                            double vatsubtotalamount = totalamount - subtotalamount;

                            double change = Math.Abs(totalamount - doublepayment);

                            p1.Inlines.Add("Sub Total: " + String.Format("{0:0.00}", subtotalamount) + " php" + "\n");
                            p1.Inlines.Add("Plus " + vat + "% Vat: " + String.Format("{0:0.00}", vatsubtotalamount) + " php" + "\n");
                            p1.Inlines.Add("TOTAL AMOUNT TO" + "\n");
                            p1.Inlines.Add("BE PAID : " + String.Format("{0:0.00}", totalamount) + " php" + "\n");
                            p1.Inlines.Add("Payment : " + payment + " php" + "\n");
                            p1.Inlines.Add("Change: " + String.Format("{0:0.00}", change) + " php" + "\n");
                            p1.Inlines.Add("Cashier: " + cashier + "\n");
                            p1.Inlines.Add("===============");

                            doc.Name = "Receipt";
                            sec.Blocks.Add(p1);
                            doc.Blocks.Add(sec);

                            doc.Name = "FlowDoc";
                            IDocumentPaginatorSource idpSource = doc;

                            PrintDocumentDialogue.IsOpen = false;

                            Nullable<Boolean> print = printDlg.ShowDialog();

                            if (print == true)
                            {
                                printDlg.PrintDocument(idpSource.DocumentPaginator, "Printing");
                            }

                        }

                    }
                }

                connection.Close();
            }

        }

        private void CancelPrintConfirmationBtnClick(object sender, RoutedEventArgs e)
        {

            PrintDocumentDialogue.IsOpen = false;
        }


        //END
    }
}
