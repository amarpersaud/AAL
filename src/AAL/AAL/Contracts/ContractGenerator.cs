using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.CExt.Extensions;
using MonoGame.CExt.Utility;

namespace AAL.Contracts
{
    /// <summary>
    /// Temporary class for contract generation
    /// </summary>
    public static class ContractGenerator
    {
        private static Random r = new Random();
        private static Person PlayerParty = new Person();
        private static List<Person> Parties = new List<Person>();
        public static List<string> ClauseStrings = new List<string>{
            "Provide {0} amount of {1}",
            "Accept {0} amount of {1}",
            "Dispose of {0} amount of {1}",
            "Provide {0} amount of {1} per day",
            "Accept {0} amount of {1} per day",
            "Dispose of {0} amount of {1} per day",
            "Build a facility capable of producing {0} amount of {1} per day",
            "Store {0} amount of {1} for a period of one day",
            "Store {0} amount of {1} for a period of one month",
            "Store {0} amount of {1} for a period of one year",
            "Store {0} amount of {1} for a period of one quarter",
        };


        private static List<string> ResourceNames = new List<string>
        {
            "Copper Ingots",
            "Iron Ingots",
            "Lumber",
            "Wagons",
            "Saddles",
            "Goats",
            "Sheep",
            "Bushells of wheat",
            "Barrels of ale"
        };

        public static void Initialize(ResourceHandler rh)
        {
            //Load male names
            List<string> Names = rh.LoadJsonObject<List<string>>("JSON/Male_FirstNames.json", rh.JsonSettings);
            //Load female names
            Names = Names.Concat(rh.LoadJsonObject<List<string>>("JSON/Female_FirstNames.json", rh.JsonSettings)).ToList();

            ///Generate parties
            for(int i = 0; i < Names.Count; i++)
            {
                Person p = new Person();
                p.Name = Names[i];
                Parties.Add(p);
            }
            PlayerParty.Name = "Player";

        }

        /// <summary>
        /// Generate a random test contract
        /// </summary>
        /// <returns>Contract with randomly selected clauses</returns>
        public static Contract GenerateContract()
        {
            Person p = Parties.Random();

            Contract c = new Contract(String.Format("Contract with {0}", p.Name), PlayerParty, p);
            c.Clauses = new List<ContractClause>();
            for(int i = 0; i < 10; i++)
            {
                string cct = String.Format(ClauseStrings.Random(), r.Next(0, 1000), ResourceNames.Random());
                ContractClause cc = new ContractClause(cct);
                c.Clauses.Add(cc);
            }

            return c;
        } 


    }
}
