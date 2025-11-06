using System.Collections.Generic;

namespace Lesson5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDamagable<Zombie> zombie = new Stack<Monster>();
            zombie.TakeDamage(new Zombie());
        }
    }
    public abstract class Monster { }
    public class Zombie : Monster { }
    public interface IDamagable<in T> where T : Monster
    {
        void TakeDamage(T damage);
    }
    class Stack<T> : IDamagable<T> where T : Monster
    {
        public void TakeDamage(T damage)
        {

        }
    }
}
