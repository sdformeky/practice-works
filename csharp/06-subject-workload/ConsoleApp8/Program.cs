using System.Reflection.Emit;

namespace ConsoleApp5
{

    internal class Program
    {
        public class Item
        {
            private string Name;
            private string Damage;
            private int Hmtu; // how much time use
            private bool Avalible_status;

            public string name
            {
                get { return Name; }
            }
            public string damage
            {
                get { return Damage; }
            }
            public int hmtu
            {
                get { return Hmtu; }
            }
            public bool avalible_status
            {
                get { return Avalible_status; }
            }
            public Item(string name, string damage, int hmtu, bool avalible_status)
            {
                this.Name = name;
                this.Damage = damage;
                this.Hmtu = hmtu;
                this.Avalible_status = avalible_status;
            }
            void change_damage(string new_damage)
            {
                switch (new_damage)
                {
                    case ("Новый"):
                        Damage = new_damage;
                        break;
                    case ("Среднее"):
                        Damage = new_damage;
                        break;
                    case ("Сломанный"):
                        Damage = new_damage;
                        break;
                    default:
                        break;
                }
            }
            void plus_hmtu()
            {
                Hmtu++;
            }
            void change_avs()
            {
                Avalible_status = !Avalible_status;
            }
            public void print_name()
            {
                Console.WriteLine(Name);
            }
            public static void damage_status(Item[] items)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Damage == "Плохое")
                    {
                        Console.WriteLine(items[i].Name + " Требует обслуживания");
                    }
                }
            }
            public static void print_avalible(Item[] items)
            {
                int count = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Avalible_status == false)
                    {
                        count++;
                    }
                }
                Console.Out.WriteLine("В аренде находися " + (count * 100 / items.Length) + "% инструментов");
            }
        }
        static void Main(string[] args)
        {
            Item[] items = new Item[3];
            Item pila = new Item("ПИЛА3000", "Нормальное", 5, true);
            Item molotok = new Item("МОЛОТОК_ОТ_ИВАНА", "Новый", 1, false);
            Item otvertka = new Item("ОТВЕРТКА_НУ_ПРЯМ_КРУТАЯ", "Плохое", 20, false);
            items[0] = pila;
            items[1] = otvertka;
            items[2] = molotok;
            bool a = true;
            int c;
            while (a)
            {
                Console.WriteLine("1. Вывести название предмета");
                Console.WriteLine("2. Узнать загруженность");
                Console.WriteLine("3. Узнать состояние предметов");
                Console.WriteLine("0. Выход");
                c = Convert.ToInt32(Console.ReadLine());
                switch (c)
                {
                    case 0:
                        Console.WriteLine("ПОКА!!!!!!");
                        a = false;
                        break;
                    case 1:
                        Console.WriteLine("Введите предмет: ");
                        string name = Console.ReadLine();
                        if (name == "Пила")
                        {
                            pila.print_name();
                        }
                        else if (name == "Молоток")
                        {
                            molotok.print_name();
                        }
                        else if (name == "Отвертка")
                        {
                            otvertka.print_name();
                        }
                        else
                        {
                            Console.WriteLine("Предмет не найден");
                        }
                        break;
                    case 2:
                        Item.print_avalible(items);
                        break;
                    case 3:
                        Item.damage_status(items);
                        break;
                    default:
                        Console.WriteLine("Нет такого выбора!!!!!!!!!!!!!!!!!!");
                        break;
                }
            }

        }
    }
}