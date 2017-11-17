namespace AWGAEventTracker
{
    partial class ManageEvents1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageEvents1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateNewEvent = new System.Windows.Forms.Button();
            this.comboBoxEventSelector = new System.Windows.Forms.ComboBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.labelTeamCount = new System.Windows.Forms.Label();
            this.labelEventName = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelFinalResultsStatus = new System.Windows.Forms.Label();
            this.labelPlayerCount = new System.Windows.Forms.Label();
            this.labelRoundCount = new System.Windows.Forms.Label();
            this.labelRoundsWResultsCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonAssignAll = new System.Windows.Forms.Button();
            this.buttonUnassign = new System.Windows.Forms.Button();
            this.buttonAssign = new System.Windows.Forms.Button();
            this.listBoxAssignedPlayers = new System.Windows.Forms.ListBox();
            this.buttonShowAddPlayersDlg = new System.Windows.Forms.Button();
            this.listBoxUnassignedPlayers = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonGenerateTeams = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.buttonViewTeams = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.GeneratePairings = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select an event to manage:";
            // 
            // btnCreateNewEvent
            // 
            this.btnCreateNewEvent.Location = new System.Drawing.Point(233, 6);
            this.btnCreateNewEvent.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateNewEvent.Name = "btnCreateNewEvent";
            this.btnCreateNewEvent.Size = new System.Drawing.Size(96, 23);
            this.btnCreateNewEvent.TabIndex = 4;
            this.btnCreateNewEvent.Text = "Create New...";
            this.btnCreateNewEvent.UseVisualStyleBackColor = true;
            this.btnCreateNewEvent.Click += new System.EventHandler(this.btnCreateNewEvent_Click);
            // 
            // comboBoxEventSelector
            // 
            this.comboBoxEventSelector.FormattingEnabled = true;
            this.comboBoxEventSelector.Location = new System.Drawing.Point(146, 9);
            this.comboBoxEventSelector.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxEventSelector.Name = "comboBoxEventSelector";
            this.comboBoxEventSelector.Size = new System.Drawing.Size(80, 21);
            this.comboBoxEventSelector.TabIndex = 5;
            this.comboBoxEventSelector.SelectedIndexChanged += new System.EventHandler(this.comboBoxEventSelector_SelectedIndexChanged);
            this.comboBoxEventSelector.Enter += new System.EventHandler(this.comboBoxEventSelector_Enter);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Enabled = false;
            this.tabControl.Location = new System.Drawing.Point(8, 40);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(382, 358);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(374, 332);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Location = new System.Drawing.Point(4, 141);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(368, 189);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Guide";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(9, 22);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(343, 91);
            this.label21.TabIndex = 0;
            this.label21.Text = resources.GetString("label21.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelEndDate);
            this.groupBox1.Controls.Add(this.labelStartDate);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.labelTeamCount);
            this.groupBox1.Controls.Add(this.labelEventName);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.labelFinalResultsStatus);
            this.groupBox1.Controls.Add(this.labelPlayerCount);
            this.groupBox1.Controls.Add(this.labelRoundCount);
            this.groupBox1.Controls.Add(this.labelRoundsWResultsCount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(368, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Info";
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(78, 109);
            this.labelEndDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(27, 13);
            this.labelEndDate.TabIndex = 15;
            this.labelEndDate.Text = "N/A";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(78, 85);
            this.labelStartDate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(27, 13);
            this.labelStartDate.TabIndex = 14;
            this.labelStartDate.Text = "N/A";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(21, 109);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(55, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "End Date:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 85);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 13);
            this.label17.TabIndex = 12;
            this.label17.Text = "Start Date:";
            // 
            // labelTeamCount
            // 
            this.labelTeamCount.AutoSize = true;
            this.labelTeamCount.Location = new System.Drawing.Point(78, 63);
            this.labelTeamCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTeamCount.Name = "labelTeamCount";
            this.labelTeamCount.Size = new System.Drawing.Size(27, 13);
            this.labelTeamCount.TabIndex = 11;
            this.labelTeamCount.Text = "N/A";
            // 
            // labelEventName
            // 
            this.labelEventName.AutoSize = true;
            this.labelEventName.Location = new System.Drawing.Point(78, 22);
            this.labelEventName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEventName.Name = "labelEventName";
            this.labelEventName.Size = new System.Drawing.Size(27, 13);
            this.labelEventName.TabIndex = 10;
            this.labelEventName.Text = "N/A";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(136, 43);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Rounds With Results:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(35, 22);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Name:";
            // 
            // labelFinalResultsStatus
            // 
            this.labelFinalResultsStatus.AutoSize = true;
            this.labelFinalResultsStatus.Location = new System.Drawing.Point(248, 63);
            this.labelFinalResultsStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFinalResultsStatus.Name = "labelFinalResultsStatus";
            this.labelFinalResultsStatus.Size = new System.Drawing.Size(27, 13);
            this.labelFinalResultsStatus.TabIndex = 7;
            this.labelFinalResultsStatus.Text = "N/A";
            // 
            // labelPlayerCount
            // 
            this.labelPlayerCount.AutoSize = true;
            this.labelPlayerCount.Location = new System.Drawing.Point(78, 43);
            this.labelPlayerCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPlayerCount.Name = "labelPlayerCount";
            this.labelPlayerCount.Size = new System.Drawing.Size(27, 13);
            this.labelPlayerCount.TabIndex = 6;
            this.labelPlayerCount.Text = "N/A";
            // 
            // labelRoundCount
            // 
            this.labelRoundCount.AutoSize = true;
            this.labelRoundCount.Location = new System.Drawing.Point(248, 22);
            this.labelRoundCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRoundCount.Name = "labelRoundCount";
            this.labelRoundCount.Size = new System.Drawing.Size(27, 13);
            this.labelRoundCount.TabIndex = 5;
            this.labelRoundCount.Text = "N/A";
            // 
            // labelRoundsWResultsCount
            // 
            this.labelRoundsWResultsCount.AutoSize = true;
            this.labelRoundsWResultsCount.Location = new System.Drawing.Point(248, 43);
            this.labelRoundsWResultsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRoundsWResultsCount.Name = "labelRoundsWResultsCount";
            this.labelRoundsWResultsCount.Size = new System.Drawing.Size(27, 13);
            this.labelRoundsWResultsCount.TabIndex = 4;
            this.labelRoundsWResultsCount.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(174, 63);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Final Results:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total Rounds:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 43);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Teams:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonAssignAll);
            this.tabPage2.Controls.Add(this.buttonUnassign);
            this.tabPage2.Controls.Add(this.buttonAssign);
            this.tabPage2.Controls.Add(this.listBoxAssignedPlayers);
            this.tabPage2.Controls.Add(this.buttonShowAddPlayersDlg);
            this.tabPage2.Controls.Add(this.listBoxUnassignedPlayers);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(374, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Players";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonAssignAll
            // 
            this.buttonAssignAll.Location = new System.Drawing.Point(172, 171);
            this.buttonAssignAll.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAssignAll.Name = "buttonAssignAll";
            this.buttonAssignAll.Size = new System.Drawing.Size(28, 28);
            this.buttonAssignAll.TabIndex = 7;
            this.buttonAssignAll.Text = ">>";
            this.buttonAssignAll.UseVisualStyleBackColor = true;
            this.buttonAssignAll.Click += new System.EventHandler(this.buttonAssignAll_Click);
            // 
            // buttonUnassign
            // 
            this.buttonUnassign.Location = new System.Drawing.Point(172, 126);
            this.buttonUnassign.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUnassign.Name = "buttonUnassign";
            this.buttonUnassign.Size = new System.Drawing.Size(28, 28);
            this.buttonUnassign.TabIndex = 6;
            this.buttonUnassign.Text = "<";
            this.buttonUnassign.UseVisualStyleBackColor = true;
            this.buttonUnassign.Click += new System.EventHandler(this.buttonUnassign_Click);
            // 
            // buttonAssign
            // 
            this.buttonAssign.Location = new System.Drawing.Point(172, 96);
            this.buttonAssign.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAssign.Name = "buttonAssign";
            this.buttonAssign.Size = new System.Drawing.Size(28, 28);
            this.buttonAssign.TabIndex = 5;
            this.buttonAssign.Text = ">";
            this.buttonAssign.UseVisualStyleBackColor = true;
            this.buttonAssign.Click += new System.EventHandler(this.buttonAssign_Click);
            // 
            // listBoxAssignedPlayers
            // 
            this.listBoxAssignedPlayers.FormattingEnabled = true;
            this.listBoxAssignedPlayers.Location = new System.Drawing.Point(212, 28);
            this.listBoxAssignedPlayers.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxAssignedPlayers.Name = "listBoxAssignedPlayers";
            this.listBoxAssignedPlayers.Size = new System.Drawing.Size(162, 264);
            this.listBoxAssignedPlayers.TabIndex = 4;
            // 
            // buttonShowAddPlayersDlg
            // 
            this.buttonShowAddPlayersDlg.Location = new System.Drawing.Point(6, 297);
            this.buttonShowAddPlayersDlg.Margin = new System.Windows.Forms.Padding(2);
            this.buttonShowAddPlayersDlg.Name = "buttonShowAddPlayersDlg";
            this.buttonShowAddPlayersDlg.Size = new System.Drawing.Size(127, 29);
            this.buttonShowAddPlayersDlg.TabIndex = 3;
            this.buttonShowAddPlayersDlg.Text = "Add/Modify Players";
            this.buttonShowAddPlayersDlg.UseVisualStyleBackColor = true;
            this.buttonShowAddPlayersDlg.Click += new System.EventHandler(this.buttonShowAddPlayersDlg_Click);
            // 
            // listBoxUnassignedPlayers
            // 
            this.listBoxUnassignedPlayers.FormattingEnabled = true;
            this.listBoxUnassignedPlayers.Location = new System.Drawing.Point(4, 28);
            this.listBoxUnassignedPlayers.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxUnassignedPlayers.Name = "listBoxUnassignedPlayers";
            this.listBoxUnassignedPlayers.Size = new System.Drawing.Size(162, 264);
            this.listBoxUnassignedPlayers.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(212, 13);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Players in Event:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Players not in event:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage5.Size = new System.Drawing.Size(374, 332);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Teams";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonGenerateTeams);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(368, 204);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Generate Teams";
            // 
            // buttonGenerateTeams
            // 
            this.buttonGenerateTeams.Location = new System.Drawing.Point(80, 145);
            this.buttonGenerateTeams.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGenerateTeams.Name = "buttonGenerateTeams";
            this.buttonGenerateTeams.Size = new System.Drawing.Size(189, 45);
            this.buttonGenerateTeams.TabIndex = 1;
            this.buttonGenerateTeams.Text = "Generate Teams";
            this.buttonGenerateTeams.UseVisualStyleBackColor = true;
            this.buttonGenerateTeams.Click += new System.EventHandler(this.buttonGenerateTeams_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 27);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(351, 104);
            this.label10.TabIndex = 0;
            this.label10.Text = resources.GetString("label10.Text");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.buttonViewTeams);
            this.groupBox3.Location = new System.Drawing.Point(3, 211);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(368, 119);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View Teams";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(336, 26);
            this.label11.TabIndex = 1;
            this.label11.Text = "Click \"View Teams\" to display teams by player level. This button is not\r\navailabl" +
    "e if teams have not yet been generated (above).";
            // 
            // buttonViewTeams
            // 
            this.buttonViewTeams.Location = new System.Drawing.Point(80, 61);
            this.buttonViewTeams.Margin = new System.Windows.Forms.Padding(2);
            this.buttonViewTeams.Name = "buttonViewTeams";
            this.buttonViewTeams.Size = new System.Drawing.Size(189, 45);
            this.buttonViewTeams.TabIndex = 0;
            this.buttonViewTeams.Text = "View Teams";
            this.buttonViewTeams.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.GeneratePairings);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(374, 332);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rounds";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // GeneratePairings
            // 
            this.GeneratePairings.Location = new System.Drawing.Point(74, 215);
            this.GeneratePairings.Name = "GeneratePairings";
            this.GeneratePairings.Size = new System.Drawing.Size(215, 64);
            this.GeneratePairings.TabIndex = 2;
            this.GeneratePairings.Text = "Generate Rounds";
            this.GeneratePairings.UseVisualStyleBackColor = true;
            this.GeneratePairings.Click += new System.EventHandler(this.GeneratePairings_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(374, 332);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Results";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 39);
            this.label5.TabIndex = 1;
            this.label5.Text = "This is where the event results will be displayed.\r\nIt will be disabled until poi" +
    "nts for all the rounds \r\nhave been entered.\r\n";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(318, 408);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(69, 34);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ManageEvents1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 448);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBoxEventSelector);
            this.Controls.Add(this.btnCreateNewEvent);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ManageEvents1";
            this.Text = "Team-Play Event Management";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateNewEvent;
        private System.Windows.Forms.ComboBox comboBoxEventSelector;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelFinalResultsStatus;
        private System.Windows.Forms.Label labelPlayerCount;
        private System.Windows.Forms.Label labelRoundCount;
        private System.Windows.Forms.Label labelRoundsWResultsCount;
        private System.Windows.Forms.Label labelEventName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelTeamCount;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button buttonUnassign;
        private System.Windows.Forms.Button buttonAssign;
        private System.Windows.Forms.ListBox listBoxAssignedPlayers;
        private System.Windows.Forms.Button buttonShowAddPlayersDlg;
        private System.Windows.Forms.ListBox listBoxUnassignedPlayers;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAssignAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonGenerateTeams;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonViewTeams;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button GeneratePairings;
    }
}