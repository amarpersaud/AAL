using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.CExt.UI;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;

using AAL.Contracts;

namespace AAL.UI
{
    public class DeskUIHandler : UIHandler
    {
        public UIContract ContractControl;
        public Panel InboxPanel;
        public Panel OutboxPanel;
        public Button InboxButton;
        public Button OutboxButton;
        public Panel Desk;
        public Button MapButton;
        public UIMap MapPanel;
        public Map WorldMap;

        public DeskUIHandler(Rectangle screen, Sprite whiterect, SpriteFont spf) : base(screen){

            Initialize(screen, whiterect, spf);
        }

        public void Initialize(Rectangle screen, Sprite whiterect, SpriteFont spf)
        {
            this.Font = spf;
            this.BackgroundColor = Color.Transparent;
            this.BackgroundSprite = whiterect;

            CoordinateHelper sch = new CoordinateHelper(screen.Width, screen.Height);

            Desk = new Panel();
            Desk.Width = sch.atoiX(0.75);
            Desk.Height = sch.atoiY(0.55);
            Desk.X = sch.atoiX(0.5) - (Desk.Width / 2);
            Desk.Y = sch.atoiY(0.60) - (Desk.Height / 2);
            Desk.Overflow = UIOverflow.Visible;
            Desk.BackgroundSprite = whiterect;
            Desk.BackgroundColor = Color.Brown;
            this.AddControl(Desk);

            CoordinateHelper dch = new CoordinateHelper(Desk.Width, Desk.Height);

            //InboxPanel = new Panel();

            InboxButton = new Button(whiterect, whiterect, spf);
            InboxButton.Width = dch.atoiX(0.15);
            InboxButton.Height = dch.atoiY(0.25);
            InboxButton.X = dch.atoiX(0.15) - (InboxButton.Width / 2);
            InboxButton.Y = dch.atoiY(0.25) - (InboxButton.Height / 2);
            InboxButton.Overflow = UIOverflow.Visible;
            InboxButton.Text = "Inbox";
            InboxButton.MouseReleased += InboxButtonClicked;
            Desk.AddControl(InboxButton);

            OutboxButton = new Button(whiterect, whiterect, spf);
            OutboxButton.Width = dch.atoiX(0.15);
            OutboxButton.Height = dch.atoiY(0.25);
            OutboxButton.X = dch.atoiX(0.85) - (OutboxButton.Width / 2);
            OutboxButton.Y = dch.atoiY(0.25) - (OutboxButton.Height / 2);
            OutboxButton.Overflow = UIOverflow.Visible;
            OutboxButton.Text = "Outbox";
            OutboxButton.Font = spf;
            OutboxButton.MouseReleased += OutboxButtonClicked;
            Desk.AddControl(OutboxButton);

            MapButton = new Button(whiterect, whiterect, spf);
            MapButton.Width = dch.atoiX(0.15);
            MapButton.Height = dch.atoiY(0.25);
            MapButton.X = dch.atoiX(0.5) - (MapButton.Width / 2);
            MapButton.Y = dch.atoiY(0.5) - (MapButton.Height / 2);
            MapButton.Overflow = UIOverflow.Visible;
            MapButton.Text = "Map";
            MapButton.Font = spf;
            MapButton.MouseReleased += MapButtonClicked;
            Desk.AddControl(MapButton);

            ContractControl = new UIContract(sch.atoiX(0.5), sch.atoiY(0.75), spf, whiterect);
            ContractControl.X = sch.atoiX(0.5) - ContractControl.Width/2;
            ContractControl.Y = sch.atoiY(0.5) - ContractControl.Height/2;
            ContractControl.BackgroundColor = Color.Tan;
            ContractControl.Font = spf;
            ContractControl.Overflow = UIOverflow.Auto;
            ContractControl.Hide();
            AddControl(ContractControl);

            ContractControl.CloseButton.MouseReleased += ContractControlCloseClicked;

            InboxPanel = new Panel();
            InboxPanel.Width = sch.atoiX(0.5);
            InboxPanel.Height = sch.atoiY(0.75);
            InboxPanel.X = sch.atoiX(0.5) - InboxPanel.Width / 2;
            InboxPanel.Y = sch.atoiY(0.5) - InboxPanel.Height / 2;
            InboxPanel.BackgroundColor = Color.Blue;
            InboxPanel.BackgroundSprite = whiterect;
            InboxPanel.Font = spf;
            InboxPanel.Padding = new Borders { Top = 10, Bottom = 10, Left = 10, Right = 10 };
            InboxPanel.Overflow = UIOverflow.Auto;
            InboxPanel.Hide();
            AddControl(InboxPanel);

            for (int j = 0; j < 10; j++)
            {
                Contract a = new Contract(String.Format("Test contract {0}", j + 1));
                for (int i = 0; i < 10; i++)
                {
                    ContractClause cc = new ContractClause(String.Format("Test clause {0}", i + 1));
                    a.Clauses.Add(cc);
                }
                AddContractToInbox(a);
            }

            OutboxPanel = new Panel();
            OutboxPanel.Width = sch.atoiX(0.5);
            OutboxPanel.Height = sch.atoiY(0.75);
            OutboxPanel.X = sch.atoiX(0.5) - OutboxPanel.Width / 2;
            OutboxPanel.Y = sch.atoiY(0.5) - OutboxPanel.Height / 2;
            OutboxPanel.BackgroundColor = Color.Blue;
            OutboxPanel.BackgroundSprite = whiterect;
            OutboxPanel.Font = spf;
            OutboxPanel.Padding = new Borders { Top = 10, Bottom = 10, Left = 10, Right = 10 };
            OutboxPanel.Overflow = UIOverflow.Auto;
            OutboxPanel.Hide();
            AddControl(OutboxPanel);

            for (int j = 0; j < 10; j++)
            {
                Contract a = new Contract(String.Format("Test outbox contract {0}", j + 1));
                for (int i = 0; i < 10; i++)
                {
                    ContractClause cc = new ContractClause(String.Format("Test clause {0}", i+1));
                    a.Clauses.Add(cc);
                }
                AddContractToOutbox(a);
            }

            WorldMap = new Map();
            WorldMap.WorldName = "Test World";
            WorldMap.Regions = new List<Region>();
            MapPanel = new UIMap(screen, WorldMap, whiterect, spf);
            MapPanel.Hide();
            AddControl(MapPanel);
            MapPanel.CloseButton.MouseReleased += MapPanelCloseButtonClicked;
            
        }

