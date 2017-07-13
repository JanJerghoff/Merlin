namespace Kartonagen
{
    partial class Sonderabfragen
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
            this.textLog = new System.Windows.Forms.TextBox();
            this.buttonFaellige = new System.Windows.Forms.Button();
            this.buttonBilanz = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textLog
            // 
            this.textLog.Location = new System.Drawing.Point(12, 346);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLog.Size = new System.Drawing.Size(1194, 427);
            this.textLog.TabIndex = 91;
            // 
            // buttonFaellige
            // 
            this.buttonFaellige.Enabled = false;
            this.buttonFaellige.Location = new System.Drawing.Point(12, 12);
            this.buttonFaellige.Name = "buttonFaellige";
            this.buttonFaellige.Size = new System.Drawing.Size(194, 66);
            this.buttonFaellige.TabIndex = 92;
            this.buttonFaellige.Text = "Fällige Kartonagen";
            this.buttonFaellige.UseVisualStyleBackColor = true;
            // 
            // buttonBilanz
            // 
            this.buttonBilanz.Location = new System.Drawing.Point(212, 12);
            this.buttonBilanz.Name = "buttonBilanz";
            this.buttonBilanz.Size = new System.Drawing.Size(194, 66);
            this.buttonBilanz.TabIndex = 93;
            this.buttonBilanz.Text = "Kartonagen-Bilanz gesamt";
            this.buttonBilanz.UseVisualStyleBackColor = true;
            this.buttonBilanz.Click += new System.EventHandler(this.buttonBilanz_Click);
            // 
            // Sonderabfragen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1218, 785);
            this.Controls.Add(this.buttonBilanz);
            this.Controls.Add(this.buttonFaellige);
            this.Controls.Add(this.textLog);
            this.Name = "Sonderabfragen";
            this.Text = "Sonderabfragen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Button buttonFaellige;
        private System.Windows.Forms.Button buttonBilanz;
    }
}