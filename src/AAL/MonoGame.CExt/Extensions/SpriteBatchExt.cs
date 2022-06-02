using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.CExt.Sprites;

namespace MonoGame.CExt.Extensions
{
    public static class SpriteBatchExt
    {
        /// <summary>
        /// Draw sprite without scaling. Texture is drawn directly without regard for scaling type
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        /// <param name="s">Sprite to be drawn</param>
        /// <param name="position">Position on screen</param>
        /// <param name="col">Color</param>
        public static void Draw(this SpriteBatch sb, Sprite s, Vector2 position, Color col)
        {
            sb.Draw(s.BaseTexture, position, col);
        }


        /*

        /// <summary>
        /// Draw sprite to destination rectangle
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="s"></param>
        /// <param name="destination"></param>
        /// <param name="col"></param>
        public static void Draw(this SpriteBatch sb, Sprite s, Rectangle destination, Color col)
        {
            if (s.ScalingType == SpriteScalingType.Stretch)
            {
                sb.Draw(s.Texture, destination, col);
            }
            else
            {
                DrawSliceSprite(sb, s, destination.Location, destination.Size, col);
            }
        }
        public static void Draw(this SpriteBatch sb, Sprite s, Point pos, Point newSize, Color col)
        {
            if (s.ScalingType == SpriteScalingType.Stretch)
            {
                sb.Draw(s.Texture, pos.ToVector2(), null, col, 0, Vector2.Zero, newSize.ToVector2(), SpriteEffects.None, 0);
            }
        }



        private static void DrawSliceSprite(SpriteBatch sb, Sprite s, Point pos, Point newSize, Color col)
        {
            int LWidth = s.Borders.Left;
            int CWidth = newSize.X - s.Borders.Left - s.Borders.Right;
            int RWidth = s.Borders.Right;

            int THeight = s.Borders.Top;
            int CHeight = newSize.Y - s.Borders.Top - s.Borders.Bottom;
            int BHeight = s.Borders.Bottom;



            Rectangle TopLeft = new Rectangle(pos.X, pos.Y, LWidth, THeight);
            Rectangle Top = new Rectangle(pos.X + LWidth, pos.Y, CWidth, THeight);
            Rectangle TopRight = new Rectangle(pos.X + LWidth + CWidth, pos.Y, RWidth, THeight);

            Rectangle CenterLeft = new Rectangle(pos.X, pos.Y + THeight, LWidth, CHeight);
            Rectangle Center = new Rectangle(pos.X + LWidth, pos.Y + THeight, CWidth, CHeight);
            Rectangle CenterRight = new Rectangle(pos.X + LWidth + CWidth, pos.Y + THeight, RWidth, CHeight);

            Rectangle BottomLeft = new Rectangle(pos.X, pos.Y + THeight + CHeight, LWidth, BHeight);
            Rectangle Bottom = new Rectangle(pos.X + LWidth, pos.Y + THeight + CHeight, CWidth, BHeight);
            Rectangle BottomRight = new Rectangle(pos.X + LWidth + CWidth, pos.Y + THeight + CHeight, RWidth, BHeight);


            sb.Draw(s.Texture, TopLeft, s.TopLeft, Color.White);
            sb.Draw(s.Texture, TopRight, s.TopRight, Color.White);
            sb.Draw(s.Texture, Top, s.Top, Color.White);

            sb.Draw(s.Texture, CenterLeft, s.CenterLeft, Color.White);
            sb.Draw(s.Texture, CenterRight, s.CenterRight, Color.White);
            sb.Draw(s.Texture, Center, s.Center, Color.White);

            sb.Draw(s.Texture, BottomLeft, s.BottomLeft, Color.White);
            sb.Draw(s.Texture, BottomRight, s.BottomRight, Color.White);
            sb.Draw(s.Texture, Bottom, s.Bottom, Color.White);

        }
        */
    }
}
