using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcWpf
{
    public partial class MainWindow : Window
    {
        private double _firstNumber = 0;
        private string _currentOperation = "";
        private bool _newInput = true;
        private string _currentSystem = "DEC";
        private int _precision = 2;      // кол-во знаков после запятой
        private bool _useScientific = false; // научный формат

        public MainWindow()
        {
            InitializeComponent();
        }

        // Фильтр ввода
        private void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9\.-]");
        }

       
        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Equal_Click(sender, e);
        }

        // Получить число из поля
        private double GetValue()
        {
            double.TryParse(InputBox.Text.Replace(',', '.'),
                NumberStyles.Any, CultureInfo.InvariantCulture, out double v);
            return v;
        }

        
        private void SetValue(double value)
        {
            string formatted;
            if (_useScientific)
            {
                formatted = value.ToString($"E{_precision}", CultureInfo.InvariantCulture);
            }
            else
            {
                formatted = value.ToString($"F{_precision}", CultureInfo.InvariantCulture);
            }
            InputBox.Text = formatted;
        }

        
        private void AddChar(string ch)
        {
            if (_newInput || InputBox.Text =="0")
            {
                InputBox.Text = ch;
                _newInput = false;
            }
            else
                InputBox.Text += ch;
        }

        // Цифры
        private void Digit_Click(object sender, RoutedEventArgs e)
            => AddChar(((Button)sender).Content.ToString());

      
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!InputBox.Text.Contains("."))
                AddChar(".");
        }

        // Операция
        private void Op_Click(object sender, RoutedEventArgs e)
        {
            _firstNumber = GetValue();
            _currentOperation = ((Button)sender).Content.ToString();
            _newInput = true;
        }

        // Равно
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double second = GetValue();
            double result = 0;

            try
            {
                switch (_currentOperation)
                {
                    case "+": result = _firstNumber + second; break;
                    case "-": result = _firstNumber - second; break;
                    case "*": result = _firstNumber * second; break;
                    case "/":
                        if (second == 0) throw new DivideByZeroException();
                        result = _firstNumber / second;
                        break;
                    default: result = second; break;
                }
                SetValue(result);
                ConvertSystem(result);
                _newInput = true;
            }
            catch
            {
                MessageBox.Show("Ошибка", "Калькулятор", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Очистка
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Text = "0";
            _firstNumber = 0;
            _currentOperation = "";
            _newInput = true;
        }

        // Backspace
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text.Length > 1)
                InputBox.Text = InputBox.Text.Substring(0, InputBox.Text.Length - 1);
            else
                InputBox.Text = "0";
        }

        // Смена знака
        private void Sign_Click(object sender, RoutedEventArgs e)
            => SetValue(-GetValue());

        // Квадратный корень
        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            double v = GetValue();
            if (v < 0)
                MessageBox.Show("Ошибка: отрицательное число");
            else
                SetValue(Math.Sqrt(v));
        }

        // Смена системы счисления (только для целой части)
        private void System_Changed(object sender, RoutedEventArgs e)
        {
            _currentSystem = ((RadioButton)sender).Content.ToString();
            ConvertSystem(GetValue());
        }

        private void ConvertSystem(double value)
        {
            if (_currentSystem == "DEC")
            {
                SetValue(value);
                return;
            }

            long intPart = (long)Math.Round(value);
            if (_currentSystem == "BIN")
                InputBox.Text = Convert.ToString(intPart, 2);
            else if (_currentSystem == "HEX")
                InputBox.Text = Convert.ToString(intPart, 16).ToUpper();
        }

        // Изменение точности
        private void Precision_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (PrecisionCombo.SelectedItem is ComboBoxItem item)
                _precision = int.Parse(item.Content.ToString());
            // пересчитать текущее значение
            SetValue(GetValue());
        }

        // Изменение формата
        private void Format_Changed(object sender, SelectionChangedEventArgs e)
        {
            _useScientific = FormatCombo.SelectedIndex == 1;
            SetValue(GetValue());
        }
    }
}