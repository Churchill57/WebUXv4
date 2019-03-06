//using GOLD;
//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using GOLD.Core.Tests;
//using System.Collections.Generic;
//using System.Linq;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json;
//using ImpromptuInterface;

//namespace GOLD.Tests
//{
//    [TestClass()]
//    public class CoreFunctionsTests
//    {
//        [TestMethod()]
//        public void CreateProxyTest1Class()
//        {
//            var proxy = CoreFunctions.CreateProxy<ITestClass1>();
//            proxy.Integer1 = 1;
//            proxy.String1 = "hello world";

//            var proxyJson = CoreFunctions.ProxyAsJson(proxy);
//            var proxy2 = CoreFunctions.CreateProxy<ITestClass1>(proxyJson);

//            Assert.AreEqual(proxy.Integer1, proxy2.Integer1);
//            Assert.AreEqual(proxy.String1, proxy2.String1);
//        }

//        [TestMethod()]
//        public void CreateProxyTest2Class()
//        {
//            var proxy = CoreFunctions.CreateProxy<ITestClass2>();
//            proxy.StringA = "string A";
//            proxy.TestClass1List = new List<ITestClass1>()
//            {
//                new TestClass1 {Integer1 = 1, String1 = "hello world"},
//                new TestClass1 {Integer1 = 2, String1 = "hello world 2"}
//            };

//            var proxyJson = CoreFunctions.ProxyAsJson(proxy);
//            var proxy2 = CoreFunctions.CreateProxy<ITestClass2>(proxyJson);

//            Assert.AreEqual(proxy.StringA, proxy2.StringA);
//            Assert.AreEqual(proxy.TestClass1List.Count, proxy2.TestClass1List.Count);
//            Assert.AreEqual(proxy.TestClass1List[0].Integer1, proxy2.TestClass1List[0].Integer1);
//            Assert.AreEqual(proxy.TestClass1List[0].String1, proxy2.TestClass1List[0].String1);
//            Assert.AreEqual(proxy.TestClass1List[1].Integer1, proxy2.TestClass1List[1].Integer1);
//            Assert.AreEqual(proxy.TestClass1List[1].String1, proxy2.TestClass1List[1].String1);


//            proxy2.TestClass1List[0].Integer1 = 2001;
//            proxy2.TestClass1List[0].String1 = "wow matey";
//            proxy2.TestClass1List[1].Integer1 = 2002;
//            proxy2.TestClass1List[1].String1 = "wow matey 2";
//            var proxyJson2 = CoreFunctions.ProxyAsJson(proxy2);

//            var proxy3 = CoreFunctions.CreateProxy<ITestClass2>(proxyJson2);

//            Assert.AreEqual(proxy2.StringA, proxy3.StringA);
//            Assert.AreEqual(proxy2.TestClass1List.Count, proxy3.TestClass1List.Count);
//            Assert.AreEqual(proxy2.TestClass1List[0].Integer1, proxy3.TestClass1List[0].Integer1);
//            Assert.AreEqual(proxy2.TestClass1List[0].String1, proxy3.TestClass1List[0].String1);
//            Assert.AreEqual(proxy2.TestClass1List[1].Integer1, proxy3.TestClass1List[1].Integer1);
//            Assert.AreEqual(proxy2.TestClass1List[1].String1, proxy3.TestClass1List[1].String1);

//        }

//        [TestMethod()]
//        public void CreateProxyTest3Class()
//        {
//            var proxy = CoreFunctions.CreateProxy<ITestClass3>();
//            proxy.StringC = "string C";
//            proxy.TestClass1Dictionary = new Dictionary<string, object>()
//            {
//                ["one"] = new TestClass1 {Integer1 = 1, String1 = "hello world"},
//                ["two"] = new TestClass1 {Integer1 = 2, String1 = "hello world 2"}
//            };

//            var proxyJson = CoreFunctions.ProxyAsJson(proxy);

//            //var deserializedOriginal = JsonConvert.DeserializeObject(proxyJson);
//            //var dictB = JObject.Parse(proxyJson).SelectToken("TestClass1Dictionary");//.ToObject<Dictionary<string, object>>();
//            //var xyx = deserializedOriginal.ActLike<ITestClass3>();



//            var proxy2 = CoreFunctions.CreateProxy<ITestClass3>(proxyJson);

