using System;
using System.Collections.Generic;
using System.Text;

namespace AAL.Resources
{
    public struct ResourceRate
    {
        /// <summary>
        /// Id of Resource
        /// </summary>
        public int ResourceId;

        /// <summary>
        /// Rate of resource production or use per day
        /// </summary>
        public double RatePerDay;

        /// <summary>
        /// If output amount should be an integer (rounded) or float
        /// </summary>
        public bool IntegerAmount;

    }
}
