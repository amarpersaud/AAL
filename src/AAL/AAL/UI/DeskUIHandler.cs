using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.CExt.UI;
using MonoGame.CExt.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace AAL.UI
{
    public class DeskUIHandler : UIHandler
    {

        public UIContract ContractControl;
        public Panel InboxPanel;
        public Panel OutboxPanel;

        public DeskUIHandler(Rectangle screen, Sprite deskTexture, Sprite inboxTexture, Sprite outboxTexture, Sprite fireTexture, SpriteFont spf) : base(screen){

            Initialize(screen, deskTexture, inboxTexture, outboxTexture, fireTexture, spf);
        }

        public void Initialize(Rectangle screen, Sprite deskTexture, Sprite inboxTexture, Sprite outboxTexture, Sprite fireTexture, SpriteFont spf)
        {
            CoordinateHelper sch = new CoordinateHelper(screen.Width, screen.Height);

            Panel Desk = new Panel();
            Desk.Width = sch.atoiX(0.75);
            Desk.Height = sch.atoiY(0.55);
            Desk.X = sch.atoiX(0.5) - (Desk.Width / 2);
            Desk.Y = sch.atoiY(0.60) - (Desk.Height / 2);
            Desk.Overflow = UIOverflow.Visible;
            Desk.BackgroundSprite = deskTexture;
            Desk.BackgroundColor = Color.Brown;
            this.AddControl(Desk);

            CoordinateHelper dch = new CoordinateHelper(Desk.Width, Desk.Height);

            //InboxPanel = new Panel();

            Button inbox = new Button(inboxTexture, inboxTexture, spf);
            inbox.Width = dch.atoiX(0.15);
            inbox.Height = dch.atoiY(0.25);
            inbox.X = dch.atoiX(0.15) - (inbox.Width / 2);
            inbox.Y = dch.atoiY(0.25) - (inbox.Height / 2);
            inbox.Overflow = UIOverflow.Visible;
            inbox.Text = "Inbox";
            Desk.AddControl(inbox);

            Button outbox = new Button(outboxTexture, outboxTexture, spf);
            outbox.Width = dch.atoiX(0.15);
            outbox.Height = dch.atoiY(0.25);
            outbox.X = dch.atoiX(0.85) - (outbox.Width / 2);
            outbox.Y = dch.atoiY(0.25) - (outbox.Height / 2);
            outbox.Overflow = UIOverflow.Visible;
            outbox.Text = "Outbox";
            outbox.Font = spf;
            Desk.AddControl(outbox);
                              
        }                     
                              
                              
    }                         
}
