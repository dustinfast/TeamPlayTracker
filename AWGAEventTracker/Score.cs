using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class Score //An abstraction of a single round's score, consisting of a putt score, a point score, and if a substitution occured.
    {
        private int nEventID;
        private int nPlayerID;
        private int nRoundNumber;
        private int nPuttScore;
        private int nPointScore;
        private bool bSub;
        
        public Score(int eventid, int playerid, int roundnumber)
        {
            nEventID = eventid;
            nPlayerID = playerid;
            nRoundNumber = roundnumber;
            nPuttScore = -1;
            nPointScore = -1;
            bSub = false;

            populateScore();
        }

        public int getPuttScore()
        {
            return nPuttScore;
        }
        
        public int getPointScore()
        {
            return nPointScore;
        }

        public bool getSubScore()
        {
            return bSub;
        }

        private void populateScore()
        {
            string dbCmd = "SELECT * FROM Scores";
            dbCmd += " WHERE eventID = " + nEventID + " AND playerID = " + nPlayerID;
            dbCmd += " AND roundNumber = " + nRoundNumber;

            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Scores");
            }
            catch (Exception ex) 
            {
                return;
            }

            if (dataSet.Tables["Scores"].Rows.Count <= 0)
                return;

            nPointScore = (int)dataSet.Tables["Scores"].Rows[0]["pointScore"];
            nPuttScore = (int)dataSet.Tables["Scores"].Rows[0]["puttScore"];
            bSub = (bool)dataSet.Tables["Scores"].Rows[0]["isSubstitution"];
        }
    }
}
