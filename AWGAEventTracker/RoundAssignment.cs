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
        int nTeamCount;
        private List<Player> lstAPlayers;
        private List<Player> lstBPlayers;
        private List<Player> lstCPlayers;
        private List<Player> lstDPlayers;

        //Constructor
        public RoundAssignment(Event selectedevent)
        {
            e = selectedevent;
            nTeamCount = e.lstAssignedPlayers.Count / 4;

            //ini the data objects contained in the event's list of rounds
            e.lstRounds = new List<Round>(e.nRounds);
            for (int i = 0; i < e.nRounds; i++)
            {
                e.lstRounds.Add(new Round(0));
                for (int j = 0; j < nTeamCount; j++)
                    e.lstRounds[i].addGroup(new GroupOfFour());
            }

            //Construct lists of A, B, C, and D players
            //Note that across these lists, players that share an index with a player in 
            // another list are on the same team.
            lstAPlayers = new List<Player>();
            lstBPlayers = new List<Player>();
            lstCPlayers = new List<Player>();
            lstDPlayers = new List<Player>();
            for (int i = 0; i < nTeamCount; i++)
            {
                lstAPlayers.Add(e.lstTeams[i].playerA);
                lstBPlayers.Add(e.lstTeams[i].playerB);
                lstCPlayers.Add(e.lstTeams[i].playerC);
                lstDPlayers.Add(e.lstTeams[i].playerD);
            }
        }

        //Called externally to start round generation
        public bool generateRounds()
        {
            //Ensure teams exists. 
            if (e.lstTeams.Count <= 0)
            {
                MessageBox.Show("ERROR: Rounds cannot be generated because no teams exist for this event.");
                return false;
            }
            
            bool bResult = solveRounds();

            if (!bResult)
            {
                MessageBox.Show("ERROR: There are only " + e.lstTeams.Count.ToString() + " teams, which is not enough for " + e.nRounds.ToString() + " rounds.");
                return false;
            }
            
            //TODO: Write the rounds/groups to the DB

            return bResult;
        }

        private bool solveRounds()
        {
            //Get the next unpopulated round, group, and level
            int round = -1;
            int group = -1;
            string level = "";
            if (!getNextUnassigned(out round, out group, out level)) 
                return true; //recursive base case. Denotes goal state

            //ini player list to operate on
            List<Player> players = null;
            if (level == "A")
                players = lstAPlayers;
            if (level == "B")
                players = lstBPlayers;
            if (level == "C")
                players = lstCPlayers;
            if (level == "D")
                players = lstDPlayers;

            for (int i = 0; i < nTeamCount; i++)
            {
                int nTeamNumber = 1 - i;
                if (nTeamNumber <= 0)
                    nTeamNumber = nTeamNumber + nTeamCount;
                Player p = players[nTeamNumber - 1];

                if (isValidAssignment(round, group, p))
                {
                    Event eventCopy = e;

                    if (level == "A")
                        e.lstRounds[round].lstGroups[group].playerA = p;
                    else if (level == "B")
                        e.lstRounds[round].lstGroups[group].playerB = p;
                    else if (level == "C")
                        e.lstRounds[round].lstGroups[group].playerC = p;
                    else if (level == "D")
                        e.lstRounds[round].lstGroups[group].playerD = p;

                    setConstraints(e.lstRounds[round].lstGroups[group]);

                    if (solveRounds())
                        return true;

                    e = eventCopy; //if we get here, backtrack
                }
            }
            return false; //if we get here, no valid assignment was found.

        }

        //gets the next player spot that needs populating. returns null if none found.
        private bool getNextUnassigned(out int round, out int group, out string level)
        {
            for (round = e.nRounds - 1; round >= 0; round--) //for every round (starting from the last)
            {
                for (group = 0; group < nTeamCount; group++) //Iterate every foursome this round will contain (starting from the first) 
                {
                    if (e.lstRounds[round].lstGroups[group].playerA == null)
                    {
                        level = "A";
                        return true;
                    }
                    else if (e.lstRounds[round].lstGroups[group].playerB == null)
                    {
                        level = "B";
                        return true;
                    }
                    else if (e.lstRounds[round].lstGroups[group].playerC == null)
                    {
                        level = "C";
                        return true;
                    }
                    else if (e.lstRounds[round].lstGroups[group].playerD == null)
                    {
                        level = "D";
                        return true;
                    }
                }
            }

            //if we get here, we found no empty player spots
            round = -1;
            group = -1;
            level = "";

            return false;
        }

        private bool isValidAssignment(int round, int group, Player p)
        {
            //ensure this player hasn't already played in this round
            if (isPlayerAlreadyInRound(p.ID, e.lstRounds[round]))
                return false;

            //ensure player hasn't already played with anyone in this group
            if (e.lstRounds[round].lstGroups[group].playerA != null && e.lstRounds[round].lstGroups[group].playerA.isConstrained(p.ID))
                return false;
            else if (e.lstRounds[round].lstGroups[group].playerB != null && e.lstRounds[round].lstGroups[group].playerB.isConstrained(p.ID))
                return false;
            else if (e.lstRounds[round].lstGroups[group].playerC != null && e.lstRounds[round].lstGroups[group].playerC.isConstrained(p.ID))
                return false;
            else if (e.lstRounds[round].lstGroups[group].playerD != null && e.lstRounds[round].lstGroups[group].playerD.isConstrained(p.ID))
                return false;

            return true;
        }

        //Returns true iff a player (by player ID) is assigned to a group in this round
        //n = the playerID of the player being checked
        private bool isPlayerAlreadyInRound(int n, Round r)
        {
            foreach (GroupOfFour g in r.getGroupsList())
            {
                if (g.playerA != null && g.playerA.ID == n)
                    return true;
                else if (g.playerB != null && g.playerB.ID == n)
                    return true;
                else if (g.playerC != null && g.playerC.ID == n)
                    return true;
                else if (g.playerD != null && g.playerD.ID == n)
                    return true;
            }
            return false;
        }

        //Given a GroupOfFour, adds each player to the list of constraints for each other player in that group
        private void setConstraints(GroupOfFour g)
        {
            //This is gross. Should do it iteritively 
            if (g.playerA != null)
                g.playerA.setConstraint(g.playerA.ID);
            if (g.playerA != null && g.playerB != null)
                g.playerA.setConstraint(g.playerB.ID);
            if (g.playerA != null && g.playerC != null)
                g.playerA.setConstraint(g.playerC.ID);
            if (g.playerA != null && g.playerD != null)
                g.playerA.setConstraint(g.playerD.ID);
            if (g.playerB != null && g.playerA != null)
                g.playerB.setConstraint(g.playerA.ID);
            if (g.playerB != null)
                g.playerB.setConstraint(g.playerB.ID);
            if (g.playerB != null && g.playerC != null)
                g.playerB.setConstraint(g.playerC.ID);
            if (g.playerB != null && g.playerD != null)
                g.playerB.setConstraint(g.playerD.ID);
            if (g.playerC != null && g.playerA != null)
                g.playerC.setConstraint(g.playerA.ID);
            if (g.playerC != null && g.playerB != null)
                g.playerC.setConstraint(g.playerB.ID);
            if (g.playerC != null)
                g.playerC.setConstraint(g.playerC.ID);
            if (g.playerC != null && g.playerD != null)
                g.playerC.setConstraint(g.playerD.ID);
            if (g.playerD != null && g.playerA != null)
                g.playerD.setConstraint(g.playerA.ID);
            if (g.playerD != null && g.playerB != null)
                g.playerD.setConstraint(g.playerB.ID);
            if (g.playerD != null && g.playerC != null)
                g.playerD.setConstraint(g.playerC.ID);
            if (g.playerD != null)
                g.playerD.setConstraint(g.playerD.ID);
        }
    }
}
