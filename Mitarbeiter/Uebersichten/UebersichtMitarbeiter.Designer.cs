namespace Mitarbeiter.Uebersichten
{
    partial class UebersichtMitarbeiter
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
            this.label3 = new System.Windows.Forms.Label();
            this.textUrlaub = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textTyp = new System.Windows.Forms.TextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(458, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 155;
            this.label3.Text = "Urlaubsstand";
            // 
            // textUrlaub
            // 
            this.textUrlaub.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textUrlaub.Location = new System.Drawing.Point(461, 59);
            this.textUrlaub.Multiline = true;
            this.textUrlaub.Name = "textUrlaub";
            this.textUrlaub.ReadOnly = true;
            this.textUrlaub.Size = new System.Drawing.Size(131, 913);
            this.textUrlaub.TabIndex = 154;
            this.textUrlaub.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 153;
            this.label2.Text = "(interne) ID";
            // 
            // textID
            // 
            this.textID.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textID.Location = new System.Drawing.Point(15, 59);
            this.textID.Multiline = true;
            this.textID.Name = "textID";
            this.textID.ReadOnly = true;
            this.textID.Size = new System.Drawing.Size(68, 913);
            this.textID.TabIndex = 152;
            this.textID.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(321, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 15);
            this.label1.TabIndex = 151;
            this.label1.Text = "Stundenanteil";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(98, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 15);
            this.label8.TabIndex = 150;
            this.label8.Text = "Mitarbeitername";
            // 
            // textTyp
            // 
            this.textTyp.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textTyp.Location = new System.Drawing.Point(324, 59);
            this.textTyp.Multiline = true;
            this.textTyp.Name = "textTyp";
            this.textTyp.ReadOnly = true;
            this.textTyp.Size = new System.Drawing.Size(131, 913);
            this.textTyp.TabIndex = 149;
            this.textTyp.TabStop = false;
            // 
            // textName
            // 
            this.textName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textName.Location = new System.Drawing.Point(89, 59);
            this.textName.Multiline = true;
            this.textName.Name = "textName";
            this.textName.ReadOnly = true;
            this.textName.Size = new System.Drawing.Size(229, 913);
            this.textName.TabIndex = 148;
            this.textName.TabStop = false;
            // 
            // UebersichtMitarbeiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1526, 911);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textUrlaub);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textTyp);
            this.Controls.Add(this.textName);
            this.Name = "UebersichtMitarbeiter";
            this.Text = "UebersichtMitarbeiter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textUrlaub;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textTyp;
        private System.Windows.Forms.TextBox textName;
    }
}