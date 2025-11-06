namespace Lesson_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var monsters = new List<Monster>();
            Console.WriteLine("Welcome to XLab Diablo\n");
            while (true)
            {
                Console.WriteLine("Choose option:");
                Console.WriteLine("1. Add Skillet");
                Console.WriteLine("2. Take Damage to the first monster");
                Console.WriteLine("3. Upgrade leather the first monster");
                Console.WriteLine("4. Upgrade iron the first monster");
                Console.WriteLine("5. Exit");
                Console.WriteLine("9. Info");
                Console.Write("Enter your choice: ");
                var input = ReadInput();
                Console.Clear();
                if (input is "1") AddSkillet(monsters);
                else if (input is "2") TakeDamageToMonster(monsters[0]);
                else if (input is "3") UpgradeLeatherMonster(monsters[0]);
                else if (input is "4") UpgradeIronMonster(monsters[0]);
                else if (input is "9") Info();
                else if (input is "5")
                {
                    break;
                }
            }
            void Info()
            {
                foreach (var monster in monsters)
                {
                    Console.WriteLine($"{monster.Name} {monster.Hp}");
                }
            }
        }
        private static void AddSkillet(List<Monster> monsters) =>
            monsters.Add(new Skelet(100, $"Monster {monsters.Count + 1}"));
        private static void TakeDamageToMonster(Monster monster)
        {
            Console.Write("Enter damage: ");
            var input = ReadInput();
            if (int.TryParse(input, out var damage))
            {
                var oldHp = monster.Hp;
                monster.TakeDamage(damage);
                var newHp = monster.Hp;
                Console.WriteLine($"{monster.Name} took {damage}. Hp: {oldHp} -> {newHp}");
            }
            else
            {
                Console.WriteLine($"Invalid damage {input}");
            }
        }
        private static void UpgradeLeatherMonster(Monster monster)
        {
            var health = monster.HealthComponent;
            bool hasArmor = HasArmor(health);
            if (!hasArmor)
            {
                health = new MissEffectHealth(25, health);
                health = new LeatherArmorHealth(health);
            }
            monster.HealthComponent = health;
        }
        private static void UpgradeIronMonster(Monster monster)
        {
            var health = monster.HealthComponent;
            bool hasArmor = HasArmor(health);
            if (!hasArmor)
            {
                health = new IronArmorHealth(health);
            }

            monster.HealthComponent = health;
        }
        private static bool HasArmor(Health health)
        {
            var current = health;
            while (current is HealthDecorator decorator)
            {
                if (decorator is IIsArmor) return true;
                current = decorator.Decorable;
            }
            return false;
        }
        private static string ReadInput() =>
            Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
    }
    public interface IDamageable
    {
        public void TakeDamage(int damage);
    }
    public abstract class Monster : IDamageable
    {
        private Health _health;
        public int Hp => _health.Value;
        public Health HealthComponent
        {
            get => _health;
            set => _health = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string Name { get; set; }
        protected Monster(int hp, string name = "Noname")
            : this(new Health(hp), name) { }
        protected Monster(Health health, string name = "Noname")
        {
            Name = name;
            HealthComponent = health;
        }
        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
    }
    public class Health : IDamageable
    {
        private int _value;
        public int Value
        {
            get => _value;
            protected set
            {
                _value = value > 0 ? value : 0;
            }
        }
        public Health(int hp)
        {
            _value = hp >= 0
                ? hp
                : throw new ArgumentException($"Hp can't be negative {hp}", nameof(hp));
        }
        public virtual void TakeDamage(int damage)
        {
            Value -= Math.Max(0, damage);
        }
    }
    public class Skelet : Monster
    {
        public Skelet(int hp, string name = "Noname")
        : base(hp, name) { }

        public Skelet(Health health, string name = "Noname")
            : base(health, name) { }
    }
    public abstract class HealthDecorator : Health
    {
        protected internal readonly Health Decorable;

        protected HealthDecorator(Health decorable)
            : base(decorable.Value)
        {
            Decorable = decorable ?? throw new ArgumentNullException(nameof(decorable));
        }
        public sealed override void TakeDamage(int damage)
        {
            Decorable.TakeDamage(AffectDamage(damage));
            Value = Decorable.Value;
        }

        protected abstract int AffectDamage(int damage);
    }
    public sealed class LeatherArmorHealth : HealthDecorator, IIsArmor
    {
        private readonly int _armor;

        public LeatherArmorHealth(Health decorator) : base(decorator)
        {
            _armor = 25;
        }

        protected override int AffectDamage(int damage)
        {
            return damage - _armor;
        }
    }
    public sealed class MissEffectHealth : HealthDecorator
    {
        private readonly int _effect;

        public MissEffectHealth(int effect, Health decorable) : base(decorable)
        {
            _effect = Math.Clamp(effect, 0, 100);
        }

        protected override int AffectDamage(int damage)
        {
            return (int)(damage * ((100 - _effect) / 100f));
        }
    }
    public sealed class IronArmorHealth : HealthDecorator, IIsArmor
    {
        private readonly int _armor;
        public bool HasIronArmor = true;
        public IronArmorHealth(Health decorable) : base(decorable)
        {
            _armor = 50;
        }
        protected override int AffectDamage(int damage)
        {
            return damage - _armor;
        }
    }
    public interface IIsArmor
    {

    }
}
