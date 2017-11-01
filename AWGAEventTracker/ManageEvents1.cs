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
        //Class Globals
        private string g_strSelectedEventID; //The ID of the currently selected event. Populated in populateEventDetails.
        private string g_strAssignedPlayers; //A comma delimited list of all the players (by ID) assigned to the currently selected event. Populated in populateEventDetails.
        private BindingList<Player> g_lstAssignedPlayers = new BindingList<Player>(); //All players objects assigned to currently selected event. Populated on Event select OR Assigned Player change
        private BindingList<Player> g_lstUnassignedPlayers = new BindingList<Player>(); //All players objects not assigned to currently selected event.

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

            //Set Teams tab button states (enabled/disabled) based on the existence of teams for this event
            bool bResult = doTeamsExistForSelectedEvent();
            buttonGenerateTeams.Enabled = !bResult;
            buttonViewTeams.Enabled = bResult;

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
            labelRoundCount.Text = dataSet.Tables["Events"].Rows[0]["numRounds"].ToString();
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
            playerNum = Regex.Matches(g_strAssignedPlayers, ",").Count;
            labelPlayerCount.Text = (playerNum + 1).ToString();
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
                listBoxAssignedPlayers.DisplayMember = "displayName";
                listBoxAssignedPlayers.ValueMember = "playerID";
                listBoxAssignedPlayers.DataSource = g_lstAssignedPlayers;
            }

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
        
    }
}
