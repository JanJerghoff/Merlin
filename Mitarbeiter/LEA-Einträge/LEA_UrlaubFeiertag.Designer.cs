﻿namespace Mitarbeiter
{
    partial class LEA_UrlaubFeiertag
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioKrankheit = new System.Windows.Forms.RadioButton();
            this.radioFeiertag = new System.Windows.Forms.RadioButton();
            this.radioUrlaub = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textSucheName = new System.Windows.Forms.TextBox();
            this.monthFahrtDatum = new System.Windows.Forms.MonthCalendar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonAbsenden = new System.Windows.Forms.Button();
            this.textStartLog = new System.Windows.Forms.TextBox();
            this.radioFeiertagEinzel = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.textSucheName);
            this.groupBox1.Controls.Add(this.monthFahrtDatum);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(361, 524);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1)";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.radioFeiertagEinzel);
            this.groupBox2.Controls.Add(this.radioKrankheit);
            this.groupBox2.Controls.Add(this.radioFeiertag);
            this.groupBox2.Controls.Add(this.radioUrlaub);
            this.groupBox2.Location = new System.Drawing.Point(12, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(176, 147);
            this.groupBox2.TabIndex = 141;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Urlaub / Feiertag";
            // 
            // radioKrankheit
            // 
            this.radioKrankheit.AutoSize = true;
            this.radioKrankheit.Location = new System.Drawing.Point(8, 52);
            this.radioKrankheit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioKrankheit.Name = "radioKrankheit";
            this.radioKrankheit.Size = new System.Drawing.Size(89, 21);
            this.radioKrankheit.TabIndex = 143;
            this.radioKrankheit.TabStop = true;
            this.radioKrankheit.Text = "Krankheit";
            this.radioKrankheit.UseVisualStyleBackColor = true;
            this.radioKrankheit.CheckedChanged += new System.EventHandler(this.radioKrankheit_CheckedChanged);
            // 
            // radioFeiertag
            // 
            this.radioFeiertag.AutoSize = true;
            this.radioFeiertag.Location = new System.Drawing.Point(8, 80);
            this.radioFeiertag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioFeiertag.Name = "radioFeiertag";
            this.radioFeiertag.Size = new System.Drawing.Size(108, 21);
            this.radioFeiertag.TabIndex = 142;
            this.radioFeiertag.TabStop = true;
            this.radioFeiertag.Text = "Feiertag Alle";
            this.radioFeiertag.UseVisualStyleBackColor = true;
            this.radioFeiertag.CheckedChanged += new System.EventHandler(this.radioFeiertag_CheckedChanged);
            // 
            // radioUrlaub
            // 
            this.radioUrlaub.AutoSize = true;
            this.radioUrlaub.Location = new System.Drawing.Point(8, 23);
            this.radioUrlaub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioUrlaub.Name = "radioUrlaub";
            this.radioUrlaub.Size = new System.Drawing.Size(71, 21);
            this.radioUrlaub.TabIndex = 141;
            this.radioUrlaub.TabStop = true;
            this.radioUrlaub.Text = "Urlaub";
            this.radioUrlaub.UseVisualStyleBackColor = true;
            this.radioUrlaub.CheckedChanged += new System.EventHandler(this.radioUrlaub_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 272);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 18);
            this.label12.TabIndex = 124;
            this.label12.Text = "Datum";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(7, 210);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(137, 18);
            this.label27.TabIndex = 119;
            this.label27.Text = "Mitarbeiter Name";
            // 
            // textSucheName
            // 
            this.textSucheName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textSucheName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textSucheName.Location = new System.Drawing.Point(8, 232);
            this.textSucheName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textSucheName.Name = "textSucheName";
            this.textSucheName.Size = new System.Drawing.Size(196, 22);
            this.textSucheName.TabIndex = 59;
            // 
            // monthFahrtDatum
            // 
            this.monthFahrtDatum.Location = new System.Drawing.Point(8, 302);
            this.monthFahrtDatum.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthFahrtDatum.Name = "monthFahrtDatum";
            this.monthFahrtDatum.ShowWeekNumbers = true;
            this.monthFahrtDatum.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Controls.Add(this.buttonAbsenden);
            this.groupBox3.Location = new System.Drawing.Point(383, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(276, 469);
            this.groupBox3.TabIndex = 142;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "2)";
            // 
            // buttonAbsenden
            // 
            this.buttonAbsenden.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.buttonAbsenden.Location = new System.Drawing.Point(8, 23);
            this.buttonAbsenden.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAbsenden.Name = "buttonAbsenden";
            this.buttonAbsenden.Size = new System.Drawing.Size(241, 100);
            this.buttonAbsenden.TabIndex = 0;
            this.buttonAbsenden.Text = "Absenden";
            this.buttonAbsenden.UseVisualStyleBackColor = true;
            this.buttonAbsenden.Click += new System.EventHandler(this.buttonAbsenden_Click);
            // 
            // textStartLog
            // 
            this.textStartLog.Location = new System.Drawing.Point(13, 546);
            this.textStartLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textStartLog.Multiline = true;
            this.textStartLog.Name = "textStartLog";
            this.textStartLog.ReadOnly = true;
            this.textStartLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textStartLog.Size = new System.Drawing.Size(644, 139);
            this.textStartLog.TabIndex = 145;
            this.textStartLog.TabStop = false;
            // 
            // radioFeiertagEinzel
            // 
            this.radioFeiertagEinzel.AutoSize = true;
            this.radioFeiertagEinzel.Location = new System.Drawing.Point(8, 109);
            this.radioFeiertagEinzel.Margin = new System.Windows.Forms.Padding(4);
            this.radioFeiertagEinzel.Name = "radioFeiertagEinzel";
            this.radioFeiertagEinzel.Size = new System.Drawing.Size(122, 21);
            this.radioFeiertagEinzel.TabIndex = 144;
            this.radioFeiertagEinzel.TabStop = true;
            this.radioFeiertagEinzel.Text = "Feiertag einzel";
            this.radioFeiertagEinzel.UseVisualStyleBackColor = true;
            // 
            // LEA_UrlaubFeiertag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1413, 721);
            this.Controls.Add(this.textStartLog);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "LEA_UrlaubFeiertag";
            this.Text = "LEA_UrlaubFeiertag";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textSucheName;
        private System.Windows.Forms.MonthCalendar monthFahrtDatum;
        private System.Windows.Forms.RadioButton radioKrankheit;
        private System.Windows.Forms.RadioButton radioFeiertag;
        private System.Windows.Forms.RadioButton radioUrlaub;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonAbsenden;
        private System.Windows.Forms.TextBox textStartLog;
        private System.Windows.Forms.RadioButton radioFeiertagEinzel;
    }
}