using System.Collections.Generic;
using System.Linq;
using CSharpLibrary.CreateTypeLibrary;

namespace CSharpLibrary.DiffLibrary
{
    public class DiffModel
    {
        public static bool DiffList<T>(List<T> xList, List<T> yList)
        {
            var isListEqual = true;

            foreach (var pair in Enumerable.Zip(xList, yList, (x, y) => new { x, y }))
            {
                var isEqual = Diff(pair.x, pair.y);
                if(!isEqual)
                {
                    isListEqual = false;
                }
            }

            return isListEqual;
        }

        public static bool Diff<T>(T classX, T classY)
        {
            var isClassEqual = true;
            var properties = classX.GetType().GetProperties();
            var diffClassInfo = new List<BaseDiffData>();

            foreach (var property in properties)
            {
                var x = property.GetValue(classX);
                var y = property.GetValue(classY);
                var diffPropertyInfo = new BaseDiffData()
                {
                    Name = property.Name,
                    xValue = x,
                    yValue = y,
                    ValueType = property.PropertyType,
                    IsEqual = x.Equals(y)
                };

                if (!diffPropertyInfo.IsEqual)
                {
                    isClassEqual = false;
                }

                diffClassInfo.Add(diffPropertyInfo);
            }
            var diffClass = CreateDiffClass(diffClassInfo);

            return isClassEqual;
        }

        private static CreateClass CreateDiffClass(List<BaseDiffData> classInfo)
        {
            var properties = classInfo.ToDictionary(info => "X" + info.Name, info => info.ValueType);
            properties = properties.Concat(classInfo.ToDictionary(info => "Y" + info.Name, info => info.ValueType)).ToDictionary(x => x.Key, y => y.Value);
            properties = properties.Concat(classInfo.ToDictionary(info => "Is" + info.Name, info => info.IsEqualType)).ToDictionary(x => x.Key, y => y.Value);
            var diffClass = new CreateClass("PersonClass", properties);
            // Set
            foreach (var property in classInfo)
            {
                diffClass.SetProperty("X" + property.Name, property.xValue, property.ValueType);
                diffClass.SetProperty("Y" + property.Name, property.yValue, property.ValueType);
                diffClass.SetProperty("Is" + property.Name, property.IsEqual, property.IsEqualType);
            }

            // Get
            foreach (var property in classInfo)
            {
                var x = diffClass.GetProperty("X" + property.Name, property.ValueType);
                var y = diffClass.GetProperty("Y" + property.Name, property.ValueType);
                var isEqual = diffClass.GetProperty("Is" + property.Name, property.IsEqualType);
            }

            return diffClass;
        }
    }
}
