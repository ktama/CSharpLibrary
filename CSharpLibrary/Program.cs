using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var testData1 = new Person()
            {
                Name = "ABC",
                Age = 20,
                Heigth = 170.0
            };
            var testData2 = new Person()
            {
                Name = "ABC",
                Age = 20,
                Heigth = 170.0
            };
            var testData3 = new Person()
            {
                Name = "XYZ",
                Age = 20,
                Heigth = 170.0
            };
            var testData4 = new Person()
            {
                Name = "XYZ",
                Age = 30,
                Heigth = 180.0
            };

            var result1 = DiffLibrary.Diff(testData1, testData1);
            var result2 = DiffLibrary.Diff(testData1, testData2);
            var result3 = DiffLibrary.Diff(testData1, testData3);
            var result4 = DiffLibrary.Diff(testData1, testData4);

            Console.WriteLine($"1:{result1}");
            Console.WriteLine($"2:{result2}");
            Console.WriteLine($"3:{result3}");
            Console.WriteLine($"4:{result4}");

            Console.ReadLine();
        }
    }
}
