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
        }


        
    }
}
