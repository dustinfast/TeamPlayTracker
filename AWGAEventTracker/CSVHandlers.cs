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
        //Builds a csv file from the teams of eventname, having an event id of e.nID.
        //We are getting the data from the database itself rather than the event object (like we 
        // do in the other buildAndOpenCSV functions in this class because at the time, 
        // those obejcts didn't exist
        public void buildAndOpenTeamsCSV(Event e)
        {
            string dbCmd = "SELECT * FROM Teams";
            dbCmd += " LEFT JOIN Players ON Teams.playerID = Players.playerID";
            dbCmd += " WHERE Teams.eventID = " + e.nID;
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
                MessageBox.Show("No Teams exist for this event."); 
                return;
            }

            //build the CSV output string
            string strPrevTeamName = dataSet.Tables["Teams"].Rows[0]["teamName"].ToString();
            string strOutput = e.strName + " Teams\n\nTeam 1\n"; //Main header and team 1 header
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
            string strOutputFile = strOutputDir + "\\" + e.strName + "Teams.csv";
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

        public void buildAndOpenRoundsCSV(Event e)
        {
            //TODO: Ensure rounds exist, else return
            
            //Since group[0] is actually the final round, we must read the list of rounds in reverse order. So push to stack then read
            Stack<Round> sRounds = new Stack<Round>();
            foreach (Round round in e.lstRounds)
                sRounds.Push(round);

            //build the CSV output string
            string strPrevRoundName = "";
            string strOutput = e.strName + " Rounds\nNote: Players play teammates during the last round.\n"; //Main header and round 1 header
            foreach (Round r in sRounds)
            {
                string strRoundName = r.strRoundName;

                //if we're starting a new round, add round header (i.e. Round name and an empty line)
                if (strPrevRoundName != strRoundName)
                {
                    strOutput += "\n" + strRoundName;
                    strPrevRoundName = strRoundName;
                }

                int nGroupName = 0;
                foreach (GroupOfFour g in r.lstGroups)
                {
                    nGroupName += 1;
                    strOutput += "\n,Flight " + nGroupName.ToString() + "\n";
                    strOutput += ",A," + g.playerA.fName + ", " + g.playerA.lName + ", " + g.playerA.phone + "\n";
                    strOutput += ",B," + g.playerB.fName + ", " + g.playerB.lName + ", " + g.playerB.phone + "\n";
                    strOutput += ",C," + g.playerC.fName + ", " + g.playerC.lName + ", " + g.playerC.phone + "\n";
                    strOutput += ",D," + g.playerD.fName + ", " + g.playerD.lName + ", " + g.playerD.phone + "\n";
                }
            }

            //Write the output string to a file
            string strOutputDir = "TemporaryFiles";
            string strOutputFile = strOutputDir + "\\" + e.strName + "Rounds.csv";
            try
            {
                System.IO.Directory.CreateDirectory(strOutputDir); //Create temp dir if it doesn't already exist
                System.IO.File.WriteAllText(strOutputFile, strOutput); //Create file with strOutput as the content
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: Could not write output file.\nEnsure you are not already viewing rounds for this event.");
                return;
            }

            //Open the file
            openFile(strOutputFile);
        }

        public void buildAndOpenScoresCSV(Event e)
        {
            //Not implemented
        }

        public void buildAndOpenResultsCSV(Event e)
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
