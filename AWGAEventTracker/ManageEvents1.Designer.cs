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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxSelectedEventID = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select an event to manage:";
            // 
            // btnCreateNewEvent
            // 
            this.btnCreateNewEvent.Location = new System.Drawing.Point(466, 12);
            this.btnCreateNewEvent.Name = "btnCreateNewEvent";
            this.btnCreateNewEvent.Size = new System.Drawing.Size(192, 45);
            this.btnCreateNewEvent.TabIndex = 4;
            this.btnCreateNewEvent.Text = "Create New...";
            this.btnCreateNewEvent.UseVisualStyleBackColor = true;
            this.btnCreateNewEvent.Click += new System.EventHandler(this.btnCreateNewEvent_Click);
            // 
            // comboBoxEventSelector
            // 
            this.comboBoxEventSelector.FormattingEnabled = true;
            this.comboBoxEventSelector.Location = new System.Drawing.Point(292, 18);
            this.comboBoxEventSelector.Name = "comboBoxEventSelector";
            this.comboBoxEventSelector.Size = new System.Drawing.Size(157, 33);
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
            this.tabControl.Location = new System.Drawing.Point(17, 77);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(765, 688);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(749, 641);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Location = new System.Drawing.Point(7, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(736, 363);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Guide";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 42);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(685, 175);
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
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(736, 259);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Event Info";
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(155, 209);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(47, 25);
            this.labelEndDate.TabIndex = 15;
            this.labelEndDate.Text = "N/A";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(155, 164);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(47, 25);
            this.labelStartDate.TabIndex = 14;
            this.labelStartDate.Text = "N/A";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(42, 209);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 25);
            this.label18.TabIndex = 13;
            this.label18.Text = "End Date:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(35, 164);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(114, 25);
            this.label17.TabIndex = 12;
            this.label17.Text = "Start Date:";
            // 
            // labelTeamCount
            // 
            this.labelTeamCount.AutoSize = true;
            this.labelTeamCount.Location = new System.Drawing.Point(155, 121);
            this.labelTeamCount.Name = "labelTeamCount";
            this.labelTeamCount.Size = new System.Drawing.Size(47, 25);
            this.labelTeamCount.TabIndex = 11;
            this.labelTeamCount.Text = "N/A";
            // 
            // labelEventName
            // 
            this.labelEventName.AutoSize = true;
            this.labelEventName.Location = new System.Drawing.Point(155, 43);
            this.labelEventName.Name = "labelEventName";
            this.labelEventName.Size = new System.Drawing.Size(47, 25);
            this.labelEventName.TabIndex = 10;
            this.labelEventName.Text = "N/A";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(271, 82);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(219, 25);
            this.label14.TabIndex = 9;
            this.label14.Text = "Rounds With Results:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(70, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 25);
            this.label13.TabIndex = 8;
            this.label13.Text = "Name:";
            // 
            // labelFinalResultsStatus
            // 
            this.labelFinalResultsStatus.AutoSize = true;
            this.labelFinalResultsStatus.Location = new System.Drawing.Point(496, 121);
            this.labelFinalResultsStatus.Name = "labelFinalResultsStatus";
            this.labelFinalResultsStatus.Size = new System.Drawing.Size(47, 25);
            this.labelFinalResultsStatus.TabIndex = 7;
            this.labelFinalResultsStatus.Text = "N/A";
            // 
            // labelPlayerCount
            // 
            this.labelPlayerCount.AutoSize = true;
            this.labelPlayerCount.Location = new System.Drawing.Point(155, 82);
            this.labelPlayerCount.Name = "labelPlayerCount";
            this.labelPlayerCount.Size = new System.Drawing.Size(47, 25);
            this.labelPlayerCount.TabIndex = 6;
            this.labelPlayerCount.Text = "N/A";
            // 
            // labelRoundCount
            // 
            this.labelRoundCount.AutoSize = true;
            this.labelRoundCount.Location = new System.Drawing.Point(496, 43);
            this.labelRoundCount.Name = "labelRoundCount";
            this.labelRoundCount.Size = new System.Drawing.Size(47, 25);
            this.labelRoundCount.TabIndex = 5;
            this.labelRoundCount.Text = "N/A";
            // 
            // labelRoundsWResultsCount
            // 
            this.labelRoundsWResultsCount.AutoSize = true;
            this.labelRoundsWResultsCount.Location = new System.Drawing.Point(496, 82);
            this.labelRoundsWResultsCount.Name = "labelRoundsWResultsCount";
            this.labelRoundsWResultsCount.Size = new System.Drawing.Size(47, 25);
            this.labelRoundsWResultsCount.TabIndex = 4;
            this.labelRoundsWResultsCount.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(347, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 25);
            this.label8.TabIndex = 3;
            this.label8.Text = "Final Results:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(344, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 25);
            this.label7.TabIndex = 2;
            this.label7.Text = "Total Rounds:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 25);
            this.label6.TabIndex = 1;
            this.label6.Text = "Players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 25);
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
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(749, 641);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Players";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonAssignAll
            // 
            this.buttonAssignAll.Location = new System.Drawing.Point(345, 329);
            this.buttonAssignAll.Name = "buttonAssignAll";
            this.buttonAssignAll.Size = new System.Drawing.Size(55, 53);
            this.buttonAssignAll.TabIndex = 7;
            this.buttonAssignAll.Text = ">>";
            this.buttonAssignAll.UseVisualStyleBackColor = true;
            this.buttonAssignAll.Click += new System.EventHandler(this.buttonAssignAll_Click);
            // 
            // buttonUnassign
            // 
            this.buttonUnassign.Location = new System.Drawing.Point(345, 243);
            this.buttonUnassign.Name = "buttonUnassign";
            this.buttonUnassign.Size = new System.Drawing.Size(55, 53);
            this.buttonUnassign.TabIndex = 6;
            this.buttonUnassign.Text = "<";
            this.buttonUnassign.UseVisualStyleBackColor = true;
            this.buttonUnassign.Click += new System.EventHandler(this.buttonUnassign_Click);
            // 
            // buttonAssign
            // 
            this.buttonAssign.Location = new System.Drawing.Point(345, 184);
            this.buttonAssign.Name = "buttonAssign";
            this.buttonAssign.Size = new System.Drawing.Size(55, 53);
            this.buttonAssign.TabIndex = 5;
            this.buttonAssign.Text = ">";
            this.buttonAssign.UseVisualStyleBackColor = true;
            this.buttonAssign.Click += new System.EventHandler(this.buttonAssign_Click);
            // 
            // listBoxAssignedPlayers
            // 
            this.listBoxAssignedPlayers.FormattingEnabled = true;
            this.listBoxAssignedPlayers.ItemHeight = 25;
            this.listBoxAssignedPlayers.Location = new System.Drawing.Point(424, 53);
            this.listBoxAssignedPlayers.Name = "listBoxAssignedPlayers";
            this.listBoxAssignedPlayers.Size = new System.Drawing.Size(319, 504);
            this.listBoxAssignedPlayers.TabIndex = 4;
            // 
            // buttonShowAddPlayersDlg
            // 
            this.buttonShowAddPlayersDlg.Location = new System.Drawing.Point(11, 571);
            this.buttonShowAddPlayersDlg.Name = "buttonShowAddPlayersDlg";
            this.buttonShowAddPlayersDlg.Size = new System.Drawing.Size(254, 55);
            this.buttonShowAddPlayersDlg.TabIndex = 3;
            this.buttonShowAddPlayersDlg.Text = "Add/Modify Players";
            this.buttonShowAddPlayersDlg.UseVisualStyleBackColor = true;
            this.buttonShowAddPlayersDlg.Click += new System.EventHandler(this.buttonShowAddPlayersDlg_Click);
            // 
            // listBoxUnassignedPlayers
            // 
            this.listBoxUnassignedPlayers.FormattingEnabled = true;
            this.listBoxUnassignedPlayers.ItemHeight = 25;
            this.listBoxUnassignedPlayers.Location = new System.Drawing.Point(7, 53);
            this.listBoxUnassignedPlayers.Name = "listBoxUnassignedPlayers";
            this.listBoxUnassignedPlayers.Size = new System.Drawing.Size(319, 504);
            this.listBoxUnassignedPlayers.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(424, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "Players in Event:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "Players not in event:";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox4);
            this.tabPage5.Controls.Add(this.groupBox3);
            this.tabPage5.Location = new System.Drawing.Point(8, 39);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(749, 641);
            this.tabPage5.TabIndex = 3;
            this.tabPage5.Text = "Teams";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(749, 641);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rounds";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(559, 50);
            this.label4.TabIndex = 1;
            this.label4.Text = "Here we will display event details, such as the number\r\nof players/teams, and if " +
    "the rounds have points assigned.";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Location = new System.Drawing.Point(8, 39);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(749, 641);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Results";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(474, 75);
            this.label5.TabIndex = 1;
            this.label5.Text = "This is where the event results will be displayed.\r\nIt will be disabled until poi" +
    "nts for all the rounds \r\nhave been entered.\r\n";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(636, 784);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(138, 65);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(45, 801);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(288, 25);
            this.label22.TabIndex = 8;
            this.label22.Text = "Selected Event ID (HIDDEN):";
            this.label22.Visible = false;
            // 
            // textBoxSelectedEventID
            // 
            this.textBoxSelectedEventID.Location = new System.Drawing.Point(330, 798);
            this.textBoxSelectedEventID.Name = "textBoxSelectedEventID";
            this.textBoxSelectedEventID.Size = new System.Drawing.Size(100, 31);
            this.textBoxSelectedEventID.TabIndex = 9;
            this.textBoxSelectedEventID.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(624, 25);
            this.label10.TabIndex = 0;
            this.label10.Text = "(Intro Text): Generate teams. This will also lock the Players Tab.";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 149);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(284, 82);
            this.button1.TabIndex = 1;
            this.button1.Text = "Generate Teams";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Location = new System.Drawing.Point(6, 454);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(737, 181);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View Teams";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(737, 442);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Generate Teams";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(159, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(378, 87);
            this.button2.TabIndex = 0;
            this.button2.Text = "View Teams (opens spreadsheet)";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // ManageEvents1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 861);
            this.Controls.Add(this.textBoxSelectedEventID);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.comboBoxEventSelector);
            this.Controls.Add(this.btnCreateNewEvent);
            this.Controls.Add(this.label1);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxSelectedEventID;
        private System.Windows.Forms.Button buttonUnassign;
        private System.Windows.Forms.Button buttonAssign;
        private System.Windows.Forms.ListBox listBoxAssignedPlayers;
        private System.Windows.Forms.Button buttonShowAddPlayersDlg;
        private System.Windows.Forms.ListBox listBoxUnassignedPlayers;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAssignAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button2;
    }
}