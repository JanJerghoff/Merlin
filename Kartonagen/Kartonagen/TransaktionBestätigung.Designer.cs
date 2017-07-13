namespace Kartonagen
{
    partial class TransaktionBestätigung
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public void AnzeigeAendern(string inhalt)
        {
            this.textBestätigung.Text += inhalt;
        }
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
            this.textBestätigung = new System.Windows.Forms.TextBox();
            this.buttonBestätigungJa = new System.Windows.Forms.Button();
            this.buttonBestätigungNein = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBestätigung
            // 
            this.textBestätigung.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBestätigung.Location = new System.Drawing.Point(12, 12);
            this.textBestätigung.Multiline = true;
            this.textBestätigung.Name = "textBestätigung";
            this.textBestätigung.ReadOnly = true;
            this.textBestätigung.Size = new System.Drawing.Size(558, 238);
            this.textBestätigung.TabIndex = 0;
            // 
            // buttonBestätigungJa
            // 
            this.buttonBestätigungJa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBestätigungJa.Location = new System.Drawing.Point(591, 12);
            this.buttonBestätigungJa.Name = "buttonBestätigungJa";
            this.buttonBestätigungJa.Size = new System.Drawing.Size(282, 116);
            this.buttonBestätigungJa.TabIndex = 1;
            this.buttonBestätigungJa.Text = "Ja, Bestätigen";
            this.buttonBestätigungJa.UseVisualStyleBackColor = true;
            // 
            // buttonBestätigungNein
            // 
            this.buttonBestätigungNein.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBestätigungNein.Location = new System.Drawing.Point(591, 134);
            this.buttonBestätigungNein.Name = "buttonBestätigungNein";
            this.buttonBestätigungNein.Size = new System.Drawing.Size(282, 116);
            this.buttonBestätigungNein.TabIndex = 2;
            this.buttonBestätigungNein.Text = "Nein, Abbruch";
            this.buttonBestätigungNein.UseVisualStyleBackColor = true;
            // 
            // TransaktionBestätigung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(885, 262);
            this.Controls.Add(this.buttonBestätigungNein);
            this.Controls.Add(this.buttonBestätigungJa);
            this.Controls.Add(this.textBestätigung);
            this.Name = "TransaktionBestätigung";
            this.Text = "TransaktionBestädtigung";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBestätigung;
        private System.Windows.Forms.Button buttonBestätigungJa;
        private System.Windows.Forms.Button buttonBestätigungNein;
    }
}