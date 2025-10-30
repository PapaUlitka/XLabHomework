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
            var a = new Ghost();
            a.TakeDamage(50);
            a.ShowInfo();

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
                        monsters[monsters.Count - 1].TakeDamage(10);
                        break;
                    case "5":
                        monsters[r.Next(1, monsters.Count)].TakeDamage(10);
                        break;
                    case "6":

                        break;
                    case "7":

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
        }
        public class Skelet : Monster
        {
            public Skelet(string name = "Oleg", string type = "skelet", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Zombie : Monster
        {
            public Zombie(string name = "Vlad", string type = "zombie", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} ходит";
        }
        public class Ghost : Monster
        {
            public Ghost(string name = "Kirill", string type = "ghost", int health = 100)
            {
                this.Name = name;
                this.Type = type;
                this.Health = health;
            }
            public override string Move() => $"{Name} летает";
        }
        public class Person
        {
            public int Age {  get; set; }
            public string Name { get; set; }
            public string Surname {  get; set; }
            public string FullName => $"{Name} {Surname}";
            public virtual void DoSomething()
            {
                Console.WriteLine("Doing something");
            }
            public override string ToString() => FullName;
        }
        public class Citizen : Person
        {
            public int CitizenId {  get; set; }

            public override void DoSomething()
            {
                Console.WriteLine("Doing something Citizen");
            }
            public override string ToString()
            {
                return base.ToString() + " " + CitizenId;
            }
        }
        public class Employee : Person
        {
            public int EmployeeID {  get; set; }
            public override void DoSomething()
            {
                Console.WriteLine("Doing something Employee");
            }
            public override string ToString()
            {
                return base.ToString() + " " + EmployeeID;
            }
        }
    }
}
