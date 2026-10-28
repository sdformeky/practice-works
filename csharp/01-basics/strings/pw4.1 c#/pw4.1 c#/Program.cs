using System.Text;

class WeirdApp
{
    static void Main(string[] args)
    {

        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.WriteLine("enter symbol: ");
        char symbol = Convert.ToChar(Console.ReadLine());

        if (char.IsDigit(symbol))
        {
            Console.WriteLine("digit");
        }
        else if ((symbol >= 'A' && symbol <= 'Z') || (symbol >= 'a' && symbol <= 'z'))
        {
            Console.WriteLine("lat");
        }
        else if ((symbol >= 'А' && symbol <= 'Я') || (symbol >= 'а' && symbol <= 'я'))
        {
            Console.WriteLine("rus");
        }
        else
        {
            Console.WriteLine("unknown symbol");
        }

    }
}