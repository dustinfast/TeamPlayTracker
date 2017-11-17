using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    public class Player //An abstraction of a player
    {
        
        public int ID { get; set; }
        public int handicap { get; set; }
        public string level { get; set; } //i.e. A, B, C, D
        public string displayName { get; } //lname + "," + fname
        public int teamName { get; set; }
        public List<int> lstConstraints; //The constraints on this players schedule. I.e. a list of player IDs this player has been scheduled to play against. Used during round generation.
        private string fName;
        private string lName;
        private string phone;

        
        public Player(int playerid, int playerhandicap, string fname, string lname, string playerphone, string playerlevel)
        {
            handicap = playerhandicap;
            ID = playerid;
            fName = fname;
            lName = lname;
            phone = playerphone;
            level = playerlevel;
            displayName = lName + ", " + fName;
            lstConstraints = new List<int>();
        }

        //returns true iff n is in lstConstraints
        public bool isConstrained(int n)
        {
            foreach (int m in lstConstraints)
                if (m == n)
                    return true;
            return false;
        }

        //sets a constraint, if it isn't already one. 
        public void setConstraint(int n)
        {
            if (!isConstrained(n))
                lstConstraints.Add(n);
        }

        //resets all constraints
        public void resetConstraints()
        {
            lstConstraints = new List<int>();
        }


        
    }
}
