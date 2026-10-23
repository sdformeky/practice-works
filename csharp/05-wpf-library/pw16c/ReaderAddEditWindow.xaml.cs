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
    /// Interaction logic for ReaderEditWindow.xaml
    /// </summary>
    public partial class ReaderAddEditWindow : Window
    {
        LibraryDBEntities context =
            new LibraryDBEntities();

        Readers currentReader;

        public ReaderAddEditWindow()
        {
            InitializeComponent();
        }

        public ReaderAddEditWindow(
            Readers reader)
        {
            InitializeComponent();

            currentReader =
                context.Readers
                .First(x =>
                    x.ReaderID ==
                    reader.ReaderID);

            txtName.Text =
                currentReader.FullName;

            txtPhone.Text =
                currentReader.Phone;

            txtAddress.Text =
                currentReader.Address;
        }

        private void Save_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtName.Text))
            {
                MessageBox.Show(
                    "Введите ФИО");

                return;
            }

            if (txtPhone.Text.Length < 5)
            {
                MessageBox.Show(
                    "Телефон указан неверно");

                return;
            }

            if (currentReader == null)
            {
                Readers reader =
                    new Readers();

                reader.FullName =
                    txtName.Text;

                reader.Phone =
                    txtPhone.Text;

                reader.Address =
                    txtAddress.Text;

                context.Readers.Add(
                    reader);
            }
            else
            {
                currentReader.FullName =
                    txtName.Text;

                currentReader.Phone =
                    txtPhone.Text;

                currentReader.Address =
                    txtAddress.Text;
            }

            context.SaveChanges();

            Close();
        }
    }
}
