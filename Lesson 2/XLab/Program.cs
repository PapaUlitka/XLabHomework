using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyClass<int> a = new MyClass<int>();
            a.Add(1);
            a.Add(2);
            a.Add(3);
            a.Add(4);
            Console.WriteLine("Мой список");//1234
            foreach (int i in a)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            Console.WriteLine("Добавляю элемент по индексу");//12314
            a.InsertAt(1, 3);
            foreach (int i in a)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            Console.WriteLine("Удаляю элемент по индексу");//2314
            a.RemoveAt(0);
            foreach (int i in a)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            Console.WriteLine("Удаляю элемент по значению");//214
            a.Remove(3);
            foreach (int i in a)
            {
                Console.Write(i);
            }
        }

        public class MyClass<T> : IEnumerable
        {
            private T[] array;
            private int size;
            private int capacity;
            public IEnumerator GetEnumerator() => array.GetEnumerator();

            public MyClass(int capacity = 8)
            {
                this.capacity = capacity;
                array = new T[capacity];
            }
            public void Resize()
            {
                T[] resized = new T[capacity * 2];
                for (int i = 0; i < capacity; i++)
                {
                    resized[i] = array[i];
                }
                array = resized;
                capacity *= 2;
            }
            public void Add(T x) 
            {
                if (size == capacity)
                {
                    Resize();
                }
                array[size] = x;
                size++;
            }

            public void InsertAt(T x, int index) 
            { 
                if (size == capacity)
                {
                    Resize();
                }
                for (int i = size; i > index; i--)
                {
                    array[i] = array[i-1];
                }
                array[index] = x;
                size++;
            }
            public void Remove(T x) 
            {
                int index = Array.IndexOf(array, x);
                for (int i = index; i < size - 1; i++)
                {
                    array[i] = array[i + 1];
                }

               
                array[size - 1] = default(T);
                size--;
            }

            public void RemoveAt(int index) 
            {
                for (int i = index;  i < size-1; i++)
                {
                    array[i] = array[i + 1];
                }
                array[size-1] = default(T);
                size--;
            }

            public void Clear() 
            { 
                array = new T[capacity];
                size = 0;
            }
        }
    }
}
