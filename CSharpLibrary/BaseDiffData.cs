using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary
{
    public class BaseDiffData
    {
        public string Name { get; set; }
        public object xValue { get; set; }
        public object yValue { get; set; }
        public Type ValueType { get; set; }
        public bool IsEqual { get; set; }
        public Type IsEqualType
        {
            get
            {
                return typeof(bool);
            }
        }
    }
}
