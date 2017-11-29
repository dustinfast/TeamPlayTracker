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
        public int teamNumber { get; set; }
        public List<int> lstConstraintsStrong; //The strong constraints on this players schedule. I.e. a list of player IDs this player has been scheduled to play against. Used during round generation.
        public List<int> lstConstraintsWeak; //The weak constraints on this players schedule. I.e. a list of player IDs this player has been scheduled to play against. Same as lstConstraintsStrong but Weak constraints are only for A vs B and C vs D matchups.
        public string fName { get; set; }
        public string lName { get; set; }
        public string phone { get; set; }

        public Player(int playerid, int playerhandicap, string fname, string lname, string playerphone, string playerlevel)
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
