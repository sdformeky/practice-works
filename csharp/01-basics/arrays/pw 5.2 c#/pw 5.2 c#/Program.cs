using System.Text;

class PW5
{
    static void Main(string[] args)
    {

        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Введите N: ");
        int n = Convert.ToInt32(Console.ReadLine());
  

        int[] a = new int[n];

        Console.WriteLine("Введите элементы массива (по одной цифре):");

        for (int i = 0; i < n; i++)
        {
            a[i] = Convert.ToInt32(Console.ReadLine());
        }

        int[] temp = new int[n];
        int index = 0;

        temp[index++] = a[0];

        for (int i = 1; i < n; i++)
        {
            if (a[i] != a[i - 1])   
            {
                temp[index++] = a[i];
            }
        }

        Console.WriteLine("Результат: ");
        for (int i = 0; i < index; i++)
        {
            Console.WriteLine(temp[i] + " ");
        }
    }
}