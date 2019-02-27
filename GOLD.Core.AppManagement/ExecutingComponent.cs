using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement
{
    public class ExecutingComponent
    {
        public int ExecutingID { get; set; } // Was TaskId in WebUXv2
        public int ParentExecutingID { get; set; }
        public string ComponentName { get; set; }
        public string Breadcrumb { get; set; }
        public string Title { get; set; }
        public string ClientRef { get; set; } // A simple differentiating label used by a parent component when using a child component it depends upon..
        public int ComponentID { get; set; } // Identifies the registered component being executed. NEEDED? REALLY?
        public int ParentComponentID { get; set; } // Identifies the registered parent component. NEEDED? REALLY?
        public Dictionary<string, object> State { get; set; }
        public string URL { get; set; } // e.g. https://ww.TestDomain.MVC/Tests/test1/123.2/
    }
}
