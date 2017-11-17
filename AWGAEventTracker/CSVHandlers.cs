using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Data;

namespace AWGAEventTracker
{
    class CSVHandlers
    {
        //builds a csv file from the teams of eventname, having an event id of eventid
        public void buildAndOpenTeamsCSV(string eventid, string eventname)
        {
            string dbCmd = "SELECT * FROM Teams";
            dbCmd += " LEFT JOIN Players ON Teams.playerID = Players.playerID";
            dbCmd += " WHERE Teams.eventID = " + eventid;
            dbCmd += " ORDER BY Teams.teamName, Teams.playerLevel";
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
                return;
            }

            //Ensure teams exists
            if (dataSet.Tables["Teams"].Rows.Count <= 0)
            {
                MessageBox.Show("ERROR: No Teams exist for this event."); 
                return;
            }

            //build the CSV output string
            string strPrevTeamName = dataSet.Tables["Teams"].Rows[0]["teamName"].ToString();
            string strOutput = eventname + " Teams\n\nTeam 1\n"; //Main header and team 1 header
            foreach (DataRow dRow in dataSet.Tables["Teams"].Rows)
            {
                string strTeamName = dRow["teamName"].ToString();

                //if we're starting a new team, add team header (i.e. Team name and an empty line)
                if (strPrevTeamName != strTeamName)
                {
                    strOutput += "\nTeam " + strTeamName + "\n";
                    strPrevTeamName = strTeamName;
                }

                //Add the player in this row to the output
                strOutput += dRow["playerLevel"].ToString() + " (" + dRow["handicap"].ToString() + "),"; //player level and handicap
                strOutput += dRow["fName"].ToString() + "," + dRow["lName"].ToString() + ","; //player name
                strOutput += dRow["phone"].ToString() + "\n";
            }

            //Write the output string to a file
            string strOutputDir = "TemporaryFiles";
            string strOutputFile = strOutputDir + "\\" + eventname + "Teams.csv";
            try
            {
                System.IO.Directory.CreateDirectory(strOutputDir); //Create temp dir if it doesn't already exist
                System.IO.File.WriteAllText(strOutputFile, strOutput); //Create file with strOutput as the content
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Could not write output file.\nEnsure you are not already viewing teams for this event.");
                return;
            }

            //Open the file
            openFile(strOutputFile);
        }

        public void buildAndOpenRoundsCSV(string eventid, string eventname)
        {
            //Not implemented
        }

        public void buildAndOpenScoresCSV(string eventid, string eventname)
        {
            //Not implemented
        }

        public void buildAndOpenResultsCSV(string eventid, string eventname)
        {
            //Not implemented
        }

        //Opens a file with localhost's default program for a file of filename's extension.
        //Ex: a CSV file may be opened with Excel, if Excel is associated with CSV files.
        public void openFile(string filename)
        {
            try
            {
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Could not open file.\n\n" + ex.ToString());
                return;
            }
        }
    }
}
