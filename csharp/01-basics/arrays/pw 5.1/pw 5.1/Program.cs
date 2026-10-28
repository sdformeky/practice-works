using System.Text;

class PW5
{
    static void Main(string[] args)
    {
        Console.InputEncoding = UTF8Encoding.UTF8;
        Console.OutputEncoding = UTF8Encoding.UTF8;

        Console.WriteLine("Введите число N: ");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Введите K: ");
        int k = Convert.ToInt32(Console.ReadLine());

        if (k <= 1)
        {
            Console.WriteLine("К должен быть больше 1");
            return;
        }

        Console.WriteLine("Введите L: ");
        int l = Convert.ToInt32(Console.ReadLine());

        int[] avgmass = new int[n];

        for (int i = 0; i < avgmass.Length; i++)
            avgmass[i] = i + 1;

        int sum = 0;
        int count = 0;

        for (int i = 0; i < avgmass.Length; i++)
        {
            if (i >= k && i <= l)
            {
                sum += avgmass[i];
                count++;
            }
        }

        double avg = (double)sum / count;

        Console.WriteLine("Среднее арифметическое: " + avg);
    }
}
