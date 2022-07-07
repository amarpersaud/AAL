using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AAL.Resources;

using Microsoft.Xna.Framework;

namespace AAL.Map
{
    public class MapLocation
    {
        /// <summary>
        /// Name of location
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Location within the map
        /// </summary>
        public Vector2 Location { get; private set; }

        /// <summary>
        /// Person representing this location
        /// </summary>
        public Person Representative { get; set; }

        /// <summary>
        /// List of resources produced by this location
        /// </summary>
        public Dictionary<int, int> ResourcesProduced = new Dictionary<int, int>();

        /// <summary>
        /// Dictionary of resources needed by this location
        /// </summary>
        public Dictionary<int, int> ResourcesNeeded = new Dictionary<int, int>();

    }
}
