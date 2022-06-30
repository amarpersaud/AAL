using Microsoft.Xna.Framework;
using MonoGame.CExt.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace AAL.UI
{
    public class UIMap : Panel
    {
        public Panel MapPanel;
        public Label TitleLabel;
        public Button CloseButton;

        public Map WorldMap;

        public UIMap(Rectangle screen, Map WorldMap, Sprite whiterect, SpriteFont spf) : base()
        {
            this.WorldMap = WorldMap;
            Initialize(screen, whiterect, spf);
        }

        private void Initialize(Rectangle screen, Sprite whiterect, SpriteFont spf)
        {
            CoordinateHelper ch = new CoordinateHelper(screen.Width, screen.Height);

            this.Width = ch.atoiX(0.75);
            this.Height = ch.atoiY(0.75);
            this.X = ch.atoiX(0.5) - (this.Width / 2);
            this.Y = ch.atoiY(0.5) - (this.Height / 2);
            this.BackgroundColor = Color.Tan;
            this.Padding = new Borders { Left = 15, Right = 15, Bottom = 15, Top = 15 };
            this.BackgroundSprite = whiterect;

            MapPanel = new Panel();
            AddControl(MapPanel);
            MapPanel.SetAnchor(Side.Left, 0);
            MapPanel.SetAnchor(Side.Right, 0);
            MapPanel.SetAnchor(Side.Top, 30);
            MapPanel.SetAnchor(Side.Bottom, 0);
            MapPanel.BackgroundColor = Color.Peru;
            MapPanel.BackgroundSprite = whiterect;
            MapPanel.Invalidate();

            CoordinateHelper mh = new CoordinateHelper(this.Width, this.Height);

            TitleLabel = new Label();
            TitleLabel.Font = spf;
            TitleLabel.Text = String.Format("Map of: {0}", WorldMap.WorldName);
            TitleLabel.X = mh.atoiX(0.5);
            TitleLabel.Y = 0;
            TitleLabel.CenterX = true;
            AddControl(TitleLabel);

            CloseButton = new Button(whiterect, whiterect, spf);
            AddControl(CloseButton);
            CloseButton.Width = 20;
            CloseButton.Height = 20;
            CloseButton.SetAnchor(Side.Right, 0);
            CloseButton.SetAnchor(Side.Top, 0);
            CloseButton.BackgroundColor = Color.Red;
            CloseButton.HoverColor = Color.OrangeRed;

        }
    }
}
