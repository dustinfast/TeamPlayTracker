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
    public partial class ManageEvents1 : Form
    {
        public ManageEvents1()
        {
            InitializeComponent();
        }

        // Called when the form loads.
        private void ManageEvents1_Load(object sender, EventArgs e)
        {
            //TODO: populate dropdown list with events from databse

        }

        //Called on Cancel btn click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //TODO: Check for unsaved changes and promnpt user "are you suer"
            this.Close();
        }

        // Called on OK btn click
        private void btnOk_Click(object sender, EventArgs e)
        {
            //TODO: save changes
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
