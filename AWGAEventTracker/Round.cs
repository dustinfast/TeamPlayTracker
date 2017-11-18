﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class Round
    {
        public int nRoundNumber { get; set; }
        public List<GroupOfFour> lstGroups;

        public Round(int roundnumber)
        {
            nRoundNumber = roundnumber;
            lstGroups = new List<GroupOfFour>();
        }

        public void addGroup(GroupOfFour g)
        {
            lstGroups.Add(g);
        }

        //Sets the GroupOfFour object at the nth position in the list.
        //public void setGroupAtIndex(int n, GroupOfFour g)
        //{
        //    lstGroups[n] = g;
        //}

        //public GroupOfFour getGroupAtIndex(int n)
        //{
        //    return lstGroups[n];
        //}

        public List<GroupOfFour> getGroupsList()
        {
            return lstGroups;
        }
    }
}
