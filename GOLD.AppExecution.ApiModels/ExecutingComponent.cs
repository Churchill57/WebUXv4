using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppExecution.ApiModels
{
    public class ExecutingComponent
    {
        public int ExecutingID { get; set; } // Was TaskId in WebUXv2
        public string InterfaceFullname { get; set; }
        public string Title { get; set; }
        public string URL { get; set; } // e.g. https://ww.TestDomain.MVC/Tests/test1/123.2/
        public string Breadcrumb { get; set; }

        public int ParentExecutingID { get; set; }
        public string ClientRef { get; set; } // A simple differentiating label used by a parent component when using a child component it depends upon..

        public Dictionary<string, object> State { get; set; }
    }
}
