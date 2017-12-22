﻿using System;
using System.Collections.Generic;
using System.Linq;
using CSharpLibrary.CreateTypeLibrary;

namespace CSharpLibrary.DiffLibrary
{
    public class DiffModel
    {
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

        private static object CreateDiffClass(List<BaseDiffData> classInfo)
        {
            var properties = classInfo.ToDictionary(info => "X" + info.Name, info => info.ValueType);
            properties = properties.Concat(classInfo.ToDictionary(info => "Y" + info.Name, info => info.ValueType)).ToDictionary(x => x.Key, y => y.Value);
            properties = properties.Concat(classInfo.ToDictionary(info => "Is" + info.Name, info => info.IsEqualType)).ToDictionary(x => x.Key, y => y.Value);
            var diffClass = new CreateType("PersonClass", properties);
            var diffInstane = diffClass.NewInstance();
            foreach (var property in classInfo)
            {
                CreateType.SetProperty(diffInstane, "X" + property.Name, property.xValue, property.ValueType);
                CreateType.SetProperty(diffInstane, "Y" + property.Name, property.yValue, property.ValueType);
                CreateType.SetProperty(diffInstane, "Is" + property.Name, property.IsEqual, property.IsEqualType);
            }

            foreach (var property in classInfo)
            {
                var x = CreateType.GetProperty(diffInstane, "X" + property.Name, property.ValueType);
                var y = CreateType.GetProperty(diffInstane, "Y" + property.Name, property.ValueType);
                var isEqual = CreateType.GetProperty(diffInstane, "Is" + property.Name, property.IsEqualType);
                Console.WriteLine($"DIFF {isEqual}, x:{x}, y:{y}");
            }

            return diffInstane;
        }
    }
}