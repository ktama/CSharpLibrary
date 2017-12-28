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
                Heigth = 170.0,
                ID = "0x01"
            };
            var testData2 = new Person()
            {
                Name = "ABC",
                Age = 20,
                Heigth = 170.0,
                ID = "0x01"
            };
            var testData3 = new Person()
            {
                Name = "XYZ",
                Age = 20,
                Heigth = 170.0,
                ID = "0x02"
            };
            var testData4 = new Person()
            {
                Name = "XYZ",
                Age = 30,
                Heigth = 180.0,
                ID = "0x02"
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

            var listResultOut1 = DiffModel.DiffList(list1, list1, out var diffResult1);
            var listResultOut2 = DiffModel.DiffList(list1, list2, out var diffResult2);
            var listResultOut3 = DiffModel.DiffList(list1, list3, out var diffResult3);
            var listResultOut4 = DiffModel.DiffList(list1, list4, out var diffResult4);



            var testData5 = new Person()
            {
                Name = "XYZ",
                Age = 30,
                Heigth = 180.0,
                ID = "0x05"
            };
            var testData6 = new Person()
            {
                Name = "XYZ",
                Age = 30,
                Heigth = 185.0,
                ID = "0x06"
            };
            var testData7 = new Person()
            {
                Name = "XYZ",
                Age = 35,
                Heigth = 190.0,
                ID = "0x07"
            };
            var testData8 = new Person()
            {
                Name = "XYZ",
                Age = 40,
                Heigth = 200.0,
                ID = "0x08"
            };
            var list5 = new List<Person>()
            {
                testData5,
                testData6,
            };
            var list6 = new List<Person>()
            {
                testData6,
                testData7,
            };
            var list7 = new List<Person>()
            {
                testData7,
                testData8,
            };
            IEnumerable<Person> p1List, p2List, p3List, p4List;
            SortLibrary.SortList.GetSortedHashList(list5, list6, "ID", out p1List, out p2List);
            SortLibrary.SortList.GetSortedHashList(list5, list7, "ID", out p3List, out p4List);
            Console.ReadLine();
        }
    }
}
