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

        //Called when the Event Selector drop down box is clicked.
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

        // Called on Cancel btn click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //TODO: Check for unsaved changes and promnpt user "are you suer"
            this.Close();
        }

        // Called on OK btn click
        private void btnOk_Click(object sender, EventArgs e)
        {
            //TODO: save changes
            this.Close();
        }

        // Called on 'Create New Event' btn click
        private void btnCreateNewEvent_Click(object sender, EventArgs e)
        {
            CreateNewEvent newevent = new CreateNewEvent();
            newevent.Show();
        }

        
    }
}
