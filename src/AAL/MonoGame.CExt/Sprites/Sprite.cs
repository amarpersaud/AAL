using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.CExt.Sprites
{
    /// <summary>
    /// Sprite class for drawing sprites to the screen
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// Base texture
        /// </summary>
        public Texture2D BaseTexture;

        /// <summary>
        /// Width of texture
        /// </summary>
        public virtual int Width { get { return BaseTexture.Width; } }

        /// <summary>
        /// Height of texture
        /// </summary>
        public virtual int Height { get { return BaseTexture.Height; } }

        /// <summary>
        /// Creates a sprite object.
        /// </summary>
        /// <param name="BaseTexture">Base texture for sprite</param>
        public Sprite(Texture2D BaseTexture)
        {
            this.BaseTexture = BaseTexture;
        }

        public virtual void Draw(SpriteBatch sb, Vector2 Position, float Scale = 1.0f)
        {
            sb.Draw(BaseTexture, Position, null, Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
}
