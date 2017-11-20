using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class GroupOfFour
    {
        public Player playerA { get; set; }
        public Player playerB { get; set; }
        public Player playerC { get; set; }
        public Player playerD { get; set; }
        public int nGroupNumber { get; set; }

        public GroupOfFour(int groupnumber)
        {
            nGroupNumber = groupnumber;
            playerA = null;
            playerB = null;
            playerC = null;
            playerD = null;
        }
    }
}
