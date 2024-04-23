using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.CExt.Utility;

namespace MonoGame.CExt.UI
{
    /// <summary>
    /// UI Handler. Holds UI Controls, manages input to the UI.
    /// </summary>
    public class UIHandler : UIControl
    {
        /// <summary>
        /// Rectangle representing the area the UI Handler covers.
        /// </summary>
        public Rectangle ScreenArea;

        /// <summary>
        /// Current selected control
        /// </summary>
        public UIControl SelectedControl { get; set; }

        /// <summary>
        /// Current Control that the mouse is on top of.
        /// </summary>
        public UIControl CurrentMouseControl { get; private set; }

        /// <summary>
        /// Create a UI Handler
        /// </summary>
        public UIHandler(ResourceHandler resourceHandler, Rectangle ScreenArea) : base(resourceHandler) {
            //Initialize Dimensions
            this.X = ScreenArea.X;
            this.Y = ScreenArea.Y;
            this.Width = ScreenArea.Width;
            this.Height = ScreenArea.Height;
            this.Margin = Borders.Zero;
            this.Padding = Borders.Zero;
            this.Parent = null;
            this.ScreenArea = ScreenArea;
        }

        /// <summary>
        /// Update UI elements and get user input to UI elements
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="ih"></param>
        /// <param name="uih"></param>
        public void Update(GameTime gameTime, InputHelper ih, UIHandler uih = null)
        {
            //Update which is the current control containing the mouse
            CurrentMouseControl = this.DetermineFrontMostDescendant(ih.MousePosition.ToPoint());

            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            //Update this as a control in case it has been clicked.
            base.Update(gameTime, deltaTime, this);

            //Update child elements
            foreach (UIControl c in Children)
            {
                c.Update(gameTime, deltaTime, this);
            }
        }
        /// <summary>
        /// Draw UI Elements
        /// </summary>
        public override void Draw()
        {
            //Draw child elements
            foreach (UIControl c in Children)
            {
                c.Draw();
            }
        }
    }
}
