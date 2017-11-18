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

        //Caled when user clicks Add/Manage players from the players tab
        private void buttonShowAddPlayersDlg_Click(object sender, EventArgs e)
        {
            ManagePlayers playersdlg = new ManagePlayers();
            playersdlg.ShowDialog();
            populatePlayersLists(); //Repopulate players list, since data for the unassigned players may have changed.
        }

        //Called when the Event Selector drop down box is clicked. We populate it here and not when the form loads 
        // because the data may have changed in between then and now.
        private void comboBoxEventSelector_Enter(object sender, EventArgs e)
        {
            //Populate drop down box with team-play events from the database
            string dbCmd = "SELECT * FROM Events";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Events");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 1: " + ex0.Message);
                return;
            }

            comboBoxEventSelector.Items.Clear(); //clear existing items from Team-Play events dropdown
            foreach (DataRow dRow in dataSet.Tables["Events"].Rows)
            {
                comboBoxEventSelector.Items.Add(dRow["eventName"].ToString()); //Add each item to dropdown 
            }
        }

        //Called when the user changes the selected event. Calls populateEvent(), where the event is updated.
        private void comboBoxEventSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateEvent();
        }

        //Updates the event
        private void populateEvent()
        {
            //Populate event details here for all tabs
            //////////////////////////////////////////////

            ///////////////////// Details tab: 

            //TODO Details Tab: number of Players, Teams, and Rounds assigned to event
            //TODO Details Tab: Calculate and display "Rounds with Results"
            //TODO Details Tab: Update Final Results display

            populateEventDetails(); //Populates event details tab and g_strAssignedPlayers
            populatePlayersLists(); //Populates Player tab assigned/unassgined player lists 
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes

            //Set Teams tab button states (enabled/disabled) and update player/team objects 
            // with their associated team and level.
            bool bResult = doTeamsExistForSelectedEvent();
            buttonGenerateTeams.Enabled = !bResult;
            buttonViewTeams.Enabled = bResult;
            if (bResult)
                updatePlayerAndTeamObjects();

            //TODO Popualte Rounds tab

            //TODO Populate Results tab
        }

        //Populates the event details tab and also populates the g_selectedEvent.strAssignedPlayers 
        void populateEventDetails()
        {
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
                MessageBox.Show("ERROR 2: " + ex0.Message);
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
            g_selectedEvent.nRounds = nTemp;
            string strStartDate = dataSet.Tables["Events"].Rows[0]["startDate"].ToString();
            string strEndDate = dataSet.Tables["Events"].Rows[0]["endDate"].ToString();
            labelStartDate.Text = strStartDate.Substring(0, strStartDate.IndexOf(' '));
            labelEndDate.Text = strEndDate.Substring(0, strEndDate.IndexOf(' '));

            g_selectedEvent.strAssignedPlayers = dataSet.Tables["Events"].Rows[0]["players"].ToString(); //Populates the assigned players string
            if (g_selectedEvent.strAssignedPlayers.Length != 0) //remove leading/trailing commas
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
            // also update player objects with their team assignment, level, etc.
            if (doTeamsExistForSelectedEvent() == true)
            {
                labelTeamCount.Text = displayNumberOfTeams().ToString();
            }
            else
                labelTeamCount.Text = "N/A";
        }

        //Populates the Players tab lists with the assigned and unassigned players. Assumes g_selectedEvent.strAssignedPlayers is populated.
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
                dbCmd = "SELECT * FROM Players WHERE playerID in (" + g_selectedEvent.strAssignedPlayers + ")";
                dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
                adapter = new OleDbDataAdapter(dbComm);

                dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet, "Players");
                }
                catch (Exception ex0)
                {
                    MessageBox.Show("ERROR 3: " + ex0.Message);
                    return;
                }

                foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
                {
                    Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                          Convert.ToInt16(dRow["handicap"].ToString()),
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

            dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            adapter = new OleDbDataAdapter(dbComm);

            dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Players");
            }
            catch (Exception ex0)
            {
                MessageBox.Show("ERROR 4: " + ex0.Message);
                return;
            }

            foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
            {
                Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                      Convert.ToInt16(dRow["handicap"].ToString()),
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
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
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
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
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
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
            populateEvent();
        }

        //Updates the count of the players at the top of the Players:assigned/unassigned boxes
        private void populatePlayersTabAssignmentCounts()
        {
            labelAssignedCount.Text = listBoxAssignedPlayers.Items.Count.ToString();
            labelUnassignedCount.Text = listBoxUnassignedPlayers.Items.Count.ToString();
        }

        //Populates g_selectedEvent.strAssignedPlayers from the data in the Players:AssignedList then writes the new string to the db
        //Note that the string is stored in the database with a leading, delimiting, and trailing comma. Though
        //the g_selectedEvent.strAssignedPlayers string only has delimiting commas. This is for convenience in using LIKE and IN SQL statements.
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
                MessageBox.Show("ERROR: Could not modify user due to an unspecified database error.");
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
            //Calls a function (generateTeams()) that ensures no teams have been assigned for this event and that numPlayers is divisible by four. 
            //each player a level (A-D), and generates numPlayers/4 teams of four players each.
            //The function returns a bool denoting the state of what the Generate Teams button should be. True = enabled, False = disabled.
            TeamAssignment t = new TeamAssignment();
            bool bResult = t.generateTeams(g_selectedEvent.nID, g_selectedEvent.lstAssignedPlayers.ToList());
            buttonGenerateTeams.Enabled = bResult;
            buttonViewTeams.Enabled = !bResult;

            //update the details tab
            populateEvent();
        }

        //Calls a function that builds a csv file from the selected events teams and opens it in localhost's default
        //external program for csv files.
        private void buttonViewTeams_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenTeamsCSV(g_selectedEvent);
        }

        //Checks for the existence of teams assigned to the eventID in g_strSelectedEvent. 
        //Should only be called after g_strSelectedEvent is populated (i.e. populateEventDetails() has been called)
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
                MessageBox.Show(ex0.Message);
            }

            if (dataSet.Tables["Teams"].Rows.Count != 0)
                return true; //Teams exist for selected event, return true
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
                return "N/A";
        }

        //For each player object, updates that object with that player's assigned team and level, and also
        // updates the team object with that player.
        // Assumes teams exist for the selected event before this function is called 
        private void updatePlayerAndTeamObjects()
        {
            int nTeamName = -1;
            string strPlayerLevel = "";

            //ini Teams list
            g_selectedEvent.lstTeams = new List<Team>(g_selectedEvent.lstAssignedPlayers.Count / 4);
            for (int i = 0; i < g_selectedEvent.lstAssignedPlayers.Count / 4; i++)
                g_selectedEvent.lstTeams.Add(new Team());

            //For every player, update the player object and team object
            for (int i = 0; i < g_selectedEvent.lstAssignedPlayers.Count; i++)
            {
                int playerID = (g_selectedEvent.lstAssignedPlayers[i] as Player).ID;

                string dbCmd = "SELECT * FROM Teams";
                dbCmd += " WHERE Teams.eventID = " + g_selectedEvent.nID;
                dbCmd += " AND Teams.playerID = " + playerID.ToString();
                OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

                OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
                DataSet dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet, "Teams");
                    int.TryParse(dataSet.Tables["Teams"].Rows[0]["teamName"].ToString(), out nTeamName);
                    strPlayerLevel = dataSet.Tables["Teams"].Rows[0]["playerLevel"].ToString();
                }
                catch (Exception ex0)
                {
                    MessageBox.Show("ERROR 5: " + ex0.Message);
                    return;
                }

                //update player object
                (g_selectedEvent.lstAssignedPlayers[i] as Player).teamName = nTeamName;
                (g_selectedEvent.lstAssignedPlayers[i] as Player).level = strPlayerLevel;

                //update team object
                if (strPlayerLevel == "A")
                    g_selectedEvent.lstTeams[nTeamName-1].playerA = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "B")
                    g_selectedEvent.lstTeams[nTeamName-1].playerB = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "C")
                    g_selectedEvent.lstTeams[nTeamName-1].playerC = (g_selectedEvent.lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "D")
                    g_selectedEvent.lstTeams[nTeamName-1].playerD = (g_selectedEvent.lstAssignedPlayers[i] as Player);
            }
        }

        // Generate pairings for the rounds tab
        private void GeneratePairings_Click(object sender, EventArgs e)
        {
            RoundAssignment ra = new RoundAssignment(g_selectedEvent);
            bool bReslt = ra.generateRounds();
            
            //TODO: Set the generate rounds button state (enabled/disabled) based on bResult
        }
    }
}
