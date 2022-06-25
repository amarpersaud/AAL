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

        public UIHandler uih;
        
        private RasterizerState _rasterizerState = new RasterizerState() { ScissorTestEnable = true, MultiSampleAntiAlias = false };

        Button uic;
        Button uic2;
        UIContract uicontract;

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

            uicontract = new UIContract();
            uicontract.Font = spf;
            uicontract.X = 245;
            uicontract.Y = 50;
            uicontract.Width = 300;
            uicontract.Height = 300;
            uicontract.BackgroundColor = Color.Tan;
            uicontract.BackgroundSprite = whiteRect;
            uicontract.Parent = uih;

            uih.Children.Add(uicontract);



            Panel uip = new Panel();
            uip.BackgroundSprite = whiteRect;
            uip.BackgroundColor = Color.Blue;

            uip.X = 100;
            uip.Y = 100;
            uip.Width = 300;
            uip.Height = 300;
            uip.Parent = uih;
            uip.Font = spf;
            uip.Overflow = UIOverflow.Scroll;

            uip.Padding = new Borders { Top = 20, Bottom = 20 };

            for (int i = 0; i < 30; i++)
            {
                Button aa1 = new Button(whiteRect, whiteRect);
                aa1.BackgroundColor = Color.White;
                aa1.PressedColor = Color.Red;
                aa1.X = 10;
                aa1.Y = 10 + (30 * i);
                aa1.Height = 20;
                aa1.Width = 280;
                aa1.Parent = uip;
                aa1.Font = spf;
                aa1.Text = String.Format("aa{0}", i+1);
                uip.Children.Add(aa1);
            }
            uih.Children.Add(uip);
            uih.BringChildToFront(uih.Children.IndexOf(uip));
        }

        protected override void Update(GameTime gameTime)
        {
            ih.Update();
            if (ih.ExitRequested)
            {
                Exit();
            }
            
            uih.Update(gameTime, ih);


            base.Update(gameTime);
        }


        Rectangle Fire = new Rectangle(660, 325, 80, 80);

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, _rasterizerState);

            //Draw
            _spriteBatch.Draw(whiteRect.BaseTexture, Fire, Color.Red);
            uih.Draw(_spriteBatch);
            _spriteBatch.DrawString(spf, "Contract", new Vector2(300, 250), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
