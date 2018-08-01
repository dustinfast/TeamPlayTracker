/// Globals.cs - Global application variables and functions.
///
/// Dustin Fast, 2017

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
        //DB connection object
        public static OleDbConnection g_dbConnection; 

        //Strips the ticks out of a string - useful for sterilizing user-input
        //insterted into SQL statements.
        public static string removeTicksFromString(string strIn)
        {
            strIn.Replace("'", "");
            return strIn;
        }

        //Swaps two items in a list. I.e. the two items changing locations in the list.
        public static void swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
