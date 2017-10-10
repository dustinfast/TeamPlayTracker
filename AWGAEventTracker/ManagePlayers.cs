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
    public partial class ManagePlayers : Form
    {
        public ManagePlayers()
        {
            InitializeComponent();
        }

        //Called on form load
        private void ManagePlayers_Load(object sender, EventArgs e)
        {

        }
        //Called on user click Close button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
