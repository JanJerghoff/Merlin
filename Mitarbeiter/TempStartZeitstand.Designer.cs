namespace Mitarbeiter
{
    partial class TempStartZeitstand
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
            this.numericID = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericSollstunden = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dateZeitpunkt = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonNeu = new System.Windows.Forms.Button();
            this.textLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSollstunden)).BeginInit();
            this.SuspendLayout();
            // 
            // numericID
            // 
            this.numericID.Location = new System.Drawing.Point(12, 12);
            this.numericID.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericID.Name = "numericID";
            this.numericID.Size = new System.Drawing.Size(74, 20);
            this.numericID.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(92, 17);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 15);
            this.label17.TabIndex = 94;
            this.label17.Text = "MitarbeiterID";
            // 
            // numericSollstunden
            // 
            this.numericSollstunden.Location = new System.Drawing.Point(12, 38);
            this.numericSollstunden.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericSollstunden.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericSollstunden.Name = "numericSollstunden";
            this.numericSollstunden.Size = new System.Drawing.Size(74, 20);
            this.numericSollstunden.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(92, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 15);
            this.label1.TabIndex = 96;
            this.label1.Text = "Sollstunden (Soll = +, Haben = - )";
            // 
            // dateZeitpunkt
            // 
            this.dateZeitpunkt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateZeitpunkt.Location = new System.Drawing.Point(12, 64);
            this.dateZeitpunkt.Name = "dateZeitpunkt";
            this.dateZeitpunkt.Size = new System.Drawing.Size(200, 20);
            this.dateZeitpunkt.TabIndex = 97;
            this.dateZeitpunkt.Value = new System.DateTime(2017, 10, 27, 15, 58, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(218, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 98;
            this.label2.Text = "Zeitpunkt";
            // 
            // buttonNeu
            // 
            this.buttonNeu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.buttonNeu.Location = new System.Drawing.Point(12, 90);
            this.buttonNeu.Name = "buttonNeu";
            this.buttonNeu.Size = new System.Drawing.Size(199, 95);
            this.buttonNeu.TabIndex = 99;
            this.buttonNeu.Text = "Mitarbeiter Stundenkonto Anlegen";
            this.buttonNeu.UseVisualStyleBackColor = true;
            this.buttonNeu.Click += new System.EventHandler(this.buttonNeu_Click);
            // 
            // textLog
            // 
            this.textLog.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textLog.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textLog.Location = new System.Drawing.Point(12, 191);
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.Size = new System.Drawing.Size(276, 20);
            this.textLog.TabIndex = 100;
            // 
            // TempStartZeitstand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 252);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.buttonNeu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateZeitpunkt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericSollstunden);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.numericID);
            this.Name = "TempStartZeitstand";
            this.Text = "TempStartZeitstand";
            ((System.ComponentModel.ISupportInitialize)(this.numericID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSollstunden)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericID;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numericSollstunden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateZeitpunkt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNeu;
        private System.Windows.Forms.TextBox textLog;
    }
}