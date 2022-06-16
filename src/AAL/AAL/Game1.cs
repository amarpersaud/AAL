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

        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true, MultiSampleAntiAlias = false };

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

            uic.X = 50;
            uic.Y = 10;
            uic.Width = 80;
            uic.Height = 120;

            uic.Text = "Inbox";

            uic2 = new Button(whiteRect, whiteRect);
            uic2.Font = spf;

            uih.Children.Add(uic2);

            uic2.Parent = uih;

            uic2.X = 660;
            uic2.Y = 10;
            uic2.Width = 80;
            uic2.Height = 120;
            uic2.BackgroundColor = Color.Pink;
            uic2.PressedColor = Color.Brown;


            uic2.Text = "Outbox";

        }

        protected override void Update(GameTime gameTime)
        {
            ih.Update();
            if (ih.ExitRequested)
                Exit();
            
            uih.Update(gameTime, ih);



            base.Update(gameTime);
        }


        Rectangle Desk = new Rectangle(245, 50, 300, 300);
        Rectangle Fire = new Rectangle(660, 325, 80, 80);


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, _rasterizerState);

            //Draw


            _spriteBatch.Draw(whiteRect.BaseTexture, Desk, Color.Tan);
            _spriteBatch.Draw(whiteRect.BaseTexture, Fire, Color.Red);
            _spriteBatch.DrawString(spf, "Contract", new Vector2(400, 250), Color.White);
            
            uih.Draw(_spriteBatch);



            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
