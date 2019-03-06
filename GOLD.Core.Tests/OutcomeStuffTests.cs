//using GOLD;
//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using GOLD.Core.Tests;
//using System.Collections.Generic;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using ImpromptuInterface;


//namespace GOLD.Tests
//{
//    [TestClass()]
//    public class OutcomeStuffTests
//    {
//        [TestMethod()]
//        public void CreateProxyTest1Class()
//        {

//            var outcome = new Outcome<CustomOutcomeData>();
//            outcome.SourceExecutionID = 1;
//            outcome.TargetExecutionID = 2;
//            var outcomeData = new CustomOutcomeData();
//            outcomeData.Items = new Dictionary<string, object>() {
//                ["one"] = 1,
//                ["two"] = 2
//            };
//            outcome.Data = outcomeData;

//            var json = JsonConvert.SerializeObject(outcome);

//            var outcome2 = JsonConvert.DeserializeObject(json);
//            var outcome3 = JsonConvert.DeserializeObject<Outcome>(json);
//            var outcome4 = JsonConvert.DeserializeObject<Outcome<CustomOutcomeData>>(json);

//            var outcome5 = outcome2.ActLike<IOutcome>();
//            var outcome6 = outcome2.ActLike<IOutcome<ICustomOutcomeData>>();

//            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
//            var json2 = JsonConvert.SerializeObject(outcome, settings);

//            var outcome22 = JsonConvert.DeserializeObject(json);
//            var outcome23 = JsonConvert.DeserializeObject<Outcome>(json);
//            var outcome24 = JsonConvert.DeserializeObject<Outcome<CustomOutcomeData>>(json);

//            var outcome25 = outcome22.ActLike<IOutcome>();
//            var outcome26 = outcome22.ActLike<IOutcome<ICustomOutcomeData>>();

//            //var wrapper = Wrapper<Outcome>.As<IOutcome<ICustomOutcomeData>>(outcome3);
//            //var DataName = wrapper.DataName;
//            //var SourceExecutionID = wrapper.SourceExecutionID;
//            //var TargetExecutionID = wrapper.TargetExecutionID;
//            //var shit = wrapper.Data;

//            var wrapper = Wrapper<Outcome, CustomOutcomeData>.As<IOutcome,CustomOutcomeData>(outcome3);
//            var DataName = wrapper.DataName;
//            var SourceExecutionID = wrapper.SourceExecutionID;
//            var TargetExecutionID = wrapper.TargetExecutionID;
//            var shit = wrapper.Data;



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
