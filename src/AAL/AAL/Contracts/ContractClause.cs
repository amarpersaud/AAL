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
        /// Format of clause prefix
        /// </summary>
        public const string NUMBERING_PREFIX_FORMAT = "%i. %s";

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

        /// <summary>
        /// Gets the clause text with the given number prefix. 
        /// </summary>
        /// <param name="number">Index of the clause in the parent contract</param>
        /// <returns>Full string with number formatted</returns>
        public string GetFullText(int number)
        {
            //Clear the clause text if it is null
            if(ClauseText == null)
            {
                this.ClauseText = String.Empty;
            }
            return String.Format(NUMBERING_PREFIX_FORMAT, number, this.ClauseText);
        }

        public ContractClause(string clauseText)
        {
            ClauseText = clauseText;

            //Set to empty string if text is null
            if(ClauseText == null)
            {
                ClauseText = String.Empty;
            }

            Effects = new List<ContractEffect>();
        }
    }
}
