using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class GoldDomainAttribute : Attribute
    {
        public GoldDomainAttribute(string guid)
        {
            Guid = guid;
        }
        public string Guid { get; }

    }
}
