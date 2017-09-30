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
            //TODO: ini database here -DF  
           
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
