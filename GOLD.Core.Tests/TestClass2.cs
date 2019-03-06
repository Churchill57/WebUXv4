using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Tests
{
    public class TestClass2 : ITestClass2
    {
        public string StringA { get; set; }
        public IList<ITestClass1> TestClass1List { get; set; }
    }
}
