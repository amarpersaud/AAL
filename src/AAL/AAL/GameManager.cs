using MonoGame.CExt.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL
{
    /// <summary>
    /// Class for handling game assets
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// List of all possible resources indexed by resource by Id
        /// </summary>
        public Dictionary<int, Resource> Resources = new Dictionary<int, Resource>();

        /// <summary>
        /// Game map
        /// </summary>
        public Map GameMap;

        /// <summary>
        /// Clock for managing in game time
        /// </summary>
        public GameClock Clock;

        /// <summary>
        /// Player object
        /// </summary>
        public Player Player;

        /// <summary>
        /// CustomExtension Resource Handler
        /// </summary>
        public ResourceHandler rh;
    }
}
