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

            ContractControl = new UIContract(sch.atoiX(0.5), sch.atoiY(0.75), spf, whiterect);
            ContractControl.X = sch.atoiX(0.5) - ContractControl.Width/2;
            ContractControl.Y = sch.atoiY(0.5) - ContractControl.Height/2;
            ContractControl.BackgroundColor = Color.Tan;
            ContractControl.Font = spf;
            ContractControl.Overflow = UIOverflow.Auto;
            ContractControl.Enabled = false;
            ContractControl.Visible = false;
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
            InboxPanel.Enabled = false;
            InboxPanel.Visible = false;
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
            OutboxPanel.Enabled = false;
            OutboxPanel.Visible = false;
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

        }

        public void DisplayContract(Contract c)
        {
            ContractControl.Contract = c;
            ContractControl.Enabled = true;
            ContractControl.Visible = true;
        }   
        
        public void HideContract()
        {
            ContractControl.Enabled = false;
            ContractControl.Visible = false;
        }

        public void ContractControlCloseClicked(object sender, UIControlClickEventArgs e)
        {
            HideContract();
        }

        public void InboxPanelButtonClicked(object sender, UIControlClickEventArgs e)
        {
            Contract c = (Contract)e.Sender.Value;
            HideInboxPanel();
            DisplayContract(c);
        }
        public void InboxButtonClicked(object sender, UIControlClickEventArgs e)
        {
            ShowInboxPanel();
        }

        public void AddContractToInbox(Contract c)
        {
            GridHelper gh = new GridHelper(new Point(0,0), new Point(InboxPanel.InnerRect.Width, 30), new Point(0, 10));
            if(c == null) { return; }

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

        public void OutboxPanelButtonClicked(object sender, UIControlClickEventArgs e)
        {
            Contract c = (Contract)e.Sender.Value;
            HideOutboxPanel();
            DisplayContract(c);
        }
        public void OutboxButtonClicked(object sender, UIControlClickEventArgs e)
        {
            ShowOutboxPanel();
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

        public void ShowInboxPanel()
        {
            InboxPanel.Enabled = true;
            InboxPanel.Visible = true;
        }
        public void HideInboxPanel()
        {
            InboxPanel.Enabled = false;
            InboxPanel.Visible = false;
        }

        public void ShowOutboxPanel()
        {
            OutboxPanel.Enabled = true;
            OutboxPanel.Visible = true;
        }
        public void HideOutboxPanel()
        {
            OutboxPanel.Enabled = false;
            OutboxPanel.Visible = false;
        }
    }                         
}
