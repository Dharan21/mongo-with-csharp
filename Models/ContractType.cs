using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDemo.Models
{
    public class ContractType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Covering { get; set; }
        public string NotCovering { get; set; }
        public bool IsActive { get; set; }
    }
}
