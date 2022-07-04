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
        public Dictionary<int, int> Resources = new Dictionary<int, int>();

        public bool HasResource(int ResourceId)
        {
            return Resources.Where(x => x.Key == ResourceId).Count() > 0;
        }
        public int GetAmount(int ResourceId)
        {
            if (!HasResource(ResourceId))
            {
                return 0;
            }
            return Resources[ResourceId];
        }
    }
}
