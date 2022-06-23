using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// UI Handler. Holds UI Controls and manages input to the UI
    /// </summary>
    public class UIHandler : UIControl
    {
        /// <summary>
        /// Rectangle representing the entire screesn.
        /// </summary>
        public Rectangle ScreenRect;

        /// <summary>
        /// Current selected control
        /// </summary>
        public UIControl SelectedControl { get; private set; }

        /// <summary>
        /// Current Control that the mouse is on top of.
        /// </summary>
        public UIControl CurrentMouseControl { get; private set; }

        /// <summary>
        /// Create a UI Handler
        /// </summary>
        public UIHandler(Rectangle ScreenRect) : base() {
            //Initialize Dimensions
            this.X = ScreenRect.X;
            this.Y = ScreenRect.Y;
            this.Width = ScreenRect.Width;
            this.Height = ScreenRect.Height;
            this.Margin = Borders.Zero;
            this.Padding = Borders.Zero;

            this.Parent = null;

            this.ScreenRect = ScreenRect;

        }

        public void Update(GameTime gameTime, InputHelper ih, UIHandler uih = null)
        {
            //Update which is the current control containing the mouse
            CurrentMouseControl = this.DetermineFrontMostDescendant(ih.MousePosition.ToPoint());

            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            //Update child elements
            foreach(UIControl c in Children)
            {
                c.Update(gameTime, deltaTime, ih, this);
            }
        }
        public override void Draw(SpriteBatch sb)
        {
            //Draw child elements
            foreach (UIControl c in Children)
            {
                c.Draw(sb);
            }
        }
    }
}
