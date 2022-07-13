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
        public int Id { get; set; }

        /// <summary>
        /// Resource Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of resource for in game flavor text
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// If true, resource must be whole number amounts
        /// </summary>
        public bool IntegerAmount { get; set; }

        /// <summary>
        /// Unit suffix appended for a single resource of this type 
        /// </summary>
        public string SingleUnit { get; set; }

        /// <summary>
        /// Unit suffix appended for multiples of this type of resource
        /// </summary>
        public string MultipleUnit { get; set; }

        /// <summary>
        /// String format when converting resource to string. D2 by default for non-integer resources.
        /// </summary>
        public string StringFormat { get; set; } = "D2";

        /// <summary>
        /// Thumbnail Sprite
        /// </summary>
        public Sprite ThumbnailSprite { get; set; }

        /// <summary>
        /// Converts an amount of a resource to a unit suffixed string
        /// </summary>
        /// <param name="amt">Amount of resource</param>
        /// <returns>String containing amount of resource plus the units</returns>
        public string GetUnitString(double amt)
        {
            StringBuilder str = new StringBuilder();
            if (IntegerAmount)
            {
                //Round Down
                str.Append(((int)amt).ToString());
            }
            else
            {
                str.Append(amt.ToString("D2"));
            }

            //Add space before units
            str.Append(' ');

            //append unit
            if (((int)amt) == 1)
            {
                str.Append(SingleUnit);
            }
            else
            {
                str.Append(MultipleUnit);
            }
            
            //return and trim spaces if no units
            return str.ToString().Trim();
        }
    }
}
