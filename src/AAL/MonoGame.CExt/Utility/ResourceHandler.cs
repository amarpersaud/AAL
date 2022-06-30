using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Utility;
using MonoGame.CExt.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.CExt.Utility
{

    public class ResourceHandler
    {
        /// <summary>
        /// Sprite batch for drawing 
        /// </summary>
        public SpriteBatch _sb { get; private set; }

        /// <summary>
        /// Input helper for handling input
        /// </summary>
        public InputHelper _ih { get; private set; }

        /// <summary>
        /// Frame counter for counting framerate
        /// </summary>
        public FrameCounter _fc { get; private set; }

        /// <summary>
        /// Screen settings, resolution and 
        /// </summary>
        public ScreenSettings ScreenSettings { get; private set; }

        /// <summary>
        /// List of sprites
        /// </summary>
        public Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
    }
}