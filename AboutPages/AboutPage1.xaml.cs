using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace PointOfSale.AboutPages
{
    /// <summary>
    /// Interaction logic for AboutPage1.xaml
    /// </summary>
    public partial class AboutPage1 : Page
    {
        public AboutPage1()
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

        //Loaded

        private async void Load(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => GetLicenseRegistration());
        }

        //EndLoaded

        public void GetLicenseRegistration()
        {
            this.Dispatcher.Invoke(() =>
            {
                CheckDatabaseFile();

                string firstname, lastname, count = "";

                using (var connection = new SqliteConnection("Data Source=" + baseDirectory + "/data/lic.db"))
                {
                    connection.ConnectionString =
           new SqliteConnectionStringBuilder(connection.ConnectionString)
           { Password = passwordLIC }
               .ToString();

                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"PRAGMA journal_mode = WAL;PRAGMA synchronous = NORMAL;PRAGMA temp_store = MEMORY;";
                    command.ExecuteNonQuery();

                    command = connection.CreateCommand();
                    command.CommandText = @"SELECT COUNT(firstname) FROM license WHERE status = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            count = reader.GetString(0);

                        }

                    }


                    if (count == "1")
                    {

                        command = connection.CreateCommand();
                        command.CommandText = @"SELECT firstname,lastname FROM license WHERE status = 1;";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                firstname = reader.GetString(0);
                                lastname = reader.GetString(1);

                                LicenseInfo.Text = "Registered to: " + firstname + " " + lastname;

                            }

                        }


                    }
                    else
                    {

                        LicenseInfo.Text = "Unregistered";

                    }


                    connection.Close();
                }

            });

        }

        //END
    }
}
