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
using System.Xml.Linq;

namespace pw16c
{
    /// <summary>
    /// Interaction logic for GenresWindow.xaml
    /// </summary>
    ///  
    public partial class GenresWindow : Window
    {
        LibraryDBEntities context = new LibraryDBEntities();

        public GenresWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgGenres.ItemsSource = context.Genres.ToList();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = txtSearch.Text.ToLower();

            dgGenres.ItemsSource = context.Genres
                .Where(x => x.GenreName.ToLower().Contains(text))
                .ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new GenreAddEditWindow().ShowDialog();
            LoadData();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Genres genre = dgGenres.SelectedItem as Genres;

            if (genre == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            new GenreAddEditWindow(genre).ShowDialog();
            LoadData();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Genres genre = dgGenres.SelectedItem as Genres;

            if (genre == null)
            {
                MessageBox.Show("Выберите запись");
                return;
            }

            context.Genres.Remove(genre);
            context.SaveChanges();

            LoadData();
        }
    }


}
