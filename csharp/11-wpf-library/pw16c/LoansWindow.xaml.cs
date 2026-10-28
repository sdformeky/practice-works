using pw16c;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pw16c
{
    public partial class LoansWindow : Window
    {
        LibraryDBEntities context =
            new LibraryDBEntities();

        public LoansWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgLoans.ItemsSource =
                context.Loans.ToList();
        }

        private void txtSearch_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            string text =
                txtSearch.Text.ToLower();

            dgLoans.ItemsSource =
                context.Loans
                .Where(x =>
                    x.ReaderID.ToString().Contains(text)
                    ||
                    x.BookID.ToString().Contains(text))
                .ToList();
        }

        private void Add_Click(object sender,
            RoutedEventArgs e)
        {
            new LoansAddEditWindow()
                .ShowDialog();

            LoadData();
        }

        private void Edit_Click(object sender,
            RoutedEventArgs e)
        {
            Loans loan =
                dgLoans.SelectedItem as Loans;

            if (loan == null)
            {
                MessageBox.Show(
                    "Выберите запись");
                return;
            }

            new LoansAddEditWindow(loan)
                .ShowDialog();

            LoadData();
        }

        private void Delete_Click(object sender,
            RoutedEventArgs e)
        {
            Loans loan =
                dgLoans.SelectedItem as Loans;

            if (loan == null)
            {
                MessageBox.Show(
                    "Выберите запись");
                return;
            }

            context.Loans.Remove(loan);

            context.SaveChanges();

            LoadData();
        }
    }
}