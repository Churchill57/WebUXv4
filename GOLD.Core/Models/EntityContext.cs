using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Models
{
    public class EntityContext //: IEntityContext
    {
        public EntityContext()
        {
            // for de-serialization
        }
        public EntityContext(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
            WhenSet = DateTime.Now;
        }
        public override string ToString()
        {
            return $"{Id}|{Name}|{Description}";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public bool IsCurrent => (SingletonService.Instance.EntityContextManager.GetCurrentContext == this);
        public DateTime WhenSet { get; internal set; }
    }
}
