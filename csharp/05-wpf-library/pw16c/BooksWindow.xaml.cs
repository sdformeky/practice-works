using pw16c;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pw16c
{
    public partial class BooksWindow : Window
    {
        LibraryDBEntities context =
            new LibraryDBEntities();

        public BooksWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgBooks.ItemsSource =
                context.Books.ToList();
        }

        private void txtSearch_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            string text =
                txtSearch.Text.ToLower();

            dgBooks.ItemsSource =
                context.Books
                .Where(x =>
                    x.Title.ToLower().Contains(text)
                    ||
                    x.Author.ToLower().Contains(text))
                .ToList();
        }

        private void Add_Click(object sender,
            RoutedEventArgs e)
        {
            new BookAddEditWindow()
                .ShowDialog();

            LoadData();
        }

        private void Edit_Click(object sender,
            RoutedEventArgs e)
        {
            Books book =
                dgBooks.SelectedItem as Books;

            if (book == null)
            {
                MessageBox.Show(
                    "Выберите запись");
                return;
            }

            new BookAddEditWindow(book)
                .ShowDialog();

            LoadData();
        }

        private void Delete_Click(object sender,
            RoutedEventArgs e)
        {
            Books book =
                dgBooks.SelectedItem as Books;

            if (book == null)
            {
                MessageBox.Show(
                    "Выберите запись");
                return;
            }

            context.Books.Remove(book);

            context.SaveChanges();

            LoadData();
        }
    }
}