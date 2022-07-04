using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL
{
    public class Inventory
    {
        public int Money { get; set; }
        public List<Resource> Resources = new List<Resource>();

        public bool HasResource(string ResourceName)
        {
            return Resources.Where(x => x.ResourceName == ResourceName).Count() > 0;
        }
        public Resource GetResource(string ResourceName)
        {
            return Resources.Where(x => x.ResourceName == ResourceName).FirstOrDefault();
        }
    }
}
