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
    /// Interaction logic for BookAddEditWindow.xaml
    /// </summary>
    public partial class BookAddEditWindow : Window
    {
        LibraryDBEntities context = new LibraryDBEntities();

        Books currentBook;

        public BookAddEditWindow()
        {
            InitializeComponent();

            cbGenre.ItemsSource =
                context.Genres.ToList();
        }

        public BookAddEditWindow(Books book)
        {
            InitializeComponent();

            cbGenre.ItemsSource =
                context.Genres.ToList();

            currentBook = context.Books
                .First(x => x.BookID == book.BookID);

            txtTitle.Text = currentBook.Title;
            txtAuthor.Text = currentBook.Author;

            cbGenre.SelectedValue =
                currentBook.GenreID;
        }

        private void Save_Click(object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Введите название");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Введите автора");
                return;
            }

            if (cbGenre.SelectedItem == null)
            {
                MessageBox.Show("Выберите жанр");
                return;
            }

            if (currentBook == null)
            {
                Books book = new Books();

                book.Title = txtTitle.Text;
                book.Author = txtAuthor.Text;
                book.GenreID = (int)cbGenre.SelectedValue;

                context.Books.Add(book);
            }
            else
            {
                currentBook.Title = txtTitle.Text;
                currentBook.Author = txtAuthor.Text;
                currentBook.GenreID = (int)cbGenre.SelectedValue;
            }

            context.SaveChanges();
            Close();
        }
    }
}
