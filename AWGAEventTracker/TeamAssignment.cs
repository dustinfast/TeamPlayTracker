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
    class TeamAssignment
    {
        //Constains a single function that generates teams from a list of player objects.
        //Before proceeding, ensures teams have not yet been generated for this event 
        // and that the number of players assigned is divisible by four. Then generates 
        // numPlayers/4 teams of four players each. Returns a bool denoting the status 
        // of what the Generate Teams button should be. I.e. True = enabled, False = disabled.

        public bool generateTeams(int eventid, List<Player> playerobjects)
        {
            //Ensure teams for this eventID do not already exist
            string dbCmd = "SELECT * FROM Teams WHERE eventID = " + eventid;
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Teams");
                if (dataSet.Tables["Teams"].Rows.Count != 0)
                {
                    MessageBox.Show("ERROR: Teams for this event have already been assigned.");
                    return false;
                }
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
                return true;
            }

            //Ensure the number of total players assigned to this event is > 0 and divisible by 4
            int nPlayers = playerobjects.Count;

            if (nPlayers <= 0 || nPlayers % 4 != 0)
            {
                MessageBox.Show("ERROR: The number of players assigned to this event (" + nPlayers.ToString() + ") must be greater than 0 and a multiple of 4.");
                return true;
            }

            //A copy of the player objects in g_lstAssignedPlayers, but this one is sorted by handicap (lowest to highest)
            List<Player> players = playerobjects.OrderBy(o => o.handicap).ToList();

            //Container for the teams. Will contain members of all players in chunks of 4, denoting team assignment.
            List<Player> teams = new List<Player>();

            //Containers for the players of each level
            List<Player> aPlayers = new List<Player>();
            List<Player> bPlayers = new List<Player>();
            List<Player> cPlayers = new List<Player>();
            List<Player> dPlayers = new List<Player>();

            //Set player handicap based on position in sorted list and set player object handicap property
            //this will result in population of the four list containers above.
            int a = players.Count / 4;
            //A players (i.e. the first quarter of the list)
            for (int i = 0; i < a; i++)
            {
                players[i].level = "A";
                aPlayers.Add(players[i]);
            }

            //B players (i.e. the second quarter of the list)
            for (int i = 0 + a; i < 2 * a; i++)
            {
                players[i].level = "B";
                bPlayers.Add(players[i]);
            }

            //C players (i.e. the third quarter of the list)
            for (int i = 0 + 2 * a; i < 3 * a; i++)
            {
                players[i].level = "C";
                cPlayers.Add(players[i]);
            }
            //Dplayers (i.e. the last quarter of the list)
            for (int i = 0 + 3 * a; i < 4 * a; i++)
            {
                players[i].level = "D";
                dPlayers.Add(players[i]);
            }

            //// Randomly shuffle the players so that skill level is randomly distributed in each list
            Random random = new Random();
            int randomNumber = random.Next();

            //// Shuffle A players
            for (int k = 0; k < aPlayers.Count; k++)
            {
                int r = k + random.Next() % (aPlayers.Count - k);
                Globals.swap(aPlayers, k, r);
            }

            //// Shuffle B players
            for (int k = 0; k < bPlayers.Count; k++)
            {
                int r = k + random.Next() % (bPlayers.Count - k);
                Globals.swap(bPlayers, k, r);
            }

            //// Shuffle C players
            for (int k = 0; k < cPlayers.Count; k++)
            {
                int r = k + random.Next() % (cPlayers.Count - k);
                Globals.swap(cPlayers, k, r);
            }

            //// Shuffle D players
            for (int k = 0; k < dPlayers.Count; k++)
            {
                int r = k + random.Next() % (dPlayers.Count - k);
                Globals.swap(dPlayers, k, r);
            }

            //Assign each player to a team. The resulting teams list will look like this:
            // team1PlayerA
            // team1PlayerB
            // team1PlayerC
            // team1PlayerD
            // team2PlayerA
            // team2PlayerB
            // etc..
            for (int i = 0; i < aPlayers.Count; i++)
            {
                teams.Add(aPlayers[i]);
                teams.Add(bPlayers[i]);
                teams.Add(cPlayers[i]);
                teams.Add(dPlayers[i]);
            }

            //Write the teams to the database
            int nTeamName = 1;
            for (int i = 0; i < teams.Count; i++)
            {
                string strCmd = "INSERT INTO Teams (eventID, playerID, teamName, playerLevel)";
                strCmd = strCmd + "VALUES (" + eventid + ", " + teams[i].ID + ", " + nTeamName + ", '" + teams[i].level + "')";

                OleDbCommand command = new OleDbCommand(strCmd, Globals.g_dbConnection);

                if (command.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("ERROR: Could not generate teams due to an unspecified database error.");
                    return true;
                }
                if ((i + 1) % 4 == 0)
                    nTeamName++;
            }

            MessageBox.Show("Success! " + (nTeamName -1).ToString() + " teams for this event have been generated. Click the \'View Teams' button to view them.");
            return false;

        }
    }
}
