/// Player.cs - The Player class: An abstration of a Team-Play tournament player.
///
/// Dustin Fast, 2017

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWGAEventTracker
{
    class Player 
    {
        public int ID { get; set; }
        public double handicap { get; set; }
        public string level { get; set; } //i.e. A, B, C, D
        public string displayName { get; } //lname + "," + fname
        public int teamNumber { get; set; }
        public List<int> lstConstraintsStrong; //The strong constraints on this players schedule. I.e. a list of player IDs this player has been scheduled to play against. Used during round generation.
        public List<int> lstConstraintsWeak; //The weak constraints on this players schedule. I.e. a list of player IDs this player has been scheduled to play against. Same as lstConstraintsStrong but Weak constraints are only for A vs B and C vs D matchups.
        public string fName { get; set; }
        public string lName { get; set; }
        public string phone { get; set; }

        public Player(int playerid, double playerhandicap, string fname, string lname, string playerphone, string playerlevel)
        {
            handicap = playerhandicap;
            ID = playerid;
            fName = fname;
            lName = lname;
            phone = playerphone;
            level = playerlevel;
            displayName = lName + ", " + fName;
            lstConstraintsStrong = new List<int>();
            lstConstraintsWeak = new List<int>();
        }

        //returns true iff a player has a substitution attached to any of their scores for the given event
        public bool hadSubbedRound(Event e)
        {
            string dbCmd = "SELECT COUNT (playerID) FROM Scores WHERE ";
            dbCmd += "playerID = " + this.ID.ToString() + " AND ";
            dbCmd += "isSubstitution = True AND ";
            dbCmd += "eventID = " + e.nID.ToString();
            OleDbCommand command = new OleDbCommand(dbCmd, Globals.g_dbConnection);
            try
            {
                if ((int)command.ExecuteScalar() == 0)
                    return false;
            }
            catch (Exception ex0)
            {
                MessageBox.Show(ex0.Message);
            }

            return true;
        }

        //returns true iff n (a player id) is in lstConstraints
        public bool isConstrainedStrong(int n)
        {
            foreach (int m in lstConstraintsStrong)
                if (m == n)
                    return true;
            return false;
        }

        //sets n (a player ID) as a constraint, if it isn't in the list
        public void setConstraintStrong(int n)
        {
            if (!isConstrainedStrong(n))
                lstConstraintsStrong.Add(n);
        }

        //returns true iff n (a player id) is in lstConstraints
        public bool isConstrainedWeak(int n)
        {
            foreach (int m in lstConstraintsWeak)
                if (m == n)
                    return true;
            return false;
        }

        //sets n (a player ID) as a constraint, if it isn't in the list
        public void setConstraintWeak(int n)
        {
            if (!isConstrainedWeak(n))
                lstConstraintsWeak.Add(n);
        }
    }
}
