using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWGAEventTracker
{
    public partial class CreateNewEvent : Form
    {
        public CreateNewEvent()
        {
            InitializeComponent();
        }

        // Called on cancel btn click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Called on btn OK click
        private void btnOk_Click(object sender, EventArgs e)
        {
            //Get user data from textbox
            string strEventName = Globals.removeTicksFromString(textBoxEventName.Text);
            string strEventStartDate = dateTimePickerStartDate.Value.Year.ToString() + "-" + dateTimePickerStartDate.Value.Month.ToString() + "-" + dateTimePickerStartDate.Value.Day.ToString();
            string strEventEndDate = dateTimePickerEndDate.Value.Year.ToString() + "-" + dateTimePickerEndDate.Value.Month.ToString() + "-" + dateTimePickerEndDate.Value.Day.ToString();

            //Ensure end date is after start date
            if (dateTimePickerEndDate.Value.Date.CompareTo(dateTimePickerStartDate.Value.Date) < 0)
            {
                MessageBox.Show("ERROR: Event was not added - Start date must occur before end date.\nPlease try again.");
                return;
            }

            //Ensure event name is populated 
            if (strEventName.Length <= 0)
            {
                MessageBox.Show("ERROR: Event was not added - Event name must be populated.\nPlease try again.");
                return;
            }

            //Check database to ensure an event with this name doesn't already exist
            string dbCmd = "SELECT * FROM Events WHERE eventName = '" + strEventName + "'";
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

            if (dataSet.Tables["Events"].Rows.Count > 0)
            {
                MessageBox.Show("ERROR: Event was not added - an event by that name already exists.\nPlease try again.");
            }
            else
            {
                string strCmd;
                strCmd = "INSERT INTO Events (eventName, startDate, EndDate) VALUES ('" + strEventName + "', '" + strEventStartDate+ "', '"+ strEventEndDate +"')";

                OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex0)
                {
                    MessageBox.Show("There was an error adding event:\n" + ex0.ToString());
                    return;
                }

                this.Close(); // on success
            }
        }
    }
}
