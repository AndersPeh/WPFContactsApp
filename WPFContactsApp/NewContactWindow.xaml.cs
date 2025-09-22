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
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactDetailsWindow : Window
    {
        public NewContactDetailsWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text,
            };

            // using disposes SQLiteConnection after the execution leaves the block of code in {}.
            // After connection.Insert(contact), SQLiteConnection will be disposed.
            using (SQLite.SQLiteConnection connection = new(App.databasePath))
            {
                // SQLiteConnection will detect contact belongs to Contact table automatically and insert contact to Contact table.
                connection.Insert(contact);

                // using will close the connection automatically after this block ends.
            }

            Close();
        }
    }
}
