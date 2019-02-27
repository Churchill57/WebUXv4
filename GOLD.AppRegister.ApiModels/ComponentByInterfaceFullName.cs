using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.AppRegister.ApiModels
{
    public class ComponentByInterfaceFullName
    {

        public int ID { get; set; }
        public string InterfaceFullname { get; set; }
        public string Title { get; set; }
        public bool IsPrimaryApp { get; set; }
        public bool IsSecondaryApp { get; set; }
        public string PrimaryAppRoute { get; set; }
        public string SecondaryAppRoute { get; set; }
        public System.Guid DomainID { get; set; }
        public string DomainName { get; set; }

    }
}
