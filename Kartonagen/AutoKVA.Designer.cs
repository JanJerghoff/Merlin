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
            this.SuspendLayout();
            // 
            // textBlock
            // 
            this.textBlock.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBlock.Location = new System.Drawing.Point(13, 113);
            this.textBlock.Margin = new System.Windows.Forms.Padding(4);
            this.textBlock.Multiline = true;
            this.textBlock.Name = "textBlock";
            this.textBlock.Size = new System.Drawing.Size(921, 859);
            this.textBlock.TabIndex = 105;
            this.textBlock.TabStop = false;
            // 
            // AutoKVA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1447, 1045);
            this.Controls.Add(this.textBlock);
            this.Name = "AutoKVA";
            this.Text = "AutoKVA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBlock;
    }
}