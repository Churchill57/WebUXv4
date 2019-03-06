using System.Collections.Generic;

namespace GOLD.Core.Tests
{
    public interface ITestClass2
    {
        string StringA { get; set; }
        IList<ITestClass1> TestClass1List { get; set; }
    }
}