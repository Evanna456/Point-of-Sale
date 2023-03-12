using System;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace PointOfSale.PointofSalePages
{
    /// <summary>
    /// Interaction logic for PointofSalePage1.xaml
    /// </summary>
    public partial class PointofSalePage1 : Page
    {

        public PointofSalePage1()
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

        //Loaded

        private async void Load(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => GetUserAuth());
        }

        //EndLoaded

        //Navigation

        private void ProfileNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage6Profile.xaml", UriKind.Relative));
        }

        private void InventoryPageNavigate(object sender, RoutedEventArgs e)
        {
            var myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.2;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = new RepeatBehavior(1.0);

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);

            Storyboard.SetTargetName(myDoubleAnimation, InventoryPage.Name);

            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Card.OpacityProperty));

            myStoryboard.Begin(this);

            TransactionPage.IsEnabled = false;
            InventoryPage.IsEnabled = false;
            UserFullName.IsEnabled = false;
            ProfileIcon.IsEnabled = false;

            Task.Delay(500).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage2Inventory.xaml", UriKind.Relative));
                });

            });

        }

        private void TransactionPageNavigate(object sender, RoutedEventArgs e)
        {

            var myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 1.0;
            myDoubleAnimation.To = 0.2;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            myDoubleAnimation.AutoReverse = true;
            myDoubleAnimation.RepeatBehavior = new RepeatBehavior(1.0);

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);

            Storyboard.SetTargetName(myDoubleAnimation, TransactionPage.Name);

            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Card.OpacityProperty));

            myStoryboard.Begin(this);

            TransactionPage.IsEnabled = false;
            InventoryPage.IsEnabled = false;
            UserFullName.IsEnabled = false;
            ProfileIcon.IsEnabled = false;

            Task.Delay(500).ContinueWith(_ =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage5Transaction.xaml", UriKind.Relative));
                });

            });

        }

        //NavigationEnd

        public void GetUserAuth()
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
                    command.CommandText = @"SELECT firstname,lastname FROM users;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstname = reader.GetString(0);
                            string lastname = reader.GetString(1);

                            UserFullName.Text = firstname + " " + lastname;

                        }

                    }
                    connection.Close();
                }

            });
        }

        //END
    }
}
