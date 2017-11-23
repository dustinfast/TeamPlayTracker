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
    public partial class EnterScores : Form
    {
        public int nCurrRound { get; set; }
        public int nCurrGroup { get; set; }
        public int nNextRound { get; set; }
        public int nNextGroup { get; set; }


        public EnterScores()
        {
            InitializeComponent();
        }

        //called on form load
        private void EnterScores_Load(object sender, EventArgs e)
        {
            //populate curr and next
            labelCurrRound.Text = nCurrRound.ToString();
            labelCurrGroup.Text = nCurrGroup.ToString();
            textBoxNextRound.Text = nNextRound.ToString();
            textBoxNextGroup.Text = nNextGroup.ToString();
            
            //Get scores from db for current round/group and populate scores if exist

            //
        }
    }
}
