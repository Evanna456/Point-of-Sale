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
    /// Interaction logic for PointofSalePage6Profile.xaml
    /// </summary>
    public partial class PointofSalePage6Profile : Page
    {
        public PointofSalePage6Profile()
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
            await Task.Run(() => GetAuthProfile());
        }

        //EndLoaded

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void ChangePasswordNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage7Credentials.xaml", UriKind.Relative));
        }

        //EndNavigation

        public void GetAuthProfile()
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
                    command.CommandText = @"SELECT firstname,lastname, username,store_name, address, contact_no FROM users where ROWID = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstname = reader.GetString(0);
                            string lastname = reader.GetString(1);
                            string username = reader.GetString(2);
                            string storename = reader.GetString(3);
                            string address = reader.GetString(4);
                            string contactno = reader.GetString(5);

                            FirstName.Text = firstname;
                            LastName.Text = lastname;
                            Username.Text = username;
                            StoreName.Text = storename;
                            Address.Text = address;
                            ContactNo.Text = contactno;

                        }

                    }
                    connection.Close();
                }

            });

        }

        private void SaveBasicInfoBtnClick(object sender, RoutedEventArgs e)
        {
            string firstname, lastname, username, storename, address, contactno;

            firstname = FirstName.Text;
            lastname = LastName.Text;
            username = Username.Text;
            storename = StoreName.Text;
            address = Address.Text;
            contactno = ContactNo.Text;

            if (Validation("variable", "Firstname", firstname, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Firstname", firstname, 1, 200), "Error");

                FirstName.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Lastname", lastname, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Lastname", lastname, 1, 200), "Error");

                LastName.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Username", username, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Username", username, 1, 200), "Error");

                LastName.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Store Name", storename, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Store Name", storename, 1, 200), "Error");

                StoreName.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Address", address, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Address", address, 1, 200), "Error");

                Address.BorderBrush = Brushes.Red;
            }
            else if (Validation("variable", "Contact No.", contactno, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Contact No.", contactno, 1, 200), "Error");

                ContactNo.BorderBrush = Brushes.Red;
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
                    command.CommandText = @"UPDATE users SET firstname = $firstname, lastname = $lastname, username = $username, store_name =$storename, address =$address, contact_no = $contactno WHERE ROWID = 1;";
                    command.Parameters.AddWithValue("$firstname", firstname);
                    command.Parameters.AddWithValue("$lastname", lastname);
                    command.Parameters.AddWithValue("$username", username);
                    command.Parameters.AddWithValue("$storename", storename);
                    command.Parameters.AddWithValue("$address", address);
                    command.Parameters.AddWithValue("$contactno", contactno);

                    command.ExecuteNonQuery();

                    connection.Close();
                }

                MessageBox.Show("Profile Updated Successfully", "Success");

            }

        }

        //End
    }
}
