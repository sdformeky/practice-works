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

namespace pw16c
{
    /// <summary>
    /// Interaction logic for ReadersWindow.xaml
    /// </summary>
    public partial class ReadersWindow : Window
    {
        LibraryDBEntities context = new LibraryDBEntities();

        public ReadersWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgReaders.ItemsSource =
                context.Readers.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ReaderAddEditWindow window =
                new ReaderAddEditWindow();

            window.ShowDialog();

            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Readers reader =
                dgReaders.SelectedItem as Readers;

            if (reader == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            ReaderAddEditWindow window =
                new ReaderAddEditWindow(reader);

            window.ShowDialog();

            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Readers reader =
                dgReaders.SelectedItem as Readers;

            if (reader == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            var loans = context.Loans.Where(l => l.ReaderID == reader.ReaderID).ToList();
            if (loans.Any() && MessageBox.Show("Delete reader and their loans?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var tx = context.Database.BeginTransaction())
                {
                    context.Loans.RemoveRange(loans);
                    context.Readers.Remove(reader);
                    context.SaveChanges();
                    tx.Commit();
                }
            }

            LoadData();
        }

        private void txtSearch_TextChanged(
            object sender,
            TextChangedEventArgs e)
        {
            string text =
                txtSearch.Text.ToLower();

            dgReaders.ItemsSource =
                context.Readers
                .Where(x =>
                    x.FullName.ToLower().Contains(text)
                    ||
                    x.Phone.Contains(text))
                .ToList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
