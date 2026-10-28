using pw16c;
using System.Linq;
using System.Windows;

namespace pw16c
{
    public partial class GenreAddEditWindow : Window
    {
        LibraryDBEntities context =
            new LibraryDBEntities();

        Genres currentGenre;

        public GenreAddEditWindow()
        {
            InitializeComponent();
        }

        public GenreAddEditWindow(Genres genre)
        {
            InitializeComponent();

            currentGenre =
                context.Genres.First(
                    x => x.GenreID == genre.GenreID);

            txtGenreName.Text =
                currentGenre.GenreName;
        }

        private void Save_Click(object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtGenreName.Text))
            {
                MessageBox.Show(
                    "Введите название жанра");

                return;
            }

            if (currentGenre == null)
            {
                Genres genre = new Genres();

                genre.GenreName =
                    txtGenreName.Text.Trim();

                context.Genres.Add(genre);
            }
            else
            {
                currentGenre.GenreName =
                    txtGenreName.Text.Trim();
            }

            context.SaveChanges();

            Close();
        }
    }
}