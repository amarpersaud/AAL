using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGame.CExt.Sprites
{
    /// <summary>
    /// A sprite consisting of stacked textures to create a pseudo 3d effect
    /// </summary>
    public class StackedSprite : Sprite
    {
        /// <summary>
        /// Scale of the sprite.
        /// </summary>
        public Vector2 Scale;

        /// <summary>
        /// Offset the sprite when drawn
        /// </summary>
        public Vector2 Offset;

        /// <summary>
        /// Angle of the sprite
        /// </summary>
        public float Angle;

        /// <summary>
        /// Separation between vertical slices
        /// </summary>
        public float VerticalSeparation;
        
        /// <summary>
        /// Origin of the sprites for rotation
        /// </summary>
        public Vector2 Origin;

        /// <summary>
        /// Sprite color
        /// </summary>
        public Color Color;

        /// <summary>
        /// Source rectangles for each sprite slice
        /// </summary>
        public Rectangle[] SourceRectangles;

        /// <summary>
        /// Sprite effects for drawing this sprite
        /// </summary>
        public SpriteEffects Effect = SpriteEffects.None;


        /// <summary>
        /// Create a sprite
        /// </summary>
        /// <param name="BaseTexture">Atlas of sprites</param>
        /// <param name="SourceRects">Source rectangles within the </param>
        public StackedSprite(Texture2D BaseTexture, Rectangle[] SourceRects) : base(BaseTexture)
        {
            this.BaseTexture = BaseTexture;
            this.SourceRectangles = SourceRects;
            this.Offset = Vector2.Zero;

            //Center of the sprites
            this.Origin = (SourceRects[0].Size).ToVector2() * 0.5f;
            
        }

    }
}
