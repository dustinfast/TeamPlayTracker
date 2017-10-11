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
            CreateNewEvent newevent = new CreateNewEvent();
            newevent.Show();
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
            catch (Exception ex1)
            {
                MessageBox.Show("Error! Unable to read from database!\n\nError Info: " + ex1.ToString());
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

            //Details tab: Get static event data (ID, name, start date, end date) and populate the details page with it
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
                MessageBox.Show("ERROR: Could not read from database due to an unspecified error");
                return;
            }

            tabControl.Enabled = true; //enable tab navigation, which was disabled until an event is selected
            textBoxSelectedEventID.Text = dataSet.Tables["Events"].Rows[0]["eventID"].ToString();
            labelEventName.Text = dataSet.Tables["Events"].Rows[0]["eventName"].ToString();
            string strStartDate = dataSet.Tables["Events"].Rows[0]["startDate"].ToString();
            string strEndDate = dataSet.Tables["Events"].Rows[0]["endDate"].ToString();
            labelStartDate.Text = strStartDate.Substring(0, strStartDate.IndexOf(' '));
            labelEndDate.Text = strEndDate.Substring(0, strEndDate.IndexOf(' '));


            //TODO Details Tab: number of Players, Teams, and Rounds assigned to event
            //TODO Details Tab: Calculate and display "Rounds with Results"
            //TODO Details Tab: Update Final Results display

            //TODO Populate Players tab

            //TODO Populate Teams tab

            //TODO Popualte Rounds tab

            //TODO Populate Results tab



        }
    }
}
