using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppExecution.ApiModels
{
    public class ExecutingComponent
    {
        public int ExecutingID { get; set; }
        public int ParentExecutingID { get; set; }
        public string ComponentName { get; set; }
        public string Breadcrumb { get; set; }
        public string Title { get; set; }
        public string ClientRef { get; set; }
        public int ComponentID { get; set; }
        public int ParentComponentID { get; set; }
        public string State { get; set; }
    }
}
