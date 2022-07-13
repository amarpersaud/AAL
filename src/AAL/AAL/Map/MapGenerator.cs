﻿using MonoGame.CExt.Extensions;

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

        public List<string> PlaceNames = new List<string> {
            "New York",
            "London",
            "England",
            "Russia",
            "Portland",
            "Hell",
            "France"
        };


        public int MinRegions { get; set; } = 4;
        public int MaxRegions { get; set; } = 10;
        public int MinLocationsPerRegion { get; set; } = 2;
        public int MaxLocationsPerRegion { get; set; } = 6;

        #endregion Parameters

        public WorldMap GenerateRandomMap()
        {
            WorldMap m = new WorldMap();
            m.Regions = new List<Region>();
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
