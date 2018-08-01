/// ManageEvents1.cs - Handlers for the Manage Events Dialog box. 
///
/// Dustin Fast and Brooks Woods, 2017

using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace AWGAEventTracker
{
    public partial class ManageEvents1 : Form
    {
        private Event g_selectedEvent = new Event(); //The currently selected event
        
        public ManageEvents1()
        {
            InitializeComponent();
        }

        //Called on close btn click
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Called on 'Create New Event' btn click
        private void btnCreateNewEvent_Click(object sender, EventArgs e)
        {
            CreateNewEvent newEvent = new CreateNewEvent();
            newEvent.Show();
        }

        //Called when user clicks Add/Manage players from the players tab
        private void buttonShowAddPlayersDlg_Click(object sender, EventArgs e)
        {
            ManagePlayers playersdlg = new ManagePlayers();
            playersdlg.ShowDialog();

            //Repopulate players list, since data for the unassigned players may have changed.
            populatePlayersLists();
        }

        //Called when the Event Selector drop down box is clicked. 
        // We populate it here and not when the form loads because the data
        // may have changed between then and now.
        private void comboBoxEventSelector_Enter(object sender, EventArgs e)
        {
            //Populate drop down box with team-play events from the database
            string dbCmd = "SELECT * FROM Events ORDER BY eventName";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Events");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1001: " + ex0.Message);
                return;
            }

            comboBoxEventSelector.Items.Clear(); //clear existing items from Team-Play events dropdown
            foreach (DataRow dRow in dataSet.Tables["Events"].Rows)
            {
                comboBoxEventSelector.Items.Add(dRow["eventName"].ToString()); //Add each item to dropdown 
            }
        }

        //Called when the user changes the selected event. Calls populateEvent().
        private void comboBoxEventSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateEvent();
        }

        //Updates the event
        private void populateEvent()
        {
            //Populate event details here for all tabs
            
            populateEventDetails(); //Populates event details tab and g_selectedEvent.strAssignedPlayers
            populatePlayersLists(); //Populates Player tab assigned/unassgined player lists 
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes

            //Set button states (enabled/disabled) based on existence of Teams
            bool bTeamsResult = doTeamsExistForSelectedEvent();
            buttonGenerateTeams.Enabled = !bTeamsResult;
            buttonViewTeams.Enabled = bTeamsResult;
            buttonGenerateRounds.Enabled = bTeamsResult;

            //Set button states (enabled/disabled) based on existence of Rounds
            bool bRoundsResult = doRoundsExistForSelectedEvent();
            buttonViewRounds.Enabled = bRoundsResult;
            buttonEnterScores.Enabled = bRoundsResult;
            buttonViewScoresByPlayer.Enabled = bRoundsResult;
            buttonViewScoresByTeam.Enabled = bRoundsResult;
            buttonViewPointStandings.Enabled = bRoundsResult;
            buttonViewPuttStandings.Enabled = bRoundsResult;
            buttonViewTeamStandings.Enabled = bRoundsResult;

            if (bTeamsResult)
                buttonGenerateRounds.Enabled = !bRoundsResult;
            else
                buttonGenerateRounds.Enabled = bRoundsResult;

            if (bTeamsResult || bRoundsResult)
                updateObjects(); //update the players objects, as well as the event objects teams, rounds, and groups objects/properties.

        }

        //Populates the event details tab and g_selectedEvent.strAssignedPlayers 
        void populateEventDetails()
        {
            //Note: Before an event is created, it is checked to ensure no events 
            // of the same name exist. Therefore, each event may be referenced by name.
            string dbCmd = "SELECT * FROM Events WHERE eventName = '" + comboBoxEventSelector.Text + "'";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);

            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Events");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1002: " + ex0.Message);
                return;
            }

            g_selectedEvent = new Event(); //re-ini selected event object
            int nTemp = 0;
            tabControl.Enabled = true; //enable tab navigation (disabled until an event is selected)
            int.TryParse(dataSet.Tables["Events"].Rows[0]["eventID"].ToString(), out nTemp);
            g_selectedEvent.nID = nTemp;
            labelEventName.Text = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            g_selectedEvent.strName = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            labelRoundCount.Text = dataSet.Tables["Events"].Rows[0]["numRounds"].ToString();
            int.TryParse(dataSet.Tables["Events"].Rows[0]["numRounds"].ToString(), out nTemp);
            numericJumpTo.Maximum = nTemp;
            g_selectedEvent.nRounds = nTemp;
            string strStartDate = dataSet.Tables["Events"].Rows[0]["startDate"].ToString();
            string strEndDate = dataSet.Tables["Events"].Rows[0]["endDate"].ToString();
            labelStartDate.Text = strStartDate.Substring(0, strStartDate.IndexOf(' '));
            labelEndDate.Text = strEndDate.Substring(0, strEndDate.IndexOf(' '));

            //Populate the assigned players string and remove leading/training commas
            g_selectedEvent.strAssignedPlayers = dataSet.Tables["Events"].Rows[0]["players"].ToString(); 
            if (g_selectedEvent.strAssignedPlayers.Length != 0)
            {
                g_selectedEvent.strAssignedPlayers = g_selectedEvent.strAssignedPlayers.Remove(0, 1); //leading
                g_selectedEvent.strAssignedPlayers = g_selectedEvent.strAssignedPlayers.Remove(g_selectedEvent.strAssignedPlayers.Length - 1, 1); //trailing
            }

            // Count the number of players and display to events page
            int playerNum = 0;
            playerNum = Regex.Matches(g_selectedEvent.strAssignedPlayers, ",").Count + 1;
            if (playerNum == 1)
                playerNum = 0;
            labelPlayerCount.Text = (playerNum).ToString();

            // If teams exist, count number of teams and display in events page
            labelTeamCount.Text = displayNumberOfTeams().ToString();

            //Ensure Delete checkbox and button are reset
            checkBoxDeleteEvent.Checked = false;
            buttonDeleteEvent.Enabled = false;
        }

        //Populates the Players tab lists with the assigned and unassigned players.
        //Assumes: g_selectedEvent.strAssignedPlayers is populated.
        void populatePlayersLists()
        {
            //Clear existing lists
            g_selectedEvent.lstUnassignedPlayers = new BindingList<Player>();
            g_selectedEvent.lstAssignedPlayers = new BindingList<Player>();

            string dbCmd = "";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);

            DataSet dataSet = new DataSet();

            //Populate Assigned players
            if (g_selectedEvent.strAssignedPlayers.Length > 0)
            {
                dbCmd = "SELECT * FROM Players WHERE playerID in (" + g_selectedEvent.strAssignedPlayers + ") ORDER BY lName";
                dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
                adapter = new OleDbDataAdapter(dbComm);

                dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet, "Players");
                }
                catch (Exception ex0)
                {
                    MessageBox.Show("ERROR 1003: " + ex0.Message);
                    return;
                }

                foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
                {
                    Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                          Convert.ToDouble(dRow["handicap"].ToString()),
                                          dRow["fName"].ToString(), dRow["lName"].ToString(),
                                          dRow["phone"].ToString(), "");
                    g_selectedEvent.lstAssignedPlayers.Add(p);
                }
            }
            listBoxAssignedPlayers.DisplayMember = "displayName";
            listBoxAssignedPlayers.ValueMember = "playerID";
            listBoxAssignedPlayers.DataSource = g_selectedEvent.lstAssignedPlayers;

            //Populate Unassigned players
            dbCmd = "SELECT * FROM Players";
            if (g_selectedEvent.strAssignedPlayers.Length != 0)
                dbCmd += " WHERE playerID not in (" + g_selectedEvent.strAssignedPlayers + ")";
            dbCmd += " ORDER BY lName";

            dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            adapter = new OleDbDataAdapter(dbComm);

            dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Players");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1004: " + ex0.Message);
                return;
            }

            foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
            {
                Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                      Convert.ToDouble(dRow["handicap"].ToString()),
                                      dRow["fName"].ToString(), dRow["lName"].ToString(),
                                      dRow["phone"].ToString(), "");
                g_selectedEvent.lstUnassignedPlayers.Add(p);
            }

            listBoxUnassignedPlayers.DisplayMember = "displayName";
            listBoxUnassignedPlayers.ValueMember = "playerID";
            listBoxUnassignedPlayers.DataSource = g_selectedEvent.lstUnassignedPlayers;
        }

        //Moves a player from the Players:Unassigned list to the Players:Assignedlist
        private void buttonAssign_Click(object sender, EventArgs e)
        {
            if (doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("Cannot modify the players assigned to this event: Teams have already been generated.");
                return;
            }
            if (listBoxUnassignedPlayers.SelectedItems.Count > 0)
            {
                Player p = listBoxUnassignedPlayers.SelectedItem as Player;
                g_selectedEvent.lstUnassignedPlayers.Remove(p);
                g_selectedEvent.lstAssignedPlayers.Add(p);
                populateAssignedPlayersString();
            }

            //Update the player counts at the top of the Players:assigned/unassigned boxes
            populatePlayersTabAssignmentCounts(); 
            populateEvent();
        }

        //Moves a player from the Players:Assigned list to the Players:Unassignedlist
        private void buttonUnassign_Click(object sender, EventArgs e)
        {
            if (doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("Cannot modify the players assigned to this event: Teams have already been generated.");
                return;
            }
            if (listBoxAssignedPlayers.SelectedItems.Count > 0)
            {
                Player p = listBoxAssignedPlayers.SelectedItem as Player;
                g_selectedEvent.lstAssignedPlayers.Remove(p);
                g_selectedEvent.lstUnassignedPlayers.Add(p);
                populateAssignedPlayersString();
            }

            //Update the player counts at the top of the Players:assigned/unassigned boxes
            populatePlayersTabAssignmentCounts(); 
            populateEvent();
        }

        //Moves all players from the Players:Unassigned list to the Players:Assignedlist
        private void buttonAssignAll_Click(object sender, EventArgs e)
        {
            if (doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("Cannot modify the players assigned to this event: Teams have already been generated.");
                return;
            }
            for (int i = 0; i < g_selectedEvent.lstUnassignedPlayers.Count; i++)
            {
                g_selectedEvent.lstAssignedPlayers.Add(g_selectedEvent.lstUnassignedPlayers[i] as Player);
            }
            populateAssignedPlayersString();
            populatePlayersLists();

            //Update the player counts at the top of the Players:assigned/unassigned boxes
            populatePlayersTabAssignmentCounts();
            populateEvent();
        }

        //Moves all players from the Players:Assigned list to the Players:Unassignedlist
        private void buttonUnassignAll_Click(object sender, EventArgs e)
        {
            if (doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("Cannot modify the players assigned to this event: Teams have already been generated.");
                return;
            }

            //Update events db table directly then update the event (in turn, causing the players lists to be rebuilt)
            string strCmd = "UPDATE events SET players = '' where eventID = " + g_selectedEvent.nID;
            OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could not modify players due to a database error.");
                return;
            }

            g_selectedEvent.strAssignedPlayers = "";
            populateEvent();
        }

        //Update the player counts at the top of the Players:assigned/unassigned boxes
        private void populatePlayersTabAssignmentCounts()
        {
            labelAssignedCount.Text = listBoxAssignedPlayers.Items.Count.ToString();
            labelUnassignedCount.Text = listBoxUnassignedPlayers.Items.Count.ToString();
        }

        //Populates g_selectedEvent.strAssignedPlayers from the data in 
        // Players:AssignedList, then writes the new string to the db.
        //Note: The string is stored in the database with a leading, delimiting,
        // and trailing comma. However, the g_selectedEvent.strAssignedPlayers 
        // string only has delimiting commas. This is for convenience in using
        // LIKE and IN SQL statements. 
        //Note: Storing the players in this way in the DB violates 1NF.
        private void populateAssignedPlayersString()
        {
            //build assigned players string
            g_selectedEvent.strAssignedPlayers = ","; //ini with leading comma
            for (int i = 0; i < listBoxAssignedPlayers.Items.Count; i++)
                g_selectedEvent.strAssignedPlayers += (listBoxAssignedPlayers.Items[i] as Player).ID + ",";

            //if all the string contains is the leading comma, just make it ""
            if (g_selectedEvent.strAssignedPlayers.Length == 1) g_selectedEvent.strAssignedPlayers = "";

            //update events db table 
            string strCmd = "UPDATE events SET players = '" + g_selectedEvent.strAssignedPlayers + "' where eventID = " + g_selectedEvent.nID;
            OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could not modify players due to a database error.");
                return;
            }

            //remove leading/trailing comma
            if (g_selectedEvent.strAssignedPlayers.Length != 0)
            {
                g_selectedEvent.strAssignedPlayers = g_selectedEvent.strAssignedPlayers.Remove(0, 1); //leading
                g_selectedEvent.strAssignedPlayers = g_selectedEvent.strAssignedPlayers.Remove(g_selectedEvent.strAssignedPlayers.Length - 1, 1); //trailing
            }
        }

        //Called on user click Teams:Generate Teams button. 
        private void buttonGenerateTeams_Click(object sender, EventArgs e)
        {
            //Confirm user action
            string strMsg = "You may no longer make changes to the players assigned to this event in Step 1 ";
            strMsg += "after this action is performed. Are you sure you want to proceed?";
            DialogResult dlgResult = MessageBox.Show(strMsg, "Are you sure?", MessageBoxButtons.YesNo);
            if (dlgResult != DialogResult.Yes)
                return;

            //Calls a function (generateTeams()) that ensures no teams have been
            // assigned for this event and that numPlayers is divisible by four. 
            // Generates numPlayers/4 teams of four players each.
            // The function returns a bool denoting the state of what the Generate
            // Teams button should be. True = enabled, False = disabled.
            TeamAssignment t = new TeamAssignment();
            bool bResult = t.generateBestTeams(g_selectedEvent.nID, g_selectedEvent.lstAssignedPlayers.ToList());
            buttonGenerateTeams.Enabled = bResult;
            buttonViewTeams.Enabled = !bResult;

            //update the details tab
            populateEvent();
        }

        //Builds a csv file from the selected event's teams and opens it in 
        // localhost's default external program for csv files.
        private void buttonViewTeams_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenTeamsCSV(g_selectedEvent);
        }

        //Checks for the existence of teams assigned to the selected event. 
        //Should only be called after populateEventDetails() has been called.
        //Returns true iff teams are in the db for the currently selected event.
        private bool doTeamsExistForSelectedEvent()
        {
            //Get list of teams for the selected event.
            string dbCmd = "SELECT * FROM Teams WHERE eventID = " + g_selectedEvent.nID.ToString();
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Teams");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1006: Could not get teams for event.\n" + ex0.Message);
            }

            if (dataSet.Tables["Teams"].Rows.Count != 0)
                return true; //Teams exist for selected event, return true
            return false; //else return false
        }

        //Checks for the existence of rounds assigned to the selected event. 
        //Should only be called after populateEventDetails() has been called.
        //Returns true iff rounds are in the db for the currently selected event.
        public bool doRoundsExistForSelectedEvent()
        {
            //Get list of teams for the selected event.
            string dbCmd = "SELECT * FROM Rounds WHERE eventID = " + g_selectedEvent.nID.ToString();
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Rounds");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1008: Could not get rounds for event.\n" + ex0.Message);
            }

            if (dataSet.Tables["Rounds"].Rows.Count != 0)
                return true; //Rounds exist for selected event, return true
            return false; //else return false
        }

        //Populate the number of teams label in the event window
        private string displayNumberOfTeams()
        {
            if (doTeamsExistForSelectedEvent() == true)
            {
                string dbCmd = "SELECT COUNT (*) FROM Teams WHERE eventID = " + g_selectedEvent.nID.ToString();
                OleDbCommand command = new OleDbCommand(dbCmd, Globals.g_dbConnection);
                int rowCount = (int)command.ExecuteScalar();
                //// TODO: catch exception
                rowCount = rowCount / 4;
                return rowCount.ToString();
            }
            else
                return "Unassigned";
        }

        //For each player object, updates it with that player's assigned level 
        // (A-D), the event's correct team object with that player, and the 
        // events rounds/groups objects.
        private void updateObjects()
        {
            //if teams don't exist, return
            if (!doTeamsExistForSelectedEvent())
                return;

            //else do player/team object population
            int nteamNumber = -1;
            string strPlayerLevel = "";

            //ini the event objects team list
            g_selectedEvent.lstTeams = new List<Team>(g_selectedEvent.lstAssignedPlayers.Count / 4);
            for (int i = 0; i < g_selectedEvent.lstAssignedPlayers.Count / 4; i++)
                g_selectedEvent.lstTeams.Add(new Team());

            //For every player, update the player object and team object
            string strCmd;
            OleDbCommand dbComm= null;
            OleDbDataAdapter dbAdapter = null;
            DataSet dataSet = null;
            for (int i = 0; i < g_selectedEvent.lstAssignedPlayers.Count; i++)
            {
                int playerID = (g_selectedEvent.lstAssignedPlayers[i] as Player).ID;

                strCmd = "SELECT * FROM Teams";
                strCmd += " WHERE Teams.eventID = " + g_selectedEvent.nID;
                strCmd += " AND Teams.playerID = " + playerID.ToString();
                strCmd += " ORDER BY teamNumber, playerLevel";
                dbComm = new OleDbCommand(strCmd, Globals.g_dbConnection);
                dbAdapter = new OleDbDataAdapter(dbComm);
                dataSet = new DataSet();
                try
                {
                    dbAdapter.Fill(dataSet, "Teams");
                    int.TryParse(dataSet.Tables["Teams"].Rows[0]["teamNumber"].ToString(), out nteamNumber);
                    strPlayerLevel = dataSet.Tables["Teams"].Rows[0]["playerLevel"].ToString();
                }
                catch (Exception ex0)
                {
                    MessageBox.Show("ERROR 1005: " + ex0.Message);
                    return;
                }

                //update the player object
                (g_selectedEvent.lstAssignedPlayers[i] as Player).teamNumber = nteamNumber;
                (g_selectedEvent.lstAssignedPlayers[i] as Player).level = strPlayerLevel;

                //update the event's team object 
                if (strPlayerLevel == "A")
                    g_selectedEvent.lstTeams[nteamNumber-1].playerA = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "B")
                    g_selectedEvent.lstTeams[nteamNumber-1].playerB = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "C")
                    g_selectedEvent.lstTeams[nteamNumber-1].playerC = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "D")
                    g_selectedEvent.lstTeams[nteamNumber-1].playerD = (g_selectedEvent.lstAssignedPlayers[i] as Player);
            }

            //In preperation for updating the event with round info, check if 
            // rounds exist. If not, return.
            if (!doRoundsExistForSelectedEvent())
                return;

            //else update the event's rounds (and the groups within those rounds)
            strCmd = "SELECT * FROM Groups LEFT JOIN Rounds ON Rounds.roundID = Groups.roundID";
            strCmd += " WHERE Rounds.eventID = " + g_selectedEvent.nID.ToString() + " ORDER BY Rounds.roundNumber, Groups.ID ASC";
            dbComm = new OleDbCommand(strCmd, Globals.g_dbConnection);
            dbAdapter = new OleDbDataAdapter(dbComm);
            dataSet = new DataSet();
            try
            {
                dbAdapter.Fill(dataSet, "Groups");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1011: " + ex0.Message);
                return;
            }

            //Init the data objects contained in the event's list of rounds
            // down to the group level so we can modify their properties from
            // the db info (below)
            g_selectedEvent.lstRounds = new List<Round>(g_selectedEvent.nRounds);
            int nTeamCount = g_selectedEvent.lstAssignedPlayers.Count / 4;
            for (int i = 0; i < g_selectedEvent.nRounds; i++)
            {
                g_selectedEvent.lstRounds.Add(new Round(i + 1));
                for (int j = 0; j < nTeamCount; j++) 
                    g_selectedEvent.lstRounds[i].addGroup(new GroupOfFour(j + 1));
            }

            //Update the properties of the objects we just initialized (above) with the db rounds/groups info
            foreach (DataRow dRow in dataSet.Tables["Groups"].Rows)
            {
                string strCurrentRound = dRow["roundName"].ToString();
                int nCurrRound = (int)dRow["roundNumber"];
                int nCurrGroup = (int)dRow["groupNumber"];

                //Ensure the group and round numbers are as expected (they always will if things are working correctly.)
                if (g_selectedEvent.lstRounds[nCurrRound - 1].nRoundNumber == nCurrRound &&
                    g_selectedEvent.lstRounds[nCurrRound - 1].lstGroups[nCurrGroup - 1].nGroupNumber == nCurrGroup)
                {
                    //populate round info
                    g_selectedEvent.lstRounds[nCurrRound - 1].strRoundName = strCurrentRound;
                    g_selectedEvent.lstRounds[nCurrRound - 1].nID = (int)dRow["Rounds.roundID"];
                    g_selectedEvent.lstRounds[nCurrRound - 1].lstGroups[nCurrGroup - 1].playerA = getPlayerObjectBtyID((int)dRow["aPlayerID"]);
                    g_selectedEvent.lstRounds[nCurrRound - 1].lstGroups[nCurrGroup - 1].playerB = getPlayerObjectBtyID((int)dRow["bPlayerID"]);
                    g_selectedEvent.lstRounds[nCurrRound - 1].lstGroups[nCurrGroup - 1].playerC = getPlayerObjectBtyID((int)dRow["cPlayerID"]);
                    g_selectedEvent.lstRounds[nCurrRound - 1].lstGroups[nCurrGroup - 1].playerD = getPlayerObjectBtyID((int)dRow["dPlayerID"]);
                }
                else
                    MessageBox.Show("ERROR 1012: An inconsistency was found while populating the event object.");
            }
        }

        //Parses all the player objects assigned to g_selectedEvent and returns
        // the one with the specified ID
        private Player getPlayerObjectBtyID(int id)
        {
            foreach (Player p in g_selectedEvent.lstAssignedPlayers)
                if (p.ID == id)
                    return p;
            return null;
        }

        //Called on user click Generate Rounds button
        private void buttonGenerateRounds_Click(object sender, EventArgs e)
        {
            // Generate pairings for the rounds tab
            RoundAssignment ra = new RoundAssignment(g_selectedEvent);
            bool bResult = ra.generateRounds();

            //on success, set the Generate Rounds button state to disabled and populate
            // the event again from the db through populateEvent()
            if (bResult)
            {
                buttonGenerateRounds.Enabled = !bResult;
                buttonViewRounds.Enabled = bResult;
                buttonEnterScores.Enabled = bResult;
                buttonViewScoresByPlayer.Enabled = bResult;
                populateEvent();
            }
        }

        //Called on user click View Rounds - Generates a CSV file with all the
        // round and group assignments, then displays it in Excel (or other default)
        private void buttonViewRounds_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenRoundsCSV(g_selectedEvent);
        }

        //called on user click Enter Scores
        private void buttonEnterScores_Click(object sender, EventArgs e)
        {
            //opens the Enter scores per round dialog box
            EnterScores dlg = new EnterScores(g_selectedEvent, (int)numericJumpTo.Value);
            dlg.ShowDialog();
        }

        //Generates a CSV file with scores per player and displays it in Excel (or other default)
        private void buttonViewScoresByPlayer_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenScoresByPlayerCSV(g_selectedEvent);
        }

        private void buttonViewScoresByTeam_Click(object sender, EventArgs e)
        {
            //Generates a CSV file with all the scores per team and displays it in Excel (or other default)
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenScoresByTeamCSV(g_selectedEvent);
        }

        private void checkBoxDeleteEvent_CheckedChanged(object sender, EventArgs e)
        {
            buttonDeleteEvent.Enabled = checkBoxDeleteEvent.Checked;
        }

        private void buttonDeleteEvent_Click(object sender, EventArgs e)
        {
            string strMsg = "This action will delete this event, including all associated rounds, groups, ";
            strMsg += "and scores. This action cannot be undone. Are you sure you wish to proceed?";
            DialogResult dlgResult = MessageBox.Show(strMsg, "Are you sure?", MessageBoxButtons.YesNo);
            if (dlgResult != DialogResult.Yes)
                return;

            List<String> lstCmds = new List<String>();
            lstCmds.Add("DELETE FROM Groups WHERE roundID IN (SELECT roundID FROM Rounds WHERE eventID = " + g_selectedEvent.nID + ")");
            lstCmds.Add("DELETE FROM Rounds WHERE eventID = " + g_selectedEvent.nID);
            lstCmds.Add("DELETE FROM Teams WHERE eventID = " + g_selectedEvent.nID);
            lstCmds.Add("DELETE FROM Scores WHERE eventID = " + g_selectedEvent.nID);
            lstCmds.Add("DELETE FROM Events WHERE eventID = " + g_selectedEvent.nID);

            foreach(String cmd in lstCmds)
            {
                try
                {
                    OleDbCommand command = new OleDbCommand(cmd, Globals.g_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR: Could not delete event due to a database error\n" + cmd + "\n" + ex.Message);
                    return;
                }
            }

            MessageBox.Show("Success! This event has been deleted. The Manage Event window will now close.");
            this.Close();
        }

        private void buttonViewPointStandings_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenPointResultsCSV(g_selectedEvent);
        }

        private void buttonViewPuttStandings_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenPuttResultsCSV(g_selectedEvent);
        }

        private void buttonViewTeamStandings_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenTeamResultsCSV(g_selectedEvent);
        }
    }
}
