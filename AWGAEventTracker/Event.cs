using System;
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
        public int nAssignmentDepth { get; set; } //A convenience variable used during round assignment. Is the tree depth at which the last player was assigned.
        public List<Team> lstTeams { get; set; } //A list of teams (i.e. team assignments) the event
        public List<Round> lstRounds { get; set; }//A list of the rounds (i.e. the schedule) for the event. Note that each round contains a number of GroupOfFour objects.
        public BindingList<Player> lstAssignedPlayers { get; set; } //All player objects assigned to currently selected event. Populated on Event select OR Assigned Player change
        public BindingList<Player> lstUnassignedPlayers { get; set; } //All player objects not assigned to currently selected event.
        
        public Event()
        {
            nID = -1;
            strName = "";
            strAssignedPlayers = "";
            nRounds = -1;
            nAssignmentDepth = 0;
            lstTeams = new List<Team>();
            lstRounds = new List<Round>();
            lstAssignedPlayers = new BindingList<Player>();
            lstUnassignedPlayers = new BindingList<Player>();
        }   

        public Event(Event e)
        {
            nID = e.nID;
            strName = e.strName;
            strAssignedPlayers = e.strAssignedPlayers;
            nRounds = e.nRounds;
            nAssignmentDepth = e.nAssignmentDepth;
            lstTeams = e.lstTeams;
            lstRounds = e.lstRounds;
            lstAssignedPlayers = e.lstAssignedPlayers;
            lstUnassignedPlayers = e.lstUnassignedPlayers;
        }

        //Parses all the player objects of the given event and returns the one with the specified ID
        public Player getPlayerObjectByID(int id)
        {
            foreach (Player p in this.lstAssignedPlayers)
                if (p.ID == id)
                    return p;
            return null;
        }
    }
}
