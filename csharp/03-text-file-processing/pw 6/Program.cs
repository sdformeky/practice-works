using System;
using System.IO;

    public class PW6
    {

        static void Main(string[] args)
        {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int choice = 1;
        string folderName = null;
        string fileName = null;
        string filePath = null;
        string endResult = null;

            while (choice != 0)
            {
                Console.WriteLine("Выберите действие (1-4)");
                Console.WriteLine("1. Определить тип символа \n" +
                        "2. Содержится ли определенный текст в файле? \n" +
                        "3. Найти длину самого длинного слова в файле \n" +
                        "4. Вывести файл без пробелов \n" +
                        "0. Выход"
                        );
                choice = Convert.ToInt32(Console.ReadLine());
            if (choice != 0)
            {
                Console.WriteLine("Введите название папки");
                folderName = Console.ReadLine();
                Console.WriteLine("Введите название файла");
                fileName = Console.ReadLine();
                filePath = Path.Combine(folderName, fileName);
            }
            else break;


            if (File.Exists(filePath))
                {
                    switch (choice)
                    {
                        case 0: break;
                        
                        case 1:
                            endResult = SymbolType(filePath);
                            FileCreate(folderName, endResult);
                            Console.WriteLine("Результат записан в ту же папку");
                        break;
                        case 2:
                            endResult = SomethingInFile(filePath);
                            FileCreate(folderName, endResult);
                            Console.WriteLine("Результат записан в ту же папку");
                        break;
                        case 3:
                            endResult = LongestWord(filePath);
                            FileCreate(folderName, endResult);
                            Console.WriteLine("Результат записан в ту же папку");
                        break;
                        case 4:
                            endResult = WithoutSpace(filePath);
                            FileCreate(folderName, endResult);
                            Console.WriteLine("Результат записан в ту же папку");
                        break;
                }
                }
                else
                {
                    Console.WriteLine("Файл не существует.");
                }
            Thread.Sleep(2000);
            Console.Clear();
            
            }
                    
        }

        static string SymbolType(string directory)
        {
            string text = File.ReadAllText(directory);
            char symbol = Convert.ToChar(text);
            string endResult = null;
            if (char.IsDigit(symbol))
            {
            endResult = "Число";
            }
            else if ((symbol >= 'A' && symbol <= 'Z') || (symbol >= 'a' && symbol <= 'z'))
            {
            endResult = "Латинский символ";
            }
            else if ((symbol >= 'А' && symbol <= 'Я') || (symbol >= 'а' && symbol <= 'я'))
            {
            endResult = "Русский символ";
            }
            else
            {
            endResult = "Неизвестный символ.";
            }
            return endResult;
        }

        static void FileCreate(string path, string endResult)
        {
        Console.WriteLine("Введите название файла для записи");
        string fileName = Path.Combine(path, Console.ReadLine());
        File.WriteAllText(fileName, endResult);
        return;
        }

    static string SomethingInFile(string directory)
    {
        string endResult = null;
        string text = File.ReadAllText(directory);
        Console.WriteLine("Введите текст, наличие которого хотите проверить");
        string textcontain = Console.ReadLine();
        bool pass = text.Contains(textcontain);
        if (pass)
        {
            endResult = $"{textcontain} содержится в {directory}";
        }
        else
        {
            endResult = $"{textcontain} не содержится в {directory}";
        }
        return endResult;
    }
    static string LongestWord(string directory)
    {
        string a = File.ReadAllText(directory);

        string[] words = a.Split();
        int length = 0;

        foreach (string i in words)
        {
            if (i.Length > length)
            {
                length = i.Length;
            }
        }

        return Convert.ToString(length) + " символов";
    }

    static string WithoutSpace(string directory) 
    {
        string a = File.ReadAllText(directory);
        string output = "";

        foreach (char i in a)
        {
            if (i != ' ')
                output += i;
        }
        return output;
    }
}

