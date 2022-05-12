using System;

namespace Synergie
{
    class Program
    {
        static void Main(string[] args)
        {
            // C# 7.0
            int SumOne(int a)
            {
                return a + 1;
            }
            
            // C# 8.0
            static int SumOneS(int a)
            {
                return a + 1;
            }


            Console.WriteLine("Hello World!");
            var r = SumOne(1);
            int? a = null;
            Test(a);
            
            
            Console.ReadKey();
        }
        
        public static void Test(int? a) => Console.WriteLine(a);
    }
}