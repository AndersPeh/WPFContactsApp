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
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactDetailsWindow newContactWindow = new NewContactDetailsWindow();
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
                contacts = connection
                    .Table<Contact>()
                    .OrderBy(connection => connection.Name)
                    .ToList();
            }

            if (contacts != null)
            {
                // For each contact, contactsListView has a ListViewItem that sets its content to the Contact instance.
                // As the content is not an UI element, WPF converts each Contact instance to text using ToString,
                // so the ToString method of Contact.cs is called.
                contactsListView.ItemsSource = contacts;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // searchTextBox will be assigned to whatever the user enters in the TextBox UI.
            TextBox searchTextBox = sender as TextBox;

            // Filter the contacts to those that contain what the user enters.
            var filteredList = contacts
                .Where(contact => contact.Name.ToLower().Contains(searchTextBox.Text.ToLower()))
                .ToList();

            contactsListView.ItemsSource = filteredList;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = contactsListView.SelectedItem as Contact;

            if (selectedContact != null)
            {
                ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(
                    selectedContact
                );

                contactDetailsWindow.ShowDialog();

                ReadDatabase();
            }
        }
    }
}
