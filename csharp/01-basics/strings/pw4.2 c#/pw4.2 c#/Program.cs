using System.Text;

public class PW42
{
    public static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.WriteLine("Введите исходные данные: ");
        string a = Console.ReadLine();
        Console.WriteLine("Введите данные, наличие которых нужно проверить: ");
        string b = Console.ReadLine();
        bool pass = a.Contains(b);
        if (pass)
        {
            Console.WriteLine(b + " содержится в " + a);
            return;
        }
        else
        {
            Console.WriteLine(b + " не содержится в " + a);
            return;
        }
    }
}