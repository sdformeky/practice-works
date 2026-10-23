using System;

class Sorts
{
    public static void BubbleSort(int[] a)
    {
        int n = a.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (a[j] > a[j + 1])
                {
                    int t = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = t;
                }
            }
        }
    }

    public static void ShakerSort(int[] a)
    {
        int left = 0;
        int right = a.Length - 1;

        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (a[i] > a[i + 1])
                {
                    int t = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = t;
                }
            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (a[i - 1] > a[i])
                {
                    int t = a[i - 1];
                    a[i - 1] = a[i];
                    a[i] = t;
                }
            }
            left++;
        }
    }

    public static void SelectionSort(int[] a)
    {
        int n = a.Length;

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (a[j] < a[minIndex])
                    minIndex = j;
            }

            int t = a[i];
            a[i] = a[minIndex];
            a[minIndex] = t;
        }
    }

    public static void InsertionSort(int[] a)
    {
        int n = a.Length;

        for (int i = 1; i < n; i++)
        {
            int x = a[i];
            int j = i - 1;

            while (j >= 0 && a[j] > x)
            {
                a[j + 1] = a[j];
                j--;
            }

            a[j + 1] = x;
        }
    }

    public static void MergeSort(int[] a, int left, int right)
    {
        if (left >= right) return;

        int mid = (left + right) / 2;

        MergeSort(a, left, mid);
        MergeSort(a, mid + 1, right);

        Merge(a, left, mid, right);
    }

    public static void Merge(int[] a, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++)
            L[i] = a[left + i];

        for (int i = 0; i < n2; i++)
            R[i] = a[mid + 1 + i];

        int p1 = 0, p2 = 0, k = left;

        while (p1 < n1 && p2 < n2)
        {
            if (L[p1] <= R[p2])
                a[k++] = L[p1++];
            else
                a[k++] = R[p2++];
        }

        while (p1 < n1)
            a[k++] = L[p1++];

        while (p2 < n2)
            a[k++] = R[p2++];
    }

    public static void Print(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
            Console.Write(a[i] + " ");
        Console.WriteLine();
    }

    static void Main()
    {
        int[] A = { 5, 2, 9, 1, 7, 3 };

        Console.WriteLine("Исходный:");
        Print(A);

        int[] B;

        B = (int[])A.Clone();
        BubbleSort(B);
        Console.WriteLine("Пузырёк:");
        Print(B);

        B = (int[])A.Clone();
        ShakerSort(B);
        Console.WriteLine("Шейкерная:");
        Print(B);

        B = (int[])A.Clone();
        SelectionSort(B);
        Console.WriteLine("Выбором:");
        Print(B);

        B = (int[])A.Clone();
        InsertionSort(B);
        Console.WriteLine("Вставками:");
        Print(B);

        B = (int[])A.Clone();
        MergeSort(B, 0, B.Length - 1);
        Console.WriteLine("Слияние:");
        Print(B);
    }
}
