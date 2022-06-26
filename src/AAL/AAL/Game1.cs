using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.CExt.Input;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.UI;
using MonoGame.CExt.Utility;
using MonoGame.CExt.Sprites;
using AAL.UI;

namespace AAL
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spf;
        private InputHelper ih = new InputHelper();

        public Sprite whiteRect;

        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true, MultiSampleAntiAlias = false };

        public DeskUIHandler duih;

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


            duih = new DeskUIHandler(new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), whiteRect, whiteRect, whiteRect, whiteRect, spf);
        }

        protected override void Update(GameTime gameTime)
        {
            ih.Update();
            if (ih.ExitRequested)
            {
                Exit();
            }
            
            duih.Update(gameTime, ih);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, _rasterizerState);

            //Draw

            duih.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
