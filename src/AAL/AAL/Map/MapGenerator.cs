using MonoGame.CExt.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AAL.Resources;

namespace AAL.Map
{
    public class MapGenerator
    {
        private Random r = new Random();

        #region Parameters

        public int MinRegions { get; set; } = 4;
        public int MaxRegions { get; set; } = 10;
        public int MinLocationsPerRegion { get; set; } = 2;
        public int MaxLocationsPerRegion { get; set; } = 6;

        #endregion Parameters

        public WorldMap GenerateRandomMap(GameManager gm)
        {
            WorldMap m = new WorldMap();
            m.Regions = new List<Region>();

            List<string> PlaceNames = gm.rh.LoadJsonObject<List<string>>("JSON/PlaceNames.json", gm.JsonSettings);

            int numRegions = r.Next(MinRegions, MaxRegions);

            for (int i = 0; i < numRegions; i++)
            {
                Region reg = new Region();
                reg.Name = PlaceNames.Random();

                int numLocations = r.Next(MinLocationsPerRegion, MaxLocationsPerRegion);

                for(int j = 0; j < numLocations; j++)
                {
                    MapLocation ml = new MapLocation();
                    ml.LocationName = PlaceNames.Random();
                    ml.ResourcesNeeded = new List<ResourceRate>();
                    //Generate resource usage and production 
                    reg.Locations.Add(ml);
                }

                m.Regions.Add(reg);
            }

            return m;
        }
    }
}
