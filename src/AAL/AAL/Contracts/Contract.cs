using System;
using System.Collections.Generic;
using System.Text;

namespace AAL.Contracts
{
    /// <summary>
    /// Represents a contract with a party
    /// </summary>
    public class Contract
    {
        /// <summary>
        /// Title of contract
        /// </summary>
        public string ContractTitle { get; set; }

        /// <summary>
        /// List of contract clauses
        /// </summary>
        public List<ContractClause> Clauses;

        /// <summary>
        /// First signer (player if applicable)
        /// </summary>
        public Party FirstParty;
 
        /// <summary>
        /// Second signer
        /// </summary>
        public Party SecondParty;

        /// <summary>
        /// True if first party has signed the contract
        /// </summary>
        public bool FirstPartySigned { get; set; } = false;

        /// <summary>
        /// True if second party has signed the contract
        /// </summary>
        public bool SecondPartySigned { get; set; } = false;

        public Contract(string contractTitle, Party firstParty = null, Party secondParty = null)
        {
            ContractTitle = contractTitle;
            Clauses = new List<ContractClause>();
            FirstParty = firstParty;
            SecondParty = secondParty;
        }

        public string GetFullText()
        {
            string s = "";

            foreach(ContractClause cc in Clauses)
            {
                s += String.Format("{0}\n\n", cc.ClauseText);
            }

            return s;
        }

    }
}
