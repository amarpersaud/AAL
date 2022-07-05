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
        private ResourceHandler _rh;
        public string ScreenName { get; private set; }
        private string SerializedUI { get; set; }
        private UIHandler uih { get; set; }

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
