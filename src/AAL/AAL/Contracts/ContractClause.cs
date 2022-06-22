using System;
using System.Collections.Generic;
using System.Text;

namespace AAL.Contracts
{
    /// <summary>
    /// Represents a clause within a contract
    /// </summary>
    public class ContractClause
    {
        /// <summary>
        /// Text to display on parent contract
        /// </summary>
        public string ClauseText { get; set; }

        /// <summary>
        /// List of effects on gameplay to exhibit when the contract is signed 
        /// </summary>
        public List<ContractEffect> Effects;

        /// <summary>
        /// True if clause is enabled / applicable to the current contract.
        /// </summary>
        public bool Enabled { get; set; }

    }
}
