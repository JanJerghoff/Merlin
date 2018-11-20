namespace Kartonagen
{
    partial class TransaktionenUebersicht
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
            this.textTransaktionLog = new System.Windows.Forms.TextBox();
            this.dataGridausstehendeKartonagen = new System.Windows.Forms.DataGridView();
            this.buttonHistorieUmzNr = new System.Windows.Forms.Button();
            this.Kundennummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Umzugsnummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transaktionsnummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Datum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kundenname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flaschenkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gläserkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kleiderkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridausstehendeKartonagen)).BeginInit();
            this.SuspendLayout();
            // 
            // textTransaktionLog
            // 
            this.textTransaktionLog.Location = new System.Drawing.Point(13, 705);
            this.textTransaktionLog.Margin = new System.Windows.Forms.Padding(4);
            this.textTransaktionLog.Multiline = true;
            this.textTransaktionLog.Name = "textTransaktionLog";
            this.textTransaktionLog.ReadOnly = true;
            this.textTransaktionLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textTransaktionLog.Size = new System.Drawing.Size(1104, 153);
            this.textTransaktionLog.TabIndex = 118;
            // 
            // dataGridausstehendeKartonagen
            // 
            this.dataGridausstehendeKartonagen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridausstehendeKartonagen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kundennummer,
            this.Umzugsnummer,
            this.Transaktionsnummer,
            this.Datum,
            this.Kundenname,
            this.Kartons,
            this.Flaschenkartons,
            this.Gläserkartons,
            this.Kleiderkartons});
            this.dataGridausstehendeKartonagen.Location = new System.Drawing.Point(13, 78);
            this.dataGridausstehendeKartonagen.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridausstehendeKartonagen.Name = "dataGridausstehendeKartonagen";
            this.dataGridausstehendeKartonagen.Size = new System.Drawing.Size(1771, 619);
            this.dataGridausstehendeKartonagen.TabIndex = 119;
            // 
            // buttonHistorieUmzNr
            // 
            this.buttonHistorieUmzNr.Location = new System.Drawing.Point(13, 13);
            this.buttonHistorieUmzNr.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHistorieUmzNr.Name = "buttonHistorieUmzNr";
            this.buttonHistorieUmzNr.Size = new System.Drawing.Size(279, 47);
            this.buttonHistorieUmzNr.TabIndex = 126;
            this.buttonHistorieUmzNr.Text = "Letzen 100 Transaktionsnummern";
            this.buttonHistorieUmzNr.UseVisualStyleBackColor = true;
            this.buttonHistorieUmzNr.Click += new System.EventHandler(this.buttonHistorieUmzNr_Click);
            // 
            // Kundennummer
            // 
            this.Kundennummer.HeaderText = "Kundennummer";
            this.Kundennummer.Name = "Kundennummer";
            this.Kundennummer.ReadOnly = true;
            // 
            // Umzugsnummer
            // 
            this.Umzugsnummer.HeaderText = "Umzugsnummer";
            this.Umzugsnummer.Name = "Umzugsnummer";
            this.Umzugsnummer.ReadOnly = true;
            // 
            // Transaktionsnummer
            // 
            this.Transaktionsnummer.HeaderText = "Transaktionsnummer";
            this.Transaktionsnummer.Name = "Transaktionsnummer";
            this.Transaktionsnummer.ReadOnly = true;
            // 
            // Datum
            // 
            this.Datum.HeaderText = "Datum";
            this.Datum.Name = "Datum";
            this.Datum.ReadOnly = true;
            // 
            // Kundenname
            // 
            this.Kundenname.HeaderText = "Kundenname";
            this.Kundenname.Name = "Kundenname";
            this.Kundenname.ReadOnly = true;
            // 
            // Kartons
            // 
            this.Kartons.HeaderText = "Kartons";
            this.Kartons.Name = "Kartons";
            this.Kartons.ReadOnly = true;
            // 
            // Flaschenkartons
            // 
            this.Flaschenkartons.HeaderText = "Flaschenkartons";
            this.Flaschenkartons.Name = "Flaschenkartons";
            this.Flaschenkartons.ReadOnly = true;
            // 
            // Gläserkartons
            // 
            this.Gläserkartons.HeaderText = "Gläserkartons";
            this.Gläserkartons.Name = "Gläserkartons";
            this.Gläserkartons.ReadOnly = true;
            // 
            // Kleiderkartons
            // 
            this.Kleiderkartons.HeaderText = "Kleiderkartons";
            this.Kleiderkartons.Name = "Kleiderkartons";
            this.Kleiderkartons.ReadOnly = true;
            // 
            // TransaktionenUebersicht
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1924, 887);
            this.Controls.Add(this.buttonHistorieUmzNr);
            this.Controls.Add(this.dataGridausstehendeKartonagen);
            this.Controls.Add(this.textTransaktionLog);
            this.Name = "TransaktionenUebersicht";
            this.Text = "TransaktionenUebersicht";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridausstehendeKartonagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textTransaktionLog;
        private System.Windows.Forms.DataGridView dataGridausstehendeKartonagen;
        private System.Windows.Forms.Button buttonHistorieUmzNr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundennummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Umzugsnummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transaktionsnummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Datum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundenname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flaschenkartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gläserkartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kleiderkartons;
    }
}