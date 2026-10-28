using Microsoft.Win32;                 // Диалоговые окна открытия и сохранения файла.
using System;
using System.IO;                        // Работа с файлами.
using System.Text;                      // Кодировка UTF-8.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace pwc15
{
    public partial class MainWindow : Window
    {
        // currentFilePath хранит путь к текущему файлу,
        // с которым работает блокнот.
        //
        // Если строка пустая, значит файл ещё новый
        // и не был сохранён на компьютер.
        private string currentFilePath = "";

        // isTextChanged хранит информацию о том,
        // изменялся ли текст после последнего сохранения.
        //
        // false — изменений нет;
        // true — есть несохранённые изменения.
        private bool isTextChanged = false;

        public MainWindow()
        {
            // InitializeComponent() загружает интерфейс,
            // описанный в файле MainWindow.xaml.
            InitializeComponent();

            // Заполняем панель форматирования начальными данными:
            // списком шрифтов, размерами и цветом по умолчанию.
            InitializeFormattingToolbar();
        }

        // =========================================================
        // СЛУЖЕБНЫЕ МЕТОДЫ ДЛЯ РАБОТЫ С ТЕКСТОМ RichTextBox
        // =========================================================

        // Служебный метод получает обычный текст из RichTextBox.
        //
        // У RichTextBox нет свойства Editor.Text,
        // потому что он хранит не простую строку,
        // а форматированный документ.
        private string GetEditorText()
        {
            TextRange textRange = new TextRange(
                Editor.Document.ContentStart,
                Editor.Document.ContentEnd);

            string text = textRange.Text;

            // У RichTextBox в конце текста часто появляется
            // служебный перевод строки. Убираем его.
            if (text.EndsWith("\r\n"))
            {
                text = text.Substring(0, text.Length - 2);
            }

            return text;
        }

        // Служебный метод записывает обычный текст в RichTextBox.
        private void SetEditorText(string text)
        {
            // Очищаем текущий документ.
            Editor.Document.Blocks.Clear();

            // Создаём новый абзац и добавляем в него текст.
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(text);

            // Добавляем абзац в документ RichTextBox.
            Editor.Document.Blocks.Add(paragraph);
        }

        // Служебный метод очищает RichTextBox.
        private void ClearEditor()
        {
            Editor.Document.Blocks.Clear();
            Editor.Document.Blocks.Add(new Paragraph());
        }

        // =========================================================
        // ФАЙЛ: СОЗДАТЬ / ОТКРЫТЬ / СОХРАНИТЬ
        // =========================================================

        // Метод отвечает за команду "Файл → Создать".
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            // Перед очисткой редактора проверяем,
            // нет ли несохранённых изменений.
            if (!CheckSaveBeforeAction())
                return;

            // Очищаем RichTextBox.
            ClearEditor();

            // Новый файл ещё не сохранён,
            // поэтому путь к файлу сбрасывается.
            currentFilePath = "";

            // После создания нового пустого файла считаем,
            // что несохранённых изменений нет.
            isTextChanged = false;

            // Обновляем заголовок окна.
            Title = "Новый файл — Блокнот";

            // Обновляем строку состояния.
            UpdateStatus();
        }

        // Метод отвечает за команду "Файл → Открыть".
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            // Перед открытием другого файла проверяем,
            // нет ли несохранённых изменений в текущем тексте.
            if (!CheckSaveBeforeAction())
                return;

            // Создаём объект стандартного окна Windows для выбора файла.
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Открыть файл";
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            // ShowDialog() возвращает true, если пользователь выбрал файл.
            if (dialog.ShowDialog() == true)
            {
                // Читаем текст из файла.
                string fileText = File.ReadAllText(dialog.FileName, Encoding.UTF8);

                // Записываем текст в RichTextBox.
                SetEditorText(fileText);

                // Запоминаем путь к открытому файлу.
                currentFilePath = dialog.FileName;

                // После открытия файла несохранённых изменений нет.
                isTextChanged = false;

                // Обновляем заголовок окна.
                Title = currentFilePath + " — Блокнот";

                // Обновляем строку состояния.
                UpdateStatus();
            }
        }

        // Обработчик пункта меню "Файл → Сохранить".
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        // Обработчик пункта меню "Файл → Сохранить как".
        private void SaveAsFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileAs();
        }

        // Служебный метод обычного сохранения файла.
        //
        // Возвращает bool:
        // true — файл был сохранён;
        // false — файл не был сохранён (например, пользователь нажал "Отмена").
        private bool SaveFile()
        {
            // Если путь неизвестен — файл новый, нужно "Сохранить как".
            if (currentFilePath == "")
            {
                return SaveFileAs();
            }

            // Получаем простой текст из RichTextBox и записываем его в файл.
            File.WriteAllText(currentFilePath, GetEditorText(), Encoding.UTF8);

            // После успешного сохранения считаем,
            // что несохранённых изменений больше нет.
            isTextChanged = false;

            // Обновляем заголовок окна (звёздочку не ставим).
            Title = currentFilePath + " — Блокнот";

            // Обновляем строку состояния.
            UpdateStatus();

            return true;
        }

        // Служебный метод "Сохранить как".
        // Открывает окно выбора папки и имени файла.
        private bool SaveFileAs()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "Сохранить файл как";
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            dialog.FileName = "Новый документ.txt";

            if (dialog.ShowDialog() == true)
            {
                // Сохраняем выбранный путь.
                currentFilePath = dialog.FileName;

                // Путь известен — выполняем обычное сохранение.
                return SaveFile();
            }

            // Пользователь нажал "Отмена".
            return false;
        }

        // =========================================================
        // ПЕЧАТЬ
        // =========================================================

        // Метод отвечает за команду "Файл → Печать".
        private void PrintFile_Click(object sender, RoutedEventArgs e)
        {
            // PrintDialog — стандартное окно печати Windows.
            PrintDialog dialog = new PrintDialog();

            if (dialog.ShowDialog() == true)
            {
                // PrintVisual печатает визуальное содержимое элемента Editor.
                dialog.PrintVisual(Editor, "Печать документа");
            }
        }

        // =========================================================
        // ВЫХОД И СПРАВКА
        // =========================================================

        // Метод отвечает за команду "Файл → Выход".
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Close() закрывает текущее (главное) окно программы.
            Close();
        }

        // Метод отвечает за команду "Справка → О программе".
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "WPF-Блокнот\n\nПрограмма позволяет создавать, открывать, редактировать, сохранять и печатать текстовые файлы.",
                "О программе",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // =========================================================
        // ПРАВКА: ОТМЕНИТЬ / ПОВТОРИТЬ / ВЫРЕЗАТЬ / КОПИРОВАТЬ / ВСТАВИТЬ / УДАЛИТЬ
        // =========================================================

        // Метод отвечает за команду "Правка → Отменить".
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            // CanUndo показывает, есть ли в истории редактора
            // действие, которое можно отменить.
            if (Editor.CanUndo)
                Editor.Undo();
        }

        // Метод отвечает за команду "Правка → Повторить".
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            // CanRedo показывает, есть ли действие, которое можно повторить.
            if (Editor.CanRedo)
                Editor.Redo();
        }

        // Метод отвечает за команду "Правка → Вырезать".
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            // Cut() копирует выделенный текст в буфер обмена
            // и удаляет его из редактора.
            Editor.Cut();
        }

        // Метод отвечает за команду "Правка → Копировать".
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            // Copy() копирует выделенный текст в буфер обмена.
            Editor.Copy();
        }

        // Метод отвечает за команду "Правка → Вставить".
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            // Paste() вставляет содержимое буфера обмена
            // в позицию курсора (или заменяет выделение).
            Editor.Paste();
        }

        // Метод отвечает за команду "Правка → Удалить".
        // (итоговая версия для RichTextBox, этап 21.4)
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // Editor.Selection — выделенный фрагмент текста в RichTextBox.
            //
            // Если выделение не пустое, заменяем его на пустую строку,
            // что фактически удаляет выделенный текст.
            if (!Editor.Selection.IsEmpty)
            {
                Editor.Selection.Text = "";
            }
        }

        // Метод отвечает за команду "Правка → Выделить всё".
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            // SelectAll() выделяет весь текст, находящийся в редакторе.
            Editor.SelectAll();
        }

        // =========================================================
        // ФОРМАТ: ПЕРЕНОС ПО СЛОВАМ / РАЗМЕР ШРИФТА (старые пункты меню)
        // =========================================================

        // Метод отвечает за команду "Формат → Перенос по словам".
        // (итоговая версия для RichTextBox, этап 29)
        private void WordWrap_Click(object sender, RoutedEventArgs e)
        {
            // sender фактически содержит тот самый MenuItem,
            // на который нажал пользователь.
            MenuItem item = sender as MenuItem;

            if (item != null)
            {
                if (item.IsChecked)
                {
                    // При включённом переносе горизонтальная прокрутка не нужна.
                    Editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                }
                else
                {
                    // При выключенном переносе разрешаем горизонтальную прокрутку.
                    Editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }
            }
        }

        // Метод отвечает за команду "Формат → Увеличить шрифт".
        // (итоговая версия для RichTextBox, этап 28 — работает с выделением)
        private void IncreaseFont_Click(object sender, RoutedEventArgs e)
        {
            // Получаем текущее значение размера шрифта у выделения.
            object value = Editor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            double currentSize = 16;

            // Если у выделения один общий размер шрифта,
            // значение можно преобразовать в double.
            if (value != DependencyProperty.UnsetValue)
            {
                currentSize = (double)value;
            }

            // Увеличиваем размер на 2.
            double newSize = currentSize + 2;
            Editor.Selection.ApplyPropertyValue(
                TextElement.FontSizeProperty,
                newSize);

            // Обновляем ComboBox размера, чтобы он показывал новое значение.
            FontSizeComboBox.Text = newSize.ToString();
            Editor.Focus();
        }

        // Метод отвечает за команду "Формат → Уменьшить шрифт".
        // (итоговая версия для RichTextBox, этап 28 — работает с выделением)
        private void DecreaseFont_Click(object sender, RoutedEventArgs e)
        {
            object value = Editor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            double currentSize = 16;

            if (value != DependencyProperty.UnsetValue)
            {
                currentSize = (double)value;
            }

            // Ограничиваем минимальный размер.
            if (currentSize > 8)
            {
                double newSize = currentSize - 2;
                Editor.Selection.ApplyPropertyValue(
                    TextElement.FontSizeProperty,
                    newSize);

                FontSizeComboBox.Text = newSize.ToString();
            }

            Editor.Focus();
        }

        // =========================================================
        // ОТСЛЕЖИВАНИЕ ИЗМЕНЕНИЙ И СТРОКА СОСТОЯНИЯ
        // =========================================================

        // Метод срабатывает каждый раз, когда изменяется текст в редакторе Editor.
        private void Editor_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Фиксируем, что текст был изменён.
            isTextChanged = true;

            // Если файл новый и ещё не сохранён.
            if (currentFilePath == "")
            {
                Title = "Новый файл * — Блокнот";
            }
            else
            {
                Title = currentFilePath + " * — Блокнот";
            }

            // После изменения текста обновляем строку состояния.
            UpdateStatus();
        }

        // Метод срабатывает, когда в редакторе меняется выделение текста
        // или положение текстового курсора.
        private void Editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        // Служебный метод обновляет строку состояния внизу окна.
        // (итоговая версия для RichTextBox, этап 21.5)
        private void UpdateStatus()
        {
            // Получаем весь простой текст редактора.
            string text = GetEditorText();

            // Получаем текст от начала документа до позиции курсора.
            TextRange rangeToCaret = new TextRange(
                Editor.Document.ContentStart,
                Editor.CaretPosition);

            string textBeforeCaret = rangeToCaret.Text;

            // У RichTextBox в служебном тексте тоже может быть лишний перенос строки.
            if (textBeforeCaret.EndsWith("\r\n"))
            {
                textBeforeCaret = textBeforeCaret.Substring(0, textBeforeCaret.Length - 2);
            }

            // Считаем строку и столбец по количеству переводов строки перед курсором.
            int line = 1;
            int column = 1;

            for (int i = 0; i < textBeforeCaret.Length; i++)
            {
                if (textBeforeCaret[i] == '\n')
                {
                    line++;
                    column = 1;
                }
                else if (textBeforeCaret[i] != '\r')
                {
                    column++;
                }
            }

            LineColumnText.Text = "Строка: " + line + ", Столбец: " + column;
            CharCountText.Text = "Символов: " + text.Length;

            if (currentFilePath == "")
            {
                FilePathText.Text = "Новый файл";
            }
            else
            {
                FilePathText.Text = currentFilePath;
            }
        }

        // =========================================================
        // ЗАЩИТА ОТ ПОТЕРИ ДАННЫХ
        // =========================================================

        // Служебный метод проверки несохранённых изменений.
        //
        // Вызывается перед действиями, при которых пользователь
        // может потерять текущий текст.
        //
        // Возвращает bool:
        // true — действие можно продолжать;
        // false — действие нужно отменить.
        private bool CheckSaveBeforeAction()
        {
            // Если изменений нет — ничего спрашивать не нужно.
            if (!isTextChanged)
                return true;

            // Если изменения есть, показываем пользователю вопрос.
            MessageBoxResult result = MessageBox.Show(
                "Файл был изменён. Сохранить изменения?",
                "Сохранение",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // SaveFile() вернёт true, если файл сохранился.
                return SaveFile();
            }

            if (result == MessageBoxResult.No)
            {
                // Пользователь сознательно отказался от сохранения.
                return true;
            }

            // Пользователь нажал "Отмена" — действие нужно отменить.
            return false;
        }

        // Метод автоматически вызывается при закрытии окна.
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Проверяем, можно ли закрывать окно.
            if (!CheckSaveBeforeAction())
            {
                // Отменяем закрытие окна — программа останется открытой.
                e.Cancel = true;
            }

            base.OnClosing(e);
        }

        // =========================================================
        // ПОИСК И ЗАМЕНА
        // =========================================================

        // Метод выполняется при нажатии пункта меню "Правка → Найти".
        private void ShowFindPanel_Click(object sender, RoutedEventArgs e)
        {
            // Показываем панель поиска.
            FindPanel.Visibility = Visibility.Visible;

            // В режиме "Найти" элементы замены не нужны — скрываем их.
            ReplaceTextBlock.Visibility = Visibility.Collapsed;
            ReplaceTextBox.Visibility = Visibility.Collapsed;
            ReplaceButton.Visibility = Visibility.Collapsed;
            ReplaceAllButton.Visibility = Visibility.Collapsed;

            // Ставим курсор в поле поиска.
            FindTextBox.Focus();
        }

        // Метод выполняется при нажатии пункта меню "Правка → Заменить".
        private void ShowReplacePanel_Click(object sender, RoutedEventArgs e)
        {
            // Показываем общую панель поиска и замены.
            FindPanel.Visibility = Visibility.Visible;

            // Показываем элементы, которые нужны для замены текста.
            ReplaceTextBlock.Visibility = Visibility.Visible;
            ReplaceTextBox.Visibility = Visibility.Visible;
            ReplaceButton.Visibility = Visibility.Visible;
            ReplaceAllButton.Visibility = Visibility.Visible;

            FindTextBox.Focus();
        }

        // Метод выполняется при нажатии кнопки "Закрыть" на панели поиска.
        private void CloseFindPanel_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем всю панель поиска и замены.
            FindPanel.Visibility = Visibility.Collapsed;

            // Возвращаем фокус в основной редактор текста.
            Editor.Focus();
        }

        // Метод выполняется при нажатии кнопки "Найти далее" на панели поиска.
        private void FindNext_Click(object sender, RoutedEventArgs e)
        {
            FindNext();
        }

        // Служебный метод поиска следующего совпадения в тексте.
        //
        // ВНИМАНИЕ: метод использует Editor.Text / Editor.SelectionStart,
        // которые относятся к версии на TextBox (этап 17).
        // После перехода на RichTextBox (этап 19+) для полноценной работы
        // этот метод нужно адаптировать аналогично GetEditorText()/Selection
        // (в методичке отдельно такая адаптация для поиска не описана).
        private void FindNext()
        {
            // Берём текст, который пользователь ввёл в поле поиска.
            string findText = FindTextBox.Text;

            if (findText == "")
            {
                MessageBox.Show("Введите текст для поиска.",
                    "Поиск",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            // StringComparison определяет режим сравнения строк.
            StringComparison comparison;

            if (CaseSensitiveCheckBox.IsChecked == true)
            {
                comparison = StringComparison.CurrentCulture;
            }
            else
            {
                comparison = StringComparison.CurrentCultureIgnoreCase;
            }

            string editorText = GetEditorText();

            // Начинаем поиск от текущей позиции курсора.
            TextRange rangeToCaret = new TextRange(
                Editor.Document.ContentStart,
                Editor.Selection.Start);
            int startIndex = rangeToCaret.Text.Length;

            if (!Editor.Selection.IsEmpty)
            {
                startIndex += Editor.Selection.Text.Length;
            }

            int index = editorText.IndexOf(findText, Math.Min(startIndex, editorText.Length), comparison);

            // Если от текущей позиции до конца ничего не найдено,
            // пробуем искать с начала документа (поиск "по кругу").
            if (index == -1 && startIndex > 0)
            {
                index = editorText.IndexOf(findText, 0, comparison);
            }

            if (index >= 0)
            {
                Editor.Focus();

                // Находим позицию начала и конца найденного фрагмента
                // в координатах TextPointer и выделяем его.
                TextPointer start = Editor.Document.ContentStart.GetPositionAtOffset(index);
                TextPointer end = Editor.Document.ContentStart.GetPositionAtOffset(index + findText.Length);

                if (start != null && end != null)
                {
                    Editor.Selection.Select(start, end);
                    start.Paragraph?.BringIntoView();
                }
            }
            else
            {
                MessageBox.Show("Текст не найден.",
                    "Поиск",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        // Метод выполняется при нажатии кнопки "Заменить".
        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            string findText = FindTextBox.Text;
            string replaceText = ReplaceTextBox.Text;

            if (findText == "")
            {
                MessageBox.Show("Введите текст для поиска.",
                    "Замена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            StringComparison comparison;

            if (CaseSensitiveCheckBox.IsChecked == true)
            {
                comparison = StringComparison.CurrentCulture;
            }
            else
            {
                comparison = StringComparison.CurrentCultureIgnoreCase;
            }

            // Если выделенный текст совпадает с искомым — заменяем.
            if (!Editor.Selection.IsEmpty &&
                Editor.Selection.Text.Equals(findText, comparison))
            {
                Editor.Selection.Text = replaceText;
            }

            // После замены сразу ищем следующее совпадение.
            FindNext();
        }

        // Метод выполняется при нажатии кнопки "Заменить всё".
        private void ReplaceAll_Click(object sender, RoutedEventArgs e)
        {
            string findText = FindTextBox.Text;
            string replaceText = ReplaceTextBox.Text;

            if (findText == "")
            {
                MessageBox.Show("Введите текст для поиска.",
                    "Замена",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            StringComparison comparison;

            if (CaseSensitiveCheckBox.IsChecked == true)
            {
                comparison = StringComparison.CurrentCulture;
            }
            else
            {
                comparison = StringComparison.CurrentCultureIgnoreCase;
            }

            // Копируем весь текст редактора во временную переменную.
            string text = GetEditorText();

            int count = 0;
            int index = 0;

            while (true)
            {
                index = text.IndexOf(findText, index, comparison);

                if (index == -1)
                    break;

                // Удаляем найденный фрагмент и вставляем новый текст.
                text = text.Remove(index, findText.Length);
                text = text.Insert(index, replaceText);

                // Переносим index за вставленный текст, чтобы избежать зацикливания.
                index = index + replaceText.Length;
                count++;
            }

            // Записываем изменённый текст обратно в редактор.
            SetEditorText(text);

            MessageBox.Show("Выполнено замен: " + count,
                "Замена",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        // =========================================================
        // ПАНЕЛЬ ФОРМАТИРОВАНИЯ: ИНИЦИАЛИЗАЦИЯ
        // =========================================================

        // Метод заполняет элементы панели форматирования.
        // Вызывается один раз при запуске окна.
        private void InitializeFormattingToolbar()
        {
            // Fonts.SystemFontFamilies содержит список шрифтов,
            // установленных в системе Windows.
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontFamilyComboBox.Items.Add(fontFamily.Source);
            }

            // Выбираем шрифт Consolas по умолчанию.
            FontFamilyComboBox.SelectedItem = "Consolas";

            // Добавляем стандартные размеры шрифта.
            FontSizeComboBox.Items.Add("8");
            FontSizeComboBox.Items.Add("10");
            FontSizeComboBox.Items.Add("12");
            FontSizeComboBox.Items.Add("14");
            FontSizeComboBox.Items.Add("16");
            FontSizeComboBox.Items.Add("18");
            FontSizeComboBox.Items.Add("20");
            FontSizeComboBox.Items.Add("24");
            FontSizeComboBox.Items.Add("28");
            FontSizeComboBox.Items.Add("32");
            FontSizeComboBox.Items.Add("36");
            FontSizeComboBox.Items.Add("48");
            FontSizeComboBox.Items.Add("72");

            // Размер по умолчанию.
            FontSizeComboBox.SelectedItem = "16";

            // Цвет по умолчанию — чёрный (первый элемент списка).
            FontColorComboBox.SelectedIndex = 0;

            // По умолчанию в списке автоформатирования ничего не выбрано,
            // чтобы при запуске программы стиль не применялся сам по себе.
            AutoFormatComboBox.SelectedIndex = -1;
        }

        // =========================================================
        // ФОРМАТИРОВАНИЕ ВЫДЕЛЕННОГО ТЕКСТА: Ж / К / Ч
        // =========================================================

        // Метод отвечает за кнопку "Жирный".
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            // ApplyPropertyValue применяет свойство к выделению.
            Editor.Selection.ApplyPropertyValue(
                TextElement.FontWeightProperty,
                FontWeights.Bold);

            Editor.Focus();
        }

        // Метод отвечает за кнопку "Курсив".
        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            Editor.Selection.ApplyPropertyValue(
                TextElement.FontStyleProperty,
                FontStyles.Italic);

            Editor.Focus();
        }

        // Метод отвечает за кнопку "Подчёркнутый".
        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            // TextDecorations.Underline включает подчёркивание текста.
            Editor.Selection.ApplyPropertyValue(
                Inline.TextDecorationsProperty,
                TextDecorations.Underline);

            Editor.Focus();
        }

        // =========================================================
        // ВЫБОР ШРИФТА, РАЗМЕРА И ЦВЕТА (ПАНЕЛЬ ИНСТРУМЕНТОВ)
        // =========================================================

        // Метод срабатывает при выборе шрифта в ComboBox.
        private void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyComboBox.SelectedItem == null)
                return;

            string selectedFont = FontFamilyComboBox.SelectedItem.ToString();

            // Применяем выбранный шрифт к выделенному тексту.
            Editor.Selection.ApplyPropertyValue(
                TextElement.FontFamilyProperty,
                new FontFamily(selectedFont));

            Editor.Focus();
        }

        // Метод срабатывает при выборе размера шрифта.
        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem == null)
                return;

            string selectedSizeText = FontSizeComboBox.SelectedItem.ToString();

            double selectedSize;
            if (double.TryParse(selectedSizeText, out selectedSize))
            {
                // Применяем размер к выделенному тексту.
                Editor.Selection.ApplyPropertyValue(
                    TextElement.FontSizeProperty,
                    selectedSize);
            }

            Editor.Focus();
        }

        // Метод срабатывает при выборе цвета текста.
        private void FontColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontColorComboBox.SelectedItem == null)
                return;

            ComboBoxItem selectedItem = FontColorComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            // В свойстве Tag хранится английское название цвета.
            string colorName = selectedItem.Tag.ToString();

            // BrushConverter преобразует строку с названием цвета в объект Brush.
            BrushConverter converter = new BrushConverter();
            Brush brush = converter.ConvertFromString(colorName) as Brush;

            if (brush != null)
            {
                // ForegroundProperty отвечает за цвет текста.
                Editor.Selection.ApplyPropertyValue(
                    TextElement.ForegroundProperty,
                    brush);
            }

            Editor.Focus();
        }

        // =========================================================
        // ВЫРАВНИВАНИЕ АБЗАЦЕВ
        // =========================================================

        // Метод выравнивает текущий абзац или выделенные абзацы по левому краю.
        private void AlignLeft_Click(object sender, RoutedEventArgs e)
        {
            Editor.Selection.ApplyPropertyValue(
                Paragraph.TextAlignmentProperty,
                TextAlignment.Left);

            Editor.Focus();
        }

        // Метод выравнивает текущий абзац или выделенные абзацы по центру.
        private void AlignCenter_Click(object sender, RoutedEventArgs e)
        {
            Editor.Selection.ApplyPropertyValue(
                Paragraph.TextAlignmentProperty,
                TextAlignment.Center);

            Editor.Focus();
        }

        // Метод выравнивает текущий абзац или выделенные абзацы по правому краю.
        private void AlignRight_Click(object sender, RoutedEventArgs e)
        {
            Editor.Selection.ApplyPropertyValue(
                Paragraph.TextAlignmentProperty,
                TextAlignment.Right);

            Editor.Focus();
        }

        // Метод выравнивает текущий абзац или выделенные абзацы по ширине.
        private void AlignJustify_Click(object sender, RoutedEventArgs e)
        {
            Editor.Selection.ApplyPropertyValue(
                Paragraph.TextAlignmentProperty,
                TextAlignment.Justify);

            Editor.Focus();
        }

        // =========================================================
        // ПАНЕЛЬ «АВТОФОРМАТИРОВАНИЕ» (Вариант 6)
        // =========================================================

        // Метод срабатывает при выборе варианта в AutoFormatComboBox.
        //
        // Если включена галочка "Автоматически применять после выбора" —
        // стиль применяется сразу же, без нажатия кнопки "Применить".
        private void AutoFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AutoApplyCheckBox.IsChecked == true)
            {
                ApplySelectedAutoFormat();
            }
        }

        // Метод отвечает за кнопку "Применить".
        //
        // Используется, когда галочка "Автоматически применять после выбора"
        // выключена — стиль в этом случае применяется только по нажатию кнопки.
        private void ApplyAutoFormatButton_Click(object sender, RoutedEventArgs e)
        {
            ApplySelectedAutoFormat();
        }

        // Служебный метод применяет выбранный в ComboBox вариант
        // автоформатирования к выделенному тексту.
        private void ApplySelectedAutoFormat()
        {
            if (AutoFormatComboBox.SelectedItem == null)
                return;

            ComboBoxItem selectedItem = AutoFormatComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            string styleName = selectedItem.Content.ToString();

            if (Editor.Selection.IsEmpty)
            {
                MessageBox.Show("Выделите текст, к которому нужно применить автоформат.",
                    "Автоформатирование",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            BrushConverter converter = new BrushConverter();

          
            switch (styleName)
            {
                case "Сделать заголовком":
                    Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 28.0);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, converter.ConvertFromString("Blue"));
                    Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    break;

                case "Сделать важным":
                    Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 18.0);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, converter.ConvertFromString("Red"));
                    Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    break;

                case "Сделать примечанием":
                    Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 14.0);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                    Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, converter.ConvertFromString("Gray"));
                    Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    break;

                case "Сделать кодом":
                    Editor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily("Consolas"));
                    Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 14.0);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, converter.ConvertFromString("Black"));
                    Editor.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, converter.ConvertFromString("#EEEEEE"));
                    Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    break;

                case "Сбросить оформление":
                    Editor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily("Consolas"));
                    Editor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, 16.0);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                    Editor.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, converter.ConvertFromString("Black"));
                    Editor.Selection.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
                    Editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                    break;
            }

            
            AutoFormatStatusText.Text = "Автоформат: " + styleName;

            Editor.Focus();
        }
    }
}