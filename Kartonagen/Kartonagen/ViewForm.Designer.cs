namespace Kartonagen
{
    partial class ViewForm
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
            this.textView = new System.Windows.Forms.TextBox();
            this.buttonViewKundennr = new System.Windows.Forms.Button();
            this.buttonViewFrist = new System.Windows.Forms.Button();
            this.textViewName = new System.Windows.Forms.TextBox();
            this.buttonViewName = new System.Windows.Forms.Button();
            this.groupViewKundennummer = new System.Windows.Forms.GroupBox();
            this.groupViewName = new System.Windows.Forms.GroupBox();
            this.labelViewAusstehend = new System.Windows.Forms.Label();
            this.numericViewKundennr = new System.Windows.Forms.NumericUpDown();
            this.groupViewKundennummer.SuspendLayout();
            this.groupViewName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericViewKundennr)).BeginInit();
            this.SuspendLayout();
            // 
            // textView
            // 
            this.textView.Location = new System.Drawing.Point(12, 152);
            this.textView.Multiline = true;
            this.textView.Name = "textView";
            this.textView.ReadOnly = true;
            this.textView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textView.Size = new System.Drawing.Size(1168, 618);
            this.textView.TabIndex = 0;
            // 
            // buttonViewKundennr
            // 
            this.buttonViewKundennr.Location = new System.Drawing.Point(6, 50);
            this.buttonViewKundennr.Name = "buttonViewKundennr";
            this.buttonViewKundennr.Size = new System.Drawing.Size(75, 23);
            this.buttonViewKundennr.TabIndex = 1;
            this.buttonViewKundennr.Text = "Suchen";
            this.buttonViewKundennr.UseVisualStyleBackColor = true;
            this.buttonViewKundennr.Click += new System.EventHandler(this.buttonViewKundennr_Click);
            // 
            // buttonViewFrist
            // 
            this.buttonViewFrist.Location = new System.Drawing.Point(439, 67);
            this.buttonViewFrist.Name = "buttonViewFrist";
            this.buttonViewFrist.Size = new System.Drawing.Size(75, 23);
            this.buttonViewFrist.TabIndex = 3;
            this.buttonViewFrist.Text = "Anzeigen";
            this.buttonViewFrist.UseVisualStyleBackColor = true;
            // 
            // textViewName
            // 
            this.textViewName.Location = new System.Drawing.Point(6, 24);
            this.textViewName.Name = "textViewName";
            this.textViewName.Size = new System.Drawing.Size(100, 20);
            this.textViewName.TabIndex = 8;
            // 
            // buttonViewName
            // 
            this.buttonViewName.Location = new System.Drawing.Point(6, 50);
            this.buttonViewName.Name = "buttonViewName";
            this.buttonViewName.Size = new System.Drawing.Size(75, 23);
            this.buttonViewName.TabIndex = 7;
            this.buttonViewName.Text = "Suchen";
            this.buttonViewName.UseVisualStyleBackColor = true;
            // 
            // groupViewKundennummer
            // 
            this.groupViewKundennummer.Controls.Add(this.numericViewKundennr);
            this.groupViewKundennummer.Controls.Add(this.buttonViewKundennr);
            this.groupViewKundennummer.Location = new System.Drawing.Point(12, 17);
            this.groupViewKundennummer.Name = "groupViewKundennummer";
            this.groupViewKundennummer.Size = new System.Drawing.Size(138, 90);
            this.groupViewKundennummer.TabIndex = 9;
            this.groupViewKundennummer.TabStop = false;
            this.groupViewKundennummer.Text = "Suche nach Kundennr.";
            // 
            // groupViewName
            // 
            this.groupViewName.Controls.Add(this.textViewName);
            this.groupViewName.Controls.Add(this.buttonViewName);
            this.groupViewName.Location = new System.Drawing.Point(234, 17);
            this.groupViewName.Name = "groupViewName";
            this.groupViewName.Size = new System.Drawing.Size(153, 89);
            this.groupViewName.TabIndex = 10;
            this.groupViewName.TabStop = false;
            this.groupViewName.Text = "Suche nach Name";
            // 
            // labelViewAusstehend
            // 
            this.labelViewAusstehend.AutoSize = true;
            this.labelViewAusstehend.Location = new System.Drawing.Point(439, 17);
            this.labelViewAusstehend.Name = "labelViewAusstehend";
            this.labelViewAusstehend.Size = new System.Drawing.Size(120, 13);
            this.labelViewAusstehend.TabIndex = 11;
            this.labelViewAusstehend.Text = "Ausleihfrist - Kandidaten";
            // 
            // numericViewKundennr
            // 
            this.numericViewKundennr.Location = new System.Drawing.Point(7, 24);
            this.numericViewKundennr.Name = "numericViewKundennr";
            this.numericViewKundennr.Size = new System.Drawing.Size(120, 20);
            this.numericViewKundennr.TabIndex = 2;
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1192, 782);
            this.Controls.Add(this.labelViewAusstehend);
            this.Controls.Add(this.groupViewName);
            this.Controls.Add(this.groupViewKundennummer);
            this.Controls.Add(this.buttonViewFrist);
            this.Controls.Add(this.textView);
            this.Name = "ViewForm";
            this.Text = "ViewForm";
            this.groupViewKundennummer.ResumeLayout(false);
            this.groupViewName.ResumeLayout(false);
            this.groupViewName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericViewKundennr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textView;
        private System.Windows.Forms.Button buttonViewKundennr;
        private System.Windows.Forms.Button buttonViewFrist;
        private System.Windows.Forms.TextBox textViewName;
        private System.Windows.Forms.Button buttonViewName;
        private System.Windows.Forms.GroupBox groupViewKundennummer;
        private System.Windows.Forms.GroupBox groupViewName;
        private System.Windows.Forms.Label labelViewAusstehend;
        private System.Windows.Forms.NumericUpDown numericViewKundennr;
    }
}