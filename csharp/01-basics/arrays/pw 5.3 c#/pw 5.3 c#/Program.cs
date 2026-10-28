using System.Collections.Generic;
using System.Text;

class PW5
{
    static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;

        Console.WriteLine("размер массива: ");
        int a = Convert.ToInt32(Console.ReadLine());

        int[] array = new int[a];

        Console.WriteLine("Введите числа");
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = Convert.ToInt32(Console.ReadLine());
        }

        int fst = -1;
        int snd = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > 0)
            {
                if (fst == -1)
                {
                    fst = i;
                }
                else snd = i;
            }
        }

        int sum = 0;

        for (int i = fst + 1; i < snd; i++)
        {
            sum += array[i];
        }

        Console.WriteLine("Ответ: " + sum);

    }
}