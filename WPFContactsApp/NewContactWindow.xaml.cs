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
using SQLite;
using WPFContactsApp.Classes;

namespace WPFContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
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
            string databaseName = "Contacts.db";
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string databasePath = System.IO.Path.Combine(folderPath, databaseName);

            // using disposes SQLiteConnection after the execution leaves the block of code in {}.
            // After connection.Insert(contact), SQLiteConnection will be disposed.
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                // This will be ignored if the table has been created.
                connection.CreateTable<Contact>();

                // SQLiteConnection will detect contact belongs to Contact table automatically and insert contact to Contact table.
                connection.Insert(contact);
            }

            Close();
        }
    }
}
