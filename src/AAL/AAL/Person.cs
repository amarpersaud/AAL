using System;
using System.Collections.Generic;
using System.Text;
using AAL.Contracts;
using MonoGame.CExt.Sprites;

namespace AAL
{
    public class Person
    {
        /// <summary>
        /// Name of person
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sprite for drawing in person
        /// </summary>
        public Sprite InPersonSprite { get; set; }

        /// <summary>
        /// Sprite for thumbnails
        /// </summary>
        public Sprite Thumbnail { get; set; }
    }
}
