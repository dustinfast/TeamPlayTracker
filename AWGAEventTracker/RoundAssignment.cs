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
        //The event object passed to the constructor is updated with the assignments as a result of this process.
        //Note that teams will be unique (players never play in a group with someone they've already played with) for 
        // as long as possible, then allow duplicates to occur.
        Event eEvent; //the event passed to constructor
        Event eBestEvent; //The event w the deepest node we were able to assign a player at without duplicates. 
        int nTeamCount; 

        //Constructor
        public RoundAssignment(Event selectedevent)
        {
            eEvent = selectedevent;
            eEvent.nAssignmentDepth = 0;
            eBestEvent = new Event();
            eBestEvent.nAssignmentDepth = 0;
            nTeamCount = eEvent.lstAssignedPlayers.Count / 4;

            //Ini the data objects contained in the event's list of rounds, down to the group level,
            // so we can modify their properties later as we generate rounds and groups.
            eEvent.lstRounds = new List<Round>(eEvent.nRounds);
            for (int i = 0; i < eEvent.nRounds; i++)
            {
                eEvent.lstRounds.Add(new Round((eEvent.nRounds - i), "Week " + (eEvent.nRounds - i).ToString()));
                for (int j = 0; j < nTeamCount; j++) //Note: The number of groups in each round is the same as the number of teams in the event
                    eEvent.lstRounds[i].addGroup(new GroupOfFour(j + 1));
            }
        }

        //Called externally to start round generation using the event object passed to the constructor.
        //Calls solveRounds(), which is the recursive assignment algorithm, then writes the round/group
        // assignments to the DB on success. Returns true iff success.
        public bool generateRounds()
        {
            //Ensure teams exists. 
            if (eEvent.lstTeams == null || eEvent.lstTeams.Count <= 0)
            {
                MessageBox.Show("ERROR: Rounds cannot be generated because no teams exist for this event.");
                return false;
            }

            //Ensure no rounds exist for this event
            string strCmd = "SELECT COUNT (*) FROM Rounds WHERE eventID =" + eEvent.nID.ToString();
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

            //If assignments failed, there were not enough teams to create unique group assignments across all rounds, 
            if (!bResult)
            {
                string strTemp = "Error: Rounds could not be generated because " + nTeamCount + " teams is not enough to create " + eEvent.nRounds + " unique rounds.";
                MessageBox.Show(strTemp);
                return false;
            }

            //Write the rounds/groups to the DB (in reverse order than how we generated them, hence the stack
            Stack<Round> sRounds = new Stack<Round>();
            foreach (Round round in eEvent.lstRounds)
                sRounds.Push(round);
            try
            {
                foreach (Round round in sRounds)
                {
                    //Write the round to the db table Rounds
                    strCmd = "INSERT INTO Rounds (eventID, roundNumber, roundName) ";
                    strCmd += "VALUES (" + eEvent.nID.ToString() + ", " + round.nRoundNumber.ToString() + ", '" + round.strRoundName + "'); ";
                    dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
                    dbCmd.ExecuteNonQuery();

                    //Get the round id of the round just written
                    strCmd = "SELECT roundID FROM Rounds WHERE eventID = ";
                    strCmd += eEvent.nID.ToString() + " AND roundNumber = " + round.nRoundNumber.ToString();
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

            MessageBox.Show("Success! " + eEvent.nRounds.ToString() + " rounds have been generated.");
            
            return bResult;
        }

        //A recursive backtracking function to generate groups for each round using constraint satisfaction.
        //The constraints are that no player can be in a group with a player that they've been in a group with before.
        //Note: Do not pass parameters to this function; they are used by the recursive calls only.
        private bool solveRounds(int offset = 0)
        {
            //Get the next unpopulated player slot's round and group index, as well as player level requirement.
            int nRound = -1; //round index
            int nGroup = -1; //group index
            string strLevel = ""; //level requirment

            if (!getNextUnassigned(out nRound, out nGroup, out strLevel)) //after this call, round, group, and level (above) are populated
                return true; //recursive base case. Denotes goal state

            //Ini index, based on the last assigned slot's offset, Unless this is the last round (i.e. the first rond we process), 
            // in that case, do not allow the "team" column to shift (i.e. teams play themselves)
            //Else, offset will later be shifted by one from the given offset like a circular linked list until all indexes are tried.
            int nIndex = 0 + offset;

            //ini a list of teams. We will try each team number once, removing it from the list as we do
            List<int> lstTried = new List<int>();
            for (int i = 0; i < nTeamCount; i++)
                lstTried.Add(i);

            //Iterate each "team column", (i.e. all players of the given level, by team)
            while (true) 
            {
                nIndex += 1;
                if (nIndex > nTeamCount && lstTried.Count != 0) //start over from 0th element, since we haven't gone 0 through nOrigOffset-1 yet
                    nIndex = 1;
                else if (lstTried.Count == 0)
                    break;

                Player p = null;
                if (strLevel == "A")
                    p = eEvent.lstTeams[nIndex-1].playerA;
                else if (strLevel == "B")
                    p = eEvent.lstTeams[nIndex-1].playerB;
                else if (strLevel == "C")
                    p = eEvent.lstTeams[nIndex-1].playerC;
                else if (strLevel == "D")
                    p = eEvent.lstTeams[nIndex-1].playerD;
                lstTried.Remove(nIndex - 1);

                //Check if the player we want to assign to the current slot has any of the potential group members as constraints
                // and if if not, assign them. 
                if (isValidAssignment(nRound, nGroup, p)) 
                {
                    Event eventCopy = eEvent; //store a copy of event before making changes, in order to backtrack to it if needed.

                    //update assignment depths, then if we're deeper than we've been before, set new best event
                    eEvent.nAssignmentDepth++;
                    if (eEvent.nAssignmentDepth > eBestEvent.nAssignmentDepth)
                        eBestEvent = eEvent;

                    //Assign player slot, based on the open slot's player level
                    int nPass = nIndex-1;
                    ////int nPass = (nIndex-1)*nRound;
                    if (strLevel == "A")
                        eEvent.lstRounds[nRound].lstGroups[nGroup].playerA = p;
                    else if (strLevel == "B")
                        eEvent.lstRounds[nRound].lstGroups[nGroup].playerB = p;
                    else if (strLevel == "C")
                        eEvent.lstRounds[nRound].lstGroups[nGroup].playerC = p;
                    else if (strLevel == "D")
                    {
                        eEvent.lstRounds[nRound].lstGroups[nGroup].playerD = p;
                        nPass = 0;
                    }

                    setConstraints(nRound, nGroup);
                    
                    if (solveRounds(nPass))
                        return true;

                    eEvent = eventCopy; //If we made it here, we're backtracking from the previous node
                }
            }
            return false;
        }

        //gets the next player spot that needs populating, starting from the last round because
        // that's when players play themselves. Returns false if no open slot found.
        private bool getNextUnassigned(out int round, out int group, out string level)
        {
            //for (round = 0; round < eEvent.nRounds; round++) //for every round (starting from the first)
            for (round = eEvent.nRounds - 1; round >= 0; round--) //for every round (starting from the last)
            {
                for (group = 0; group < nTeamCount; group++) //Iterate every foursome this round will contain
                {
                    if (eEvent.lstRounds[round].lstGroups[group].playerA == null)
                    {
                        level = "A";
                        return true;
                    }
                    else if (eEvent.lstRounds[round].lstGroups[group].playerB == null)
                    {
                        level = "B";
                        return true;
                    }
                    else if (eEvent.lstRounds[round].lstGroups[group].playerC == null)
                    {
                        level = "C";
                        return true;
                    }
                    else if (eEvent.lstRounds[round].lstGroups[group].playerD == null)
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
            if (isPlayerAlreadyInRound(p.ID, eEvent.lstRounds[round]))
                return false;

            //ensure player hasn't already played with anyone in this group
            if (eEvent.lstRounds[round].lstGroups[group].playerA != null && eEvent.lstRounds[round].lstGroups[group].playerA.isConstrained(p.ID))
                return false;
            else if (eEvent.lstRounds[round].lstGroups[group].playerB != null && eEvent.lstRounds[round].lstGroups[group].playerB.isConstrained(p.ID))
                return false;
            else if (eEvent.lstRounds[round].lstGroups[group].playerC != null && eEvent.lstRounds[round].lstGroups[group].playerC.isConstrained(p.ID))
                return false;
            else if (eEvent.lstRounds[round].lstGroups[group].playerD != null && eEvent.lstRounds[round].lstGroups[group].playerD.isConstrained(p.ID))
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
        private void setConstraints(int round, int group)
        {
            GroupOfFour g = eEvent.lstRounds[round].lstGroups[group];

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
