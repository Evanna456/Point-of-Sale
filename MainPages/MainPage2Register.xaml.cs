using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Bcr = BCrypt.Net;

namespace PointOfSale.MainPages
{
    /// <summary>
    /// Interaction logic for MainPage2Register.xaml
    /// </summary>
    public partial class MainPage2Register : Page
    {

        public MainPage2Register()
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

        private void RegisterBtnClick(object sender, RoutedEventArgs e)
        {

            string firstname, lastname, storename, address, contactno, activationkey, username, password, repassword, hashedpassword, count = "0";

            firstname = FirstName.Text;
            lastname = LastName.Text;
            storename = StoreName.Text;
            address = Address.Text;
            contactno = ContactNo.Text;
            activationkey = ActivationKey.Text;
            username = Username.Text;
            password = Password.Password.ToString();
            repassword = RePassword.Password.ToString();


            if (Validation("variable", "Activation Key", activationkey, 1, 200) != "Validated")
            {
                MessageBox.Show(Validation("variable", "Serial Key", activationkey, 1, 200), "Error");

                FirstName.BorderBrush = Brushes.Red;
            }
            else
            {

                using (var connection = new SqliteConnection("Data Source=" + baseDirectory + "/data/lic.db"))
                {

                    connection.ConnectionString =
        new SqliteConnectionStringBuilder(connection.ConnectionString)
        { Password = passwordLIC }
            .ToString();

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                    command.CommandText = @"SELECT COUNT(activation_key) FROM license WHERE activation_key = $activationkey";
                    command.Parameters.AddWithValue("$activationkey", activationkey);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            count = reader.GetString(0);

                        }
                    }
                    connection.Close();
                }

            }

            if (count == "1")
            {


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
                else if (Validation("variable", "Username", username, 1, 200) != "Validated")
                {
                    MessageBox.Show(Validation("variable", "Username", username, 1, 200), "Error");

                    Username.BorderBrush = Brushes.Red;
                }
                else if (Validation("alphanum", "Password", password, 6, 200) != "Validated")
                {
                    MessageBox.Show(Validation("variable", "Password", password, 6, 200), "Error");

                    Password.BorderBrush = Brushes.Red;
                }
                else if (password != repassword)
                {
                    MessageBox.Show("Password does not match ", "Error");

                    Password.BorderBrush = Brushes.Red;
                    RePassword.BorderBrush = Brushes.Red;
                }
                else
                {

                    hashedpassword = Bcr.BCrypt.HashPassword(password);

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
                        command.CommandText = @"INSERT INTO users (firstname, lastname, store_name, address, contact_no, username, password) VALUES($firstname, $lastname, $storename, $address, $contactno, $username, $password);";
                        command.Parameters.AddWithValue("$firstname", firstname);
                        command.Parameters.AddWithValue("$lastname", lastname);
                        command.Parameters.AddWithValue("$storename", storename);
                        command.Parameters.AddWithValue("$address", address);
                        command.Parameters.AddWithValue("$contactno", contactno);
                        command.Parameters.AddWithValue("$username", username);
                        command.Parameters.AddWithValue("$password", hashedpassword);

                        command.ExecuteNonQuery();

                        connection.Close();
                    }

                    using (var connection = new SqliteConnection("Data Source=" + baseDirectory + "/data/lic.db"))
                    {

                        connection.ConnectionString =
            new SqliteConnectionStringBuilder(connection.ConnectionString)
            { Password = passwordLIC }
                .ToString();

                        connection.Open();

                        var command = connection.CreateCommand();
                        command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                        command.CommandText = @"UPDATE license SET status = 1 WHERE activation_key = $activationkey;";
                        command.Parameters.AddWithValue("$activationkey", activationkey);

                        command.ExecuteNonQuery();

                        connection.Close();
                    }

                    NavigationService.Navigate(new Uri("MainPages/MainPage1.xaml", UriKind.Relative));

                    MessageBox.Show("Registered Successfully", "Success");

                }

            }
            else if (count == "0")
            {
                MessageBox.Show("Invalid Activation Key", "Error");

                ActivationKey.BorderBrush = Brushes.Red;
            }

        }

        //end
    }
}
