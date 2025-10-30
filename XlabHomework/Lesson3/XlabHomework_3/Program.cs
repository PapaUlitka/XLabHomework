using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static XlabHomework_3.Program;

namespace XlabHomework_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            var monsters = new List<Monster>();
            Random r = new Random();
            do
            {
                Console.WriteLine("Welcome to My Diablo");
                OpenMenu();
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        monsters.Add(new Skelet());
                        break;
                    case "2":
                        monsters.Add(new Zombie());
                        break;
                    case "3":
                        monsters.Add(new Ghost());
                        break;
                    case "4":

                        foreach (Monster monster in monsters)
                        {
                            Console.WriteLine(monster.Type + " " + monster.Name);
                        }


                        //Console.WriteLine("Выберите монстра");
                        //for (int i = 1; i <= monsters.Count; i++)
                        //{
                        //    Console.WriteLine("монстр: " + (8+i));
                        //    if (input == (i+8).ToString()) monsters[i].TakeDamage(50);
                        //}

                        //monsters[monsters.Count - 1].TakeDamage(10);
                        break;
                    case "5":
                        monsters[r.Next(0, monsters.Count)].TakeDamage(10);
                        break;
                    case "6":
                        //monsters[monsters.Count - 1].UpgradeMonster();
                        break;
                    case "7":
                        
                        //monsters[monsters.Count - 1].Remove();
                        break;
                    case "8":
                        foreach (var item in monsters) item.ShowInfo();
                        break;
                }
            }
            while (input.Trim().ToLower() != "выход");
        }
        private static void OpenMenu()
        {
            Console.WriteLine("Меню (выберете один из вариантов)");
            Console.WriteLine("1) Добавить скелета");
            Console.WriteLine("2) Добавить зомби");
            Console.WriteLine("3) Добавить призрака");
            Console.WriteLine("4) Нанести урон выбранному монстру");
            Console.WriteLine("5) Нанести урон случайному монстру");
            Console.WriteLine("6) Улучшить выбранного монстра");
            Console.WriteLine("7) Уничтожить выбранного монстра");
            Console.WriteLine("8) Вывести данные о всех текущих монстрах");
            Console.WriteLine("Введите <выход> для выхода");
        }
        public class Monster
        {
            private int _health;
            public int Health 
            {
                get
                {
                    return _health;
                }
                set
                {
                    _health = value;
                }
            }
            public string Name { get; set; }
            public string Type {  get; set; }
            public void TakeDamage(int damage)
            {
                _health -= damage;
            }
            public virtual string Move() => $"{Name} передвигается";

            public void ShowInfo()
            {
                Console.WriteLine($"Имя: {Name}\nТип: {Type}\nЗдоровье: {Health}");
            }
            public void UpgradeMonster()
            {
                _health += 20;
            }
        }
        public class Skelet : Monster
        {
            public Skelet(string name = "Oleg", string type = "скелет", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Zombie : Monster
        {
            public Zombie(string name = "Vlad", string type = "зомби", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Ghost : Monster
        {
            public Ghost(string name = "Kirill", string type = "призрак", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} летает";
        }
    }
}
