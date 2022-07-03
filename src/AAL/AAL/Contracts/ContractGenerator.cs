using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.CExt.Extensions;

namespace AAL.Contracts
{
    /// <summary>
    /// Temporary class for contract generation
    /// </summary>
    public static class ContractGenerator
    {
        private static Random r = new Random();
        private static Party PlayerParty = new Party();
        private static List<Party> Parties;
        private static List<string> Names = "Albert – Alexander – Alfred – Algernon – Allen – Ambrose – Andrew – Anthony – Archibald – Archie – Arthur – Aubrey – August – Augustine – Augustus – Basil – Ben – Benjamin – Bernard – Bert – Bertram – Carl – Cecil – Cedric – Charles – Charley – Charlie – Chester – Clarence – Claude – Clement – Clifford – Clyde – Cornelius – Cuthbert – Cyril – Daniel – David – Donald – Douglas – Duncan – Earl – Ebenezer – Ed – Eddie – Edgar – Edmund – Edward – Edwin – Elmer – Ernest – Eugene – Eustace – Evan – Everett – Ewart – Felix – Fergus – Floyd – Francis – Frank – Franklin – Fred – Frederick – Geoffrey – George – Gerald – Gilbert – Grover – Guy – Harold – Harry – Harvey – Henry – Herbert – Herman – Horace – Howard – Hubert – Hugh – Hugo – Humphrey – Ira – Isaac – Ivan – Ivor – Jack – Jacob – James – Jasper – Jessie – Jim – Joe – John – Jonathan – Joseph – Julian – Julius – Kenneth – Laurence – Lawrence – Lee – Leo – Leonard – Leopold – Leroy – Leslie – Lewis – Lionel – Llewellyn – Lloyd – Louis – Luther – Malcolm – Marion – Martin – Maurice – Maxwell – Michael – Miles – Montague – Neville – Nigel – Oliver – Oscar – Otto – Owen – Patrick – Paul – Percival – Percy – Peter – Philip – Ralph – Randolph – Ray – Raymond – Reginald – Reuben – Richard – Robert – Roderick – Roger – Roy – Rufus – Rupert – Sam – Samuel – Septimus – Sidney – Silas – Simeon – Stanley – Stephen – Theodore – Thomas – Timothy – Tom – Valentine – Vernon – Victor – Vincent – Walter – Warren – Wilfred – Will – William – Willie".Split(" – ").ToList();
0       public static List<string> ClauseStrings = new List<string>()
        {
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

        public static void Initialize()
        {
            ///Generate parties
            for(int i = 0; i < Names.Count; i++)
            {
                Party p = new Party();
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
            Party p = Parties.Random();

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
