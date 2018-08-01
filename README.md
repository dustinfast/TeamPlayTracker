# An AI solution to Team-Play Tournament Tracking

A Windows based tool developed for the Alaska Women's Golf Association to facilitate management of "Team-Play" golf tournaments. Two key features of the application are the generation of balanced teams according to client-specified criteria, and the creation of unique player pairings across an arbitrary number of rounds of golf. These features were implemented with two common AI search-space algorithms, described below in *Notable Algorithms*.

## Installation and Usage

**User**  
To install this application, run the included `TeamPlayInstaller.msi` and follow the on-screen prompts.  
For help setting up and managing events, see `TeamPlayHowTo.pdf`.

**Developer**  
Open the included solution file `AWGAEventTracker.sln` in Microsoft Visual Studio 2015 or higher. Persistent data is stored in `AWGA.mdb` with the data store's clear text password appearing in `main.cs`.

## "Team-Play" Tournament Description

Each Team-Play tournament is a single event that takes place over a number of weeks, with each week constituting one round of play.
  
**Teams**  
Teams of four are generated from the pool of all players, and each player must be assigned a skill-level of A, B, C, or D, based on their individual golf handicap in relation to all other players in the pool (i.e. players with a handicap in the top 25th percentile are given a rating of A, players in the 50th to 75th percentile are assigned a rating of B, and so on). The four players per team then consist of one player from each skill level.  
Additionally, teams must be balanced to ensure a fair Team-Play experience. For example, if a team consists of only the top-ranked player (by handicap) at each level, that team has a competitive advantage over all other teams. This should not occur.

**Rounds**  
 For each round groups of four-players each are arranged, again one from each skill level. Players may not play with members of their team until the last round, when everyone plays with members of their own team. If possible, no player should play in the same group with another player more than once over the course of the event. If this is unavoidable, the duplicate pairings should only occur near the end of the event. Further, no A-player may ever play with the same B-player twice, and no C-player may ever play with the same D-player twice.

**Scoring**  
Scoring for Team-Play tournaments is calculated at both the team and individual level: 
 
Teams compete against each other team. At the end of the event, the team with the best score, aggregated across all rounds, wins.  

Individual players compete against each other player at their level (A-D). At the end of the tournament the player at each level with the best score, aggregated across all rounds, wins.
  
## Notable Algorithms

**Random Search**  
A Random Search algorithm evaluated with a statistical heuristic was implemented in `TeamAssignment.cs` for the purposes of generating balanced teams. "Balanced teams" may be defined as a configuration of player-to-team assignments giving the fairest (or nearly fairest, as Random Search does not guarantee an optimal solution) distribution of player-skill (by handicap) across teams.
  
To determine balanced teams, first recall that each team must consists of four slots, one for each skill level, A-D, as previously described. Now, consider the following set of players and two possible ways to distribute them amongst the three resulting teams of four -

Players (PLIST):
| Player  | Handicap  | Skill level |
| ------- |---------- |------------ |
| 1       | 2         | A           |
| 2       | 15        | A           |
| 3       | 20        | A           |
| 4       | 25        | B           |
| 5       | 39        | B           |
| 6       | 42        | B           |
| 7       | 43        | C           |
| 8       | 55        | C           |
| 9       | 65        | C           |
| 10      | 66        | D           |
| 11      | 70        | D           |
| 12      | 90        | D           |
Mean Player Handicap: 44.3

Possible Teams A (PTA):
| Team  | Players     | Mean Team Handicap |
| ----- | ----------- | ------------------ |
| Red   | 1, 4, 7, 10 | 34                 |
| Blu   | 2, 5, 8, 11 | 45                 |
| Grn   | 3, 6, 9, 12 | 54                 |

Possible Teams B (PTB):
| Team | Players     | Mean Team Handicap |
| ---- | ----------- | ------------------ |
| Red  | 1, 6, 7, 12 | 39                 |
| Blu  | 3, 4, 9, 10 | 44                 |
| Grn  | 2, 5, 8, 11 | 45                 |

Notice that in PTA, Red team contains the top-ranked player in each skill level while, Grn team contains the lowest-ranked player in each. Clearly, Red team has a competitive advantage over Grn Team. However, if teams are arranged instead according to PTB, the teams are fair - each team has a mix of the higher and lower ranked players. This "fairness" of each team can be quantified mathematically by examining the standard deviation of each team's mean handicap from the mean of all player handicaps. The smaller the standard deviation, the fairer the teams. In this way, the standard deviation is the heuristic used to evaluate the Random Search algorithm's results.

(Note: An argument can be made that a bottom-ranked "A" player is more valuable than a top-ranked "B" player. This algorithm does not consider that metric. However it may easily be accomplished by weighting the handicaps appropriately in our calculation, possibly according to the variance among handicaps at each level.)

To implement team generation in this way, the following Random Search algorithm was used:

1. Perform the following 1,000 times -
    1. Generate teams by randomly assigning each player to an approriate slot (A, B, C, or D) on a team.
    2. Compute the mean player handicap for each team.
    3. Using the sum of the values from step 2, compute the standard deviation of all teams from the mean of all player handicaps. The resulting value is our heuristic.
2. Select the player-to-team assignments resulting in the smallest standard deviation to be used as the official team assignments.

(Note: Hill climbing algorithms will alaways give a solution, but results may not be optimal. However, when used in this context for a reasonable number of players, an optimal solution is statistically likley.)

**Constraint Propagation**  
According to the Team-Play specification, no player should play in a group with the same player twice over the course of the event. If this cannot be avoided (due to the specific number of players and rounds) the duplicate pairings should occur at the end of the event (excepting the last round, where team members must always play with other team members). Further, no A-player may ever play with the same B-player twice, and no C-player may ever play with the same D-player twice.

To accomplish this, a recursive constraint propagation alogorithm with backtracking was implemented which operates by traversing the search space as a tree in a depth-first and post-order manner.  
The processing of a node in this search-tree represents the assignment of one player to a previously unpopulated group spot. When this occurs, that player is propagated as a constraint to every other player.  
Backtracking when any player is assigned to a group that violates the constraint, the algorithm continues until a goal state is reached (i.e. groups for all rounds have been populated and no constraint violations exist) or the search space is exhausted. In case of the latter, the constraint is lifted for the A/D and B/C player matchups and the algorithm resumes from it's last "best" state (i.e. the state with the most groups populated without constraint violations).

## Contributors

This application was designed and developed by Dustin Fast with contributions from Brooks Woods.
