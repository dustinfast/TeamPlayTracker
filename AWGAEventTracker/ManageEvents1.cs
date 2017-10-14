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

namespace AWGAEventTracker
{
    public partial class ManageEvents1 : Form
    {
        //Class Globals
        private string strSelectedEventID; //The ID of the currently selected event.
        private string strAssignedPlayers; //A comma delimited list of all the players (by ID) assigned to the currently selected event.
        private BindingList<Player> lstAssignedPlayers = new BindingList<Player>(); //All players objects assigned to currently selected event. Populated on Event select OR Assigned Player change
        private BindingList<Player> lstUnassignedPlayers = new BindingList<Player>(); //All players objects not assigned to currently selected event.

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

            populateEventDetails(); //Populates event details tab and strAssignedPlayers
            populatePlayersLists(); //Populates Player tab assigned/unassgined player lists 

            //TODO Populate Teams tab

            //TODO Popualte Rounds tab

            //TODO Populate Results tab

        }

        //Populates the event details tab and also sets the strAssignedPlayers global var
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
            //textBoxSelectedEventID.Text = dataSet.Tables["Events"].Rows[0]["eventID"].ToString();
            strSelectedEventID = dataSet.Tables["Events"].Rows[0]["eventID"].ToString(); //populates the strSelectedEventID global var
            labelEventName.Text = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            strAssignedPlayers = dataSet.Tables["Events"].Rows[0]["players"].ToString(); //Populates the strAssignedPlayers global var
            string strStartDate = dataSet.Tables["Events"].Rows[0]["startDate"].ToString();
            string strEndDate = dataSet.Tables["Events"].Rows[0]["endDate"].ToString();
            labelStartDate.Text = strStartDate.Substring(0, strStartDate.IndexOf(' '));
            labelEndDate.Text = strEndDate.Substring(0, strEndDate.IndexOf(' '));
        }

        //Populates the Players tab lists with the assigned and unassigned players. Should be called after strAssignedPlayers is populated.
        void populatePlayersLists()
        {
            //Clear existing lists
            lstUnassignedPlayers = new BindingList<Player>();
            lstAssignedPlayers = new BindingList<Player>();

            
            string dbCmd = "";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);

            DataSet dataSet = new DataSet();

            //Populate Assigned players
            if (strAssignedPlayers.Length > 0)
            {
                dbCmd = "SELECT * FROM Players WHERE playerID in (" + strAssignedPlayers + ")";
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
                    lstAssignedPlayers.Add(p);
                }
                listBoxAssignedPlayers.DisplayMember = "displayName";
                listBoxAssignedPlayers.ValueMember = "playerID";
                listBoxAssignedPlayers.DataSource = lstAssignedPlayers;
            }

            //Populate Unassigned players
            dbCmd = "SELECT * FROM Players";
            if (strAssignedPlayers.Length != 0)
                dbCmd += " WHERE playerID not in (" + strAssignedPlayers + ")";

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
                lstUnassignedPlayers.Add(p);
            }

            listBoxUnassignedPlayers.DisplayMember = "displayName";
            listBoxUnassignedPlayers.ValueMember = "playerID";
            listBoxUnassignedPlayers.DataSource = lstUnassignedPlayers;
        }

        //Moves a player from the Players:Unassigned list to the Players:Assignedlist
        private void buttonAssign_Click(object sender, EventArgs e)
        {
            if (listBoxUnassignedPlayers.SelectedItems.Count > 0)
            {
                Player p = listBoxUnassignedPlayers.SelectedItem as Player;
                lstUnassignedPlayers.Remove(p);
                lstAssignedPlayers.Add(p);
                populateAssignedPlayersString();
            }
        }

        //Moves a player from the Players:Assigned list to the Players:Unassignedlist
        private void buttonUnassign_Click(object sender, EventArgs e)
        {
            if (listBoxAssignedPlayers.SelectedItems.Count > 0)
            {
                Player p = listBoxAssignedPlayers.SelectedItem as Player;
                lstAssignedPlayers.Remove(p);
                lstUnassignedPlayers.Add(p);
                populateAssignedPlayersString();
            }
        }

        private void buttonAssignAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstUnassignedPlayers.Count; i++)
            {
                lstAssignedPlayers.Add(lstUnassignedPlayers[i] as Player);
            }
            populateAssignedPlayersString();
            populatePlayersLists(); 
        }

        //Populates strAssignedPlayers from the data in the Players:AssignedList then writes the new string to the db
        private void populateAssignedPlayersString()
        {
            //build assigned players string
            strAssignedPlayers = "";
            for (int i = 0; i < listBoxAssignedPlayers.Items.Count; i++)
                strAssignedPlayers += (listBoxAssignedPlayers.Items[i] as Player).playerID + ",";
            if (strAssignedPlayers.Length != 0)
                if (strAssignedPlayers[strAssignedPlayers.Length - 1] == ',')
                    strAssignedPlayers.Remove(strAssignedPlayers.Length - 1, 1); //remove trailing comma

            //update events db table 
            string strCmd = "UPDATE events SET players = '" + strAssignedPlayers + "' where eventID = " + strSelectedEventID; ;
             OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);
            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could modify user due to an unspecified database error.");
                return;
            }
}
    }
}
