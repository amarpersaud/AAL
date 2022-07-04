using MonoGame.CExt.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAL.Resources
{
    public class Resource
    {
        /// <summary>
        /// Resource Id number
        /// </summary>
        public int ResourceId { get; set; }

        /// <summary>
        /// Resource Name
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Thumbnail Sprite
        /// </summary>
        public Sprite ThumbnailSprite { get; set; }
    }
}
