namespace Kartonagen
{
    partial class TransaktionErinnerung
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
            this.textZeit = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textKunde = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericKleiderKarton = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericGlaeserkarton = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.numericFlaschenKarton = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.numericKarton = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textAdresse = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBemerkung = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textRechnungsnummer = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericKleiderKarton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGlaeserkarton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFlaschenKarton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKarton)).BeginInit();
            this.SuspendLayout();
            // 
            // textZeit
            // 
            this.textZeit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textZeit.Location = new System.Drawing.Point(16, 54);
            this.textZeit.Name = "textZeit";
            this.textZeit.ReadOnly = true;
            this.textZeit.Size = new System.Drawing.Size(181, 26);
            this.textZeit.TabIndex = 0;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(12, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(135, 20);
            this.label33.TabIndex = 187;
            this.label33.Text = "Datum / Uhrzeit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(199, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 189;
            this.label1.Text = "Kunde";
            // 
            // textKunde
            // 
            this.textKunde.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textKunde.Location = new System.Drawing.Point(203, 54);
            this.textKunde.Name = "textKunde";
            this.textKunde.ReadOnly = true;
            this.textKunde.Size = new System.Drawing.Size(262, 26);
            this.textKunde.TabIndex = 188;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(130, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 197;
            this.label2.Text = "Gläserkartons";
            // 
            // numericKleiderKarton
            // 
            this.numericKleiderKarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericKleiderKarton.Location = new System.Drawing.Point(16, 194);
            this.numericKleiderKarton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericKleiderKarton.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericKleiderKarton.Name = "numericKleiderKarton";
            this.numericKleiderKarton.Size = new System.Drawing.Size(108, 26);
            this.numericKleiderKarton.TabIndex = 193;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(130, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 196;
            this.label3.Text = "Kleiderkartons";
            // 
            // numericGlaeserkarton
            // 
            this.numericGlaeserkarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericGlaeserkarton.Location = new System.Drawing.Point(16, 162);
            this.numericGlaeserkarton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericGlaeserkarton.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericGlaeserkarton.Name = "numericGlaeserkarton";
            this.numericGlaeserkarton.Size = new System.Drawing.Size(108, 26);
            this.numericGlaeserkarton.TabIndex = 192;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(130, 130);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(142, 20);
            this.label26.TabIndex = 195;
            this.label26.Text = "Flaschenkartons";
            // 
            // numericFlaschenKarton
            // 
            this.numericFlaschenKarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericFlaschenKarton.Location = new System.Drawing.Point(16, 130);
            this.numericFlaschenKarton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericFlaschenKarton.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericFlaschenKarton.Name = "numericFlaschenKarton";
            this.numericFlaschenKarton.Size = new System.Drawing.Size(108, 26);
            this.numericFlaschenKarton.TabIndex = 191;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(130, 100);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(142, 20);
            this.label25.TabIndex = 194;
            this.label25.Text = "Normale Kartons";
            // 
            // numericKarton
            // 
            this.numericKarton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericKarton.Location = new System.Drawing.Point(16, 100);
            this.numericKarton.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericKarton.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericKarton.Name = "numericKarton";
            this.numericKarton.Size = new System.Drawing.Size(108, 26);
            this.numericKarton.TabIndex = 190;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(467, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 199;
            this.label4.Text = "Adresse";
            // 
            // textAdresse
            // 
            this.textAdresse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textAdresse.Location = new System.Drawing.Point(471, 54);
            this.textAdresse.Name = "textAdresse";
            this.textAdresse.ReadOnly = true;
            this.textAdresse.Size = new System.Drawing.Size(308, 26);
            this.textAdresse.TabIndex = 198;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(781, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 201;
            this.label5.Text = "Bemerkung";
            // 
            // textBemerkung
            // 
            this.textBemerkung.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBemerkung.Location = new System.Drawing.Point(785, 54);
            this.textBemerkung.Multiline = true;
            this.textBemerkung.Name = "textBemerkung";
            this.textBemerkung.Size = new System.Drawing.Size(262, 166);
            this.textBemerkung.TabIndex = 200;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(16, 259);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 70);
            this.button1.TabIndex = 202;
            this.button1.Text = "Abbrechen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSpeichern.Location = new System.Drawing.Point(766, 259);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(144, 70);
            this.buttonSpeichern.TabIndex = 203;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(467, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 20);
            this.label6.TabIndex = 205;
            this.label6.Text = "Rechnungsnummer";
            // 
            // textRechnungsnummer
            // 
            this.textRechnungsnummer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRechnungsnummer.Location = new System.Drawing.Point(471, 114);
            this.textRechnungsnummer.Name = "textRechnungsnummer";
            this.textRechnungsnummer.ReadOnly = true;
            this.textRechnungsnummer.Size = new System.Drawing.Size(308, 26);
            this.textRechnungsnummer.TabIndex = 204;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(916, 259);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 70);
            this.button2.TabIndex = 206;
            this.button2.Text = "Speichern und nächste Erinnerung";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TransaktionErinnerung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 384);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textRechnungsnummer);
            this.Controls.Add(this.buttonSpeichern);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBemerkung);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textAdresse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericKleiderKarton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericGlaeserkarton);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.numericFlaschenKarton);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.numericKarton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textKunde);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.textZeit);
            this.Name = "TransaktionErinnerung";
            this.Text = "TransaktionAlertW";
            ((System.ComponentModel.ISupportInitialize)(this.numericKleiderKarton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericGlaeserkarton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFlaschenKarton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericKarton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textZeit;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textKunde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericKleiderKarton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericGlaeserkarton;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numericFlaschenKarton;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown numericKarton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textAdresse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBemerkung;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textRechnungsnummer;
        private System.Windows.Forms.Button button2;
    }
}