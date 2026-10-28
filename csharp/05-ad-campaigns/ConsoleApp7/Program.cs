using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketingAgency
{
    public struct Campaign
    {
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public long Impressions { get; set; }
        public double ConversionRate { get; set; }

        public Campaign(string name, decimal budget, long impressions, double conversionRate)
        {
            Name = name;
            Budget = budget;
            Impressions = impressions;
            ConversionRate = conversionRate;
        }

        public double CalculateEffectiveness()
        {
            if (Budget <= 0) return 0;
            double conversions = Impressions * (ConversionRate / 100.0);
            return conversions / (double)Budget;
        }

        public override string ToString()
        {
            return $"{Name} | {Budget:F2} | {Impressions} | {ConversionRate:F2}%";
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<Campaign> campaigns = new List<Campaign>
            {
                new Campaign("Лето 2024", 100000m, 50000, 2.5),
                new Campaign("Осень 2024", 200000m, 120000, 1.8),
                new Campaign("Зима 2024", 50000m, 30000, 3.0),
                new Campaign("Весна 2024", 150000m, 80000, 2.0),
                new Campaign("Тестовая", 0m, 1000, 5.0)
            };

            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Анализ рекламных кампаний ===");
                Console.WriteLine("1. Вывести список кампаний");
                Console.WriteLine("2. Рассчитать эффективность кампаний");
                Console.WriteLine("3. Выявить наименее эффективные кампании (топ-3)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== Список рекламных кампаний ===");
                        Console.WriteLine("Название | Бюджет | Показы | Конверсия");
                        Console.WriteLine(new string('-', 50));
                        foreach (var c in campaigns)
                            Console.WriteLine(c);
                        Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Эффективность кампаний ===");
                        Console.WriteLine("Название | Бюджет | Показы | Конверсия | Эффективность");
                        Console.WriteLine(new string('-', 70));
                        foreach (var c in campaigns)
                        {
                            double eff = c.CalculateEffectiveness();
                            string effStr = eff < 0.0001 ? eff.ToString("e4") : eff.ToString("f6");
                            Console.WriteLine($"{c.Name} | {c.Budget:F2} | {c.Impressions} | {c.ConversionRate:F2}% | {effStr}");
                        }
                        Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== Наименее эффективные кампании (топ-3) ===");
                        var sorted = campaigns.OrderBy(c => c.CalculateEffectiveness()).Take(3).ToList();
                        if (sorted.Count == 0)
                            Console.WriteLine("Нет данных для отображения.");
                        else
                        {
                            Console.WriteLine("Название | Бюджет | Показы | Конверсия | Эффективность");
                            Console.WriteLine(new string('-', 70));
                            foreach (var c in sorted)
                            {
                                double eff = c.CalculateEffectiveness();
                                string effStr = eff < 0.0001 ? eff.ToString("e4") : eff.ToString("f6");
                                Console.WriteLine($"{c.Name} | {c.Budget:F2} | {c.Impressions} | {c.ConversionRate:F2}% | {effStr}");
                            }
                        }
                        Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
                        Console.ReadKey();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}