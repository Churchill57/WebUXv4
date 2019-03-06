using System;
using System.Collections.Generic;
using System.Linq;
using GOLD.Core.Outcomes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace GOLD.Tests
{
    [TestClass]
    public class OutcomeTests
    {
        [TestMethod]
        public void TestMethod1()
        {

            var myOutcome = new MyOutcome();
            myOutcome.MyInteger1 = 123;
            myOutcome.SourceExecutionID = 1;
            myOutcome.TargetExecutionID = 2;
            myOutcome.Data.Add("one", "1");
            myOutcome.Data.Add("two", 2);
            myOutcome.Data.Add("three", new DateTime(2001,12,20));

            var json = JsonConvert.SerializeObject(myOutcome);

            var outcome2 = JsonConvert.DeserializeObject(json);
            var outcome3 = JsonConvert.DeserializeObject<Outcome>(json);
            var outcome4 = JsonConvert.DeserializeObject<MyOutcome>(json);


            var result = (
                from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                select t
            ).FirstOrDefault(t => t.FullName == outcome4.TypeName);

            var obj = Activator.CreateInstance(result);

            var result2 = CoreFunctions.GetType(outcome4.TypeName);


        }
    }

    public class MyOutcome : Outcome<MyOutcome>
    {
        public int MyInteger1 { get; set; }
    }




}
