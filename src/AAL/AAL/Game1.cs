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
using System.Collections.Generic;

namespace AAL
{
    public class Game1 : Game
    {
        private GameManager gm;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputHelper ih = new InputHelper();
        private FrameCounter fc = new FrameCounter();
        private WindowSettings ss = new WindowSettings();

        private ResourceHandler _rh;

        List<string> fonts = new List<string> { "Arial" };
        List<string> textures = new List<string>();

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
            ss = new WindowSettings();
            ss.WindowDimensions = new Point(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            ss.GameScreenDimensions = ss.WindowDimensions.ToVector2();
            _rh = new ResourceHandler(Content, _spriteBatch, ih, fc, ss);


            gm = new GameManager();
            gm.rh = _rh;
            //gm.Clock = new GameClock();
            //gm.Player = new Player();
            //gm.Settings = new GameSettings();
            //gm.GameMap = new Map();
            //gm.Resources = new Dictionary<int, Resources.Resource>();

            //Load Fonts
            foreach (string s in fonts)
            {
                SpriteFont spf = Content.Load<SpriteFont>(s);
                string fontname = System.IO.Path.GetFileName(s);
                _rh.Fonts.Add(fontname, spf);
            }

            //Load textures
            foreach (string t in textures)
            {
                Texture2D tex = Content.Load<Texture2D>(t);
                string texturename = System.IO.Path.GetFileName(t);
                Sprite s = new Sprite(tex);
                _rh.Sprites.Add(texturename, s);
            }

            //Create white rectangle placeholder graphic
            Texture2D wr = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            Color[] col = new Color[] { Color.White };
            wr.SetData<Color>(col);
            whiteRect = new Sprite(wr);

            _rh.Sprites.Add("whiteRect", whiteRect);

            duih = new DeskUIHandler(_rh, ss.WindowRectangle);
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

            duih.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
