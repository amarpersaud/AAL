using System;
using System.Collections.Generic;
using System.Text;

using MonoGame.CExt;
using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using MonoGame.CExt.Input;
using MonoGame.CExt.Sprites;
using MonoGame.CExt.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AAL.Contracts;

namespace AAL.UI
{
    /// <summary>
    /// A UI Control for physically displaying the contract on screen
    /// </summary>
    public class UIContract : Panel
    {
        public Label titleLabel;
        public Label contentLabel;
        public Button CloseButton;

        private Contract _contract;

        /// <summary>
        /// Contract object that holds contract information
        /// </summary>
        public Contract Contract
        {
            get { return _contract; }
            set
            {
                _contract = value;
                if (_contract != null)
                {
                    if(_contract.ContractTitle != null)
                    {
                        titleLabel.Text = _contract.ContractTitle;
                    }
                    contentLabel.Text = _contract.GetFullText();
                }
            }
        }

        public UIContract(int Width, int Height, SpriteFont spf, Sprite whiteRect) : base()
        {
            this.Width = Width;
            this.Height = Height;
            this.BackgroundSprite = whiteRect;
            Initialize(Width, Height, spf, whiteRect);
        }

        public void Initialize(int Width, int Height, SpriteFont spf, Sprite whiteRect)
        {
            this.Overflow = UIOverflow.Auto;
            this.Padding = new Borders { Top = 15, Bottom = 15, Left = 10, Right = 10 };            
            
            CoordinateHelper ch = new CoordinateHelper(Width, Height);
            titleLabel = new Label();
            titleLabel.X = ch.atoiX(0.5);
            titleLabel.Y = 10;
            titleLabel.Centered = true;
            titleLabel.Font = spf;
            titleLabel.ForeColor = Color.Black;
            AddControl(titleLabel);

            contentLabel = new Label();
            contentLabel.X = 0;
            contentLabel.Y = 30;
            contentLabel.Font = spf;
            contentLabel.Centered = false;
            contentLabel.ForeColor = Color.Black;
            AddControl(contentLabel);

            CloseButton = new Button(whiteRect, whiteRect, spf);
            CloseButton.Width = 20;
            CloseButton.Height = 20;
            CloseButton.X = this.InnerRect.Width - 20;
            CloseButton.Y = 0;
            CloseButton.BackgroundColor = Color.Red;
            CloseButton.HoverColor = Color.Pink;
            AddControl(CloseButton);

        }

        public override void Update(GameTime gameTime, double timeScale, InputHelper ih, UIHandler uih)
        {
            base.Update(gameTime, timeScale, ih, uih);

        }
        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }
    }
}
