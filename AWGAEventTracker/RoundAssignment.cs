using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWGAEventTracker
{
    class RoundAssignment 
    {
        //Contains the functionality that generates rounds and groups and writes them to the DB. 
        //The event object passed to the constructor is also updated with the assignments 
        // as an artifiact of this process.
        Event theEvent;
        int nTeamCount;
        private List<Player> lstAPlayers;
        private List<Player> lstBPlayers;
        private List<Player> lstCPlayers;
        private List<Player> lstDPlayers;

        //Constructor
        public RoundAssignment(Event selectedevent)
        {
            theEvent = selectedevent;
            nTeamCount = theEvent.lstAssignedPlayers.Count / 4;

            //Ini the data objects contained in the event's list of rounds, down to the group level,
            // so we can modify their properties later as we generate rounds and groups.
            theEvent.lstRounds = new List<Round>(theEvent.nRounds);
            for (int i = 0; i < theEvent.nRounds; i++)
            {
                theEvent.lstRounds.Add(new Round(i + 1));
                for (int j = 0; j < nTeamCount; j++) //Note: The number of groups in each round is the same as the number of teams in the event
                    theEvent.lstRounds[i].addGroup(new GroupOfFour(j + 1));
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
                lstAPlayers.Add(theEvent.lstTeams[i].playerA);
                lstBPlayers.Add(theEvent.lstTeams[i].playerB);
                lstCPlayers.Add(theEvent.lstTeams[i].playerC);
                lstDPlayers.Add(theEvent.lstTeams[i].playerD);
            }
        }

        //Called externally to start round generation using the event object passed to the constructor.
        //Calls solveRounds(), which is the recursive assignment algorithm, then writes the round/group
        // assignments to the DB on success. Returns true iff success.
        public bool generateRounds()
        {
            //Ensure teams exists. 
            if (theEvent.lstTeams.Count <= 0)
            {
                MessageBox.Show("ERROR: Rounds cannot be generated because no teams exist for this event.");
                return false;
            }

            //Ensure no rounds exist for this event
            string strCmd = "SELECT COUNT (*) FROM Rounds WHERE eventID =" + theEvent.nID.ToString();
            OleDbCommand dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
            try
            {
                if ((int)dbCmd.ExecuteScalar() > 0)
                {
                    MessageBox.Show("ERROR: Rounds cannot be generated because rounds for this event already exist.");
                    return false;
                }
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1009: Could not get rounds for event.\n" + ex0.Message);
            }

            //Do the round/group assignments
            bool bResult = solveRounds();

            //If assignments failed, there were not enough teams to create unique group assignments across all rounds.
            if (!bResult)
            {
                MessageBox.Show("ERROR: There are only " + theEvent.lstTeams.Count.ToString() + " teams, which is not enough for " + theEvent.nRounds.ToString() + " rounds.");
                return false;
            }

            //Write the rounds/groups to the DB
            try
            {
                foreach (Round round in theEvent.lstRounds)
                {
                    //Write the round to the db table Rounds
                    strCmd = "INSERT INTO Rounds (eventID, roundNumber, roundName) ";
                    strCmd += "VALUES (" + theEvent.nID.ToString() + ", " + round.nRoundNumber.ToString() + ", 'Week " + round.nRoundNumber.ToString() + "'); ";
                    dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
                    dbCmd.ExecuteNonQuery();

                    //Get the round id of the round just written
                    strCmd = "SELECT roundID FROM Rounds WHERE eventID = ";
                    strCmd += theEvent.nID.ToString() + " AND roundNumber = " + round.nRoundNumber.ToString();
                    dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
                    int nRoundID = (int)dbCmd.ExecuteScalar();
                    
                    //Write each group to the DB table Groups. 
                    foreach (GroupOfFour g in round.lstGroups)
                    {
                        strCmd = "INSERT INTO Groups (roundID, groupNumber, aPlayerID, bPlayerID, cPlayerID, dPlayerID) VALUES (";
                        strCmd += nRoundID.ToString() + ", " + g.nGroupNumber + ", " + g.playerA.ID + ", " + g.playerB.ID + ", " + g.playerC.ID + ", " + g.playerD.ID + ")";
                        dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
                        dbCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR 1007: Could not add rounds due to a database error.\n" + ex.Message);
                return false;
            }

            MessageBox.Show("Success! " + theEvent.nRounds.ToString() + " rounds have been generated.");
            
            return bResult;
        }

        //A recursive backtracking function to generate groups for each round using constraint satisfaction.
        //The constraints are that no player can be in a group with a player that they've been in a group with before.
        private bool solveRounds()
        {
            //Get the next unpopulated player slot's round and group index, as well as player level requirement.
            int round = -1; //round index
            int group = -1; //group index
            string level = ""; //level requirment
            if (!getNextUnassigned(out round, out group, out level)) //after this call, round, group, and level (above) are populated
                return true; //recursive base case. Denotes goal state

            //Ini player list to operate on. Will be the list for a player of level A-D, depending on which level of player
            // the current slot needs.
            List<Player> players = null; 
            if (level == "A")
                players = lstAPlayers;
            if (level == "B")
                players = lstBPlayers;
            if (level == "C")
                players = lstCPlayers;
            if (level == "D")
                players = lstDPlayers;

            //iterate each "team column" in Nadines spreadsheet and shift each col down by 1 until we find a valid player 
            // assignment for the current player slot.
            for (int i = 0; i < nTeamCount; i++) 
            {
                //starting with team 1 for each rounds  
                int nTeamNumber = 1 - i;
                if (nTeamNumber <= 0)
                    nTeamNumber = nTeamNumber + nTeamCount;
                Player p = players[nTeamNumber - 1];

                //Check if the player we want to assign to the current slot hs any of the potential group members as constraints
                // and if if not, assign them. 
                if (isValidAssignment(round, group, p)) 
                {
                    Event eventCopy = theEvent;

                    if (level == "A")
                        theEvent.lstRounds[round].lstGroups[group].playerA = p;
                    else if (level == "B")
                        theEvent.lstRounds[round].lstGroups[group].playerB = p;
                    else if (level == "C")
                        theEvent.lstRounds[round].lstGroups[group].playerC = p;
                    else if (level == "D")
                        theEvent.lstRounds[round].lstGroups[group].playerD = p;

                    setConstraints(theEvent.lstRounds[round].lstGroups[group]);

                    if (solveRounds())
                        return true;

                    theEvent = eventCopy; //if we get here, backtrack and try the next value
                }
            }
            return false; //if we get here, no valid assignment was found.

        }

        //gets the next player spot that needs populating, starting from the last round because
        // that's when players play themselves. Returns null if none found.
        private bool getNextUnassigned(out int round, out int group, out string level)
        {
            for (round = theEvent.nRounds - 1; round >= 0; round--) //for every round (starting from the last)
            {
                for (group = 0; group < nTeamCount; group++) //Iterate every foursome this round will contain (starting from the first) 
                {
                    if (theEvent.lstRounds[round].lstGroups[group].playerA == null)
                    {
                        level = "A";
                        return true;
                    }
                    else if (theEvent.lstRounds[round].lstGroups[group].playerB == null)
                    {
                        level = "B";
                        return true;
                    }
                    else if (theEvent.lstRounds[round].lstGroups[group].playerC == null)
                    {
                        level = "C";
                        return true;
                    }
                    else if (theEvent.lstRounds[round].lstGroups[group].playerD == null)
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
            if (isPlayerAlreadyInRound(p.ID, theEvent.lstRounds[round]))
                return false;

            //ensure player hasn't already played with anyone in this group
            if (theEvent.lstRounds[round].lstGroups[group].playerA != null && theEvent.lstRounds[round].lstGroups[group].playerA.isConstrained(p.ID))
                return false;
            else if (theEvent.lstRounds[round].lstGroups[group].playerB != null && theEvent.lstRounds[round].lstGroups[group].playerB.isConstrained(p.ID))
                return false;
            else if (theEvent.lstRounds[round].lstGroups[group].playerC != null && theEvent.lstRounds[round].lstGroups[group].playerC.isConstrained(p.ID))
                return false;
            else if (theEvent.lstRounds[round].lstGroups[group].playerD != null && theEvent.lstRounds[round].lstGroups[group].playerD.isConstrained(p.ID))
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
