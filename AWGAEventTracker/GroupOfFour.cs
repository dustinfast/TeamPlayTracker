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

        public GroupOfFour()
        {
            playerA = null;
            playerB = null;
            playerC = null;
            playerD = null;
        }

        //Allows us to acces the playersA, playersB, etc, variables as a list.
        public Player getPlayerAt(int n)
        {
            if (n == 0)
                return playerA;
            else if (n == 1)
                return playerB;
            else if (n == 2)
                return playerC;
            else if (n == 3)
                return playerD;
            return null;
        }
    }

}
