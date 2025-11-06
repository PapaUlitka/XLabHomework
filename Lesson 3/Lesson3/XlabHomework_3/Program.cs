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
                        int input1 = Convert.ToInt32(Console.ReadLine());
                        monsters[input1].TakeDamage(100);
                        break;
                    case "5":
                        monsters[r.Next(0, monsters.Count)].TakeDamage(100);
                        break;
                    case "6":
                        foreach (Monster monster in monsters)
                        {
                            Console.WriteLine(monster.Type + " " + monster.Name);
                        }
                        int input2 = Convert.ToInt32(Console.ReadLine());
                        monsters[input2].UpgradeMonster();
                        break;
                    case "7":
                        foreach (Monster monster in monsters)
                        {
                            Console.WriteLine(monster.Type + " " + monster.Name);
                        }
                        int input3 = Convert.ToInt32(Console.ReadLine());
                        monsters.RemoveAt(input3);
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
            private int _armor;
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
            public int Armor 
            {
                get
                {
                    return _armor;
                }
                set
                {
                    _armor = value;
                }
            }
            public void TakeDamage(int damage)
            {           
                _health -= damage - _armor;
                _armor -= damage;
                if (_armor <= 0) _armor = 0;
                if (_health <= 0) Console.WriteLine($"Монстр {Type} {Name} погиб");
            }
            public virtual string Move() => $"{Name} передвигается";

            public void ShowInfo()
            {
                Console.WriteLine("######################################");
                Console.WriteLine($"Имя: {Name}\nТип: {Type}\nЗдоровье: {Health}\nБроня: {Armor}");
                Console.WriteLine("######################################");
            }
            public void UpgradeMonster()
            {
                _armor += 20;
            }
        }
        public class Skelet : Monster
        {
            public Skelet(string name = "Oleg", string type = "скелет", int health = 100, int armor = 0)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
                this.Armor = armor;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Zombie : Monster
        {
            public Zombie(string name = "Vlad", string type = "зомби", int health = 100, int armor = 0)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
                this.Armor = armor;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Ghost : Monster
        {
            public Ghost(string name = "Kirill", string type = "призрак", int health = 100, int armor = 0)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
                this.Armor = armor;
            }
            public override string Move() => $"{Name} летает";
        }
    }
}
