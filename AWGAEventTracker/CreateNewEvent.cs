using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AWGAEventTracker
{
    public partial class CreateNewEvent : Form
    {
        public CreateNewEvent()
        {
            InitializeComponent();
        }

        // Called on btn cancel click
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Called on btn OK click
        private void btnOk_Click(object sender, EventArgs e)
        {
            //get user data from textbox
            string strInput = textBoxEventYear.Text;
            MessageBox.Show("You entered " + strInput);

            //TODO: check database to ensure this event doesn't already exist

            //TODO: Write new event to DB.
        }
    }
}
