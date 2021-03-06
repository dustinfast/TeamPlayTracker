﻿/// EnterScores.cs - Event handlers for the Enter Scores dialog box.
///
/// Dustin Fast, 2017

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
    partial class EnterScores : Form
    {
        Event theEvent;
        public int nBegin;
        
        public EnterScores(Event e, int round)
        {
            theEvent = e;
            nBegin = round;
            InitializeComponent();
        }

        //called on form load
        private void EnterScores_Load(object sender, EventArgs e)
        {
            //Starts the "enter scores" process with round 1, group 1 (unless otherwise specified by jumpto)
            populateData((nBegin-1), 0, (nBegin - 1), 1);
        }

        //Populates the form, given a round and group. Assumes isValidGroup(round, group) == true
        private void populateData(int round, int group, int nextround, int nextgroup)
        {
            //populate curr and next
            labelCurrRound.Text = (round + 1).ToString();
            labelCurrGroup.Text = (group + 1).ToString();
            textBoxNextRound.Text = (nextround + 1).ToString();
            textBoxNextGroup.Text = (nextgroup + 1).ToString();

            //populate Player names
            GroupOfFour g = theEvent.lstRounds[round].lstGroups[group];
            labelAPlayer.Text = g.playerA.displayName.PadRight(21).Substring(0, 21);
            labelBPlayer.Text = g.playerB.displayName.PadRight(21).Substring(0, 21);
            labelCPlayer.Text = g.playerC.displayName.PadRight(21).Substring(0, 21);
            labelDPlayer.Text = g.playerD.displayName.PadRight(21).Substring(0, 21);

            //Get scores from db for current round/group and populate scores/subs if they exist. Else populate with zeroes.
            if (!doScoresExistFor(theEvent.nID, (round + 1), (group + 1)))
            {
                textBoxAPutts.Text = "0";
                textBoxBPutts.Text = "0";
                textBoxCPutts.Text = "0";
                textBoxDPutts.Text = "0";
                textBoxAScore.Text = "0";
                textBoxBScore.Text = "0";
                textBoxCScore.Text = "0";
                textBoxDScore.Text = "0";
                checkBoxASub.Checked = false;
                checkBoxBSub.Checked = false;
                checkBoxCSub.Checked = false;
                checkBoxDSub.Checked = false;
            }
            else
            {
                string strCmd = "SELECT * FROM Scores WHERE eventID = " + theEvent.nID;
                strCmd += " AND roundNumber = " + (round + 1) + " AND groupNumber = " + (group + 1);
                strCmd += " ORDER BY rowID ASC";
                OleDbCommand dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
                OleDbDataAdapter dbAdapter = new OleDbDataAdapter(dbCmd);
                DataSet dataSet = new DataSet();
                try
                {
                    dbAdapter.Fill(dataSet, "Scores");
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR 1013: Could not write to database.\n" + ex.Message);
                    return;
                }

                int nRow = 0;
                foreach (DataRow r in dataSet.Tables["Scores"].Rows)
                {
                    if (nRow == 0) //0th element
                    {
                        textBoxAPutts.Text = r["puttScore"].ToString();
                        textBoxAScore.Text = r["pointScore"].ToString();
                        checkBoxASub.Checked = Convert.ToBoolean(r["isSubstitution"].ToString());
                    }
                    else if (nRow == 1)
                    {
                        textBoxBScore.Text = r["pointScore"].ToString();
                        textBoxBPutts.Text = r["puttScore"].ToString();
                        checkBoxBSub.Checked = Convert.ToBoolean(r["isSubstitution"].ToString());
                    }
                    else if (nRow == 2)
                    {
                        textBoxCPutts.Text = r["puttScore"].ToString();
                        textBoxCScore.Text = r["pointScore"].ToString();
                        checkBoxCSub.Checked = Convert.ToBoolean(r["isSubstitution"].ToString());
                    }
                    else if (nRow == 3)
                    {
                        textBoxDPutts.Text = r["puttScore"].ToString();
                        textBoxDScore.Text = r["pointScore"].ToString();
                        checkBoxDSub.Checked = Convert.ToBoolean(r["isSubstitution"].ToString());
                    }
                    nRow++;
                }
            }
        }

        //Validates a round/group. Return true iff valid
        private bool isValidGroup(int round, int group)
        {
            if (round < 0 || round >= theEvent.lstRounds.Count || group < 0 || group >= theEvent.lstTeams.Count)
                return false;
            return true;
        }

        //From given round/group, populate next round/group
        private void getNextGroup(int round, int group, out int nextround, out int nextgroup)
        {
            if (group >= theEvent.lstTeams.Count)
            {
                nextgroup = 0;
                nextround = round + 1;
                if (nextround >= theEvent.lstRounds.Count)
                    nextround = 0;
            }
            else
            {
                nextround = round;
                nextgroup = group + 1;

                if(nextgroup >= theEvent.lstTeams.Count)
                {
                    nextgroup = 0;
                    nextround = round + 1;
                    if (nextround >= theEvent.lstRounds.Count)
                        nextround = 0;
                }
            }

            if(!isValidGroup(round, group))
            {
                nextround = 0;
                nextgroup = 0;
            }
        }

        //Called on user click Save and Next
        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (!validateInput())
                return;

            int nCurrRound = Convert.ToUInt16(labelCurrRound.Text)-1;
            int nCurrGroup = Convert.ToUInt16(labelCurrGroup.Text)-1;
            int nNextRound = Convert.ToUInt16(textBoxNextRound.Text)-1;
            int nNextGroup = Convert.ToUInt16(textBoxNextGroup.Text)-1;

            writeScores(nCurrRound, nCurrGroup);

            if(!isValidGroup(nNextRound, nNextGroup))
            {
                nNextRound = 0;
                nNextGroup = 0;
            }

            getNextGroup(nNextRound, nNextGroup, out nCurrRound, out nCurrGroup);

            populateData(nNextRound, nNextGroup, nCurrRound, nCurrGroup);

            textBoxAScore.Focus();
        }

        //Called on user click Save and Close
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (!validateInput())
                return;

            int nCurrRound = Convert.ToUInt16(labelCurrRound.Text) - 1;
            int nCurrGroup = Convert.ToUInt16(labelCurrGroup.Text) - 1;

            writeScores(nCurrRound, nCurrGroup);

            this.Close();
        }

        //Writes scores to the db. Assumes input has already been validated
        private void writeScores(int round, int group)
        {
            int nRoundID = theEvent.lstRounds[round].nID;
            int nAID = theEvent.lstRounds[round].lstGroups[group].playerA.ID;
            int nBID = theEvent.lstRounds[round].lstGroups[group].playerB.ID;
            int nCID = theEvent.lstRounds[round].lstGroups[group].playerC.ID;
            int nDID = theEvent.lstRounds[round].lstGroups[group].playerD.ID;
            int nATeamID = theEvent.lstRounds[round].lstGroups[group].playerA.teamNumber;
            int nBTeamID = theEvent.lstRounds[round].lstGroups[group].playerB.teamNumber;
            int nCTeamID = theEvent.lstRounds[round].lstGroups[group].playerC.teamNumber;
            int nDTeamID = theEvent.lstRounds[round].lstGroups[group].playerD.teamNumber;
            int nAScore = Convert.ToUInt16(textBoxAScore.Text);
            int nBScore = Convert.ToUInt16(textBoxBScore.Text);
            int nCScore = Convert.ToUInt16(textBoxCScore.Text);
            int nDScore = Convert.ToUInt16(textBoxDScore.Text);
            int nAPutts = Convert.ToUInt16(textBoxAPutts.Text);
            int nBPutts = Convert.ToUInt16(textBoxBPutts.Text);
            int nCPutts = Convert.ToUInt16(textBoxCPutts.Text);
            int nDPutts = Convert.ToUInt16(textBoxDPutts.Text);
            int nASub = Convert.ToInt16(checkBoxASub.Checked);
            int nBSub = Convert.ToInt16(checkBoxBSub.Checked);
            int nCSub = Convert.ToInt16(checkBoxCSub.Checked);
            int nDSub = Convert.ToInt16(checkBoxDSub.Checked);
            string strACmd = "";
            string strBCmd = "";
            string strCCmd = "";
            string strDCmd = "";

            //if scores for this round/group already exist, update them. Else insert them
            if (doScoresExistFor(theEvent.nID, (round + 1), (group + 1))) 
            {
                strACmd = "UPDATE Scores SET puttScore = " + nAPutts + ", pointScore = " + nAScore + ", isSubstitution = " + nASub;
                strACmd += " WHERE playerID = " + nAID + " AND roundID = " + nRoundID + " AND roundNumber = " + (round + 1);
                strACmd += " AND groupNumber = " + (group + 1) + " AND teamNumber = " + nATeamID;

                strBCmd = "UPDATE Scores SET puttScore = " + nBPutts + ", pointScore = " + nBScore + ", isSubstitution = " + nBSub;
                strBCmd += " WHERE playerID = " + nBID + " AND roundID = " + nRoundID + " AND roundNumber = " + (round + 1);
                strBCmd += " AND groupNumber = " + (group + 1) + " AND teamNumber = " + nBTeamID;

                strCCmd = "UPDATE Scores SET puttScore = " + nCPutts + ", pointScore = " + nCScore + ", isSubstitution = " + nCSub;
                strCCmd += " WHERE playerID = " + nCID + " AND roundID = " + nRoundID + " AND roundNumber = " + (round + 1);
                strCCmd += " AND groupNumber = " + (group + 1) + " AND teamNumber = " + nCTeamID;

                strDCmd = "UPDATE Scores SET puttScore = " + nDPutts + ", pointScore = " + nDScore + ", isSubstitution = " + nDSub;
                strDCmd += " WHERE playerID = " + nDID + " AND roundID = " + nRoundID + " AND roundNumber = " + (round + 1);
                strDCmd += " AND groupNumber = " + (group + 1) + " AND teamNumber = " + nDTeamID;
            }
            else
            {
                strACmd = "INSERT INTO Scores (playerID, roundID, roundNumber, groupNumber, puttScore, pointScore, isSubstitution, teamNumber, eventID) ";
                strACmd += "VALUES (" + nAID + "," + nRoundID + "," + (round + 1) + "," + (group + 1) + "," + nAPutts + "," + nAScore + ",";
                strACmd += nASub + "," + nATeamID + "," + theEvent.nID + ")";

                strBCmd = "INSERT INTO Scores (playerID, roundID, roundNumber, groupNumber, puttScore, pointScore, isSubstitution, teamNumber, eventID) ";
                strBCmd += "VALUES (" + nBID + "," + nRoundID + "," + (round + 1) + "," + (group + 1) + "," + nBPutts + "," + nBScore + ",";
                strBCmd += nBSub + "," + nBTeamID + "," + theEvent.nID + ")";

                strCCmd = "INSERT INTO Scores (playerID, roundID, roundNumber, groupNumber, puttScore, pointScore, isSubstitution, teamNumber, eventID) ";
                strCCmd += "VALUES (" + nCID + "," + nRoundID + "," + (round + 1) + "," + (group + 1) + "," + nCPutts + "," + nCScore + ",";
                strCCmd += nCSub + "," + nCTeamID + "," + theEvent.nID + ")";

                strDCmd = "INSERT INTO Scores (playerID, roundID, roundNumber, groupNumber, puttScore, pointScore, isSubstitution, teamNumber, eventID) ";
                strDCmd += "VALUES (" + nDID + "," + nRoundID + "," + (round + 1) + "," + (group + 1) + "," + nDPutts + "," + nDScore + ",";
                strDCmd += nDSub + "," + nDTeamID + "," + theEvent.nID + ")";
            }

            OleDbCommand cmdA = new OleDbCommand(strACmd, Globals.g_dbConnection);
            OleDbCommand cmdB = new OleDbCommand(strBCmd, Globals.g_dbConnection);
            OleDbCommand cmdC = new OleDbCommand(strCCmd, Globals.g_dbConnection);
            OleDbCommand cmdD = new OleDbCommand(strDCmd, Globals.g_dbConnection);

            try
            {
                cmdA.ExecuteNonQuery();
                cmdB.ExecuteNonQuery();
                cmdC.ExecuteNonQuery();
                cmdD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR 1013: Could not write to database.\n" + ex.Message);
                return;
            }
        }

        //validates the input box data.
        private bool validateInput()
        {
            //Ensure textboxes contain only positive integers
            try
            {
                Convert.ToUInt16(textBoxNextRound.Text);
                Convert.ToUInt16(textBoxNextGroup.Text);
                Convert.ToUInt16(textBoxAScore.Text);
                Convert.ToUInt16(textBoxBScore.Text);
                Convert.ToUInt16(textBoxCScore.Text);
                Convert.ToUInt16(textBoxDScore.Text);
                Convert.ToUInt16(textBoxAPutts.Text);
                Convert.ToUInt16(textBoxBPutts.Text);
                Convert.ToUInt16(textBoxCPutts.Text);
                Convert.ToUInt16(textBoxDPutts.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Error: Only positive numbers may be entered. Please correct your input and try again.");
                return false;
            }

            return true;
        }

        //returns the rowID 
        private bool doScoresExistFor(int eventid, int roundnum, int groupnum)
        {
            string strCmd = "SELECT COUNT (*) FROM Scores WHERE eventID = " + eventid;
            strCmd += " AND roundNumber = " + roundnum + " AND groupNumber = " + groupnum;
            OleDbCommand dbCmd = new OleDbCommand(strCmd, Globals.g_dbConnection);
            if ((int)dbCmd.ExecuteScalar() > 0)
                return true;
            return false;
        }

        //closes the dialog without saving the current user entries
        private void buttonCloseNoSave_click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Checks to see if the entered key was 'enter', if so, "submits" the form with the Save and Next Button.
        private void onScoreBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonNext_Click(sender, e);
            }
        }
    }
}
