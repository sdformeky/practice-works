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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pw16c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Readers_Click(object sender, RoutedEventArgs e)
        {
            new ReadersWindow().Show();
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            new BooksWindow().Show();
        }

        private void Genres_Click(object sender, RoutedEventArgs e)
        {
            new GenresWindow().Show();
        }

        private void Loans_Click(object sender, RoutedEventArgs e)
        {
            new LoansWindow().Show();
        }
    }
}
