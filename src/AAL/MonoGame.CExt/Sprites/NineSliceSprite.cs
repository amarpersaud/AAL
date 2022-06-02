using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.CExt.Sprites
{
    /// <summary>
    /// A Nine Slice Sprite
    /// </summary>
    public class NineSliceSprite : Sprite
    {
        /// <summary>
        /// Source Rectangle in texture
        /// </summary>
        public Rectangle[] SourceRect;

        /// <summary>
        /// Destination rectangle
        /// </summary>
        public Rectangle[] DestRect;

        /// <summary>
        /// Offset of the destination rectangle
        /// </summary>
        public Point[] DestRectOffset;

        /// <summary>
        /// Borders within the source texture to slice the sprite
        /// </summary>
        public Borders Borders { get; private set; }

        /// <summary>
        /// Width of scaled texture
        /// </summary>
        public override int Width { get { return Size.X; } }

        /// <summary>
        /// Height of scaled texture
        /// </summary>
        public override int Height { get { return Size.Y; } }

        /// <summary>
        /// Size of scaled texture
        /// </summary>
        public Point Size { get; private set; }

        /// <summary>
        /// Create a Nine Slice sprite
        /// </summary>
        /// <param name="BaseTexture">Base texture that is resized</param>
        /// <param name="Borders">Borders to slice at</param>
        /// <param name="NewSize">New size of sprite</param>
        public NineSliceSprite(Texture2D BaseTexture, Borders Borders, Point NewSize) : base(BaseTexture)
        {
            this.Borders = Borders;
            this.BaseTexture = BaseTexture;

            SourceRect = new Rectangle[9];
            DestRect = new Rectangle[9];
            DestRectOffset = new Point[9];

            this.Size = NewSize;

            int CenterWidth = BaseTexture.Width - Borders.Left - Borders.Right;
            int CenterHeight = BaseTexture.Height - Borders.Top - Borders.Bottom;

            SourceRect[0] = new Rectangle(0,                            0,                          Borders.Left,    Borders.Top);
            SourceRect[1] = new Rectangle(Borders.Left,                 0,                          CenterWidth,     Borders.Top);
            SourceRect[2] = new Rectangle(Borders.Left + CenterWidth,   0,                          Borders.Right,   Borders.Top);

            SourceRect[3] = new Rectangle(0,                            Borders.Top,                Borders.Left,   CenterHeight);
            SourceRect[4] = new Rectangle(Borders.Left,                 Borders.Top,                CenterWidth,    CenterHeight);
            SourceRect[5] = new Rectangle(Borders.Left + CenterWidth,   Borders.Top,                Borders.Right,  CenterHeight);

            SourceRect[6] = new Rectangle(0,                            Borders.Top + CenterHeight, Borders.Left,   Borders.Bottom);
            SourceRect[7] = new Rectangle(Borders.Left,                 Borders.Top + CenterHeight, CenterWidth,    Borders.Bottom);
            SourceRect[8] = new Rectangle(Borders.Left + CenterWidth,   Borders.Top + CenterHeight, Borders.Right,  Borders.Bottom);

            DestRectOffset[0] = new Point(0,                            0);
            DestRectOffset[1] = new Point(Borders.Left,                 0);
            DestRectOffset[2] = new Point(NewSize.X - Borders.Right,    0);

            DestRectOffset[3] = new Point(0,                            Borders.Top);
            DestRectOffset[4] = new Point(Borders.Left,                 Borders.Top);
            DestRectOffset[5] = new Point(NewSize.X - Borders.Right,    Borders.Top);

            DestRectOffset[6] = new Point(0,                            NewSize.Y - Borders.Bottom);
            DestRectOffset[7] = new Point(Borders.Left,                 NewSize.Y - Borders.Bottom);
            DestRectOffset[8] = new Point(NewSize.X - Borders.Right,    NewSize.Y - Borders.Bottom);

            Point DRCenter = new Point(NewSize.X - Borders.Left - Borders.Right, NewSize.Y - Borders.Top - Borders.Bottom);

            DestRect[0] = new Rectangle(0, 0, Borders.Left,     Borders.Top);
            DestRect[1] = new Rectangle(0, 0, DRCenter.X,       Borders.Top);
            DestRect[2] = new Rectangle(0, 0, Borders.Right,    Borders.Top);

            DestRect[3] = new Rectangle(0, 0, Borders.Left,     DRCenter.Y);
            DestRect[4] = new Rectangle(0, 0, DRCenter.X,       DRCenter.Y);
            DestRect[5] = new Rectangle(0, 0, Borders.Right,    DRCenter.Y);

            DestRect[6] = new Rectangle(0, 0, Borders.Left,     Borders.Bottom);
            DestRect[7] = new Rectangle(0, 0, DRCenter.X,       Borders.Bottom);
            DestRect[8] = new Rectangle(0, 0, Borders.Right,    Borders.Bottom);
        }

        public void Draw(SpriteBatch sb, Vector2 Position)
        {
            Point pos = Position.ToPoint();
            for(int i = 0; i < 9; i++)
            {
                DestRect[i].Location = DestRectOffset[i] + pos;
                sb.Draw(this.BaseTexture, DestRect[i], SourceRect[i], Color.White);
            }
        }

        public void Draw(SpriteBatch sb, Point Position)
        {
            for (int i = 0; i < 9; i++)
            {
                DestRect[i].Location = DestRectOffset[i] + Position;
                sb.Draw(this.BaseTexture, DestRect[i], SourceRect[i], Color.White);
            }
        }
    }

    
}
