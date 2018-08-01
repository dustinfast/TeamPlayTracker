/// Main.cs - the main application class for the Team-Play Tournament Tracker.
///
/// Designed/Developed by Dustin Fast (dustin.fast@outlook.com)
/// with contributions from Brooks Woods (woodsak49@hotmail.com), 2017.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace AWGAEventTracker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //Called when form loads 
        private void Form1_Load(object sender, EventArgs e)
        {
            //Open the database
            Globals.g_dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=AWGA.mdb;Jet OLEDB:Database Password=@Wg@!2017"); //AWGA.dat is a .mdb file

            try
            {
                Globals.g_dbConnection.Open();
            }
            catch (Exception ex0)
            {
                if (Globals.g_dbConnection != null) Globals.g_dbConnection.Dispose(); //Close db if it was opened
                MessageBox.Show("Error! Unable to connect to database!\n\nError Info: " + ex0.ToString());
                this.Close();
            }

            //Do a test SELECT from the DB, to ensure we can read from it.
            string dbCmd = "SELECT * FROM Events'";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter adapter = new OleDbDataAdapter(dbComm);
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "Events");
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error! Unable to read from database!\n\nError Info: " + ex1.ToString());
                if (Globals.g_dbConnection != null) Globals.g_dbConnection.Dispose(); //Close the db
                this.Close();
            }
        }

        //Called when the form closes
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close the db
            if (Globals.g_dbConnection != null) Globals.g_dbConnection.Dispose();

            //Delete temporary files (i.e. files in the TemporaryFiles directory)
            try
            {
                string[] fileList = Directory.GetFiles("TemporaryFiles", "*.*");

                foreach (string file in fileList)
                {
                    File.Delete(file);
                }
                Directory.Delete("TemporaryFiles");
            }
            catch { }

        }

        //Called on user click File->Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //close the application (calls Form1_FormClosing first)
        }

        //Called on user click Manage->Events
        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Displays the "Manage Event" dialog by instantiating a new ManageEvents1 
            // object and then calling it's ShowDialog function. 
            ManageEvents1 dlgManageEvents = new ManageEvents1();
            dlgManageEvents.ShowDialog();
        }

        //Called on user click Manage->Players
        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManagePlayers dlgManagePlayers = new ManagePlayers();
            dlgManagePlayers.ShowDialog();
        }
    }
}
