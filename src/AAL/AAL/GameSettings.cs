using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL
{
    public class GameSettings
    {
        /// <summary>
        /// Max FPS
        /// </summary>
        public int FPS_Max { get; set; }

        /// <summary>
        /// Resolution of game window
        /// </summary>
        public Point GameResolution { get; set; }
        
        /// <summary>
        /// Set player name
        /// </summary>
        public string PlayerName { get; set; }


    }
}
