using System;
using System.Collections.Immutable;

//0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811
namespace fibonacci
{
    internal class Fibonacci
    {
        public static void Main(string[] args)
        {
            // Imperative, mutable, and side-effecting
            void Fibs1(int len)
            {
                int a = 0;
                int b = 1;
                int i = 0;
                while (i < len)
                {
                    // Can we reorder any of these statements?
                    // Can we inline any of them?
                    // What refactorings are safe?
                    Console.Write(a);
                    if (i + 1 < len) Console.Write(", ");
                    int c = a + b;
                    a = b;
                    b = c;
                    i++;
                }
            }

            Fibs1(20);

            Console.WriteLine();

            // Separate out the side effects
            int[] Fibs2(int len)
            {
                int a = 0;
                int b = 1;
                int[] array = new int[len];
                int i = 0;
                while (i < len)
                {
                    array[i] = a;
                    int c = a + b;
                    a = b;
                    b = c;
                    i++;
                }
                return array;
            }

            Console.WriteLine(String.Join(", ", Fibs2(20)));

            // Move mutability to the top: variables are not themselves mutable
            ImmutableList<int> Fib3(int len)
            {
                int a = 0;
                int b = 1;
                ImmutableList<int> list = ImmutableList<int>.Empty;
                int i = 0;
                while (i < len)
                {
                    list = list.Add(a);
                    int c = a + b;
                    a = b;
                    b = c;
                    i++;
                }
                return list;
            }

            Console.WriteLine(String.Join(", ", Fib3(20)));

            // Convert to functional
            // 1. State we need to read inside the loop becomes a parameter
            // 2. State we need to read after the loop becomes the return value
            // 3. The looping condition determines whether to continue recursing
            // 4. Initial state becomes arguments to outer call, or argument defaults
            ImmutableList<int> Fib4(int len)
            {
                ImmutableList<int> Loop(ImmutableList<int> list, int i = 0, int a = 0, int b = 1)
                {
                    if (i < len)
                        return Loop(list.Add(a), i + 1, b, a + b);
                    else
                        return list;
                }

                return Loop(ImmutableList<int>.Empty);
            }

            Console.WriteLine(String.Join(", ", Fib4(20)));
        }
    }
}