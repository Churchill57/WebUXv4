//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ImpromptuInterface;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace GOLD.Core.Tests
//{

//    public class Wrapper<T, IData> : DynamicObject where T : class where IData : class
//    {
//        private static T _subject;

//        //public Wrapper(T subject)
//        //{
//        //    _subject = subject;
//        //}

//        public static I As<I, I2>(T subject) where I : class where I2 : class
//        {
//            if (!typeof(I).IsInterface)
//                throw new ArgumentException("I must be an interface type!");

//            //if (!typeof(IData).IsInterface)
//            //    throw new ArgumentException("IData must be an interface type!");

//            _subject = subject;

//            return new Wrapper<T, I2>().ActLike<I>();
//        }

//        public override bool TryGetMember(GetMemberBinder binder, out object result)
//        {
//            var text = $"{_subject.GetType().Name} - {binder.Name}";

//            result = typeof(T).GetProperty(binder.Name).GetValue(_subject, null);
//            if (result.GetType() == typeof(JObject))
//            {
//                //result = result.ActLike<IData>();
//                result = JsonConvert.DeserializeObject<IData>(result.ToString());
//            }
//            return true;
//        }

//    }


//    //public class CustomOutcomeData
//    //{
//    //    public List<string> List { get; set; } = new List<string>();
//    //}
//    //public class CustomOutcome : Outcome<CustomOutcomeData>
//    //{
//    //}


//    public class CustomOutcomeData : ICustomOutcomeData
//    {
//        public Dictionary<string, object> Items { get; set; }
//    }
//    public interface ICustomOutcomeData
//    {
//        Dictionary<string, object> Items { get; set; }
//    }

//    //public interface ICustomOutcomeData : IDictionary<string, object>
//    //{

//    //}

//    public class Outcome<T> : Outcome, IOutcome<T> where T : class //, new()
//    {
//        public Outcome()
//        {
//            DataName = typeof(T).Name;
//            //Data = new T();
//        }
//        //public T Data { get; set; }
//    }

//    public interface IOutcome<T> : IOutcome
//    {

//    }

//    public class Outcome : CustomOutcomeData
//    {
//        public string DataName { get; set; }
//        public int SourceExecutionID { get; set; }
//        public int TargetExecutionID { get; set; }
//        public object Data { get; set; }
//    }

//    public interface IOutcome
//    {
//        string DataName { get; }
//        int SourceExecutionID { get; set; }
//        int TargetExecutionID { get; set; }
//        object Data { get; set; }
//    }

//}
