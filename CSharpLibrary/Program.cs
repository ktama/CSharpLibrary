using System;
using System.Collections.Generic;
using CSharpLibrary.TestModel;
using CSharpLibrary.DiffLibrary;

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

            var result1 = DiffModel.Diff(testData1, testData1);
            var result2 = DiffModel.Diff(testData1, testData2);
            var result3 = DiffModel.Diff(testData1, testData3);
            var result4 = DiffModel.Diff(testData1, testData4);

            Console.WriteLine($"1:{result1}");
            Console.WriteLine($"2:{result2}");
            Console.WriteLine($"3:{result3}");
            Console.WriteLine($"4:{result4}");

            var list1 = new List<Person>()
            {
                testData1,
                testData2,
                testData3,
                testData4,
            };
            var list2 = new List<Person>()
            {
                testData1,
                testData2,
                testData3,
                testData4,
            };
            var list3 = new List<Person>()
            {
                testData1,
                testData1,
                testData3,
                testData4,
            };
            var list4 = new List<Person>()
            {
                testData4,
                testData3,
                testData2,
                testData1,
            };

            var listResult1 = DiffModel.DiffList(list1, list1);
            var listResult2 = DiffModel.DiffList(list1, list2);
            var listResult3 = DiffModel.DiffList(list1, list3);
            var listResult4 = DiffModel.DiffList(list1, list4);

            Console.WriteLine($"1:{listResult1}");
            Console.WriteLine($"2:{listResult2}");
            Console.WriteLine($"3:{listResult3}");
            Console.WriteLine($"4:{listResult4}");
            Console.ReadLine();
        }
    }
}
