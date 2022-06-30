using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL
{
    /// <summary>
    /// Class for handling in game time
    /// </summary>
    public class GameClock
    {
        /// <summary>
        /// Defines number of ticks per second of clock time
        /// </summary>
        public int TicksPerSecond { get; set; }

        /// <summary>
        /// Day of in game date
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Month of in game date
        /// </summary>
        public int Month { get; set; } 
        
        /// <summary>
        /// Year for in game date
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Hours for in game time
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Minutes for in game time
        /// </summary>
        public int Minutes { get; set; }
        /// <summary>
        /// Seconds for in game time
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Number of ticks for the current day
        /// </summary>
        public int DayTicks { get; set; }

        
    }
}
