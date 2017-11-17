using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWGAEventTracker
{
    class RoundAssignment
    {
        //Generates the round schedule, including unqiue player "pairings" across groups, and assigns them to Event e.
        //returns true if round generation succeeds. false otherwise.
        public bool generateRounds(Event e)
        {
            int nTeamsCount = e.lstAssignedPlayers.Count / 4;
            Random rand = new Random(); //ini randomizer

            //Ensure teams exists. 
            if (e.lstTeams.Count <= 0)
            {
                MessageBox.Show("ERROR: Rounds cannot be generated because no teams exist for this event.");
                return false;
            }

            //if number of players divided by four (i.e. number of teams) is less than the
            // number of rounds for this event, there will not be enough players for all the rounds.
            if (nTeamsCount < e.nRounds)
            {
                MessageBox.Show("ERROR: There are only " + e.lstTeams.Count.ToString() + " teams, which is not enough for " + e.nRounds.ToString() + " rounds.");
                return false;
            }

            //ini the data objects contained in the event's list of rounds so we can modify them
            e.lstRounds = new List<Round>(e.nRounds);
            for (int i = 0; i < e.nRounds; i++)
            {
                e.lstRounds.Add(new Round(0));
                for (int j = 0; j < nTeamsCount; j++)
                    e.lstRounds[i].addGroup(new GroupOfFour());
            }
       
            //Build the rounds, starting with the last round because thats where teams play each other
            // and those constraints must be established before processing the other rounds.
            for (int i = e.nRounds; i > 0; i--) //for every round as specified in Events:numRounds, 
            {
                //Round round = new Round(i); //ini a round with i as it's round number
                e.lstRounds[i - 1].nRoundNumber = i;

                //If this is the last round, 
                if (i == e.nRounds)
                {
                    for (int j = 0; j < nTeamsCount; j++) //for every foursome this round will contain
                    {
                        GroupOfFour foursome = new GroupOfFour();
                        foursome.playerA = e.lstTeams[j].playerA;
                        foursome.playerB = e.lstTeams[j].playerB;
                        foursome.playerC = e.lstTeams[j].playerC;
                        foursome.playerD = e.lstTeams[j].playerD;

                        //TODO: Set the constraints on each player we just picked
                        e.lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                    }
                }
                else
                {
                    for (int j = 0; j < nTeamsCount; j++) //for every foursome this round will contain. Note that foursomes/round is always the same number as nTeamsCount 
                    {
                        GroupOfFour foursome = new GroupOfFour();
                        bool bFlag = true; //flag used to determine when to break out of the below while loop

                        while (bFlag)
                        {
                            //get players from each level from random teams
                            foursome.playerA = e.lstTeams[rand.Next(1, nTeamsCount)].playerA;
                            foursome.playerB = e.lstTeams[rand.Next(1, nTeamsCount)].playerB;
                            foursome.playerC = e.lstTeams[rand.Next(1, nTeamsCount)].playerC;
                            foursome.playerD = e.lstTeams[rand.Next(1, nTeamsCount)].playerD;
                            break;
                        }
                        e.lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                    }
                }
            }
            return true;
        }
    }
}
