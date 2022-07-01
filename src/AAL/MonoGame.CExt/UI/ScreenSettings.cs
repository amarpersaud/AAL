using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// Keeps track of window and game screen coordinate systems
    /// </summary>
    public class ScreenSettings
    {
        /// <summary>
        /// Dimensions in pixels of the game window
        /// </summary>
        public Point WindowDimensions { get; set; }

        /// <summary>
        /// Dimensions, in the game screen coordinate system, of the screen
        /// </summary>
        public Vector2 GameScreenDimensions { get; set; }

        /// <summary>
        /// Aspect ratio of game screen
        /// </summary>
        public float GameAspectRatio => GameScreenDimensions.X / (float)(GameScreenDimensions.Y);
        
        /// <summary>
        /// Aspect ratio of window
        /// </summary>
        public float WindowAspectRatio => WindowDimensions.X / (float)(WindowDimensions.Y);
        
        /// <summary>
        /// Rectangle representing window
        /// </summary>
        public Rectangle WindowRectangle => new Rectangle(Point.Zero, WindowDimensions);

    }
}
