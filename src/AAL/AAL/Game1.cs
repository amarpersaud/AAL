using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.CExt.Input;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using System;
using MonoGame.CExt.Sprites;

namespace AAL
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spf;
        private InputHelper ih = new InputHelper();

        public Sprite whiteRect;

        public UIHandler uih;

        Button uic;
        Button uic2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            spf = Content.Load<SpriteFont>("Arial");

            Texture2D wr = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            Color[] col = new Color[] { Color.White };
            wr.SetData<Color>(col);
            whiteRect = new Sprite(wr);


            uih = new UIHandler(new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));

            uic = new Button(whiteRect, whiteRect);
            uic.Font = spf;

            uih.Children.Add(uic);

            uic.Parent = uih;

            uic.X = 10;
            uic.Y = 10;
            uic.Width = 200;
            uic.Height = 60;

            uic2 = new Button(whiteRect, whiteRect);
            uic2.Font = spf;

            uic.Children.Add(uic2);

            uic2.Parent = uic;

            uic2.X = 10;
            uic2.Y = 10;
            uic2.Width = 180;
            uic2.Height = 40;
            uic2.BackgroundColor = Color.Pink;
            uic2.PressedColor = Color.Brown;


            //uic2.Anchor = Anchor.None;

            uic2.SetAnchor(Side.Left);
            uic2.SetAnchor(Side.Right);
            //uic2.SetAnchor(Side.Top);
            uic2.SetAnchor(Side.Bottom);



            uic2.Text = "Two door";
            uic2.Text = "Two door";
        }

        int i = 0;
        protected override void Update(GameTime gameTime)
        {
            ih.Update();
            if (ih.ExitRequested)
                Exit();
            
            uih.Update(gameTime, ih);

            int f = 1;

            if (ih.IsCurPress(Keys.Right))
            {
                i++;
                if(i%f == 0)
                {
                    uic.Width++;
                }
            }
            else if (ih.IsCurPress(Keys.Left))
            {
                i++;
                if(i%f == 0 && uic.Width > 0)
                {
                    uic.Width--;
                }
            }


            if (ih.IsCurPress(Keys.Down))
            {
                i++;
                if (i % f == 0)
                {
                    uic.Height++;
                }
            }
            else if (ih.IsCurPress(Keys.Up))
            {
                i++;
                if (i % f == 0 && uic.Height > 0)
                {
                    uic.Height--;
                }
            }




            if (ih.IsCurPress(Keys.D))
            {
                i++;
                if (i % f == 0)
                {
                    uic.Left++;
                }
            }
            else if (ih.IsCurPress(Keys.A))
            {
                i++;
                if (i % f == 0 && uic.Width > 0)
                {
                    uic.Left--;
                }
            }


            if (ih.IsCurPress(Keys.S))
            {
                i++;
                if (i % f == 0)
                {
                    uic.Top++;
                }
            }
            else if (ih.IsCurPress(Keys.W))
            {
                i++;
                if (i % f == 0 && uic.Height > 0)
                {
                    uic.Top--;
                }
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            //Draw
            uih.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
