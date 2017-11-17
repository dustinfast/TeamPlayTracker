using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class Team
    {
        public int nTeamNumber { get; set; }
        public Player playerA { get; set; }
        public Player playerB { get; set; }
        public Player playerC { get; set; }
        public Player playerD { get; set; }

        public Team()
        {
            playerA = null;
            playerB = null;
            playerC = null;
            playerD = null;
        }
        public Team(Player aplayer, Player bplayer, Player cplayer, Player dplayer)
        {
            playerA = aplayer;
            playerB = bplayer;
            playerC = cplayer;
            playerD = dplayer;
        }

    }
}
