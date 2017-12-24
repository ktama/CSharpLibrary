using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ExcelLibrary
{
    // 引用元：Qiita || 【C#】ClosedXMLでExcelテーブルをIEnumerable<T>オブジェクトに変換
    // https://qiita.com/penguinshunya/items/dd586b1e42b7a66e552e
    public class ExcelIO
    {
        public static void SetProperty<T>(T obj, string name, object value)
        {
            var property = typeof(T).GetProperty(name,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            property?.SetValue(obj, Convert.ChangeType(value, property.PropertyType));
        }

        public static IEnumerable<T> ConvertExcelTable<T>(IXLTable table) where T : new()
        {
            var names = table.Fields.Select(field => field.Name).ToArray();

            foreach (var row in table.DataRange.Rows())
            {
                var obj = new T();
                foreach (var name in names)
                {
                    SetProperty(obj, name, row.Field(name).Value);
                }
                yield return obj;
            }
        }
        
        public static IEnumerable<T> CreateExcelTable<T>(IXLTable table) where T : new()
        {
            var names = table.Fields.Select(field => field.Name).ToArray();

            return null;
        }
    }
}
