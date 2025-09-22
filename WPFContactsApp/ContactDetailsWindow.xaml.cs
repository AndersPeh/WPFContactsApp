using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFContactsApp.Classes;

namespace WPFContactsApp
{
    /// <summary>
    /// Interaction logic for ContactDetailsWindow.xaml
    /// </summary>
    public partial class ContactDetailsWindow : Window
    {
        Contact contact;

        public ContactDetailsWindow(Contact contact)
        {
            InitializeComponent();

            this.contact = contact;

            // Assign textbox texts to the current contact data.
            nameTextBox.Text = contact.Name;
            emailTextBox.Text = contact.Email;
            phoneTextBox.Text = contact.Phone;
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // As user may make changes to any of the textbox text, assign the textbox text to the current contact.
            // then update the current contact with text from edited textboxes.
            contact.Name = nameTextBox.Text;
            contact.Email = emailTextBox.Text;
            contact.Phone = phoneTextBox.Text;

            using (SQLite.SQLiteConnection connection = new(App.databasePath))
            {
                // SQLiteConnection will detect contact belongs to Contact table automatically and update contact to Contact table.

                connection.Update(contact);

                // using will close the connection automatically after this block ends.
            }

            Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (SQLite.SQLiteConnection connection = new(App.databasePath))
            {
                // SQLiteConnection will detect contact belongs to Contact table automatically and delete contact to Contact table.

                connection.Delete(contact);

                // using will close the connection automatically after this block ends.
            }

            Close();
        }
    }
}
