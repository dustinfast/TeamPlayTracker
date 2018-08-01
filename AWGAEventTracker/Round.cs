/// Round.cs - The Round class: An abstration of a Team-Play round.
///
/// Dustin Fast, 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class Round
    {
        public int nID { get; set;  }
        public int nRoundNumber { get; set; }
        public string strRoundName { get; set; }
        public List<GroupOfFour> lstGroups; // A list of all playgroups for this round

        public Round(int roundnumber)
        {
            nRoundNumber = roundnumber;
            strRoundName = "";
            lstGroups = new List<GroupOfFour>();
        }

        public Round(int roundnumber, string roundname)
        {
            nRoundNumber = roundnumber;
            strRoundName = roundname;
            lstGroups = new List<GroupOfFour>();
        }

        public void addGroup(GroupOfFour g)
        {
            lstGroups.Add(g);
        }

        public List<GroupOfFour> getGroupsList()
        {
            return lstGroups;
        }
    }
}
