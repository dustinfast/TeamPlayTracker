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
        //Generates the rounds schedule. Uses constraint propogation to ensure groups are unique across all rounds
        //Returns true and assigns rouds to event if round generation succeeds. false otherwise.
        //Note that in this implementation, the number of teams must be <= the number of rounds (otherwise players would play with
        // a player they've already played with) we may need to change this.
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

                        //Propogate constraints on each player we just picked
                        setConstraints(foursome);

                        e.lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                    }
                }
                else
                {
                    for (int j = 0; j < nTeamsCount; j++) //for every foursome this round will contain. Note that foursomes/round count is always the same number as nTeamsCount 
                    {
                        GroupOfFour foursome = new GroupOfFour();

                        //get players from each level from random teams
                        //TODO: Check constraints before assignment. Propage constraint after each assignment

                        foursome.playerA = e.lstTeams[j].playerA; //the A player can always be from the jth index in the teams list
                        foursome.playerB = getPlayer("B", foursome, e.lstRounds[i - 1], e); //getPlayer() gets a valid, unconstrained player that hasn't played in this round yet.
                        foursome.playerC = getPlayer("C", foursome, e.lstRounds[i - 1], e);
                        foursome.playerD = getPlayer("D", foursome, e.lstRounds[i - 1], e);

                        //Propogate constraints on each player we just picked
                        setConstraints(foursome);

                        e.lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                    }
                }
            }
            //TODO: Write the rounds/groups to the DB

            return true; //success
        }

        //Given a player level, find a player of that level who hasn't played against anyone in the foursome previoulsy
        // and also hasn't played yet in this round.
        //level must be either A, B, C, or D and is the level of the player we're currently checking for
        private Player getPlayer(string level, GroupOfFour foursome, Round round, Event e)
        {
            int nGroupCount = e.lstTeams.Count;
            int nTeamNumber = -1;
            
            if (level == "B")
            {
                for (int i = 1; i <= nGroupCount; i++)
                {
                    nTeamNumber = 1 - i;
                    if (nTeamNumber <= 0)
                        nTeamNumber = nTeamNumber + nGroupCount;
                    int nPlayerID = e.lstTeams[nTeamNumber - 1].playerB.ID;

                    //ensure this player hasn't already played in this round
                    if (isPlayerAlreadyInRound(nPlayerID, round))
                        continue;

                    //ensure player hasn't already played with anyone in this group
                    if (!foursome.playerA.isConstrained(nPlayerID))
                        break;
                }
                return e.lstTeams[nTeamNumber - 1].playerB;
            }
            else if (level == "C")
            {
                for (int i = 1; i <= nGroupCount; i++)
                {
                    nTeamNumber = 1 - i;
                    if (nTeamNumber <= 0)
                        nTeamNumber = nTeamNumber + nGroupCount;
                    int nPlayerID = e.lstTeams[nTeamNumber - 1].playerC.ID;

                    //ensure this player hasn't already played in this round
                    if (isPlayerAlreadyInRound(nPlayerID, round))
                        continue;

                    //ensure player hasn't already played with anyone in this group
                    if (!foursome.playerA.isConstrained(nPlayerID) &&
                        !foursome.playerB.isConstrained(nPlayerID))
                        break;
                }
                return e.lstTeams[nTeamNumber - 1].playerC;
            }
            else if (level == "D")
            {
                for (int i = 1; i <= nGroupCount; i++)
                {
                    nTeamNumber = 1 - i;
                    if (nTeamNumber <= 0)
                        nTeamNumber = nTeamNumber + nGroupCount;
                    int nPlayerID = e.lstTeams[nTeamNumber - 1].playerD.ID;

                    //ensure this player hasn't already played in this round
                    if (isPlayerAlreadyInRound(nPlayerID, round))
                        continue;

                    //ensure player hasn't already played with anyone in this group this round
                    if (!foursome.playerA.isConstrained(nPlayerID) &&
                        !foursome.playerB.isConstrained(nPlayerID) &&
                        !foursome.playerC.isConstrained(nPlayerID))
                        break;
                }
                return e.lstTeams[nTeamNumber - 1].playerD;
            }
            else
                return null; //shouldn't happen if the function is called correctly. TODO: add error handling if it does
        }

        //Returns true iff a player (by player ID) is assigned to a group in this round
        //n = the playerID of the player being checked
        private bool isPlayerAlreadyInRound(int n, Round r)
        {
            foreach (GroupOfFour g in r.getGroupsList())
            {
                if (g.playerA != null && g.playerA.ID == n)
                    return true;
                if (g.playerB != null && g.playerB.ID == n)
                    return true;
                if (g.playerC != null && g.playerC.ID == n)
                    return true;
                if (g.playerD != null && g.playerD.ID == n)
                    return true;
            }
            return false;
        }

        //Given a GroupOfFour, adds each player to the list of constraints for each other player in that group
        private void setConstraints(GroupOfFour g)
        {
            g.playerA.setConstraint(g.playerA.ID);
            g.playerA.setConstraint(g.playerB.ID);
            g.playerA.setConstraint(g.playerC.ID);
            g.playerA.setConstraint(g.playerD.ID);
            g.playerB.setConstraint(g.playerA.ID);
            g.playerB.setConstraint(g.playerB.ID);
            g.playerB.setConstraint(g.playerC.ID);
            g.playerB.setConstraint(g.playerD.ID);
            g.playerC.setConstraint(g.playerA.ID);
            g.playerC.setConstraint(g.playerB.ID);
            g.playerC.setConstraint(g.playerC.ID);
            g.playerC.setConstraint(g.playerD.ID);
            g.playerD.setConstraint(g.playerA.ID);
            g.playerD.setConstraint(g.playerB.ID);
            g.playerD.setConstraint(g.playerC.ID);
            g.playerD.setConstraint(g.playerD.ID);
        }
    }
}
