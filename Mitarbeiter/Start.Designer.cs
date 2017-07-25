namespace Mitarbeiter
{
    partial class Start
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupMainBenutzer = new System.Windows.Forms.GroupBox();
            this.radioMainBenutzerSonst = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerJan = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerEva = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerJonas = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerRita = new System.Windows.Forms.RadioButton();
            this.buttonStammdaten = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.buttonMehrfachUmzug = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonEintragUmzug = new System.Windows.Forms.Button();
            this.buttonTourenFahrzeuge = new System.Windows.Forms.Button();
            this.textStartLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonStundenübersicht = new System.Windows.Forms.Button();
            this.buttonEintragTabelle = new System.Windows.Forms.Button();
            this.groupMainBenutzer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupMainBenutzer
            // 
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerSonst);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerJan);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerEva);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerJonas);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerRita);
            this.groupMainBenutzer.Location = new System.Drawing.Point(12, 12);
            this.groupMainBenutzer.Name = "groupMainBenutzer";
            this.groupMainBenutzer.Size = new System.Drawing.Size(105, 148);
            this.groupMainBenutzer.TabIndex = 1;
            this.groupMainBenutzer.TabStop = false;
            this.groupMainBenutzer.Text = "Benutzer";
            // 
            // radioMainBenutzerSonst
            // 
            this.radioMainBenutzerSonst.AutoSize = true;
            this.radioMainBenutzerSonst.Checked = true;
            this.radioMainBenutzerSonst.Location = new System.Drawing.Point(6, 112);
            this.radioMainBenutzerSonst.Name = "radioMainBenutzerSonst";
            this.radioMainBenutzerSonst.Size = new System.Drawing.Size(66, 17);
            this.radioMainBenutzerSonst.TabIndex = 4;
            this.radioMainBenutzerSonst.TabStop = true;
            this.radioMainBenutzerSonst.Text = "Sonstige";
            this.radioMainBenutzerSonst.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerJan
            // 
            this.radioMainBenutzerJan.AutoSize = true;
            this.radioMainBenutzerJan.Location = new System.Drawing.Point(6, 89);
            this.radioMainBenutzerJan.Name = "radioMainBenutzerJan";
            this.radioMainBenutzerJan.Size = new System.Drawing.Size(42, 17);
            this.radioMainBenutzerJan.TabIndex = 3;
            this.radioMainBenutzerJan.Text = "Jan";
            this.radioMainBenutzerJan.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerEva
            // 
            this.radioMainBenutzerEva.AutoSize = true;
            this.radioMainBenutzerEva.Location = new System.Drawing.Point(6, 66);
            this.radioMainBenutzerEva.Name = "radioMainBenutzerEva";
            this.radioMainBenutzerEva.Size = new System.Drawing.Size(44, 17);
            this.radioMainBenutzerEva.TabIndex = 2;
            this.radioMainBenutzerEva.Text = "Eva";
            this.radioMainBenutzerEva.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerJonas
            // 
            this.radioMainBenutzerJonas.AutoSize = true;
            this.radioMainBenutzerJonas.Location = new System.Drawing.Point(6, 43);
            this.radioMainBenutzerJonas.Name = "radioMainBenutzerJonas";
            this.radioMainBenutzerJonas.Size = new System.Drawing.Size(53, 17);
            this.radioMainBenutzerJonas.TabIndex = 1;
            this.radioMainBenutzerJonas.Text = "Jonas";
            this.radioMainBenutzerJonas.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerRita
            // 
            this.radioMainBenutzerRita.AutoSize = true;
            this.radioMainBenutzerRita.Location = new System.Drawing.Point(6, 20);
            this.radioMainBenutzerRita.Name = "radioMainBenutzerRita";
            this.radioMainBenutzerRita.Size = new System.Drawing.Size(44, 17);
            this.radioMainBenutzerRita.TabIndex = 0;
            this.radioMainBenutzerRita.Text = "Rita";
            this.radioMainBenutzerRita.UseVisualStyleBackColor = true;
            // 
            // buttonStammdaten
            // 
            this.buttonStammdaten.Location = new System.Drawing.Point(124, 17);
            this.buttonStammdaten.Name = "buttonStammdaten";
            this.buttonStammdaten.Size = new System.Drawing.Size(177, 92);
            this.buttonStammdaten.TabIndex = 2;
            this.buttonStammdaten.Text = "Stammdaten Mitarbeiter";
            this.buttonStammdaten.UseVisualStyleBackColor = true;
            this.buttonStammdaten.Click += new System.EventHandler(this.buttonStammdaten_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonEintragTabelle);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.buttonMehrfachUmzug);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.buttonEintragUmzug);
            this.groupBox1.Location = new System.Drawing.Point(124, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 134);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LEA-Einträge";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(738, 19);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(177, 92);
            this.button4.TabIndex = 7;
            this.button4.Text = "Eintrag Urlaub / Feiertag";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // buttonMehrfachUmzug
            // 
            this.buttonMehrfachUmzug.Location = new System.Drawing.Point(555, 19);
            this.buttonMehrfachUmzug.Name = "buttonMehrfachUmzug";
            this.buttonMehrfachUmzug.Size = new System.Drawing.Size(177, 92);
            this.buttonMehrfachUmzug.TabIndex = 6;
            this.buttonMehrfachUmzug.Text = "Eintrag Schilder / Kartons / Besichtigungen";
            this.buttonMehrfachUmzug.UseVisualStyleBackColor = true;
            this.buttonMehrfachUmzug.Click += new System.EventHandler(this.buttonMehrfachUmzug_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(372, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 92);
            this.button2.TabIndex = 5;
            this.button2.Text = "Eintrag Kundenzahl / Stück";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonEintragUmzug
            // 
            this.buttonEintragUmzug.Location = new System.Drawing.Point(189, 19);
            this.buttonEintragUmzug.Name = "buttonEintragUmzug";
            this.buttonEintragUmzug.Size = new System.Drawing.Size(177, 92);
            this.buttonEintragUmzug.TabIndex = 4;
            this.buttonEintragUmzug.Text = "Eintrag Umzug";
            this.buttonEintragUmzug.UseVisualStyleBackColor = true;
            this.buttonEintragUmzug.Click += new System.EventHandler(this.buttonEintragUmzug_Click);
            // 
            // buttonTourenFahrzeuge
            // 
            this.buttonTourenFahrzeuge.Location = new System.Drawing.Point(307, 17);
            this.buttonTourenFahrzeuge.Name = "buttonTourenFahrzeuge";
            this.buttonTourenFahrzeuge.Size = new System.Drawing.Size(177, 92);
            this.buttonTourenFahrzeuge.TabIndex = 4;
            this.buttonTourenFahrzeuge.Text = "Touren  / Fahrzeuge";
            this.buttonTourenFahrzeuge.UseVisualStyleBackColor = true;
            this.buttonTourenFahrzeuge.Click += new System.EventHandler(this.buttonTourenFahrzeuge_Click);
            // 
            // textStartLog
            // 
            this.textStartLog.Location = new System.Drawing.Point(12, 437);
            this.textStartLog.Multiline = true;
            this.textStartLog.Name = "textStartLog";
            this.textStartLog.ReadOnly = true;
            this.textStartLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textStartLog.Size = new System.Drawing.Size(1136, 114);
            this.textStartLog.TabIndex = 144;
            this.textStartLog.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.buttonStundenübersicht);
            this.groupBox2.Location = new System.Drawing.Point(124, 256);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(861, 134);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Abfragen";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(372, 19);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(177, 92);
            this.button3.TabIndex = 6;
            this.button3.Text = "Repertoire";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 92);
            this.button1.TabIndex = 5;
            this.button1.Text = "Detailsuche LEA";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonStundenübersicht
            // 
            this.buttonStundenübersicht.Location = new System.Drawing.Point(6, 19);
            this.buttonStundenübersicht.Name = "buttonStundenübersicht";
            this.buttonStundenübersicht.Size = new System.Drawing.Size(177, 92);
            this.buttonStundenübersicht.TabIndex = 4;
            this.buttonStundenübersicht.Text = "Stundenübersicht";
            this.buttonStundenübersicht.UseVisualStyleBackColor = true;
            this.buttonStundenübersicht.Click += new System.EventHandler(this.buttonStundenübersicht_Click);
            // 
            // buttonEintragTabelle
            // 
            this.buttonEintragTabelle.Location = new System.Drawing.Point(6, 19);
            this.buttonEintragTabelle.Name = "buttonEintragTabelle";
            this.buttonEintragTabelle.Size = new System.Drawing.Size(177, 92);
            this.buttonEintragTabelle.TabIndex = 7;
            this.buttonEintragTabelle.Text = "Eintrag Tabelle";
            this.buttonEintragTabelle.UseVisualStyleBackColor = true;
            this.buttonEintragTabelle.Click += new System.EventHandler(this.buttonEintragTabelle_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 666);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textStartLog);
            this.Controls.Add(this.buttonTourenFahrzeuge);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonStammdaten);
            this.Controls.Add(this.groupMainBenutzer);
            this.Name = "Start";
            this.Text = "Form1";
            this.groupMainBenutzer.ResumeLayout(false);
            this.groupMainBenutzer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupMainBenutzer;
        private System.Windows.Forms.RadioButton radioMainBenutzerSonst;
        private System.Windows.Forms.RadioButton radioMainBenutzerJan;
        private System.Windows.Forms.RadioButton radioMainBenutzerEva;
        private System.Windows.Forms.RadioButton radioMainBenutzerJonas;
        private System.Windows.Forms.RadioButton radioMainBenutzerRita;
        private System.Windows.Forms.Button buttonStammdaten;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button buttonMehrfachUmzug;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonEintragUmzug;
        private System.Windows.Forms.Button buttonTourenFahrzeuge;
        private System.Windows.Forms.TextBox textStartLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonStundenübersicht;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonEintragTabelle;
    }
}

