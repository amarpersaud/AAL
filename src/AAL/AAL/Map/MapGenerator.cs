using MonoGame.CExt.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL.Map
{
    public class MapGenerator
    {
        private Random r = new Random();

        #region Parameters

        public List<string> PlaceNames = new List<string> {
            "New York",
            "London",
            "England",
            "Russia",
            "Portland",
            "Hell",
            "France"
        };


        public int MinRegions { get; set; }
        public int MaxRegions { get; set; }
        public int MinLocationsPerRegion { get; set; }
        public int MaxLocationsPerRegion { get; set; }

        #endregion Parameters

        public WorldMap GenerateRandomMap()
        {
            WorldMap m = new WorldMap();
            m.Regions = new List<Region>();
            int numRegions = r.Next(MinRegions, MaxRegions + 1);

            for (int i = 0; i < numRegions; i++)
            {
                Region reg = new Region();
                reg.Name = PlaceNames.Random();
                m.Regions.Add(reg);
            }

            return m;
        }
    }
}
