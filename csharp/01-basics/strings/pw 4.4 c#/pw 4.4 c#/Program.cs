using System.Text;

class PW4
{
    static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;

        Console.WriteLine("Введите текст");
        string a = Console.ReadLine();
        string output = "";
        
        foreach (char i in a)
        {
            if (i != ' ')
                output += i;
        }
        Console.WriteLine(output);
    }
}