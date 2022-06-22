using System;
using System.Collections.Generic;
using System.Text;
using AAL.Contracts;

namespace AAL
{
    public class Party
    {
        /// <summary>
        /// Name of party
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of signed contracts with that party
        /// </summary>
        public List<Contract> SignedContracts;

    }
}
