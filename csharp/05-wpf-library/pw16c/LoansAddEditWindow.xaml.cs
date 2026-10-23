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
    /// Interaction logic for LoansAddEditWindow.xaml
    /// </summary>
    public partial class LoansAddEditWindow : Window
    {
        LibraryDBEntities context = new LibraryDBEntities();

        Loans currentLoan;

        public LoansAddEditWindow()
        {
            InitializeComponent();

            cbReader.ItemsSource =
                context.Readers.ToList();

            cbBook.ItemsSource =
                context.Books.ToList();
        }

        public LoansAddEditWindow(Loans loan)
            : this()
        {
            if (loan != null)
            {
                currentLoan = loan;
                cbReader.SelectedValue = loan.ReaderID;
                cbBook.SelectedValue = loan.BookID;
                dpIssue.SelectedDate = loan.IssueDate;
                dpReturn.SelectedDate = loan.ReturnDate;
            }
        }

        private void Save_Click(object sender,
            RoutedEventArgs e)
        {
            if (cbReader.SelectedItem == null)
            {
                MessageBox.Show("Выберите читателя");
                return;
            }

            if (cbBook.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу");
                return;
            }

            Loans loan = new Loans();

            loan.ReaderID =
                (int)cbReader.SelectedValue;

            loan.BookID =
                (int)cbBook.SelectedValue;

            loan.IssueDate =
                dpIssue.SelectedDate.Value;

            loan.ReturnDate =
                dpReturn.SelectedDate;

            context.Loans.Add(loan);

            context.SaveChanges();

            Close();
        }
    }
}
