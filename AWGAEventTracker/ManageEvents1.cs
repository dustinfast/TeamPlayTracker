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
        //Class Globals. These are properties of the currently selected event and should be converted to a class of type Event
        //Class Globals 
        private string g_strSelectedEventID; //The ID of the currently selected event. Populated in populateEventDetails.
        private string g_strEventName; //The name of the currently selected event. Populated in populateEventDetails.
        private string g_strAssignedPlayers; //A comma delimited list of all the players (by ID) assigned to the currently selected event. Populated in populateEventDetails.
        private int g_nEventRounds; //The number of rounds for the selected event
        private List<Team> g_lstTeams; //A list of teams (i.e. team assignments) for the currently selected event
        private List<Round> g_lstRounds; //A list of the rounds (i.e. the schedule) for the currently selected event
        private BindingList<Player> g_lstAssignedPlayers = new BindingList<Player>(); //All player objects assigned to currently selected event. Populated on Event select OR Assigned Player change
        private BindingList<Player> g_lstUnassignedPlayers = new BindingList<Player>(); //All player objects not assigned to currently selected event.

        public ManageEvents1()
        {
            InitializeComponent();
           // GeneratePairings.Click += new EventHandler(GeneratePairings_Click);
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
                MessageBox.Show(ex0.Message);
                return;
            }

            comboBoxEventSelector.Items.Clear(); //clear existing items from Team-Play events dropdown
            foreach (DataRow dRow in dataSet.Tables["Events"].Rows)
            {
                comboBoxEventSelector.Items.Add(dRow["eventName"].ToString()); //Add each item to dropdown 
            }
        }

        //Called when the user changes the selected event
        private void comboBoxEventSelector_SelectedIndexChanged(object sender, EventArgs e)
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

        //Populates the event details tab and also sets the g_strAssignedPlayers global var
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
                MessageBox.Show(ex0.Message);
                return;
            }

            tabControl.Enabled = true; //enable tab navigation, which was disabled until an event is selected
            g_strSelectedEventID = dataSet.Tables["Events"].Rows[0]["eventID"].ToString(); //populates the g_strSelectedEventID global var
            labelEventName.Text = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            g_strEventName = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            labelRoundCount.Text = dataSet.Tables["Events"].Rows[0]["numRounds"].ToString();
            int.TryParse(dataSet.Tables["Events"].Rows[0]["numRounds"].ToString(), out g_nEventRounds);
            string strStartDate = dataSet.Tables["Events"].Rows[0]["startDate"].ToString();
            string strEndDate = dataSet.Tables["Events"].Rows[0]["endDate"].ToString();
            labelStartDate.Text = strStartDate.Substring(0, strStartDate.IndexOf(' '));
            labelEndDate.Text = strEndDate.Substring(0, strEndDate.IndexOf(' '));

            g_strAssignedPlayers = dataSet.Tables["Events"].Rows[0]["players"].ToString(); //Populates the g_strAssignedPlayers global var, removing the leading and trailing commas
            if (g_strAssignedPlayers.Length != 0)
            {
                g_strAssignedPlayers = g_strAssignedPlayers.Remove(0, 1); //leading
                g_strAssignedPlayers = g_strAssignedPlayers.Remove(g_strAssignedPlayers.Length - 1, 1); //trailing
            }
            // Count the number of players and display to events page
            int playerNum = 0;
            playerNum = Regex.Matches(g_strAssignedPlayers, ",").Count + 1;
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

        //Populates the Players tab lists with the assigned and unassigned players. Should be called after g_strAssignedPlayers is populated.
        void populatePlayersLists()
        {
            //Clear existing lists
            g_lstUnassignedPlayers = new BindingList<Player>();
            g_lstAssignedPlayers = new BindingList<Player>();

            string dbCmd = "";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);

            DataSet dataSet = new DataSet();

            //Populate Assigned players
            if (g_strAssignedPlayers.Length > 0)
            {
                dbCmd = "SELECT * FROM Players WHERE playerID in (" + g_strAssignedPlayers + ")";
                dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
                adapter = new OleDbDataAdapter(dbComm);

                dataSet = new DataSet();
                try
                {
                    adapter.Fill(dataSet, "Players");
                }
                catch (Exception ex0)
                {
                    MessageBox.Show(ex0.Message);
                    return;
                }

                foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
                {
                    Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                          Convert.ToInt16(dRow["handicap"].ToString()),
                                          dRow["fName"].ToString(), dRow["lName"].ToString(),
                                          dRow["phone"].ToString(), "");
                    g_lstAssignedPlayers.Add(p);
                }
            }
            listBoxAssignedPlayers.DisplayMember = "displayName";
            listBoxAssignedPlayers.ValueMember = "playerID";
            listBoxAssignedPlayers.DataSource = g_lstAssignedPlayers;

            //Populate Unassigned players
            dbCmd = "SELECT * FROM Players";
            if (g_strAssignedPlayers.Length != 0)
                dbCmd += " WHERE playerID not in (" + g_strAssignedPlayers + ")";

            dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            adapter = new OleDbDataAdapter(dbComm);

            dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Players");
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
                return;
            }

            foreach (DataRow dRow in dataSet.Tables["Players"].Rows)
            {
                Player p = new Player(Convert.ToInt32(dRow["playerID"].ToString()),
                                      Convert.ToInt16(dRow["handicap"].ToString()),
                                      dRow["fName"].ToString(), dRow["lName"].ToString(),
                                      dRow["phone"].ToString(), "");
                g_lstUnassignedPlayers.Add(p);
            }

            listBoxUnassignedPlayers.DisplayMember = "displayName";
            listBoxUnassignedPlayers.ValueMember = "playerID";
            listBoxUnassignedPlayers.DataSource = g_lstUnassignedPlayers;
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
                g_lstUnassignedPlayers.Remove(p);
                g_lstAssignedPlayers.Add(p);
                populateAssignedPlayersString();
            }
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
            populateEventDetails();
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
                g_lstAssignedPlayers.Remove(p);
                g_lstUnassignedPlayers.Add(p);
                populateAssignedPlayersString();
            }
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
            populateEventDetails();
        }

        //Moves all players from the Players:Unassigned list to the Players:Assignedlist
        private void buttonAssignAll_Click(object sender, EventArgs e)
        {
            if (doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("Cannot modify the players assigned to this event: Teams have already been generated.");
                return;
            }
            for (int i = 0; i < g_lstUnassignedPlayers.Count; i++)
            {
                g_lstAssignedPlayers.Add(g_lstUnassignedPlayers[i] as Player);
            }
            populateAssignedPlayersString();
            populatePlayersLists();
            populatePlayersTabAssignmentCounts(); //update the count of the players at the top of the Players:assigned/unassigned boxes
            populateEventDetails();
        }

        //Updates the count of the players at the top of the Players:assigned/unassigned boxes
        private void populatePlayersTabAssignmentCounts()
        {
            labelAssignedCount.Text = listBoxAssignedPlayers.Items.Count.ToString();
            labelUnassignedCount.Text = listBoxUnassignedPlayers.Items.Count.ToString();
        }

        //Populates g_strAssignedPlayers from the data in the Players:AssignedList then writes the new string to the db
        //Note that the string is stored in the database with a leading, delimiting, and trailing comma. Though
        //the g_strAssignedPlayers string only has delimiting commas. This is for convenience in using LIKE and IN SQL statements.
        private void populateAssignedPlayersString()
        {
            //build assigned players string
            g_strAssignedPlayers = ","; //ini with leading comma
            for (int i = 0; i < listBoxAssignedPlayers.Items.Count; i++)
                g_strAssignedPlayers += (listBoxAssignedPlayers.Items[i] as Player).ID + ",";

            //if all the string contains is the leading comma, just make it ""
            if (g_strAssignedPlayers.Length == 1) g_strAssignedPlayers = "";

            //update events db table 
            string strCmd = "UPDATE events SET players = '" + g_strAssignedPlayers + "' where eventID = " + g_strSelectedEventID; ;
            OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could not modify user due to an unspecified database error.");
                return;
            }

            //remove trailing comma
            if (g_strAssignedPlayers.Length != 0)
            {
                g_strAssignedPlayers = g_strAssignedPlayers.Remove(0, 1); //leading
                g_strAssignedPlayers = g_strAssignedPlayers.Remove(g_strAssignedPlayers.Length - 1, 1); //trailing
            }
        }

        //Called on user click Teams:Generate Teams button. 
        private void buttonGenerateTeams_Click(object sender, EventArgs e)
        {
            //Calls a function (generateTeams()) that ensures no teams have been assigned for this event and that numPlayers is divisible by four. 
            //each player a level (A-D), and generates numPlayers/4 teams of four players each.
            //The function returns a bool denoting the status of what the Generate Teams button should be. True = enabled, False = disabled.
            TeamAssignment t = new TeamAssignment();
            bool bResult = t.generateTeams(g_strSelectedEventID, g_lstAssignedPlayers.ToList());
            buttonGenerateTeams.Enabled = bResult;
            buttonViewTeams.Enabled = !bResult;

            //update the details tab
            populateEventDetails();
        }

        //Calls a function that builds a csv file from the selected events teams and opens it in localhost's default
        //external program for csv files.
        private void buttonViewTeams_Click(object sender, EventArgs e)
        {
            CSVHandlers h = new CSVHandlers();
            h.buildAndOpenTeamsCSV(g_strSelectedEventID, g_strEventName);
        }

        //Checks for the existence of teams assigned to the eventID in g_strSelectedEvent. 
        //Should only be called after g_strSelectedEvent is populated (i.e. populateEventDetails() has been called)
        //Returns true iff teams are in the db for the currently selected event.
        private bool doTeamsExistForSelectedEvent()
        {
            //Get list of teams for the selected event.
            string dbCmd = "SELECT * FROM Teams WHERE eventID = " + g_strSelectedEventID;
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
                string dbCmd = "SELECT COUNT (*) FROM Teams WHERE eventID = " + g_strSelectedEventID;
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
            g_lstTeams = new List<Team>(g_lstAssignedPlayers.Count / 4);
            for (int i = 0; i < g_lstAssignedPlayers.Count / 4; i++)
                g_lstTeams.Add(new Team());

            //For every player, update the player object and team object
            for (int i = 0; i < g_lstAssignedPlayers.Count; i++)
            {
                int playerID = (g_lstAssignedPlayers[i] as Player).ID;

                string dbCmd = "SELECT * FROM Teams";
                dbCmd += " WHERE Teams.eventID = " + g_strSelectedEventID;
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
                    MessageBox.Show(ex0.Message);
                    return;
                }

                //update player object
                (g_lstAssignedPlayers[i] as Player).teamName = nTeamName;
                (g_lstAssignedPlayers[i] as Player).level = strPlayerLevel;

                //update team object
                if (strPlayerLevel == "A")
                    g_lstTeams[nTeamName-1].playerA = (g_lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "B")
                    g_lstTeams[nTeamName-1].playerB = (g_lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "C")
                    g_lstTeams[nTeamName-1].playerC = (g_lstAssignedPlayers[i] as Player);
                else if (strPlayerLevel == "D")
                    g_lstTeams[nTeamName-1].playerD = (g_lstAssignedPlayers[i] as Player);
            }
        }

        // Generate pairings for the rounds tab
        //Note that in this implementation, the number of teams must be <= the number of rounds (otherwise players would play in the same group again).
        // we may need to change this
        private void GeneratePairings_Click(object sender, EventArgs e)
        {
            //Ensure teams exists. 
            if (!doTeamsExistForSelectedEvent())
            {
                MessageBox.Show("ERROR: Rounds cannot be generated because no teams exist for this event.");
                return;
            }

            int nTeamsCount = g_lstAssignedPlayers.Count / 4;
            Random rand = new Random(); //ini randomizer

            //ini the data objects contained in the event's list of rounds so we can modify them
            g_lstRounds = new List<Round>(g_nEventRounds);
            for (int i = 0; i < g_nEventRounds; i ++)
            {
                g_lstRounds.Add(new Round(0));
                for (int j = 0; j < nTeamsCount; j++)
                    g_lstRounds[i].addGroup(new GroupOfFour());
            }
            //if number of players divided by four (i.e. number of teams) is less than the
            // number of rounds for this event, there will not be enough players for all the rounds.
            if (nTeamsCount < g_nEventRounds)
            {
                MessageBox.Show("ERROR: There are only " + g_lstTeams.Count.ToString() + " teams, which is not enough for " + g_nEventRounds.ToString() + " rounds.");
                return;
            }

            //Build the rounds, starting with the last round because thats where teams play each other
            // and those constraints must be established before processing the other rounds.
            for (int i = g_nEventRounds; i > 0; i--) //for every round as specified in Events:numRounds, 
            {
                //Round round = new Round(i); //ini a round with i as it's round number
                g_lstRounds[i-1].nRoundNumber = i;

                //If this is the last round, 
                if (i == g_nEventRounds)
                {
                    for (int j = 0; j < nTeamsCount; j++) //for every foursome this round will contain
                    {
                        GroupOfFour foursome = new GroupOfFour();
                        foursome.playerA = g_lstTeams[j].playerA;
                        foursome.playerB = g_lstTeams[j].playerB;
                        foursome.playerC = g_lstTeams[j].playerC;
                        foursome.playerD = g_lstTeams[j].playerD;

                        //TODO: Set the constraints on each player we just picked
                        g_lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
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
                            foursome.playerA = g_lstTeams[rand.Next(1, nTeamsCount)].playerA;
                            foursome.playerB = g_lstTeams[rand.Next(1, nTeamsCount)].playerB;
                            foursome.playerC = g_lstTeams[rand.Next(1, nTeamsCount)].playerC;
                            foursome.playerD = g_lstTeams[rand.Next(1, nTeamsCount)].playerD;
                            break;
                        }
                        g_lstRounds[i - 1].setGroupAtIndex(j, foursome); //Add this foursome to the round.
                    }
                }
                //g_lstRounds[i-1] = round; //"Push" this round to the list of the event's rounds (remember, we're iterating the rounds backwards.)
            } 
        }
    }
}
