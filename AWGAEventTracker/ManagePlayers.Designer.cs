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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox1.Size = new System.Drawing.Size(589, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Member:";
            // 
            // textBoxAddPhone
            // 
            this.textBoxAddPhone.BeepOnError = true;
            this.textBoxAddPhone.Location = new System.Drawing.Point(455, 24);
            this.textBoxAddPhone.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAddPhone.Mask = "(999) 000-0000";
            this.textBoxAddPhone.Name = "textBoxAddPhone";
            this.textBoxAddPhone.Size = new System.Drawing.Size(102, 20);
            this.textBoxAddPhone.TabIndex = 8;
            this.textBoxAddPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.textBoxAddPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // btnAddNewPlayer
            // 
            this.btnAddNewPlayer.Location = new System.Drawing.Point(505, 51);
            this.btnAddNewPlayer.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddNewPlayer.Name = "btnAddNewPlayer";
            this.btnAddNewPlayer.Size = new System.Drawing.Size(63, 25);
            this.btnAddNewPlayer.TabIndex = 0;
            this.btnAddNewPlayer.Text = "Add";
            this.btnAddNewPlayer.UseVisualStyleBackColor = true;
            this.btnAddNewPlayer.Click += new System.EventHandler(this.btnAddNewPlayer_Click);
            // 
            // textBoxAddHandicap
            // 
            this.textBoxAddHandicap.Location = new System.Drawing.Point(370, 24);
            this.textBoxAddHandicap.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAddHandicap.Name = "textBoxAddHandicap";
            this.textBoxAddHandicap.Size = new System.Drawing.Size(35, 20);
            this.textBoxAddHandicap.TabIndex = 7;
            this.textBoxAddHandicap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // textBoxAddLN
            // 
            this.textBoxAddLN.Location = new System.Drawing.Point(227, 24);
            this.textBoxAddLN.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxAddLN.Name = "textBoxAddLN";
            this.textBoxAddLN.Size = new System.Drawing.Size(77, 20);
            this.textBoxAddLN.TabIndex = 6;
            this.textBoxAddLN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // textBoxAddFN
            // 
            this.textBoxAddFN.Location = new System.Drawing.Point(77, 24);
            this.textBoxAddFN.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxAddFN.Name = "textBoxAddFN";
            this.textBoxAddFN.Size = new System.Drawing.Size(77, 20);
            this.textBoxAddFN.TabIndex = 5;
            this.textBoxAddFN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onAddNewUserKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(413, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Phone:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Handicap:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
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
            this.groupBox2.Location = new System.Drawing.Point(7, 94);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox2.Size = new System.Drawing.Size(589, 408);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modify/Remove Members:";
            // 
            // textBoxEditPhone
            // 
            this.textBoxEditPhone.BeepOnError = true;
            this.textBoxEditPhone.Enabled = false;
            this.textBoxEditPhone.Location = new System.Drawing.Point(455, 307);
            this.textBoxEditPhone.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEditPhone.Mask = "(999) 000-0000";
            this.textBoxEditPhone.Name = "textBoxEditPhone";
            this.textBoxEditPhone.Size = new System.Drawing.Size(102, 20);
            this.textBoxEditPhone.TabIndex = 17;
            this.textBoxEditPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 328);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "ID (Hidden):";
            this.label10.Visible = false;
            // 
            // radioButtonSortLN
            // 
            this.radioButtonSortLN.AutoSize = true;
            this.radioButtonSortLN.Checked = true;
            this.radioButtonSortLN.Location = new System.Drawing.Point(493, 14);
            this.radioButtonSortLN.Margin = new System.Windows.Forms.Padding(1);
            this.radioButtonSortLN.Name = "radioButtonSortLN";
            this.radioButtonSortLN.Size = new System.Drawing.Size(76, 17);
            this.radioButtonSortLN.TabIndex = 23;
            this.radioButtonSortLN.TabStop = true;
            this.radioButtonSortLN.Text = "Last Name";
            this.radioButtonSortLN.UseVisualStyleBackColor = true;
            this.radioButtonSortLN.CheckedChanged += new System.EventHandler(this.radioButtonSortLN_CheckedChanged);
            // 
            // radioButtonSortFN
            // 
            this.radioButtonSortFN.AutoSize = true;
            this.radioButtonSortFN.Location = new System.Drawing.Point(415, 14);
            this.radioButtonSortFN.Margin = new System.Windows.Forms.Padding(1);
            this.radioButtonSortFN.Name = "radioButtonSortFN";
            this.radioButtonSortFN.Size = new System.Drawing.Size(75, 17);
            this.radioButtonSortFN.TabIndex = 22;
            this.radioButtonSortFN.TabStop = true;
            this.radioButtonSortFN.Text = "First Name";
            this.radioButtonSortFN.UseVisualStyleBackColor = true;
            this.radioButtonSortFN.CheckedChanged += new System.EventHandler(this.radioButtoSortFN1_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(367, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Sort By:";
            // 
            // textBoxEditID
            // 
            this.textBoxEditID.Location = new System.Drawing.Point(77, 326);
            this.textBoxEditID.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxEditID.Name = "textBoxEditID";
            this.textBoxEditID.Size = new System.Drawing.Size(77, 20);
            this.textBoxEditID.TabIndex = 20;
            this.textBoxEditID.Visible = false;
            // 
            // buttonModifyPlayer
            // 
            this.buttonModifyPlayer.Enabled = false;
            this.buttonModifyPlayer.Location = new System.Drawing.Point(481, 334);
            this.buttonModifyPlayer.Margin = new System.Windows.Forms.Padding(1);
            this.buttonModifyPlayer.Name = "buttonModifyPlayer";
            this.buttonModifyPlayer.Size = new System.Drawing.Size(87, 25);
            this.buttonModifyPlayer.TabIndex = 19;
            this.buttonModifyPlayer.Text = "Modify Member";
            this.buttonModifyPlayer.UseVisualStyleBackColor = true;
            this.buttonModifyPlayer.Click += new System.EventHandler(this.buttonModifyPlayer_Click);
            // 
            // buttonDeletePlayer
            // 
            this.buttonDeletePlayer.Enabled = false;
            this.buttonDeletePlayer.Location = new System.Drawing.Point(387, 334);
            this.buttonDeletePlayer.Margin = new System.Windows.Forms.Padding(1);
            this.buttonDeletePlayer.Name = "buttonDeletePlayer";
            this.buttonDeletePlayer.Size = new System.Drawing.Size(91, 25);
            this.buttonDeletePlayer.TabIndex = 18;
            this.buttonDeletePlayer.Text = "Delete Member";
            this.buttonDeletePlayer.UseVisualStyleBackColor = true;
            this.buttonDeletePlayer.Click += new System.EventHandler(this.buttonDeletePlayer_Click);
            // 
            // textBoxEditHandicap
            // 
            this.textBoxEditHandicap.Enabled = false;
            this.textBoxEditHandicap.Location = new System.Drawing.Point(370, 307);
            this.textBoxEditHandicap.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxEditHandicap.Name = "textBoxEditHandicap";
            this.textBoxEditHandicap.Size = new System.Drawing.Size(35, 20);
            this.textBoxEditHandicap.TabIndex = 16;
            // 
            // textBoxEditLN
            // 
            this.textBoxEditLN.Enabled = false;
            this.textBoxEditLN.Location = new System.Drawing.Point(227, 307);
            this.textBoxEditLN.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxEditLN.Name = "textBoxEditLN";
            this.textBoxEditLN.Size = new System.Drawing.Size(77, 20);
            this.textBoxEditLN.TabIndex = 15;
            // 
            // textBoxEditFN
            // 
            this.textBoxEditFN.Enabled = false;
            this.textBoxEditFN.Location = new System.Drawing.Point(77, 307);
            this.textBoxEditFN.Margin = new System.Windows.Forms.Padding(1);
            this.textBoxEditFN.Name = "textBoxEditFN";
            this.textBoxEditFN.Size = new System.Drawing.Size(77, 20);
            this.textBoxEditFN.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(413, 308);
            this.label5.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Phone:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 308);
            this.label6.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Last Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(315, 308);
            this.label7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Handicap:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 308);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "First Name:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.Location = new System.Drawing.Point(3, 34);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(1);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView.RowTemplate.Height = 33;
            this.dataGridView.Size = new System.Drawing.Size(570, 261);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.Click += new System.EventHandler(this.dataGridView_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(511, 470);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(1);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(63, 25);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ManagePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(619, 545);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(1);
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
