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

    public partial class  ManagePlayers : Form
    {
        private string g_strLastSelectCmd;

        public ManagePlayers()
        {
            InitializeComponent();
        }

        //Called on form load
        private void ManagePlayers_Load(object sender, EventArgs e)
        {
            //Load data from Players table and populate gridview, sorted by lname
            showPlayerData("SELECT * FROM Players ORDER BY lName");
            //radioButtonSortLN.Checked = true;

            //Set focus to the first text box
            this.ActiveControl = textBoxAddFN;
        }

        //utility function to get data from db, according to the select command,
        // then populate the gridview with that data, and format the gridview.
        private void showPlayerData(string sqlselectcmd)
        {
            try
            {
                // Create a new data adapter based on the specified query.
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                dataAdapter = new OleDbDataAdapter(sqlselectcmd, Globals.g_dbConnection);

                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                dataGridView.DataSource = table;
                dataGridView.Columns["playerID"].Visible = false;
                dataGridView.Columns["fName"].HeaderText = "First Name";
                dataGridView.Columns["lName"].HeaderText = "Last Name";
                dataGridView.Columns["phone"].HeaderText = "Phone";
                dataGridView.Columns["handicap"].HeaderText = "Handicap";
                dataGridView.Columns["fName"].Width = 170;
                dataGridView.Columns["lName"].Width = 170;
                dataGridView.Columns["phone"].Width = 90;
                dataGridView.Columns["handicap"].Width = 80;
                dataGridView.Columns["phone"].DefaultCellStyle.Format = "(999) 000-0000";
                dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView.MultiSelect = false;
                dataGridView.ReadOnly = true;
                dataGridView.ClearSelection();

                //Set the last used command, to be used later if we need it.
                g_strLastSelectCmd = sqlselectcmd;

            }
            catch (OleDbException)
            {
                MessageBox.Show("ERROR: Could not load players due to an unspecified database error.");
            }
        }

        //Called on user click Close button
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ADD NEW USER HANDLERS
        //////////////////////////

        //called on user click add new player
        private void btnAddNewPlayer_Click(object sender, EventArgs e)
        {
            //Get User Input
            string strFName = Globals.removeTicksFromString(textBoxAddFN.Text); 
            string strLName = Globals.removeTicksFromString(textBoxAddLN.Text);
            string strPhone = Globals.removeTicksFromString(textBoxAddPhone.Text).Replace("-", "").Replace(".", "");
            string strHandicap = Globals.removeTicksFromString(textBoxAddHandicap.Text);
            double dTemp = 0;

            //Validate user input
            if (strFName == "")
            {
                MessageBox.Show("ERROR: First name must be populated");
                return;
            }

            if (strLName == "")
            {
                MessageBox.Show("ERROR: Last name must be populated.");
                return;
            }

            if (strPhone.Length != 10 || strPhone.Length != 7)
            {
                MessageBox.Show("ERROR: Invalid phone number.");
                return;
            }

            if (strHandicap == "")
            {
                MessageBox.Show("ERROR: Handicap must be populated.");
                return;
            }
            else
            {
                if (!double.TryParse(strHandicap, out dTemp))
                {
                    MessageBox.Show("ERROR: Handicap must be a numeric value.");
                    return;
                }
            }

            //Insert user into database
            string strCmd;
            if (strHandicap == "") //format the query based existence of the handicap field
            {
                strCmd = "INSERT INTO Players (fName, lName, phone)";
                strCmd = strCmd + "VALUES('" + strFName + "', '" + strLName + "', '" + strPhone + "')";
            }
            else
            {
                strCmd = "INSERT INTO Players (fName, lName, phone, handicap)";
                strCmd = strCmd + "VALUES('" + strFName + "', '" + strLName + "', '" + strPhone + "', " + strHandicap + ")";
            }

            OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could not add user due to an unspecified database error.");
                return;
            }

            //Update display and set focus back to the name field.
            showPlayerData(g_strLastSelectCmd); //refresh datagrid
            textBoxAddFN.Text = "";
            textBoxAddLN.Text = "";
            textBoxAddPhone.Text = "";
            textBoxAddHandicap.Text = "";
            this.ActiveControl = textBoxAddFN;
            textBoxEditFN.Text = "";
            textBoxEditLN.Text = "";
            textBoxEditPhone.Text = "";
            textBoxEditHandicap.Text = "";
            buttonDeletePlayer.Enabled = false;
            buttonModifyPlayer.Enabled = false;
            textBoxEditFN.Enabled = false;
            textBoxEditLN.Enabled = false;
            textBoxEditPhone.Enabled = false;
            textBoxEditHandicap.Enabled = false;
        }

        //Called on key up, to make the "enter" key on any of the Add New Users text boxes
        //"click" the Add button
        private void onAddNewUserKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddNewPlayer_Click(sender, e);
            }
        }



        // MODIFY USER HANDLERS
        //////////////////////////

        //Called on user click sort by LN
        private void radioButtonSortLN_CheckedChanged(object sender, EventArgs e)
        {
            //Load data from Players table and populate gridview, sorted by lname
            if (radioButtonSortLN.Checked == true)
                showPlayerData("SELECT * FROM Players ORDER BY lName");
        }

        //Called on user click sort by FN
        private void radioButtoSortFN1_CheckedChanged(object sender, EventArgs e)
        {
            //Load data from Players table and populate gridview, sorted by lname
            if (radioButtonSortFN.Checked == true)
                showPlayerData("SELECT * FROM Players ORDER BY fName");
        }

        //Called when a user clicks a row in the players list
        private void dataGridView_Click(object sender, EventArgs e)
        {
            //Populate the "Edit player" boxes with data from the selected gridview row
            textBoxEditID.Text = dataGridView.SelectedRows[0].Cells[0].Value.ToString();
            textBoxEditFN.Text = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            textBoxEditLN.Text = dataGridView.SelectedRows[0].Cells[2].Value.ToString();
            textBoxEditPhone.Text = dataGridView.SelectedRows[0].Cells[3].Value.ToString();
            textBoxEditHandicap.Text = dataGridView.SelectedRows[0].Cells[4].Value.ToString();
            

            if (textBoxEditID.Text.Length > 0)
            {
                buttonDeletePlayer.Enabled = true;
                buttonModifyPlayer.Enabled = true;
                textBoxEditFN.Enabled = true;
                textBoxEditLN.Enabled = true;
                textBoxEditPhone.Enabled = true;
                textBoxEditHandicap.Enabled = true;
            }
        }

        //Called on user click Delete Player
        private void buttonDeletePlayer_Click(object sender, EventArgs e)
        {
            //Ensure player to be deleted is not already assigned to an event
            string dbCmd = "SELECT * FROM Events WHERE players LIKE '%," + textBoxEditID.Text + ",%'";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Events");
                if (dataSet.Tables["Events"].Rows.Count > 0)
                {
                    MessageBox.Show("Cannot Delete: Player is currently assigned to an event.");
                    return;
                }
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
                return;
            }

            //Prompt to confirm delete
            if (MessageBox.Show("Are you sure you wish to delete this player? This cannot be undone.", "Delete Player?", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            dbCmd = "DELETE FROM Players WHERE playerID = " + textBoxEditID.Text;
            OleDbCommand command = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            // TODO Insert Try Black around this query execute
            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could not delete user due to an unspecified database error.");
                return;
            }

            //Set edit controls back to disabled until another player is selected
            showPlayerData(g_strLastSelectCmd); //refresh datagrid
            textBoxEditFN.Text = "";
            textBoxEditLN.Text = "";
            textBoxEditPhone.Text = "";
            textBoxEditHandicap.Text = "";
            buttonDeletePlayer.Enabled = false;
            buttonModifyPlayer.Enabled = false;
            textBoxEditFN.Enabled = false;
            textBoxEditLN.Enabled = false;
            textBoxEditPhone.Enabled = false;
            textBoxEditHandicap.Enabled = false;
            dataGridView.ClearSelection();
        }

        //Called on user click modify player
        private void buttonModifyPlayer_Click(object sender, EventArgs e)
        {
            //Get User Input
            string strFName = Globals.removeTicksFromString(textBoxEditFN.Text);
            string strLName = Globals.removeTicksFromString(textBoxEditLN.Text);
            string strPhone = Globals.removeTicksFromString(textBoxEditPhone.Text).Replace("-", "").Replace(".", "");
            string strHandicap = Globals.removeTicksFromString(textBoxEditHandicap.Text);
            double dTemp = 0;

            //Validate user input
            if (strFName == "")
            {
                MessageBox.Show("ERROR: First name must be populated");
                return;
            }

            if (strLName == "")
            {
                MessageBox.Show("ERROR: Last name must be populated.");
                return;
            }
            
            if (strPhone.Length != 10 || strPhone.Length != 7)
            {
                MessageBox.Show("ERROR: Phone must contain 7 or 10 digits");
                return;
            }

            if (strHandicap == "")
            {
                MessageBox.Show("ERROR: Handicap must be populated.");
                return;
            }
            else
            {
                if (!double.TryParse(strHandicap, out dTemp))
                {
                    MessageBox.Show("ERROR: Handicap must be a numeric value.");
                    return;
                }
            }


            //Insert user into database
            string strCmd;
            if (strHandicap == "") //format the query based existence of the handicap field
            {
                strCmd = "UPDATE players set fName = '" + strFName + "', lName = '" + strLName + "', phone = '" + strPhone + "', handicap = null WHERE playerID = " + textBoxEditID.Text;
            }
            else
            {
                strCmd = "UPDATE players set fName = '" + strFName + "', lName = '" + strLName + "', phone = '" + strPhone + "', handicap = " + strHandicap + " WHERE playerID = " + textBoxEditID.Text;
            }

            OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

            // Insert a try around this query exexute 
            if (command.ExecuteNonQuery() == 0)
            {
                MessageBox.Show("ERROR: Could modify user due to an unspecified database error.");
                return;
            }

            //Update display and set focus back to the name field.
            showPlayerData(g_strLastSelectCmd); //refresh datagrid
            textBoxEditFN.Text = "";
            textBoxEditLN.Text = "";
            textBoxEditPhone.Text = "";
            textBoxEditHandicap.Text = "";
            buttonDeletePlayer.Enabled = false;
            buttonModifyPlayer.Enabled = false;
            textBoxEditFN.Enabled = false;
            textBoxEditLN.Enabled = false;
            textBoxEditPhone.Enabled = false;
            textBoxEditHandicap.Enabled = false;
            dataGridView.ClearSelection();
        }

        private void textBoxAddPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTipPhoneNum.ToolTipTitle = "Invalid Input";
            toolTipPhoneNum.Show("The phone number input is invalid. Valid inputs are a digits 0-9. Area code is not required.", textBoxAddPhone, textBoxAddPhone.Location, 5000);

        }

        private void toolTipPhoneNum_Popup(object sender, PopupEventArgs e)
        {

        }
        private void textBoxEditPhone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTipPhoneNum.ToolTipTitle = "Invalid Input";
            toolTipPhoneNum.Show("The phone number input is invalid. Valid inputs are a digits 0-9. Area code is not required.", textBoxEditPhone, textBoxEditPhone.Location, 5000);

        }
    }
}
