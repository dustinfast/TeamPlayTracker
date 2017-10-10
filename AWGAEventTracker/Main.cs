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
            Globals.g_dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=AWGA.mdb;Jet OLEDB:Database Password=AWGA");

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
            string dbCmd = "SELECT * FROM Event'";
            OleDbCommand dbComm = new OleDbCommand(dbCmd, Globals.g_dbConnection);

            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(dbComm);
            DataSet thisDataSet = new DataSet();
            try
            {
                thisAdapter.Fill(thisDataSet, "Events");
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error! Unable to read from database!\n\nError Info: " + ex1.ToString());
                if (Globals.g_dbConnection != null) Globals.g_dbConnection.Dispose(); //Close the db
                this.Close();
            }
        }

        //Called on user click File->Exit
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //close the application
        }

        //Called on user click File->Manage->Events
        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Displays the "Manage Event" dialog by instantiating a new ManageEvents1 (the form name for this class) object
            // and then calling it's ShowDialog function. 
            ManageEvents1 dlgManageEvents = new ManageEvents1();
            dlgManageEvents.ShowDialog();
        }
    }
}
