namespace AWGAEventTracker
{
    partial class ManagePlayers
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxAddPhone = new System.Windows.Forms.MaskedTextBox();
            this.btnAddNewPlayer = new System.Windows.Forms.Button();
            this.textBoxAddHandicap = new System.Windows.Forms.TextBox();
            this.textBoxAddLN = new System.Windows.Forms.TextBox();
            this.textBoxAddFN = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxEditPhone = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.radioButtonSortLN = new System.Windows.Forms.RadioButton();
            this.radioButtonSortFN = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxEditID = new System.Windows.Forms.TextBox();
            this.buttonModifyPlayer = new System.Windows.Forms.Button();
            this.buttonDeletePlayer = new System.Windows.Forms.Button();
            this.textBoxEditHandicap = new System.Windows.Forms.TextBox();
            this.textBoxEditLN = new System.Windows.Forms.TextBox();
            this.textBoxEditFN = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonClose = new System.Windows.Forms.Button();
            this.toolTipPhoneNum = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAddPhone);
            this.groupBox1.Controls.Add(this.btnAddNewPlayer);
            this.groupBox1.Controls.Add(this.textBoxAddHandicap);
            this.groupBox1.Controls.Add(this.textBoxAddLN);
            this.groupBox1.Controls.Add(this.textBoxAddFN);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(866, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Player:";
            // 
            // textBoxAddPhone
            // 
            this.textBoxAddPhone.BeepOnError = true;
            this.textBoxAddPhone.Location = new System.Drawing.Point(997, 46);
            this.textBoxAddPhone.Mask = "(999) 000-0000";
            this.textBoxAddPhone.Name = "textBoxAddPhone";
            this.textBoxAddPhone.Size = new System.Drawing.Size(151, 31);
            this.textBoxAddPhone.TabIndex = 8;
            this.textBoxAddPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            this.textBoxAddPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.textBoxAddPhone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.textBoxAddPhone_MaskInputRejected);
            // 
            // btnAddNewPlayer
            // 
            this.btnAddNewPlayer.Location = new System.Drawing.Point(758, 78);
            this.btnAddNewPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddNewPlayer.Name = "btnAddNewPlayer";
            this.btnAddNewPlayer.Size = new System.Drawing.Size(95, 39);
            this.btnAddNewPlayer.TabIndex = 0;
            this.btnAddNewPlayer.Text = "Add";
            this.btnAddNewPlayer.UseVisualStyleBackColor = true;
            this.btnAddNewPlayer.Click += new System.EventHandler(this.btnAddNewPlayer_Click);
            // 
            // textBoxAddHandicap
            // 

            this.textBoxAddHandicap.Location = new System.Drawing.Point(741, 46);
            this.textBoxAddHandicap.Name = "textBoxAddHandicap";
            this.textBoxAddHandicap.Size = new System.Drawing.Size(151, 31);
            this.textBoxAddHandicap.TabIndex = 7;
            this.textBoxAddHandicap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);

            // textBoxAddLN
            // 
            this.textBoxAddLN.Location = new System.Drawing.Point(340, 37);
            this.textBoxAddLN.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAddLN.Name = "textBoxAddLN";
            this.textBoxAddLN.Size = new System.Drawing.Size(114, 26);
            this.textBoxAddLN.TabIndex = 6;
            this.textBoxAddLN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // textBoxAddFN
            // 
            this.textBoxAddFN.Location = new System.Drawing.Point(116, 37);
            this.textBoxAddFN.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAddFN.Name = "textBoxAddFN";
            this.textBoxAddFN.Size = new System.Drawing.Size(114, 26);
            this.textBoxAddFN.TabIndex = 5;
            this.textBoxAddFN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(464, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Location = new System.Drawing.Point(911, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Phone:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(654, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Location = new System.Drawing.Point(626, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Handicap:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "First Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxEditPhone);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.radioButtonSortLN);
            this.groupBox2.Controls.Add(this.radioButtonSortFN);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.textBoxEditID);
            this.groupBox2.Controls.Add(this.buttonModifyPlayer);
            this.groupBox2.Controls.Add(this.buttonDeletePlayer);
            this.groupBox2.Controls.Add(this.textBoxEditHandicap);
            this.groupBox2.Controls.Add(this.textBoxEditLN);
            this.groupBox2.Controls.Add(this.textBoxEditFN);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Location = new System.Drawing.Point(10, 145);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(865, 564);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modify/Remove Players:";
            // 
            // textBoxEditPhone
            // 
            this.textBoxEditPhone.Enabled = false;
            this.textBoxEditPhone.Location = new System.Drawing.Point(528, 471);
            this.textBoxEditPhone.Mask = "(999) 000-0000";
            this.textBoxEditPhone.Name = "textBoxEditPhone";
            this.textBoxEditPhone.Size = new System.Drawing.Size(120, 26);
            this.textBoxEditPhone.TabIndex = 25;
            this.textBoxEditPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.textBoxEditPhone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.textBoxEditPhone_MaskInputRejected);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(19, 504);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 20);
            this.label10.TabIndex = 24;
            this.label10.Text = "ID (Hidden):";
            this.label10.Visible = false;
            // 
            // radioButtonSortLN
            // 
            this.radioButtonSortLN.AutoSize = true;
            this.radioButtonSortLN.Checked = true;
            this.radioButtonSortLN.Location = new System.Drawing.Point(740, 22);
            this.radioButtonSortLN.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonSortLN.Name = "radioButtonSortLN";
            this.radioButtonSortLN.Size = new System.Drawing.Size(111, 24);
            this.radioButtonSortLN.TabIndex = 23;
            this.radioButtonSortLN.TabStop = true;
            this.radioButtonSortLN.Text = "Last Name";
            this.radioButtonSortLN.UseVisualStyleBackColor = true;
            this.radioButtonSortLN.CheckedChanged += new System.EventHandler(this.radioButtonSortLN_CheckedChanged);
            // 
            // radioButtonSortFN
            // 
            this.radioButtonSortFN.AutoSize = true;
            this.radioButtonSortFN.Location = new System.Drawing.Point(622, 22);
            this.radioButtonSortFN.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonSortFN.Name = "radioButtonSortFN";
            this.radioButtonSortFN.Size = new System.Drawing.Size(111, 24);
            this.radioButtonSortFN.TabIndex = 22;
            this.radioButtonSortFN.TabStop = true;
            this.radioButtonSortFN.Text = "First Name";
            this.radioButtonSortFN.UseVisualStyleBackColor = true;
            this.radioButtonSortFN.CheckedChanged += new System.EventHandler(this.radioButtoSortFN1_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(551, 24);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "Sort By:";
            // 
            // textBoxEditID
            // 
            this.textBoxEditID.Location = new System.Drawing.Point(115, 502);
            this.textBoxEditID.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEditID.Name = "textBoxEditID";
            this.textBoxEditID.Size = new System.Drawing.Size(114, 26);
            this.textBoxEditID.TabIndex = 20;
            this.textBoxEditID.Visible = false;
            // 
            // buttonModifyPlayer
            // 
            this.buttonModifyPlayer.Enabled = false;
            this.buttonModifyPlayer.Location = new System.Drawing.Point(722, 514);
            this.buttonModifyPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonModifyPlayer.Name = "buttonModifyPlayer";
            this.buttonModifyPlayer.Size = new System.Drawing.Size(131, 39);
            this.buttonModifyPlayer.TabIndex = 19;
            this.buttonModifyPlayer.Text = "Modify Player";
            this.buttonModifyPlayer.UseVisualStyleBackColor = true;
            this.buttonModifyPlayer.Click += new System.EventHandler(this.buttonModifyPlayer_Click);
            // 
            // buttonDeletePlayer
            // 
            this.buttonDeletePlayer.Enabled = false;
            this.buttonDeletePlayer.Location = new System.Drawing.Point(580, 514);
            this.buttonDeletePlayer.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeletePlayer.Name = "buttonDeletePlayer";
            this.buttonDeletePlayer.Size = new System.Drawing.Size(137, 39);
            this.buttonDeletePlayer.TabIndex = 18;
            this.buttonDeletePlayer.Text = "Delete Player";
            this.buttonDeletePlayer.UseVisualStyleBackColor = true;
            this.buttonDeletePlayer.Click += new System.EventHandler(this.buttonDeletePlayer_Click);
            // 
            // textBoxEditHandicap
            // 
            this.textBoxEditHandicap.Enabled = false;
            this.textBoxEditHandicap.Location = new System.Drawing.Point(740, 590);
            this.textBoxEditHandicap.Name = "textBoxEditHandicap";
            this.textBoxEditHandicap.Size = new System.Drawing.Size(151, 31);
            this.textBoxEditHandicap.TabIndex = 16;
            // 
            // textBoxEditPhone
            // 
            this.textBoxEditPhone.Enabled = false;
            this.textBoxEditPhone.Location = new System.Drawing.Point(996, 590);
            this.textBoxEditPhone.Name = "textBoxEditPhone";
            this.textBoxEditPhone.Size = new System.Drawing.Size(151, 31);
            this.textBoxEditPhone.TabIndex = 17;
            this.textBoxEditPhone.BeepOnError = true;
            this.textBoxEditPhone.Mask = "(999) 000-0000";
            this.textBoxEditPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.textBoxEditPhone.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.textBoxEditPhone_MaskInputRejected);
            // 
            // textBoxEditLN
            // 
            this.textBoxEditLN.Enabled = false;
            this.textBoxEditLN.Location = new System.Drawing.Point(340, 472);
            this.textBoxEditLN.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEditLN.Name = "textBoxEditLN";
            this.textBoxEditLN.Size = new System.Drawing.Size(114, 26);
            this.textBoxEditLN.TabIndex = 15;
            // 
            // textBoxEditFN
            // 
            this.textBoxEditFN.Enabled = false;
            this.textBoxEditFN.Location = new System.Drawing.Point(115, 472);
            this.textBoxEditFN.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEditFN.Name = "textBoxEditFN";
            this.textBoxEditFN.Size = new System.Drawing.Size(114, 26);
            this.textBoxEditFN.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(464, 474);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Location = new System.Drawing.Point(910, 593);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Phone:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 474);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "Last Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(653, 474);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Location = new System.Drawing.Point(625, 593);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Handicap:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 474);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "First Name:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView.Location = new System.Drawing.Point(5, 52);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView.RowTemplate.Height = 33;
            this.dataGridView.Size = new System.Drawing.Size(855, 401);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.Click += new System.EventHandler(this.dataGridView_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(767, 723);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(95, 39);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // toolTipPhoneNum
            // 
            this.toolTipPhoneNum.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTipPhoneNum_Popup);
            // 
            // ManagePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 969);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ManagePlayers";
            this.Text = "Manage All Players";
            this.Load += new System.EventHandler(this.ManagePlayers_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddNewPlayer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAddHandicap;
        private System.Windows.Forms.TextBox textBoxAddLN;
        private System.Windows.Forms.TextBox textBoxAddFN;
        private System.Windows.Forms.MaskedTextBox textBoxAddPhone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radioButtonSortLN;
        private System.Windows.Forms.RadioButton radioButtonSortFN;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxEditID;
        private System.Windows.Forms.Button buttonModifyPlayer;
        private System.Windows.Forms.Button buttonDeletePlayer;
        private System.Windows.Forms.TextBox textBoxEditHandicap;
        private System.Windows.Forms.TextBox textBoxEditLN;
        private System.Windows.Forms.TextBox textBoxEditFN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ToolTip toolTipPhoneNum;
        private System.Windows.Forms.MaskedTextBox textBoxEditPhone;
    }
}