//            var dictA = JObject.Parse(proxyJson).SelectToken("TestClass1Dictionary").ToObject<Dictionary<string, object>>();
//            proxy2.TestClass1Dictionary = dictA;

//            var dict = new Dictionary<string, object>();
//            foreach (KeyValuePair<string, object> x in proxy2.TestClass1Dictionary)
//            {
//                dict.Add(x.Key, x.Value);
//            }
//            proxy2.TestClass1Dictionary = dict;

//            Assert.AreEqual(proxy.StringC, proxy2.StringC);
//            ////Assert.AreEqual(proxy.TestClass1Dictionary.Count, proxy2.TestClass1Dictionary.Count);
//            //Assert.AreEqual(proxy.TestClass1Dictionary["one"].Integer1, proxy2.TestClass1Dictionary["one"].Integer1);
//            //Assert.AreEqual(proxy.TestClass1Dictionary["one"].String1, proxy2.TestClass1Dictionary["one"].String1);
//            //Assert.AreEqual(proxy.TestClass1Dictionary["two"].Integer1, proxy2.TestClass1Dictionary["two"].Integer1);
//            //Assert.AreEqual(proxy.TestClass1Dictionary["two"].String1, proxy2.TestClass1Dictionary["two"].String1);


//            //proxy2.TestClass1Dictionary["one"].Integer1 = 2001;
//            //proxy2.TestClass1Dictionary["one"].String1 = "wow matey";
//            //proxy2.TestClass1Dictionary["two"].Integer1 = 2002;
//            //proxy2.TestClass1Dictionary["two"].String1 = "wow matey 2";
//            //var proxyJson2 = CoreFunctions.ProxyAsJson(proxy2);

//            //var proxy3 = CoreFunctions.CreateProxy<ITestClass3>(proxyJson2);

//            //Assert.AreEqual(proxy2.StringC, proxy3.StringC);
//            ////Assert.AreEqual(proxy2.TestClass1Dictionary.Count, proxy3.TestClass1Dictionary.Count);
//            //Assert.AreEqual(proxy2.TestClass1Dictionary["one"].Integer1, proxy3.TestClass1Dictionary["one"].Integer1);
//            //Assert.AreEqual(proxy2.TestClass1Dictionary["one"].String1, proxy3.TestClass1Dictionary["one"].String1);
//            //Assert.AreEqual(proxy2.TestClass1Dictionary["two"].Integer1, proxy3.TestClass1Dictionary["two"].Integer1);
//            //Assert.AreEqual(proxy2.TestClass1Dictionary["two"].String1, proxy3.TestClass1Dictionary["two"].String1);

//        }

//    }
//}


////public void SerializeInterfaceProxy()
////{
////    // Create a proxy object which implements the interface IParametersTest.
////    var proxy = CoreFunctions.CreateProxy<IUxCustomerAdvSearchCriteria>();

////    // Set any properties on the proxy as required.
////    proxy.ShowSwitchToBasic = true;
////    proxy.Criteria.Age = 21;
////    proxy.Criteria.FirstName = "John";
////    proxy.Criteria.Gender = Gender.Male;
////    proxy.Criteria.NINO = "AB123456C";
////    proxy.Criteria.PostCode = "CR3 7DH";
////    proxy.Criteria.Surname = "Smith";
////    proxy.Criteria.Town = "Croydon";

////    proxy.Criterias = new List<ICustomerAdvancedSearchCriteria>();
////    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 11 });
////    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 22 });
////    proxy.Criterias.Add(new CustomerAdvancedSearchCriteria { Age = 22 });

////    var jsonToPersist = CoreFunctions.ProxyAsJson(proxy);

////    var rehydratedProxy = CoreFunctions.CreateProxy<IUxCustomerAdvSearchCriteria>(jsonToPersist);

////    // Set any properties on the proxy as required. 
////    rehydratedProxy.Criteria.Age = 19;
////    rehydratedProxy.ShowSwitchToBasic = false;
////    rehydratedProxy.Criteria.NINO = "XY123456Z";
////    rehydratedProxy.Criterias.ElementAt(0).Age = 999;
////    rehydratedProxy.Criterias.ElementAt(1).Age = 888;
////    rehydratedProxy.Criterias.ElementAt(2).Age = 777;


////}
