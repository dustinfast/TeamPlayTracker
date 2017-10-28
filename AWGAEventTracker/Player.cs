using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    public class Player //An abstraction of a player
    {
        
        private int ID { get; set; }
        private int handicap { get; set; }
        private string fName { get; set; }
        private string lName { get; set; }
        private string phone { get; set; }
        private string level { get; set; } //i.e. A, B, C, D
        public string displayName
        {
            get
            {
                return lName + ", " + fName;
            }
        }
        public int playerID
        {
            get
            {
                return ID;
            }
        }

        public Player(int playerid, int playerhandicap, string fname, string lname, string playerphone, string playerlevel)
        {
            handicap = playerhandicap;
            ID = playerid;
            fName = fname;
            lName = lname;
            phone = playerphone;
            level = playerlevel;
        }


        
    }
}
