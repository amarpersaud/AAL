using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.CExt.UI
{
    public class GameScreen
    {
        /// <summary>
        /// Handle to resource handler for input, graphics, etc
        /// </summary>
        private ResourceHandler _rh;

        /// <summary>
        /// Name of the screen shown
        /// </summary>
        public string ScreenName { get; private set; }

        /// <summary>
        /// String from which the UI is deserialized
        /// </summary>
        private string SerializedUI { get; set; }

        /// <summary>
        /// UIHandler root object
        /// </summary>
        private UIHandler uih { get; set; }


        /// <summary>
        /// Create Game Screen
        /// </summary>
        /// <param name="rh"></param>
        /// <param name="screenName"></param>
        public GameScreen(ResourceHandler rh, string screenName)
        {
            this._rh = rh;
            this.ScreenName = screenName;
            
            //TODO: load UI String through rh
            this.SerializedUI = String.Empty;

            //TODO: Deserialize UI string
            //uih = 
        }
    }
}
