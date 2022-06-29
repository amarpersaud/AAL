using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AAL.Contracts;

namespace AAL
{
    public class Player
    {
        /// <summary>
        /// List of contracts player has made
        /// </summary>
        public List<Contract> Contracts;

        /// <summary>
        /// Player name
        /// </summary>
        public string Name { get; set; }


    }
}
