using System.Text;

class PW5
{
    static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;

        Console.WriteLine("Введите N");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[n];
        int[] barray = new int[n];

        Console.WriteLine("Введите числа");
        for (int i = 0; i < n; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        int index = 0;

        for (int i = 1; i < n; i += 2)
        {
            barray[index] = array[i];
            index++;
        }

        for (int i = 0; i < n; i += 2)
        {
            barray[index] = array[i];
            index++;
        }

     
        for (int i = 0; i < n; i++)
        {
            Console.Write(barray[i] + " ");
        }
    }
}