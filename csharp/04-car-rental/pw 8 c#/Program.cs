namespace pw_8_c_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            CarRent rent = new CarRent();

            int choose = -1;

            while (choose != 0)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Добавить автомобиль");
                Console.WriteLine("2. Показать все автомобили");
                Console.WriteLine("3. Показать доступные автомобили");
                Console.WriteLine("4. Показать авто, требующие обслуживания");
                Console.WriteLine("0. Выход");

                choose = Convert.ToInt32(Console.ReadLine());

                switch (choose)
                {
                    case 1:
                        rent.AddCar();
                        break;

                    case 2:
                        rent.ShowAllCars();
                        break;

                    case 3:
                        rent.ShowAvailableCars();
                        break;

                    case 4:
                        rent.ShowCarsNeedingService();
                        break;

                    case 0:
                        Console.WriteLine("Выход...");
                        break;

                    default:
                        Console.WriteLine("Ошибка выбора");
                        break;
                }
            }
        }
    }

    class Transport
    {
        protected string[] models = new string[100];
        protected int count = 0;

        public void ShowModels()
        {
            Console.WriteLine("\nСписок моделей:");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(models[i]);
            }
        }
    }

    class CarRent : Transport
    {
        private int[] mileage = new int[100];
        private int[] fuel = new int[100];
        private bool[] available = new bool[100];

        public void AddCar()
        {
            Console.Write("Введите модель: ");
            models[count] = Console.ReadLine()!;

            Console.Write("Введите пробег: ");
            mileage[count] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите количество топлива: ");
            fuel[count] = Convert.ToInt32(Console.ReadLine());

            Console.Write("Доступна ли машина (true/false): ");
            available[count] = Convert.ToBoolean(Console.ReadLine());

            count++;
        }

        public void ShowAllCars()
        {
            Console.WriteLine("\nВсе автомобили:");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(
                    $"Модель: {models[i]}, " +
                    $"Пробег: {mileage[i]}, " +
                    $"Топливо: {fuel[i]}, " +
                    $"Доступность: {(available[i] ? "Да" : "Нет")}");
            }
        }

        public void ShowAvailableCars()
        {
            Console.WriteLine("\nДоступные автомобили:");

            bool found = false;

            for (int i = 0; i < count; i++)
            {
                if (available[i])
                {
                    Console.WriteLine(models[i]);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Нет доступных машин");
            }
        }

        public void ShowCarsNeedingService()
        {
            Console.WriteLine("\nТребуют обслуживания:");

            bool found = false;

            for (int i = 0; i < count; i++)
            {
                if (fuel[i] < 10 || mileage[i] > 15000)
                {
                    Console.WriteLine(models[i]);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Все машины в порядке");
            }
        }
    }
}