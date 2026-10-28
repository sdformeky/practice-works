using pw16cs;
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

namespace pw16cs
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WpfShopDbEntities context = new WpfShopDbEntities();

        public MainWindow()
        {
            InitializeComponent();

            dgCategories.ItemsSource = context.categories.ToList();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                categories category = new categories();
                category.title = txtTitle.Text.Trim();
                context.categories.Add(category);
                context.SaveChanges();
                dgCategories.ItemsSource = context.categories.ToList();

                txtTitle.Text = null;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                if (!context.categories.Any(c => c.title == txtTitle.Text.Trim()) {
                    categories category = new categories();
                    category.title = txtTitle.Text.Trim();
                    context.categories.Add(category);
                    context.SaveChanges();
                    dgCategories.ItemsSource = context.categories.ToList();

                    txtTitle.Text = null;
                }
                
            }
        }
    }
}