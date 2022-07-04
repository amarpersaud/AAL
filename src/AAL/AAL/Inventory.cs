﻿using AAL.Resources;
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
        public bool HasResource(Resource res)
        {
            return Resources.Where(x => x.Key == res.ResourceId).Count() > 0;
        }

        public int GetAmount(int ResourceId)
        {
            if (!HasResource(ResourceId))
            {
                return 0;
            }
            return Resources[ResourceId];
        }

        public int GetAmount(Resource res)
        {
            if (!HasResource(res))
            {
                return 0;
            }
            return Resources[res.ResourceId];
        }

    }
}
