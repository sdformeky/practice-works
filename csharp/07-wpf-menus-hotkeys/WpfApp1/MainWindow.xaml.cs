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
using static System.Collections.Specialized.BitVector32;


namespace WpfApp35
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        String[] history = new string[5];
        int count = 0;
        String action = "";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Box.Background = Brushes.Red;
            action = "Сменил цвет на красный";
            HistoryAddition(action);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Box.Background = Brushes.Green;
            action = "Сменил цвет на зеленый";
            HistoryAddition(action);
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Box.Background = Brushes.Blue;
            action = "Сменил цвет на синий";
            HistoryAddition(action);
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Box.BorderBrush = Brushes.Red;
            action = "Сменил цвет границы на красный";
            HistoryAddition(action);
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Box.BorderBrush = Brushes.Green;
            action = "Сменил цвет границы на зеленый";
            HistoryAddition(action);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Box.BorderBrush = Brushes.Blue;
            action = "Сменил цвет границы на синий";
            HistoryAddition(action);
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Box.BorderThickness = new Thickness(5);
            action = "Увеличил границу до 5px";
            HistoryAddition(action);
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            Box.BorderThickness = new Thickness(10);
            action = "Увеличил границу до 10px";
            HistoryAddition(action);
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            Box.BorderThickness = new Thickness(15);
            action = "Увеличил границу до 15px";
            HistoryAddition(action);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.G)
            {
                Box.Background = Brushes.Green;
                action = "Сменил цвет на зеленый";
                HistoryAddition(action);
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.R)
            {
                Box.Background = Brushes.Red;
                action = "Сменил цвет на красный";
                HistoryAddition(action);
            }

            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B)
            {
                Box.Background = Brushes.Blue;
                action = "Сменил цвет на синий";
                HistoryAddition(action);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Привет!", "Заголовок", MessageBoxButton.YesNoCancel, MessageBoxImage.Error);

            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("ДА");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Точно закрываемся?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        void ShowHistory(string[] history)
        {
            HistoryMenu.Items.Clear();

            for (int i = 0; i < count; i++)
            {
                HistoryMenu.Items.Add(new MenuItem
                {
                    Header = history[i]
                });
            }
        }

        private void MenuItem_HistoryClear(object sender, RoutedEventArgs e)
        {
            var clear = MessageBox.Show("Удалить историю?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (clear == MessageBoxResult.Yes) { history = new string[5]; count = 0; }
            ShowHistory(history);
        }

        void HistoryAddition(string action)
        {
            if (count < history.Length) { 
                history[count] = action;
                count++;
            }
            else
            {
                count = 0;
                history[count] = action;
                count++;
            }
            ShowHistory(history);
        }
    }
}
