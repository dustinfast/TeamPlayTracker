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
        Event e;
        int nTeamsCount;

        //Constructor
        public RoundAssignment(Event selectedevent)
        {
            e = selectedevent;
            nTeamsCount = e.lstAssignedPlayers.Count / 4;

            //ini the data objects contained in the event's list of rounds
            e.lstRounds = new List<Round>(e.nRounds);
            for (int i = 0; i < e.nRounds; i++)
            {
                e.lstRounds.Add(new Round(0));
                for (int j = 0; j < nTeamsCount; j++)
                    e.lstRounds[i].addGroup(new GroupOfFour());
            }
        }

        //Called externally to start round generation
        public bool generateRounds(Event e)
        {
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

            e.bRoundsScheduled = false;

            return solveRounds();

            
            //TODO: Write the rounds/groups to the DB
        }

        private bool solveRounds()
        {
            if (e.bRoundsScheduled) //recursive base case, denoting when scheduling has been "solved"
                return true;

            //Build the rounds, starting with the last round because thats when teams play each other
            for (int i = e.nRounds; i > 0; i--) //for every round as specified in Events:numRounds, 
            {
                e.lstRounds[i - 1].nRoundNumber = i; //Set the round number. 

                for (int j = 0; j < nTeamsCount; j++) //Iterate every foursome this round will contain. Note that foursomes/round count is always the same number as nTeamsCount 
                {
                    GroupOfFour foursome = new GroupOfFour();
                    bool bTerminalNode = false;
                    Event copy = e;

                    //get valid players from each level. If we can't find one, set this as a terminal node.
                    foursome.playerA = e.lstTeams[j].playerA; //the A player can always be from the jth index in the teams list
                    foursome.playerB = getPlayer("B", foursome, e.lstRounds[i - 1]); //getPlayer() gets a valid, unconstrained player that hasn't played in this round yet.
                    if (foursome.playerB == null)
                        bTerminalNode = true;
                    foursome.playerC = getPlayer("C", foursome, e.lstRounds[i - 1]);
                    if (foursome.playerC == null)
                        bTerminalNode = true;
                    foursome.playerD = getPlayer("D", foursome, e.lstRounds[i - 1]);
                    if (foursome.playerD == null)
                        bTerminalNode = true;

                    if (!bTerminalNode)
                    {
                        //Set constraints on each player we just picked
                        setConstraints(foursome);
                        e.lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                        if (solveRounds())
                            return true;
                    }

                    e = copy;
                }
            }

            return false;
        }

        //Given a player level, find a player of that level who hasn't played against anyone in the given foursome previoulsy
        // and also hasn't played yet in the given round.
        //level must be either A, B, C, or D and is the level of the player we need to find.
        private Player getPlayer(string level, GroupOfFour foursome, Round round)
        {
            int nGroupCount = e.lstTeams.Count;
            int nTeamNumber = -1;

            if (level == "B")
            {
                for (int i= 0; i <= nGroupCount; i++)
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
                        return e.lstTeams[nTeamNumber - 1].playerB;
                }
                return null; //return null if we don't find a player
            }
            else if (level == "C")
            {
                for (int i = 0; i <= nGroupCount; i++)
                {
                    nTeamNumber = 1 - i;
                    if (nTeamNumber <= 0)
                        nTeamNumber = nTeamNumber + nGroupCount;
                    int nPlayerID = e.lstTeams[nTeamNumber - 1].playerC.ID;

                    //ensure this player hasn't already played in this round
                    if (isPlayerAlreadyInRound(nPlayerID, round))
                        continue;

                    //ensure player hasn't already played with anyone in this group
                    if (!foursome.playerA.isConstrained(nPlayerID))
                        return e.lstTeams[nTeamNumber - 1].playerC;
                }
                return null; //return null if we don't find a player
            }
            else if (level == "D")
            {
                for (int i = 0; i <= nGroupCount; i++)
                {
                    nTeamNumber = 1 - i;
                    if (nTeamNumber <= 0)
                        nTeamNumber = nTeamNumber + nGroupCount;
                    int nPlayerID = e.lstTeams[nTeamNumber - 1].playerD.ID;

                    //ensure this player hasn't already played in this round
                    if (isPlayerAlreadyInRound(nPlayerID, round))
                        continue;

                    //ensure player hasn't already played with anyone in this group
                    if (!foursome.playerA.isConstrained(nPlayerID))
                        return e.lstTeams[nTeamNumber - 1].playerD;
                }
                return null; //return null if we don't find a player
            }

            return null; //shouldn't ever get here
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
