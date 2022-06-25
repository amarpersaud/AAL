using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using MonoGame.CExt.UI;
using MonoGame.CExt.Sprites;
namespace AAL.UI
{
    public class DeskUIHandler : UIHandler
    {
        
        public DeskUIHandler(Rectangle screen, Sprite deskTexture, Sprite inboxTexture, Sprite outboxTexture, Sprite fireTexture) : base(screen){

            Initialize( deskTexture,  inboxTexture,  outboxTexture,  fireTexture);
        }

        public void Initialize(Sprite deskTexture, Sprite inboxTexture, Sprite outboxTexture, Sprite fireTexture)
        {
            Panel Desk = new Panel();
            Desk.Width = this.ScreenRect.Width / 2;
            Desk.Height = this.ScreenRect.Width / 4;
            Desk.X = (this.ScreenRect.Width / 2) - (Desk.Width / 2);
            Desk.Y = (this.ScreenRect.Height / 2) - (Desk.Height / 2);
            Desk.Overflow = UIOverflow.Visible;
            Desk.Parent = this;
            Desk.BackgroundSprite = deskTexture;
            this.Children.Add(Desk);

            Button inbox = new Button(inboxTexture, inboxTexture);
            inbox.Width = this.ScreenRect.Width / 2;
            inbox.Height = this.ScreenRect.Width / 4;
            inbox.X = (this.ScreenRect.Width / 2) - (inbox.Width / 2);
            inbox.Y = (this.ScreenRect.Height / 2) - (inbox.Height / 2);
            inbox.Overflow = UIOverflow.Visible;
            inbox.Parent = this;
            this.Children.Add(Desk);

        }


    }
}
