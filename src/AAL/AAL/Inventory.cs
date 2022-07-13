using AAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL
{
    public class Inventory
    {
        /// <summary>
        /// Amount of money
        /// </summary>
        public double Money { get; set; }

        /// <summary>
        /// Resource amounts: (Resource Id, Resource amount).
        /// </summary>
        public Dictionary<int, double> Resources = new Dictionary<int, double>();

        /// <summary>
        /// Check if inventory contains resource
        /// </summary>
        /// <param name="ResourceId">Resource Id of resource to check if inventory contains</param>
        /// <returns>true if inventory contains resource</returns>
        public bool HasResource(int ResourceId)
        {
            return Resources.Where(x => x.Key == ResourceId).Count() > 0;
        }

        /// <summary>
        /// Check if inventory contains resource
        /// </summary>
        /// <param name="res">Resource to check if inventory contains</param>
        /// <returns>true if inventory contains resource</returns>
        public bool HasResource(Resource res)
        {
            return Resources.Where(x => x.Key == res.Id).Count() > 0;
        }

        /// <summary>
        /// Get quantity of resource 
        /// </summary>
        /// <param name="ResourceId">Id of resource to get amount of</param>
        /// <returns>Quantity of resource</returns>
        public double GetAmount(int ResourceId)
        {
            if (!HasResource(ResourceId))
            {
                return 0;
            }
            return Resources[ResourceId];
        }

        /// <summary>
        /// Get quantity of resource 
        /// </summary>
        /// <param name="res">Resource to get amount of</param>
        /// <returns>Quantity of resource</returns>
        public double GetAmount(Resource res)
        {
            if (!HasResource(res))
            {
                return 0;
            }
            return Resources[res.Id];
        }

    }
}
