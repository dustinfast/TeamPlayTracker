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

        private void ManageEvents1_Load(object sender, EventArgs e)
        {
            //TODO: populate dropdown list with events from databse

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
