﻿namespace Kartonagen
{
    partial class ausstehendeKartonagen
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
            this.dataGridausstehendeKartonagen = new System.Windows.Forms.DataGridView();
            this.textMainLog = new System.Windows.Forms.TextBox();
            this.Kundennummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kundenname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefonnummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flaschenkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gläserkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kleiderkartons = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Letzte_Transaktion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridausstehendeKartonagen)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridausstehendeKartonagen
            // 
            this.dataGridausstehendeKartonagen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridausstehendeKartonagen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kundennummer,
            this.Kundenname,
            this.Email,
            this.Telefonnummer,
            this.Kartons,
            this.Flaschenkartons,
            this.Gläserkartons,
            this.Kleiderkartons,
            this.Letzte_Transaktion});
            this.dataGridausstehendeKartonagen.Location = new System.Drawing.Point(16, 15);
            this.dataGridausstehendeKartonagen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridausstehendeKartonagen.Name = "dataGridausstehendeKartonagen";
            this.dataGridausstehendeKartonagen.Size = new System.Drawing.Size(1771, 619);
            this.dataGridausstehendeKartonagen.TabIndex = 0;
            // 
            // textMainLog
            // 
            this.textMainLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textMainLog.Location = new System.Drawing.Point(16, 641);
            this.textMainLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textMainLog.Multiline = true;
            this.textMainLog.Name = "textMainLog";
            this.textMainLog.ReadOnly = true;
            this.textMainLog.Size = new System.Drawing.Size(1600, 146);
            this.textMainLog.TabIndex = 1;
            // 
            // Kundennummer
            // 
            this.Kundennummer.HeaderText = "Kundennummer";
            this.Kundennummer.Name = "Kundennummer";
            this.Kundennummer.ReadOnly = true;
            // 
            // Kundenname
            // 
            this.Kundenname.HeaderText = "Kundenname";
            this.Kundenname.Name = "Kundenname";
            this.Kundenname.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // Telefonnummer
            // 
            this.Telefonnummer.HeaderText = "Telefonnummer";
            this.Telefonnummer.Name = "Telefonnummer";
            this.Telefonnummer.ReadOnly = true;
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
            // Letzte_Transaktion
            // 
            this.Letzte_Transaktion.HeaderText = "Letzte_Transaktion";
            this.Letzte_Transaktion.Name = "Letzte_Transaktion";
            this.Letzte_Transaktion.ReadOnly = true;
            // 
            // ausstehendeKartonagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 908);
            this.Controls.Add(this.textMainLog);
            this.Controls.Add(this.dataGridausstehendeKartonagen);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ausstehendeKartonagen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridausstehendeKartonagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridausstehendeKartonagen;
        private System.Windows.Forms.TextBox textMainLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundennummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundenname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefonnummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flaschenkartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gläserkartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kleiderkartons;
        private System.Windows.Forms.DataGridViewTextBoxColumn Letzte_Transaktion;
    }
}