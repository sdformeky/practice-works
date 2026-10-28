using System;
using System.Globalization; // работа с форматами чисел
using System.Text.RegularExpressions; // регулярные выражения
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace pwc14
{
    public partial class MainWindow : Window
    {
        // Первый операнд
        double a = 0;
        // Текущая операция (+, -, *, /)
        string op = "";
        // Флаг нового ввода (нужно ли очищать поле)
        bool newInput = true;
        // Текущая система счисления
        string system = "DEC";
        public MainWindow()
        {
            InitializeComponent(); // загрузка XAML и привязка элементов
        }
        // Фильтрация ввода символов
        private void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Разрешены только цифры, точка и минус
            e.Handled = !Regex.IsMatch(e.Text, "[0-9\\.-]");
        }
        // Обработка Enter
        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Equal_Click(sender, e);
                    e.Handled = true; // предотвратить звуковой сигнал
                    break;
                case Key.OemPlus:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        Equal_Click(sender, e);
                        e.Handled = true;
                    }
                    else if (Keyboard.Modifiers == ModifierKeys.Shift)
                    {
                        Op_Click(new Button { Content = "+" }, e);
                        e.Handled = true;
                    }
                    break;
                case Key.Back:
                    Back_Click(sender, e);
                    e.Handled = true;
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                    Dot_Click(sender, e);
                    e.Handled = true;
                    break;
                case Key.OemMinus:
                    Op_Click(new Button { Content = "-" }, e);
                    e.Handled = true;
                    break;
                case Key.Multiply:
                    Op_Click(new Button { Content = "*" }, e);
                    e.Handled = true;
                    break;
                case Key.Divide:
                    Op_Click(new Button { Content = "/" }, e);
                    e.Handled = true;
                    break;
            }
        }

        // Получение числа из TextBox
        double GetVal()
        {
            double v;
            // Замена запятой на точку + безопасный парсинг
            double.TryParse(InputBox.Text.Replace(',', '.'),

            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out v);
            return v;
        }
        // Установка значения в TextBox
        void SetVal(double v)
        {
            // формат с 2 знаками после запятой
            InputBox.Text = v.ToString("F2", CultureInfo.InvariantCulture);
        }
        // Добавление символа
        void Add(string t)
        {
            // если новый ввод или 0 → заменить
            if (newInput || InputBox.Text == "0")
            {
                InputBox.Text = t;
                newInput = false;
            }
            else
                InputBox.Text += t; // иначе дописать
        }
        // Обработка цифр
        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            Add(((Button)sender).Content.ToString());
        }
        // Добавление точки
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            // только одна точка в числе
            if (!InputBox.Text.Contains("."))
                Add(".");
        }
        // Выбор операции
        private void Op_Click(object sender, RoutedEventArgs e)
        {
            a = GetVal(); // сохранить первое число
            op = ((Button)sender).Content.ToString(); // сохранить операцию
            InputBox.Text = "";
            newInput = true; // следующий ввод — новое число
        }
        // Вычисление
        private void Equal_Click(object sender, RoutedEventArgs e)
                
        {
            double b = GetVal(); // второе число
            double r = 0;
            try
            {
                switch (op)
                {
                    case "+": r = a + b; break;
                    case "-": r = a - b; break;
                    case "*": r = a * b; break;
                    case "/":
                        if (b == 0) throw new Exception("Деление на 0");
                        r = a / b;
                        break;
                }
                SetVal(r); // вывод результата
                ConvertSystem(r); // перевод в систему счисления
                newInput = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // вывод ошибки
            }
        }
        // Очистка
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Text = "0";
        }
        // Удаление последнего символа
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text.Length > 1)
                InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1);
            else
                InputBox.Text = "0";
        }
        // Смена знака
        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            SetVal(-GetVal());
        }
        // Квадратный корень
        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {

            double v = GetVal();
            if (v < 0)
            {
                MessageBox.Show("Ошибка"); // нельзя извлечь корень
                return;
            }
            SetVal(Math.Sqrt(v));
        }
        // Смена системы счисления
        private void System_Checked(object sender, RoutedEventArgs e)
        {
            system = ((RadioButton)sender).Content.ToString();
            ConvertSystem(GetVal());
        }
        // Преобразование числа
        void ConvertSystem(double v)
        {
            int n = (int)Math.Round(v); // только целые
            if (system == "BIN")
                InputBox.Text = Convert.ToString(n, 2);
            else if (system == "HEX")
                InputBox.Text = Convert.ToString(n, 16).ToUpper();
            else
                SetVal(v);
        }
        // Изменение цвета
        private void Color_Checked(object sender, RoutedEventArgs e)
        {
            string c = ((RadioButton)sender).Content.ToString();
            if (c == "Белый") InputBox.Background = Brushes.White;
            if (c == "Серый") InputBox.Background = Brushes.LightGray;
            if (c == "Жёлтый") InputBox.Background = Brushes.LightYellow;
            if (c == "Зелёный") InputBox.Background = Brushes.LightGreen;
        }
        // Изменение размера шрифта
        private void Font_Checked(object sender, RoutedEventArgs e)
        {
            int size = int.Parse(((RadioButton)sender).Tag.ToString());
            InputBox.FontSize = size;
        }
        // Изменение стиля
        private void Style_Changed(object sender, RoutedEventArgs e)

        {
            bool bold = false;
            bool italic = false;
            var panel = ((CheckBox)sender).Parent as StackPanel;
            foreach (var item in panel.Children)
            {
                if (item is CheckBox cb)
                {
                    if (cb.Content.ToString() == "Жирный" && cb.IsChecked == true)
                        bold = true;
                    if (cb.Content.ToString() == "Курсив" && cb.IsChecked == true)
                        italic = true;
                }
            }
            // тернарный оператор
            InputBox.FontWeight = bold ? FontWeights.Bold : FontWeights.Normal;
            InputBox.FontStyle = italic ? FontStyles.Italic : FontStyles.Normal;
        }
        // Переключение темы
        private void Theme_Toggled(object sender, RoutedEventArgs e)
        {
            if (DarkMode.IsChecked == true)
            {
                Background = Brushes.Black;
                InputBox.Foreground = Brushes.White;
            }
            else
            {
                Background = Brushes.White;
                InputBox.Foreground = Brushes.Black;
            }
        }
        // Выравнивание текста
        private void Align_Changed(object sender, SelectionChangedEventArgs e)
        {
            string val = ((ComboBoxItem)AlignBox.SelectedItem).Content.ToString();
            if (val == "Лево") InputBox.TextAlignment = TextAlignment.Left;
            if (val == "Центр") InputBox.TextAlignment = TextAlignment.Center;
            if (val == "Право") InputBox.TextAlignment = TextAlignment.Right;
        }
    }
}