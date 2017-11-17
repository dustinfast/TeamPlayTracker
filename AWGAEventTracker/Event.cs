﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWGAEventTracker
{
    class Event
    {
        public int nID { get; set; } //The ID of the event
        public string strName { get; set; } //The name of the event
        public string strAssignedPlayers { get; set; } //A comma delimited list of all the players (by ID) assigned to the event. For convenience
        public int nRounds { get; set; } //The number of rounds for the event
        public List<Team> lstTeams { get; set; } //A list of teams (i.e. team assignments) the event
        public List<Round> lstRounds { get; set; }//A list of the rounds (i.e. the schedule) for the event
        public BindingList<Player> lstAssignedPlayers { get; set; } //All player objects assigned to currently selected event. Populated on Event select OR Assigned Player change
        public BindingList<Player> lstUnassignedPlayers { get; set; } //All player objects not assigned to currently selected event.
    }
}
