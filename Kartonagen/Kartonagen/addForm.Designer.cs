namespace Kartonagen
{
    partial class addForm
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
            this.splitoffHorizontal = new System.Windows.Forms.SplitContainer();
            this.splitoffVertical = new System.Windows.Forms.SplitContainer();
            this.textKundeInfo = new System.Windows.Forms.TextBox();
            this.buttonKundeFertig = new System.Windows.Forms.Button();
            this.labelKundeTelefon = new System.Windows.Forms.Label();
            this.labelKundeDatum = new System.Windows.Forms.Label();
            this.labelKundeName = new System.Windows.Forms.Label();
            this.textKundeTelefon = new System.Windows.Forms.TextBox();
            this.textKundeName = new System.Windows.Forms.TextBox();
            this.dateKunde = new System.Windows.Forms.DateTimePicker();
            this.leftLabel = new System.Windows.Forms.Label();
            this.textTransaktionInfo = new System.Windows.Forms.TextBox();
            this.buttonTransaktionBestätigen = new System.Windows.Forms.Button();
            this.labelTransaktionDatum = new System.Windows.Forms.Label();
            this.dateTransaktionsdatum = new System.Windows.Forms.DateTimePicker();
            this.numericTransaktionAnzahl = new System.Windows.Forms.NumericUpDown();
            this.numericTransaktionKundenNr = new System.Windows.Forms.NumericUpDown();
            this.labelTransaktionMenge = new System.Windows.Forms.Label();
            this.labelTransaktionKundennr = new System.Windows.Forms.Label();
            this.rightLabel = new System.Windows.Forms.Label();
            this.textLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitoffHorizontal)).BeginInit();
            this.splitoffHorizontal.Panel1.SuspendLayout();
            this.splitoffHorizontal.Panel2.SuspendLayout();
            this.splitoffHorizontal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitoffVertical)).BeginInit();
            this.splitoffVertical.Panel1.SuspendLayout();
            this.splitoffVertical.Panel2.SuspendLayout();
            this.splitoffVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTransaktionAnzahl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTransaktionKundenNr)).BeginInit();
            this.SuspendLayout();
            // 
            // splitoffHorizontal
            // 
            this.splitoffHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitoffHorizontal.Location = new System.Drawing.Point(0, 0);
            this.splitoffHorizontal.Name = "splitoffHorizontal";
            this.splitoffHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitoffHorizontal.Panel1
            // 
            this.splitoffHorizontal.Panel1.Controls.Add(this.splitoffVertical);
            // 
            // splitoffHorizontal.Panel2
            // 
            this.splitoffHorizontal.Panel2.Controls.Add(this.textLog);
            this.splitoffHorizontal.Size = new System.Drawing.Size(1095, 582);
            this.splitoffHorizontal.SplitterDistance = 451;
            this.splitoffHorizontal.TabIndex = 0;
            // 
            // splitoffVertical
            // 
            this.splitoffVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitoffVertical.Location = new System.Drawing.Point(0, 0);
            this.splitoffVertical.Name = "splitoffVertical";
            // 
            // splitoffVertical.Panel1
            // 
            this.splitoffVertical.Panel1.Controls.Add(this.textKundeInfo);
            this.splitoffVertical.Panel1.Controls.Add(this.buttonKundeFertig);
            this.splitoffVertical.Panel1.Controls.Add(this.labelKundeTelefon);
            this.splitoffVertical.Panel1.Controls.Add(this.labelKundeDatum);
            this.splitoffVertical.Panel1.Controls.Add(this.labelKundeName);
            this.splitoffVertical.Panel1.Controls.Add(this.textKundeTelefon);
            this.splitoffVertical.Panel1.Controls.Add(this.textKundeName);
            this.splitoffVertical.Panel1.Controls.Add(this.dateKunde);
            this.splitoffVertical.Panel1.Controls.Add(this.leftLabel);
            // 
            // splitoffVertical.Panel2
            // 
            this.splitoffVertical.Panel2.Controls.Add(this.textTransaktionInfo);
            this.splitoffVertical.Panel2.Controls.Add(this.buttonTransaktionBestätigen);
            this.splitoffVertical.Panel2.Controls.Add(this.labelTransaktionDatum);
            this.splitoffVertical.Panel2.Controls.Add(this.dateTransaktionsdatum);
            this.splitoffVertical.Panel2.Controls.Add(this.numericTransaktionAnzahl);
            this.splitoffVertical.Panel2.Controls.Add(this.numericTransaktionKundenNr);
            this.splitoffVertical.Panel2.Controls.Add(this.labelTransaktionMenge);
            this.splitoffVertical.Panel2.Controls.Add(this.labelTransaktionKundennr);
            this.splitoffVertical.Panel2.Controls.Add(this.rightLabel);
            this.splitoffVertical.Size = new System.Drawing.Size(1095, 451);
            this.splitoffVertical.SplitterDistance = 552;
            this.splitoffVertical.TabIndex = 0;
            // 
            // textKundeInfo
            // 
            this.textKundeInfo.Location = new System.Drawing.Point(308, 104);
            this.textKundeInfo.Multiline = true;
            this.textKundeInfo.Name = "textKundeInfo";
            this.textKundeInfo.ReadOnly = true;
            this.textKundeInfo.Size = new System.Drawing.Size(241, 298);
            this.textKundeInfo.TabIndex = 8;
            // 
            // buttonKundeFertig
            // 
            this.buttonKundeFertig.Location = new System.Drawing.Point(12, 375);
            this.buttonKundeFertig.Name = "buttonKundeFertig";
            this.buttonKundeFertig.Size = new System.Drawing.Size(200, 27);
            this.buttonKundeFertig.TabIndex = 7;
            this.buttonKundeFertig.Text = "Kunde Speichern";
            this.buttonKundeFertig.UseVisualStyleBackColor = true;
            this.buttonKundeFertig.Click += new System.EventHandler(this.buttonKundeFertig_Click);
            // 
            // labelKundeTelefon
            // 
            this.labelKundeTelefon.AutoSize = true;
            this.labelKundeTelefon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKundeTelefon.Location = new System.Drawing.Point(58, 284);
            this.labelKundeTelefon.Name = "labelKundeTelefon";
            this.labelKundeTelefon.Size = new System.Drawing.Size(120, 20);
            this.labelKundeTelefon.TabIndex = 6;
            this.labelKundeTelefon.Text = "Telefonnummer";
            // 
            // labelKundeDatum
            // 
            this.labelKundeDatum.AutoSize = true;
            this.labelKundeDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKundeDatum.Location = new System.Drawing.Point(58, 187);
            this.labelKundeDatum.Name = "labelKundeDatum";
            this.labelKundeDatum.Size = new System.Drawing.Size(113, 20);
            this.labelKundeDatum.TabIndex = 5;
            this.labelKundeDatum.Text = "Umzugsdatum";
            // 
            // labelKundeName
            // 
            this.labelKundeName.AutoSize = true;
            this.labelKundeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKundeName.Location = new System.Drawing.Point(58, 82);
            this.labelKundeName.Name = "labelKundeName";
            this.labelKundeName.Size = new System.Drawing.Size(86, 20);
            this.labelKundeName.TabIndex = 4;
            this.labelKundeName.Text = "Nachname";
            // 
            // textKundeTelefon
            // 
            this.textKundeTelefon.Location = new System.Drawing.Point(12, 307);
            this.textKundeTelefon.Name = "textKundeTelefon";
            this.textKundeTelefon.Size = new System.Drawing.Size(200, 20);
            this.textKundeTelefon.TabIndex = 3;
            // 
            // textKundeName
            // 
            this.textKundeName.Location = new System.Drawing.Point(12, 105);
            this.textKundeName.Name = "textKundeName";
            this.textKundeName.Size = new System.Drawing.Size(200, 20);
            this.textKundeName.TabIndex = 2;
            // 
            // dateKunde
            // 
            this.dateKunde.Location = new System.Drawing.Point(12, 210);
            this.dateKunde.Name = "dateKunde";
            this.dateKunde.Size = new System.Drawing.Size(200, 20);
            this.dateKunde.TabIndex = 1;
            // 
            // leftLabel
            // 
            this.leftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.leftLabel.AutoSize = true;
            this.leftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftLabel.Location = new System.Drawing.Point(150, 9);
            this.leftLabel.Name = "leftLabel";
            this.leftLabel.Size = new System.Drawing.Size(237, 31);
            this.leftLabel.TabIndex = 0;
            this.leftLabel.Text = "Kunde Hinzufügen";
            // 
            // textTransaktionInfo
            // 
            this.textTransaktionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textTransaktionInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textTransaktionInfo.Location = new System.Drawing.Point(295, 104);
            this.textTransaktionInfo.Multiline = true;
            this.textTransaktionInfo.Name = "textTransaktionInfo";
            this.textTransaktionInfo.ReadOnly = true;
            this.textTransaktionInfo.Size = new System.Drawing.Size(241, 298);
            this.textTransaktionInfo.TabIndex = 14;
            this.textTransaktionInfo.Text = "Kundennummer in seperatem Fenster nachsehen, wenn unbekannt.\r\n\r\nKartonmenge posit" +
    "iv wenn Kartons ausgeliefert / abgeholt, negativ wenn Kartons zurückgebracht";
            // 
            // buttonTransaktionBestätigen
            // 
            this.buttonTransaktionBestätigen.Location = new System.Drawing.Point(69, 375);
            this.buttonTransaktionBestätigen.Name = "buttonTransaktionBestätigen";
            this.buttonTransaktionBestätigen.Size = new System.Drawing.Size(200, 27);
            this.buttonTransaktionBestätigen.TabIndex = 13;
            this.buttonTransaktionBestätigen.Text = "Transaktion Speichern";
            this.buttonTransaktionBestätigen.UseVisualStyleBackColor = true;
            this.buttonTransaktionBestätigen.Click += new System.EventHandler(this.buttonTransaktionBestätigen_Click);
            // 
            // labelTransaktionDatum
            // 
            this.labelTransaktionDatum.AutoSize = true;
            this.labelTransaktionDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransaktionDatum.Location = new System.Drawing.Point(65, 284);
            this.labelTransaktionDatum.Name = "labelTransaktionDatum";
            this.labelTransaktionDatum.Size = new System.Drawing.Size(57, 20);
            this.labelTransaktionDatum.TabIndex = 12;
            this.labelTransaktionDatum.Text = "Datum";
            // 
            // dateTransaktionsdatum
            // 
            this.dateTransaktionsdatum.Location = new System.Drawing.Point(69, 304);
            this.dateTransaktionsdatum.Name = "dateTransaktionsdatum";
            this.dateTransaktionsdatum.Size = new System.Drawing.Size(200, 20);
            this.dateTransaktionsdatum.TabIndex = 11;
            // 
            // numericTransaktionAnzahl
            // 
            this.numericTransaktionAnzahl.Location = new System.Drawing.Point(69, 209);
            this.numericTransaktionAnzahl.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericTransaktionAnzahl.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
            this.numericTransaktionAnzahl.Name = "numericTransaktionAnzahl";
            this.numericTransaktionAnzahl.Size = new System.Drawing.Size(120, 20);
            this.numericTransaktionAnzahl.TabIndex = 10;
            // 
            // numericTransaktionKundenNr
            // 
            this.numericTransaktionKundenNr.Location = new System.Drawing.Point(69, 105);
            this.numericTransaktionKundenNr.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericTransaktionKundenNr.Name = "numericTransaktionKundenNr";
            this.numericTransaktionKundenNr.Size = new System.Drawing.Size(120, 20);
            this.numericTransaktionKundenNr.TabIndex = 9;
            // 
            // labelTransaktionMenge
            // 
            this.labelTransaktionMenge.AutoSize = true;
            this.labelTransaktionMenge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransaktionMenge.Location = new System.Drawing.Point(65, 187);
            this.labelTransaktionMenge.Name = "labelTransaktionMenge";
            this.labelTransaktionMenge.Size = new System.Drawing.Size(105, 20);
            this.labelTransaktionMenge.TabIndex = 8;
            this.labelTransaktionMenge.Text = "Kartonmenge";
            // 
            // labelTransaktionKundennr
            // 
            this.labelTransaktionKundennr.AutoSize = true;
            this.labelTransaktionKundennr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransaktionKundennr.Location = new System.Drawing.Point(65, 82);
            this.labelTransaktionKundennr.Name = "labelTransaktionKundennr";
            this.labelTransaktionKundennr.Size = new System.Drawing.Size(122, 20);
            this.labelTransaktionKundennr.TabIndex = 6;
            this.labelTransaktionKundennr.Text = "Kundennummer";
            // 
            // rightLabel
            // 
            this.rightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rightLabel.AutoSize = true;
            this.rightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightLabel.Location = new System.Drawing.Point(119, 9);
            this.rightLabel.Name = "rightLabel";
            this.rightLabel.Size = new System.Drawing.Size(302, 31);
            this.rightLabel.TabIndex = 1;
            this.rightLabel.Text = "Transaktion Hinzufügen";
            // 
            // textLog
            // 
            this.textLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textLog.Location = new System.Drawing.Point(12, 15);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(1071, 100);
            this.textLog.TabIndex = 0;
            // 
            // addForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 582);
            this.Controls.Add(this.splitoffHorizontal);
            this.Name = "addForm";
            this.Text = "addForm";
            this.splitoffHorizontal.Panel1.ResumeLayout(false);
            this.splitoffHorizontal.Panel2.ResumeLayout(false);
            this.splitoffHorizontal.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitoffHorizontal)).EndInit();
            this.splitoffHorizontal.ResumeLayout(false);
            this.splitoffVertical.Panel1.ResumeLayout(false);
            this.splitoffVertical.Panel1.PerformLayout();
            this.splitoffVertical.Panel2.ResumeLayout(false);
            this.splitoffVertical.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitoffVertical)).EndInit();
            this.splitoffVertical.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericTransaktionAnzahl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTransaktionKundenNr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitoffHorizontal;
        private System.Windows.Forms.SplitContainer splitoffVertical;
        private System.Windows.Forms.Label labelKundeName;
        private System.Windows.Forms.TextBox textKundeTelefon;
        private System.Windows.Forms.TextBox textKundeName;
        private System.Windows.Forms.DateTimePicker dateKunde;
        private System.Windows.Forms.Label leftLabel;
        private System.Windows.Forms.Label rightLabel;
        private System.Windows.Forms.Button buttonKundeFertig;
        private System.Windows.Forms.Label labelKundeTelefon;
        private System.Windows.Forms.Label labelKundeDatum;
        private System.Windows.Forms.TextBox textKundeInfo;
        private System.Windows.Forms.TextBox textTransaktionInfo;
        private System.Windows.Forms.Button buttonTransaktionBestätigen;
        private System.Windows.Forms.Label labelTransaktionDatum;
        private System.Windows.Forms.DateTimePicker dateTransaktionsdatum;
        private System.Windows.Forms.NumericUpDown numericTransaktionAnzahl;
        private System.Windows.Forms.NumericUpDown numericTransaktionKundenNr;
        private System.Windows.Forms.Label labelTransaktionMenge;
        private System.Windows.Forms.Label labelTransaktionKundennr;
        private System.Windows.Forms.TextBox textLog;
    }
}