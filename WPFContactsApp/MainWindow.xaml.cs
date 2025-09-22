using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFContactsApp.Classes;

namespace WPFContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            // ShowDialog forbids user from interacting with the main window unless the newContactWindow is closed.
            // Show allows user to interacting with the main window when the newContactWindow is opened.
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        void ReadDatabase()
        {
            // create and/ or open a SQLiteConnection to the database file.
            using (SQLite.SQLiteConnection connection = new(App.databasePath))
            {
                // This will be ignored if the table has been created.
                connection.CreateTable<Contact>();

                // Without ToList, contacts will only be a table query.
                // ToList converts the table query to a list of contact.
                var contacts = connection.Table<Contact>().ToList();
            }
        }
    }
}
