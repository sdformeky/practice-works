using System.Text;

class PW5
{
    public static void Main(string[] args) {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.WriteLine("Введите количество строк: ");
        int m = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите количество столбцов: ");
        int n = int.Parse(Console.ReadLine());
        int[,] array = new int[m,n];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                array[i, j] = int.Parse(Console.ReadLine());
            }   
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(array[i, j] + "  ");
            }
            Console.WriteLine();
        }


        int count = 2;
        int sum = 0;
        for (int i = 0; i < m; i++)
        {
            while (count < n)
            {
                sum += array[i, count];
                count += 2;
            }
            count = 2;
        }

        Console.Write(sum.ToString());

    }
}