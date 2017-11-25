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
            dbCmd += " ORDER BY Teams.teamNumber, Teams.playerLevel";
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
            string strPrevteamNumber = dataSet.Tables["Teams"].Rows[0]["teamNumber"].ToString();
            string strOutput = e.strName + " Teams\n\nTeam 1\n"; //Main header and team 1 header
            foreach (DataRow dRow in dataSet.Tables["Teams"].Rows)
            {
                string strteamNumber = dRow["teamNumber"].ToString();

                //if we're starting a new team, add team header (i.e. Team name and an empty line)
                if (strPrevteamNumber != strteamNumber)
                {
                    strOutput += "\nTeam " + strteamNumber + "\n";
                    strPrevteamNumber = strteamNumber;
                }

                //Add the player in this row to the output
                //strOutput += dRow["handicap"].ToString() + ","; //playerhandicap
                strOutput += dRow["playerLevel"].ToString() + " (" + dRow["handicap"].ToString() + "),"; //player level and handicap
                strOutput += dRow["fName"].ToString() + "," + dRow["lName"].ToString() + ","; //player name
                strOutput += dRow["phone"].ToString() + "\n";
            }

            //Write the output string to a file
            string strOutputFile = doFileWrite(e.strName + "Teams.csv", strOutput);

            //Open the file
            openFile(strOutputFile);
        }

        public void buildAndOpenRoundsCSV(Event e)
        {
            //Ensure rounds exist, else return
            if (e.lstRounds == null || e.lstRounds[0].lstGroups[0] == null)
                return;

            //build the CSV output string
            string strPrevRoundName = "";
            string strOutput = e.strName + " Rounds\nNote: Players play teammates during the last round.\n"; //Main header and round 1 header
            foreach (Round r in e.lstRounds)
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
                    strOutput += "\n,Group " + nGroupName.ToString() + "\n";
                    strOutput += ",A," + g.playerA.fName + ", " + g.playerA.lName + ", " + g.playerA.phone + "\n";
                    strOutput += ",B," + g.playerB.fName + ", " + g.playerB.lName + ", " + g.playerB.phone + "\n";
                    strOutput += ",C," + g.playerC.fName + ", " + g.playerC.lName + ", " + g.playerC.phone + "\n";
                    strOutput += ",D," + g.playerD.fName + ", " + g.playerD.lName + ", " + g.playerD.phone + "\n";
                }
            }

            //Write the output string to a file
            string strOutputFile = doFileWrite(e.strName + "Rounds.csv", strOutput);

            //Open the file
            openFile(strOutputFile);
        }

        //Builds and opens the scores for the current round in an alphabetized list by player
        public void buildAndOpenScoresCSV(Event e)
        {
            string dbCmd = "SELECT * FROM Scores";
            dbCmd += " LEFT JOIN Players ON Scores.playerID = Players.playerID";
            dbCmd += " WHERE Scores.eventID = " + e.nID;
            dbCmd += " ORDER BY Players.lName, Scores.roundNumber";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Scores");
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
                return;
            }

            //Ensure Scores exists
            if (dataSet.Tables["Scores"].Rows.Count <= 0)
            {
                MessageBox.Show("No Scores have been entered for this event.");
                return;
            }

            //Start building the CSV output string
            int nPrevPlayerID = -1;
            int nRowCount = 0;
            string strOutput = e.strName + " Scores\n\n"; //Title 

            //build column headers
            strOutput += " ,Last Name,First Name,Team,Position,"; 
            for(int i = 1; i <= e.nRounds; i++)
            {
                strOutput += " Rnd " + i.ToString() + " Points,";
                strOutput += " Rnd " + i.ToString() + " Putts,";
            }
            strOutput += "Total Points,Total Putts\n";

            foreach (DataRow dRow in dataSet.Tables["Scores"].Rows)
            {
                int nPlayerID = (int)dRow["Scores.playerID"];
                nRowCount++;

                //Start a new player line
                if (nPrevPlayerID != nPlayerID)
                {
                    strOutput += nRowCount.ToString() + "," + dRow["lName"].ToString() + "," + dRow["fName"].ToString() + ",";
                    strOutput += dRow["teamNumber"].ToString() + "," + "X" + ",";

                    int nTotalPoints = 0;
                    int nTotalPutts = 0;
                    bool bSubFlag = false;
                    for (int i = 0; i < e.nRounds; i++) //for each round we have for this player
                    {
                        Score s = new Score(e.nID, nPlayerID, (i + 1));

                        string strPointsDisp = "";
                        int nPoints = s.getPointScore();
                        if (nPoints != -1)
                        {
                            nTotalPoints += nPoints;
                            strPointsDisp = nPoints.ToString();
                        }

                        string strPuttsDisp = "";
                        int nPutts = s.getPuttScore();
                        if (nPutts != -1)
                        {
                            nTotalPutts += nPutts;
                            strPuttsDisp = nPutts.ToString();
                        }

                        if (s.getSubScore())
                            bSubFlag = true;

                        strOutput += strPointsDisp + "," + strPuttsDisp;

                        if (s.getSubScore())
                            strOutput += "s";
                        strOutput += ",";
                    }

                    strOutput += nTotalPoints.ToString() + ",";
                    if (!bSubFlag)
                        strOutput += nTotalPutts.ToString();
                    else
                        strOutput += "n/e";
                    strOutput += "\n";

                    nPrevPlayerID = nPlayerID;
                }
            }

            //Write the output string to a file
            string strOutputFile = doFileWrite(e.strName + "Scores.csv", strOutput);

            //Open the file
            openFile(strOutputFile);
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
                //MessageBox.Show("ERROR: Could not open file.\n\n" + ex.ToString());
                return;
            }
        }

        //writes csvstring to the file specified
        //returns the path/filename of the written file, relative to CWD
        private string doFileWrite(string filename, string csvstring)
        {
            string strOutputDir = "TemporaryFiles";
            string strOutputFile = strOutputDir + "\\" + filename;
            try
            {
                System.IO.Directory.CreateDirectory(strOutputDir); //Create temp dir if it doesn't already exist
                System.IO.File.WriteAllText(strOutputFile, csvstring); //Create file with strOutput as the content
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Could not write output file.\nEnsure the report is not already open, then try again.");
                return "";
            }

            return strOutputFile;

        }
    }
}
