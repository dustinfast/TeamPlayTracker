using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AWGAEventTracker
{
    class Globals
    {
        //We will put things here that are global to the application, such as variables and utiliity functions.

        //DB connection object
        public static OleDbConnection g_dbConnection; 

        //A handy function we'll need to strip the tick mark out of user entries that go inside SQL statements
        // such as when a user enteres a new player name or points. The tick mark would break the insert statement, so we
        // remove it by calling this function on the user input - DF
        public static string removeTicksFromString(string strIn)
        {
            strIn.Replace("'", "");
            return strIn;
        }
    }
}
