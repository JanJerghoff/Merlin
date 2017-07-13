namespace Mitarbeiter
{
    partial class Tour_Fahrzeug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tour_Fahrzeug));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textKennzeichen = new System.Windows.Forms.TextBox();
            this.buttonFahrzeugLeeren = new System.Windows.Forms.Button();
            this.buttonFahrzeugChange = new System.Windows.Forms.Button();
            this.buttonFahrzeugAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericCentProKM = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericZaehlerstand = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textFahrzeugID = new System.Windows.Forms.TextBox();
            this.buttonFahrzeugSuchen = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.textFahrzeugName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonTourLeeren = new System.Windows.Forms.Button();
            this.buttonTourChange = new System.Windows.Forms.Button();
            this.buttonTourAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.numericTyp = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericTourKM = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textTourID = new System.Windows.Forms.TextBox();
            this.buttonTourSuchen = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textTourName = new System.Windows.Forms.TextBox();
            this.textLog = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCentProKM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZaehlerstand)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTyp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTourKM)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textKennzeichen);
            this.groupBox1.Controls.Add(this.buttonFahrzeugLeeren);
            this.groupBox1.Controls.Add(this.buttonFahrzeugChange);
            this.groupBox1.Controls.Add(this.buttonFahrzeugAdd);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericCentProKM);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericZaehlerstand);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textFahrzeugID);
            this.groupBox1.Controls.Add(this.buttonFahrzeugSuchen);
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.textFahrzeugName);
            this.groupBox1.Location = new System.Drawing.Point(10, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 418);
            this.groupBox1.TabIndex = 140;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 237);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 15);
            this.label8.TabIndex = 151;
            this.label8.Text = "Kennzeichen";
            // 
            // textKennzeichen
            // 
            this.textKennzeichen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textKennzeichen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textKennzeichen.Location = new System.Drawing.Point(6, 255);
            this.textKennzeichen.Name = "textKennzeichen";
            this.textKennzeichen.Size = new System.Drawing.Size(148, 20);
            this.textKennzeichen.TabIndex = 150;
            // 
            // buttonFahrzeugLeeren
            // 
            this.buttonFahrzeugLeeren.Location = new System.Drawing.Point(260, 25);
            this.buttonFahrzeugLeeren.Name = "buttonFahrzeugLeeren";
            this.buttonFahrzeugLeeren.Size = new System.Drawing.Size(66, 45);
            this.buttonFahrzeugLeeren.TabIndex = 149;
            this.buttonFahrzeugLeeren.Text = "leeren";
            this.buttonFahrzeugLeeren.UseVisualStyleBackColor = true;
            this.buttonFahrzeugLeeren.Click += new System.EventHandler(this.buttonFahrzeugLeeren_Click);
            // 
            // buttonFahrzeugChange
            // 
            this.buttonFahrzeugChange.Location = new System.Drawing.Point(175, 338);
            this.buttonFahrzeugChange.Name = "buttonFahrzeugChange";
            this.buttonFahrzeugChange.Size = new System.Drawing.Size(164, 74);
            this.buttonFahrzeugChange.TabIndex = 148;
            this.buttonFahrzeugChange.Text = "Fahrzeug ändern";
            this.buttonFahrzeugChange.UseVisualStyleBackColor = true;
            this.buttonFahrzeugChange.Click += new System.EventHandler(this.buttonFahrzeugChange_Click);
            // 
            // buttonFahrzeugAdd
            // 
            this.buttonFahrzeugAdd.Location = new System.Drawing.Point(5, 338);
            this.buttonFahrzeugAdd.Name = "buttonFahrzeugAdd";
            this.buttonFahrzeugAdd.Size = new System.Drawing.Size(164, 74);
            this.buttonFahrzeugAdd.TabIndex = 147;
            this.buttonFahrzeugAdd.Text = "Fahrzeug neu hinzufügen";
            this.buttonFahrzeugAdd.UseVisualStyleBackColor = true;
            this.buttonFahrzeugAdd.Click += new System.EventHandler(this.buttonFahrzeugAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 15);
            this.label3.TabIndex = 146;
            this.label3.Text = "Kosten / Kilometer (in Cent)";
            // 
            // numericCentProKM
            // 
            this.numericCentProKM.Location = new System.Drawing.Point(5, 199);
            this.numericCentProKM.Margin = new System.Windows.Forms.Padding(2);
            this.numericCentProKM.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericCentProKM.Name = "numericCentProKM";
            this.numericCentProKM.Size = new System.Drawing.Size(147, 20);
            this.numericCentProKM.TabIndex = 145;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 15);
            this.label2.TabIndex = 144;
            this.label2.Text = "Kilometerstand bei Eintrag";
            // 
            // numericZaehlerstand
            // 
            this.numericZaehlerstand.Location = new System.Drawing.Point(5, 143);
            this.numericZaehlerstand.Margin = new System.Windows.Forms.Padding(2);
            this.numericZaehlerstand.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericZaehlerstand.Name = "numericZaehlerstand";
            this.numericZaehlerstand.Size = new System.Drawing.Size(147, 20);
            this.numericZaehlerstand.TabIndex = 143;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 142;
            this.label1.Text = "ID (intern)";
            // 
            // textFahrzeugID
            // 
            this.textFahrzeugID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textFahrzeugID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textFahrzeugID.Location = new System.Drawing.Point(6, 88);
            this.textFahrzeugID.Name = "textFahrzeugID";
            this.textFahrzeugID.ReadOnly = true;
            this.textFahrzeugID.Size = new System.Drawing.Size(148, 20);
            this.textFahrzeugID.TabIndex = 141;
            // 
            // buttonFahrzeugSuchen
            // 
            this.buttonFahrzeugSuchen.Location = new System.Drawing.Point(159, 25);
            this.buttonFahrzeugSuchen.Name = "buttonFahrzeugSuchen";
            this.buttonFahrzeugSuchen.Size = new System.Drawing.Size(95, 45);
            this.buttonFahrzeugSuchen.TabIndex = 140;
            this.buttonFahrzeugSuchen.Text = "Fahrzeug suchen";
            this.buttonFahrzeugSuchen.UseVisualStyleBackColor = true;
            this.buttonFahrzeugSuchen.Click += new System.EventHandler(this.buttonFahrzeugSuchen_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(6, 20);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(109, 15);
            this.label27.TabIndex = 119;
            this.label27.Text = "Fahrzeug Name";
            // 
            // textFahrzeugName
            // 
            this.textFahrzeugName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textFahrzeugName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textFahrzeugName.Location = new System.Drawing.Point(6, 38);
            this.textFahrzeugName.Name = "textFahrzeugName";
            this.textFahrzeugName.Size = new System.Drawing.Size(148, 20);
            this.textFahrzeugName.TabIndex = 59;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Controls.Add(this.buttonTourLeeren);
            this.groupBox2.Controls.Add(this.buttonTourChange);
            this.groupBox2.Controls.Add(this.buttonTourAdd);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericTyp);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numericTourKM);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textTourID);
            this.groupBox2.Controls.Add(this.buttonTourSuchen);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.textTourName);
            this.groupBox2.Location = new System.Drawing.Point(363, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 418);
            this.groupBox2.TabIndex = 149;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1)";
            // 
            // buttonTourLeeren
            // 
            this.buttonTourLeeren.Location = new System.Drawing.Point(260, 25);
            this.buttonTourLeeren.Name = "buttonTourLeeren";
            this.buttonTourLeeren.Size = new System.Drawing.Size(66, 45);
            this.buttonTourLeeren.TabIndex = 150;
            this.buttonTourLeeren.Text = "leeren";
            this.buttonTourLeeren.UseVisualStyleBackColor = true;
            this.buttonTourLeeren.Click += new System.EventHandler(this.buttonTourLeeren_Click);
            // 
            // buttonTourChange
            // 
            this.buttonTourChange.Location = new System.Drawing.Point(176, 338);
            this.buttonTourChange.Name = "buttonTourChange";
            this.buttonTourChange.Size = new System.Drawing.Size(164, 74);
            this.buttonTourChange.TabIndex = 148;
            this.buttonTourChange.Text = "Tour ändern";
            this.buttonTourChange.UseVisualStyleBackColor = true;
            this.buttonTourChange.Click += new System.EventHandler(this.buttonTourChange_Click);
            // 
            // buttonTourAdd
            // 
            this.buttonTourAdd.Location = new System.Drawing.Point(6, 338);
            this.buttonTourAdd.Name = "buttonTourAdd";
            this.buttonTourAdd.Size = new System.Drawing.Size(164, 74);
            this.buttonTourAdd.TabIndex = 147;
            this.buttonTourAdd.Text = "Tour neu hinzufügen";
            this.buttonTourAdd.UseVisualStyleBackColor = true;
            this.buttonTourAdd.Click += new System.EventHandler(this.buttonTourAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 15);
            this.label4.TabIndex = 146;
            this.label4.Text = "Typ / Kategorie";
            // 
            // numericTyp
            // 
            this.numericTyp.Location = new System.Drawing.Point(5, 199);
            this.numericTyp.Margin = new System.Windows.Forms.Padding(2);
            this.numericTyp.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericTyp.Name = "numericTyp";
            this.numericTyp.Size = new System.Drawing.Size(147, 20);
            this.numericTyp.TabIndex = 145;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(306, 15);
            this.label5.TabIndex = 144;
            this.label5.Text = "Durchschnittliche KM (0 wenn nicht zutreffend)";
            // 
            // numericTourKM
            // 
            this.numericTourKM.Location = new System.Drawing.Point(5, 143);
            this.numericTourKM.Margin = new System.Windows.Forms.Padding(2);
            this.numericTourKM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericTourKM.Name = "numericTourKM";
            this.numericTourKM.Size = new System.Drawing.Size(147, 20);
            this.numericTourKM.TabIndex = 143;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 15);
            this.label6.TabIndex = 142;
            this.label6.Text = "ID (intern)";
            // 
            // textTourID
            // 
            this.textTourID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textTourID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textTourID.Location = new System.Drawing.Point(6, 88);
            this.textTourID.Name = "textTourID";
            this.textTourID.ReadOnly = true;
            this.textTourID.Size = new System.Drawing.Size(148, 20);
            this.textTourID.TabIndex = 141;
            // 
            // buttonTourSuchen
            // 
            this.buttonTourSuchen.Location = new System.Drawing.Point(159, 25);
            this.buttonTourSuchen.Name = "buttonTourSuchen";
            this.buttonTourSuchen.Size = new System.Drawing.Size(95, 45);
            this.buttonTourSuchen.TabIndex = 140;
            this.buttonTourSuchen.Text = "Tour suchen";
            this.buttonTourSuchen.UseVisualStyleBackColor = true;
            this.buttonTourSuchen.Click += new System.EventHandler(this.buttonTourSuchen_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 15);
            this.label7.TabIndex = 119;
            this.label7.Text = "Tour Name";
            // 
            // textTourName
            // 
            this.textTourName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textTourName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textTourName.Location = new System.Drawing.Point(6, 38);
            this.textTourName.Name = "textTourName";
            this.textTourName.Size = new System.Drawing.Size(148, 20);
            this.textTourName.TabIndex = 59;
            // 
            // textLog
            // 
            this.textLog.Location = new System.Drawing.Point(10, 435);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLog.Size = new System.Drawing.Size(1136, 114);
            this.textLog.TabIndex = 150;
            this.textLog.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(716, 11);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(429, 418);
            this.textBox4.TabIndex = 151;
            this.textBox4.TabStop = false;
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // Tour_Fahrzeug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 598);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Tour_Fahrzeug";
            this.Text = "Tour_Fahrzeug";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericCentProKM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericZaehlerstand)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTyp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTourKM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textFahrzeugName;
        private System.Windows.Forms.Button buttonFahrzeugChange;
        private System.Windows.Forms.Button buttonFahrzeugAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericCentProKM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericZaehlerstand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFahrzeugID;
        private System.Windows.Forms.Button buttonFahrzeugSuchen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonTourChange;
        private System.Windows.Forms.Button buttonTourAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericTyp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericTourKM;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTourID;
        private System.Windows.Forms.Button buttonTourSuchen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textTourName;
        private System.Windows.Forms.Button buttonFahrzeugLeeren;
        private System.Windows.Forms.Button buttonTourLeeren;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textKennzeichen;
    }
}