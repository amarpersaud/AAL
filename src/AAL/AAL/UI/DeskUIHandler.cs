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
        public Button inbox;
        public Button outbox;
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

            inbox = new Button(whiterect, whiterect, spf);
            inbox.Width = dch.atoiX(0.15);
            inbox.Height = dch.atoiY(0.25);
            inbox.X = dch.atoiX(0.15) - (inbox.Width / 2);
            inbox.Y = dch.atoiY(0.25) - (inbox.Height / 2);
            inbox.Overflow = UIOverflow.Visible;
            inbox.Text = "Inbox";
            inbox.MouseReleased += InboxButtonClicked;
            Desk.AddControl(inbox);

            outbox = new Button(whiterect, whiterect, spf);
            outbox.Width = dch.atoiX(0.15);
            outbox.Height = dch.atoiY(0.25);
            outbox.X = dch.atoiX(0.85) - (outbox.Width / 2);
            outbox.Y = dch.atoiY(0.25) - (outbox.Height / 2);
            outbox.Overflow = UIOverflow.Visible;
            outbox.Text = "Outbox";
            outbox.Font = spf;
            Desk.AddControl(outbox);

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
                Contract a = new Contract(String.Format("Test contract {0}", j));
                for (int i = 0; i < 10; i++)
                {
                    ContractClause cc = new ContractClause(String.Format("Test clause {0}", i));
                    a.Clauses.Add(cc);
                }
                AddContractToInbox(a);
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
            GridHelper gh = new GridHelper(new Point(0,0), new Point(InboxPanel.InnerRect.Width, 20), new Point(0, 10));
            if(c == null) { return; }

            Rectangle r = gh.GetRect(0, InboxPanel.Children.Count);

            Button b = new Button(this.BackgroundSprite, this.BackgroundSprite, inbox.Font);
            b.X = r.X;
            b.Y = r.Y;
            b.Width = r.Width;
            b.Height = r.Height;
            b.SetAnchor(Side.Left);
            b.SetAnchor(Side.Right);
            b.HoverColor = Color.Yellow;
            b.BackgroundColor = Color.Violet;
            b.Font = this.Font;


            InboxPanel.AddControl(b);
            b.Value = c;
            b.MouseReleased += InboxPanelButtonClicked;

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
    }                         
}
