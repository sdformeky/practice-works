using System.Text;

public class PW4
{

    public static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;

        Console.WriteLine("Введите слова: ");
        string a = Console.ReadLine();
        string[] words = a.Split();
        int length = 0;

        foreach (string i in words)
        {
            if (i.Length > length)
            {
                length = i.Length;
            }
        }

        Console.WriteLine(length);
    }
}