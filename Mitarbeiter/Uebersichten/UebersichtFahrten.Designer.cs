namespace Mitarbeiter.Uebersichten
{
    partial class UebersichtFahrten
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
            this.label2 = new System.Windows.Forms.Label();
            this.textID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textMitarbeiter = new System.Windows.Forms.TextBox();
            this.textDatum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textTour = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textDauer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 145;
            this.label2.Text = "(interne) ID";
            // 
            // textID
            // 
            this.textID.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textID.Location = new System.Drawing.Point(15, 36);
            this.textID.Multiline = true;
            this.textID.Name = "textID";
            this.textID.ReadOnly = true;
            this.textID.Size = new System.Drawing.Size(68, 913);
            this.textID.TabIndex = 144;
            this.textID.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(410, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 15);
            this.label1.TabIndex = 143;
            this.label1.Text = "Mitarbeiter";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(98, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 142;
            this.label8.Text = "Datum";
            // 
            // textMitarbeiter
            // 
            this.textMitarbeiter.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textMitarbeiter.Location = new System.Drawing.Point(413, 36);
            this.textMitarbeiter.Multiline = true;
            this.textMitarbeiter.Name = "textMitarbeiter";
            this.textMitarbeiter.ReadOnly = true;
            this.textMitarbeiter.Size = new System.Drawing.Size(197, 913);
            this.textMitarbeiter.TabIndex = 141;
            this.textMitarbeiter.TabStop = false;
            // 
            // textDatum
            // 
            this.textDatum.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textDatum.Location = new System.Drawing.Point(89, 36);
            this.textDatum.Multiline = true;
            this.textDatum.Name = "textDatum";
            this.textDatum.ReadOnly = true;
            this.textDatum.Size = new System.Drawing.Size(181, 913);
            this.textDatum.TabIndex = 140;
            this.textDatum.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(273, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 147;
            this.label3.Text = "Tour";
            // 
            // textTour
            // 
            this.textTour.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textTour.Location = new System.Drawing.Point(276, 36);
            this.textTour.Multiline = true;
            this.textTour.Name = "textTour";
            this.textTour.ReadOnly = true;
            this.textTour.Size = new System.Drawing.Size(131, 913);
            this.textTour.TabIndex = 146;
            this.textTour.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(613, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 149;
            this.label4.Text = "Dauer (in Min)";
            // 
            // textDauer
            // 
            this.textDauer.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textDauer.Location = new System.Drawing.Point(616, 36);
            this.textDauer.Multiline = true;
            this.textDauer.Name = "textDauer";
            this.textDauer.ReadOnly = true;
            this.textDauer.Size = new System.Drawing.Size(145, 913);
            this.textDauer.TabIndex = 148;
            this.textDauer.TabStop = false;
            // 
            // UebersichtFahrten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1527, 891);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textDauer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textTour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textMitarbeiter);
            this.Controls.Add(this.textDatum);
            this.Name = "UebersichtFahrten";
            this.Text = "UebersichtFahrten";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textMitarbeiter;
        private System.Windows.Forms.TextBox textDatum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textTour;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textDauer;
    }
}