        public void ContractControlCloseClicked(object sender, UIControlClickEventArgs e)
        {
            ContractControl.Hide();
        }
        public void InboxPanelButtonClicked(object sender, UIControlClickEventArgs e)
        {
            Contract c = (Contract)e.Sender.Value;
            InboxPanel.Hide();
            ContractControl.Contract = c;
            ContractControl.Show();
        }
        public void InboxButtonClicked(object sender, UIControlClickEventArgs e)
        {
            InboxPanel.Show();
        }
        public void OutboxPanelButtonClicked(object sender, UIControlClickEventArgs e)
        {
            Contract c = (Contract)e.Sender.Value;
            OutboxPanel.Hide();
            ContractControl.Contract = c;
            ContractControl.Show();
        }
        public void OutboxButtonClicked(object sender, UIControlClickEventArgs e)
        {
            OutboxPanel.Show();
        }

        public void AddContractToInbox(Contract c)
        {
            GridHelper gh = new GridHelper(new Point(0, 0), new Point(InboxPanel.InnerRect.Width, 30), new Point(0, 10));
            if (c == null) { return; }

            Rectangle r = gh.GetRect(0, InboxPanel.Children.Count);

            Button b = new Button(this.BackgroundSprite, this.BackgroundSprite, InboxButton.Font);
            b.X = r.X;
            b.Y = r.Y;
            b.Width = r.Width;
            b.Height = r.Height;
            b.SetAnchor(Side.Left);
            b.SetAnchor(Side.Right);
            b.HoverColor = Color.Yellow;
            b.BackgroundColor = Color.Violet;
            b.Font = this.Font;
            b.Text = c.ContractTitle;


            InboxPanel.AddControl(b);
            b.Value = c;
            b.MouseReleased += InboxPanelButtonClicked;

        }

        public void AddContractToOutbox(Contract c)
        {
            GridHelper gh = new GridHelper(new Point(0, 0), new Point(OutboxPanel.InnerRect.Width, 30), new Point(0, 10));
            if (c == null) { return; }

            Rectangle r = gh.GetRect(0, OutboxPanel.Children.Count);

            Button b = new Button(this.BackgroundSprite, this.BackgroundSprite, OutboxButton.Font);
            b.X = r.X;
            b.Y = r.Y;
            b.Width = r.Width;
            b.Height = r.Height;
            b.SetAnchor(Side.Left);
            b.SetAnchor(Side.Right);
            b.HoverColor = Color.Purple;
            b.BackgroundColor = Color.Gray;
            b.Font = this.Font;
            b.Text = c.ContractTitle;


            OutboxPanel.AddControl(b);
            b.Value = c;
            b.MouseReleased += OutboxPanelButtonClicked;

        }
        public void MapButtonClicked(object sender, UIControlClickEventArgs e)
        {
            MapPanel.Show();
        }

        public void MapPanelCloseButtonClicked(object sender, UIControlClickEventArgs e)
        {
            MapPanel.Hide();
        }
    }                         
}
