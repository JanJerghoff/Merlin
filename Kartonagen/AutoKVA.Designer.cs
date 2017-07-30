namespace Kartonagen
{
    partial class AutoKVA
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
            this.textBlock = new System.Windows.Forms.TextBox();
            this.buttonPauschal = new System.Windows.Forms.Button();
            this.textUmzugsnummer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textUmzugsname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textUmzugsdatum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBlock
            // 
            this.textBlock.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBlock.Location = new System.Drawing.Point(10, 92);
            this.textBlock.Multiline = true;
            this.textBlock.Name = "textBlock";
            this.textBlock.Size = new System.Drawing.Size(692, 699);
            this.textBlock.TabIndex = 105;
            this.textBlock.TabStop = false;
            // 
            // buttonPauschal
            // 
            this.buttonPauschal.Location = new System.Drawing.Point(12, 63);
            this.buttonPauschal.Name = "buttonPauschal";
            this.buttonPauschal.Size = new System.Drawing.Size(132, 23);
            this.buttonPauschal.TabIndex = 106;
            this.buttonPauschal.Text = "Pauschal";
            this.buttonPauschal.UseVisualStyleBackColor = true;
            this.buttonPauschal.Click += new System.EventHandler(this.buttonPauschal_Click);
            // 
            // textUmzugsnummer
            // 
            this.textUmzugsnummer.Location = new System.Drawing.Point(10, 37);
            this.textUmzugsnummer.Name = "textUmzugsnummer";
            this.textUmzugsnummer.ReadOnly = true;
            this.textUmzugsnummer.Size = new System.Drawing.Size(181, 20);
            this.textUmzugsnummer.TabIndex = 107;
            this.textUmzugsnummer.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 108;
            this.label1.Text = "Umzugsnummer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(199, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 110;
            this.label2.Text = "Umzugsname";
            // 
            // textUmzugsname
            // 
            this.textUmzugsname.Location = new System.Drawing.Point(197, 37);
            this.textUmzugsname.Name = "textUmzugsname";
            this.textUmzugsname.ReadOnly = true;
            this.textUmzugsname.Size = new System.Drawing.Size(181, 20);
            this.textUmzugsname.TabIndex = 109;
            this.textUmzugsname.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(386, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 112;
            this.label3.Text = "Umzugsdatum";
            // 
            // textUmzugsdatum
            // 
            this.textUmzugsdatum.Location = new System.Drawing.Point(384, 37);
            this.textUmzugsdatum.Name = "textUmzugsdatum";
            this.textUmzugsdatum.ReadOnly = true;
            this.textUmzugsdatum.Size = new System.Drawing.Size(181, 20);
            this.textUmzugsdatum.TabIndex = 111;
            this.textUmzugsdatum.TabStop = false;
            // 
            // AutoKVA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1085, 849);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textUmzugsdatum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textUmzugsname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textUmzugsnummer);
            this.Controls.Add(this.buttonPauschal);
            this.Controls.Add(this.textBlock);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AutoKVA";
            this.Text = "AutoKVA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBlock;
        private System.Windows.Forms.Button buttonPauschal;
        private System.Windows.Forms.TextBox textUmzugsnummer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textUmzugsname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textUmzugsdatum;
    }
}