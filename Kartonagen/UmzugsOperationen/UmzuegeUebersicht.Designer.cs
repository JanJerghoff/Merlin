namespace Kartonagen
{
    partial class UmzuegeUebersicht
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
            this.buttonHistorieUmzNr = new System.Windows.Forms.Button();
            this.buttonUmzugstermin = new System.Windows.Forms.Button();
            this.buttonBesichtigungstermin = new System.Windows.Forms.Button();
            this.DataGridUmzugsuebersicht = new System.Windows.Forms.DataGridView();
            this.Kundennummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Umzugsnummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kundenname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefonnummer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Besichtigung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Umzug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrAuszug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrtAuszug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrEinzug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrtEinzug = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridUmzugsuebersicht)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonHistorieUmzNr
            // 
            this.buttonHistorieUmzNr.Location = new System.Drawing.Point(16, 12);
            this.buttonHistorieUmzNr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonHistorieUmzNr.Name = "buttonHistorieUmzNr";
            this.buttonHistorieUmzNr.Size = new System.Drawing.Size(279, 47);
            this.buttonHistorieUmzNr.TabIndex = 125;
            this.buttonHistorieUmzNr.Text = "Letzen 100 Umzuegsnummern";
            this.buttonHistorieUmzNr.UseVisualStyleBackColor = true;
            this.buttonHistorieUmzNr.Click += new System.EventHandler(this.buttonHistorie_Click);
            // 
            // buttonUmzugstermin
            // 
            this.buttonUmzugstermin.Location = new System.Drawing.Point(303, 12);
            this.buttonUmzugstermin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonUmzugstermin.Name = "buttonUmzugstermin";
            this.buttonUmzugstermin.Size = new System.Drawing.Size(279, 47);
            this.buttonUmzugstermin.TabIndex = 132;
            this.buttonUmzugstermin.Text = "Letzen 100 UmzuegsTermine";
            this.buttonUmzugstermin.UseVisualStyleBackColor = true;
            this.buttonUmzugstermin.Click += new System.EventHandler(this.buttonUmzugstermin_Click);
            // 
            // buttonBesichtigungstermin
            // 
            this.buttonBesichtigungstermin.Location = new System.Drawing.Point(589, 12);
            this.buttonBesichtigungstermin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBesichtigungstermin.Name = "buttonBesichtigungstermin";
            this.buttonBesichtigungstermin.Size = new System.Drawing.Size(279, 47);
            this.buttonBesichtigungstermin.TabIndex = 133;
            this.buttonBesichtigungstermin.Text = "Letzen 100 BesichtigungsTermine";
            this.buttonBesichtigungstermin.UseVisualStyleBackColor = true;
            this.buttonBesichtigungstermin.Click += new System.EventHandler(this.buttonBesichtigungstermin_Click);
            // 
            // DataGridUmzugsuebersicht
            // 
            this.DataGridUmzugsuebersicht.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridUmzugsuebersicht.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kundennummer,
            this.Umzugsnummer,
            this.Kundenname,
            this.Telefonnummer,
            this.Email,
            this.Besichtigung,
            this.Umzug,
            this.StrAuszug,
            this.OrtAuszug,
            this.StrEinzug,
            this.OrtEinzug});
            this.DataGridUmzugsuebersicht.Location = new System.Drawing.Point(16, 67);
            this.DataGridUmzugsuebersicht.Margin = new System.Windows.Forms.Padding(4);
            this.DataGridUmzugsuebersicht.Name = "DataGridUmzugsuebersicht";
            this.DataGridUmzugsuebersicht.Size = new System.Drawing.Size(1895, 918);
            this.DataGridUmzugsuebersicht.TabIndex = 134;
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
            // Kundenname
            // 
            this.Kundenname.HeaderText = "Kundenname";
            this.Kundenname.Name = "Kundenname";
            this.Kundenname.ReadOnly = true;
            // 
            // Telefonnummer
            // 
            this.Telefonnummer.HeaderText = "Telefonnummer";
            this.Telefonnummer.Name = "Telefonnummer";
            this.Telefonnummer.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // Besichtigung
            // 
            this.Besichtigung.HeaderText = "Besichtigung";
            this.Besichtigung.Name = "Besichtigung";
            this.Besichtigung.ReadOnly = true;
            // 
            // Umzug
            // 
            this.Umzug.HeaderText = "Umzug";
            this.Umzug.Name = "Umzug";
            this.Umzug.ReadOnly = true;
            // 
            // StrAuszug
            // 
            this.StrAuszug.HeaderText = "Straße Auszug";
            this.StrAuszug.Name = "StrAuszug";
            this.StrAuszug.ReadOnly = true;
            // 
            // OrtAuszug
            // 
            this.OrtAuszug.HeaderText = "Ort Auszug";
            this.OrtAuszug.Name = "OrtAuszug";
            this.OrtAuszug.ReadOnly = true;
            // 
            // StrEinzug
            // 
            this.StrEinzug.HeaderText = "Straße Einzug";
            this.StrEinzug.Name = "StrEinzug";
            this.StrEinzug.ReadOnly = true;
            // 
            // OrtEinzug
            // 
            this.OrtEinzug.HeaderText = "Ort Einzug";
            this.OrtEinzug.Name = "OrtEinzug";
            this.OrtEinzug.ReadOnly = true;
            // 
            // UmzuegeUebersicht
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 998);
            this.Controls.Add(this.DataGridUmzugsuebersicht);
            this.Controls.Add(this.buttonBesichtigungstermin);
            this.Controls.Add(this.buttonUmzugstermin);
            this.Controls.Add(this.buttonHistorieUmzNr);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UmzuegeUebersicht";
            this.Text = "UmzuegeUebersicht";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridUmzugsuebersicht)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHistorieUmzNr;
        private System.Windows.Forms.Button buttonUmzugstermin;
        private System.Windows.Forms.Button buttonBesichtigungstermin;
        private System.Windows.Forms.DataGridView DataGridUmzugsuebersicht;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundennummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Umzugsnummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kundenname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefonnummer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Besichtigung;
        private System.Windows.Forms.DataGridViewTextBoxColumn Umzug;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrAuszug;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrtAuszug;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrEinzug;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrtEinzug;
    }
}