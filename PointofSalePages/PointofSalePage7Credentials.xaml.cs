using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Bcr = BCrypt.Net;

namespace PointOfSale.PointofSalePages
{
    /// <summary>
    /// Interaction logic for PointofSalePage7Credentials.xaml
    /// </summary>
    public partial class PointofSalePage7Credentials : Page
    {
        public PointofSalePage7Credentials()
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

        //EndLoaded

        //Navigation

        private void HomeNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage1.xaml", UriKind.Relative));
        }

        private void ChangeProfileNavigate(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("PointofSalePages/PointofSalePage6Profile.xaml", UriKind.Relative));
        }

        //NavigationEnd


        private void SaveCredentialsBtnClick(object sender, RoutedEventArgs e)
        {
            string oldpassword, newpassword, repassword, password = "", hashedpassword;

            oldpassword = OldPassword.Password.ToString();
            newpassword = NewPassword.Password.ToString();
            repassword = RePassword.Password.ToString();

            if (Validation("alphanum", "Old Password", oldpassword, 6, 200) != "Validated")
            {
                MessageBox.Show(Validation("alphanum", "Old Password", oldpassword, 6, 200), "Error");

                OldPassword.BorderBrush = Brushes.Red;
            }
            else if (Validation("alphanum", "New Password", newpassword, 6, 200) != "Validated")
            {
                MessageBox.Show(Validation("alphanum", "New Password", newpassword, 6, 200), "Error");

                NewPassword.BorderBrush = Brushes.Red;
            }
            else if (Validation("alphanum", "Re-Typed Password", repassword, 6, 200) != "Validated")
            {
                MessageBox.Show(Validation("alphanum", "Re-Typed Password", repassword, 6, 200), "Error");

                NewPassword.BorderBrush = Brushes.Red;
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
                    command.CommandText = @"SELECT password FROM users WHERE ROWID = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            password = reader.GetString(0);

                        }

                    }
                    connection.Close();
                }

                bool passwordstatus = Bcr.BCrypt.Verify(oldpassword, password);

                if (passwordstatus == true)
                {

                    if (newpassword == repassword)
                    {

                        hashedpassword = Bcr.BCrypt.HashPassword(newpassword);

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
                            command.CommandText = @"UPDATE users SET password = $password WHERE ROWID = 1;";
                            command.Parameters.AddWithValue("$password", hashedpassword);

                            command.ExecuteNonQuery();

                            connection.Close();
                        }

                        OldPassword.Password = "";
                        NewPassword.Password = "";
                        RePassword.Password = "";

                        MessageBox.Show("Password Updated", "Success");

                    }
                    else if (newpassword != repassword)
                    {

                        MessageBox.Show("Re-Typed Password Doesnt Match", "Error");
                    }

                }
                else if (passwordstatus == false)
                {

                    MessageBox.Show("Invalid Old Password", "Error");
                }

            }

        }

        //END
    }
}
