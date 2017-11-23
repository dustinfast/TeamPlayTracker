using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class GroupOfFour
    {
        public int nID { get; set;  }
        public Player playerA { get; set; }
        public Player playerB { get; set; }
        public Player playerC { get; set; }
        public Player playerD { get; set; }
        public int scoreA { get; set; }
        public int scoreB { get; set; }
        public int scoreC { get; set; }
        public int scoreD { get; set; }
        public int nGroupNumber { get; set; }

        public GroupOfFour(int groupnumber)
        {
            nID = -1;
            playerA = null;
            playerB = null;
            playerC = null;
            playerD = null;
            scoreA = -1;
            scoreB = -1;
            scoreC = -1;
            scoreD = -1;
            nGroupNumber = groupnumber;
        }
    }
}
