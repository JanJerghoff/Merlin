namespace Mitarbeiter
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
            this.label12 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textSucheName = new System.Windows.Forms.TextBox();
            this.monthFahrtDatum = new System.Windows.Forms.MonthCalendar();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(313, 469);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Urlaub";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(12, 23);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(176, 70);
            this.groupBox2.TabIndex = 141;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Urlaub / Feiertag";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 155);
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
            this.label27.Location = new System.Drawing.Point(9, 95);
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
            this.textSucheName.Location = new System.Drawing.Point(9, 117);
            this.textSucheName.Margin = new System.Windows.Forms.Padding(4);
            this.textSucheName.Name = "textSucheName";
            this.textSucheName.Size = new System.Drawing.Size(196, 22);
            this.textSucheName.TabIndex = 59;
            // 
            // monthFahrtDatum
            // 
            this.monthFahrtDatum.Location = new System.Drawing.Point(9, 184);
            this.monthFahrtDatum.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthFahrtDatum.Name = "monthFahrtDatum";
            this.monthFahrtDatum.ShowWeekNumbers = true;
            this.monthFahrtDatum.TabIndex = 0;
            // 
            // LEA_UrlaubFeiertag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1413, 721);
            this.Controls.Add(this.groupBox1);
            this.Name = "LEA_UrlaubFeiertag";
            this.Text = "LEA_UrlaubFeiertag";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textSucheName;
        private System.Windows.Forms.MonthCalendar monthFahrtDatum;
    }
}