using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.CExt.UI;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;

using AAL.Contracts;
using MonoGame.CExt.Utility;
using AAL.Map;

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

        public DeskUIHandler(ResourceHandler resourceHandler, Rectangle screen) : base(resourceHandler, screen){

            Initialize(screen);
        }

        public void Initialize(Rectangle screen)
        {
            this.BackgroundColor = Color.Transparent;
            this.BackgroundSprite = _rh.GetSprite("whiteRect");

            CoordinateHelper sch = new CoordinateHelper(screen);

            Desk = new Panel(_rh);
            Desk.Bounds = sch.GetRectCentered(0.5, 0.6, 0.75, 0.55);

            Desk.Overflow = UIOverflow.Visible;
            Desk.BackgroundSprite = _rh.GetSprite("whiteRect");
            Desk.BackgroundColor = Color.Brown;
            this.AddControl(Desk);

            CoordinateHelper dch = new CoordinateHelper(Desk.Bounds, true);

            InboxButton = new Button(_rh);
            InboxButton.Bounds = dch.GetRectCentered(0.15, 0.25, 0.15, 0.25);
            InboxButton.Overflow = UIOverflow.Visible;
            InboxButton.Text = "Inbox";
            InboxButton.MouseReleased += InboxButtonClicked;
            Desk.AddControl(InboxButton);

            OutboxButton = new Button(_rh);
            OutboxButton.Bounds = dch.GetRectCentered(0.85, 0.25, 0.15, 0.25);
            OutboxButton.Overflow = UIOverflow.Visible;
            OutboxButton.Text = "Outbox";
            OutboxButton.MouseReleased += OutboxButtonClicked;
            Desk.AddControl(OutboxButton);

            MapButton = new Button(_rh);
            MapButton.Bounds = dch.GetRectCentered(0.5, 0.5, 0.15, 0.25);
            MapButton.Overflow = UIOverflow.Visible;
            MapButton.Text = "Map";
            MapButton.MouseReleased += MapButtonClicked;
            Desk.AddControl(MapButton);

            ContractControl = new UIContract(_rh, sch.GetRectCentered(0.5, 0.5, 0.5, 0.75));
            ContractControl.BackgroundColor = Color.Tan;
            ContractControl.Overflow = UIOverflow.Auto;
            ContractControl.Hide();
            AddControl(ContractControl);

            ContractControl.CloseButton.MouseReleased += ContractControlCloseClicked;

            InboxPanel = new Panel(_rh);
            InboxPanel.Bounds = sch.GetRectCentered(0.5, 0.5, 0.5, 0.75);
            InboxPanel.BackgroundColor = Color.Blue;
            InboxPanel.BackgroundSprite = _rh.GetSprite("whiteRect");
            InboxPanel.Padding = new Borders { Top = 10, Bottom = 10, Left = 10, Right = 10 };
            InboxPanel.Overflow = UIOverflow.Auto;
            InboxPanel.Hide();
            AddControl(InboxPanel);

            ContractGenerator.Initialize();

            for (int j = 0; j < 10; j++)
            {
                Contract c = ContractGenerator.GenerateContract();
                AddContractToInbox(c);
            }

            OutboxPanel = new Panel(_rh);
            OutboxPanel.Bounds = sch.GetRectCentered(0.5, 0.5, 0.5, 0.75);
            OutboxPanel.BackgroundColor = Color.Blue;
            OutboxPanel.BackgroundSprite = _rh.GetSprite("whiteRect");
            OutboxPanel.Padding = new Borders { Top = 10, Bottom = 10, Left = 10, Right = 10 };
            OutboxPanel.Overflow = UIOverflow.Auto;
            OutboxPanel.Hide();
            AddControl(OutboxPanel);

            for (int j = 0; j < 10; j++)
            {
                Contract c = ContractGenerator.GenerateContract();
                AddContractToOutbox(c);
            }

            WorldMap = new Map();
            WorldMap.WorldName = "Test World";
            WorldMap.Regions = new List<Region>();
            MapPanel = new UIMap(_rh, screen, WorldMap);
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

            Button b = new Button(_rh);
            b.X = r.X;
            b.Y = r.Y;
            b.Width = r.Width;
            b.Height = r.Height;
            b.SetAnchor(Side.Left);
            b.SetAnchor(Side.Right);
            b.HoverColor = Color.Yellow;
            b.BackgroundColor = Color.Violet;
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

            Button b = new Button(_rh);
            b.X = r.X;
            b.Y = r.Y;
            b.Width = r.Width;
            b.Height = r.Height;
            b.SetAnchor(Side.Left);
            b.SetAnchor(Side.Right);
            b.HoverColor = Color.Purple;
            b.BackgroundColor = Color.Gray;
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
