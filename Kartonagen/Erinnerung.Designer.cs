namespace Kartonagen
{
    partial class Erinnerung
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
            this.buttonSonderabfragen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonSonderabfragen
            // 
            this.buttonSonderabfragen.Location = new System.Drawing.Point(50, 66);
            this.buttonSonderabfragen.Name = "buttonSonderabfragen";
            this.buttonSonderabfragen.Size = new System.Drawing.Size(179, 117);
            this.buttonSonderabfragen.TabIndex = 5;
            this.buttonSonderabfragen.Text = "Nächste Erinnerung";
            this.buttonSonderabfragen.UseVisualStyleBackColor = true;
            // 
            // Erinnerung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonSonderabfragen);
            this.Name = "Erinnerung";
            this.Text = "Erinnerung";
            this.Load += new System.EventHandler(this.Erinnerung_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSonderabfragen;
    }
}