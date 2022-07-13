using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL.Map
{
    public class Region
    {
        /// <summary>
        /// Region name. Randomly assigned.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of locations
        /// </summary>
        public List<MapLocation> Locations = new List<MapLocation>();

        /// <summary>
        /// List of vertices of outside edges
        /// </summary>
        public List<Vector2> Vertices;

    }
}
