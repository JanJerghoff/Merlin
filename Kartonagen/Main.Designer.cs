namespace Kartonagen
{
    partial class mainForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.PDFRead = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLaufzettel = new System.Windows.Forms.Button();
            this.buttonLaufKarton = new System.Windows.Forms.Button();
            this.buttonErinnerungen = new System.Windows.Forms.Button();
            this.buttonSonderfunktion = new System.Windows.Forms.Button();
            this.groupMainKartonagen = new System.Windows.Forms.GroupBox();
            this.buttonUebersichtKartons = new System.Windows.Forms.Button();
            this.buttonMainKartonagenChange = new System.Windows.Forms.Button();
            this.buttonMainKartonagenAdd = new System.Windows.Forms.Button();
            this.groupMainUmzuege = new System.Windows.Forms.GroupBox();
            this.buttonMainUmzuegeShow = new System.Windows.Forms.Button();
            this.buttonMainUmzuegeChange = new System.Windows.Forms.Button();
            this.buttonMainUmzuegeAdd = new System.Windows.Forms.Button();
            this.groupMainKunden = new System.Windows.Forms.GroupBox();
            this.buttonMainKundenShow = new System.Windows.Forms.Button();
            this.buttonMainKundenChange = new System.Windows.Forms.Button();
            this.buttonMainKundenAdd = new System.Windows.Forms.Button();
            this.groupMainBenutzer = new System.Windows.Forms.GroupBox();
            this.radioMainBenutzerNora = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerSonst = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerJan = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerEva = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerJonas = new System.Windows.Forms.RadioButton();
            this.radioMainBenutzerRita = new System.Windows.Forms.RadioButton();
            this.textMainLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupMainKartonagen.SuspendLayout();
            this.groupMainUmzuege.SuspendLayout();
            this.groupMainKunden.SuspendLayout();
            this.groupMainBenutzer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.PDFRead);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonErinnerungen);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSonderfunktion);
            this.splitContainer1.Panel1.Controls.Add(this.groupMainKartonagen);
            this.splitContainer1.Panel1.Controls.Add(this.groupMainUmzuege);
            this.splitContainer1.Panel1.Controls.Add(this.groupMainKunden);
            this.splitContainer1.Panel1.Controls.Add(this.groupMainBenutzer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textMainLog);
            this.splitContainer1.Size = new System.Drawing.Size(1779, 774);
            this.splitContainer1.SplitterDistance = 602;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1225, 410);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(239, 144);
            this.button1.TabIndex = 9;
            this.button1.Text = "Abgleich Termine\r\nKalender <-> Datenbank\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PDFRead
            // 
            this.PDFRead.Enabled = false;
            this.PDFRead.Location = new System.Drawing.Point(1460, 46);
            this.PDFRead.Margin = new System.Windows.Forms.Padding(4);
            this.PDFRead.Name = "PDFRead";
            this.PDFRead.Size = new System.Drawing.Size(239, 144);
            this.PDFRead.TabIndex = 8;
            this.PDFRead.Text = "PDF einlesen (TEST)";
            this.PDFRead.UseVisualStyleBackColor = true;
            this.PDFRead.Click += new System.EventHandler(this.PDFRead_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLaufzettel);
            this.groupBox1.Controls.Add(this.buttonLaufKarton);
            this.groupBox1.Location = new System.Drawing.Point(944, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(508, 175);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ausdrucke";
            // 
            // buttonLaufzettel
            // 
            this.buttonLaufzettel.Location = new System.Drawing.Point(8, 23);
            this.buttonLaufzettel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLaufzettel.Name = "buttonLaufzettel";
            this.buttonLaufzettel.Size = new System.Drawing.Size(239, 144);
            this.buttonLaufzettel.TabIndex = 5;
            this.buttonLaufzettel.Text = "Laufzettel Besichtigung";
            this.buttonLaufzettel.UseVisualStyleBackColor = true;
            this.buttonLaufzettel.Click += new System.EventHandler(this.buttonLaufzettel_Click);
            // 
            // buttonLaufKarton
            // 
            this.buttonLaufKarton.Location = new System.Drawing.Point(255, 23);
            this.buttonLaufKarton.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLaufKarton.Name = "buttonLaufKarton";
            this.buttonLaufKarton.Size = new System.Drawing.Size(239, 144);
            this.buttonLaufKarton.TabIndex = 6;
            this.buttonLaufKarton.Text = "Laufzettel Kartons";
            this.buttonLaufKarton.UseVisualStyleBackColor = true;
            this.buttonLaufKarton.Click += new System.EventHandler(this.buttonLaufKarton_Click);
            // 
            // buttonErinnerungen
            // 
            this.buttonErinnerungen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonErinnerungen.Location = new System.Drawing.Point(979, 410);
            this.buttonErinnerungen.Margin = new System.Windows.Forms.Padding(4);
            this.buttonErinnerungen.Name = "buttonErinnerungen";
            this.buttonErinnerungen.Size = new System.Drawing.Size(239, 144);
            this.buttonErinnerungen.TabIndex = 7;
            this.buttonErinnerungen.Text = "Fällige Kartons";
            this.buttonErinnerungen.UseVisualStyleBackColor = true;
            this.buttonErinnerungen.Click += new System.EventHandler(this.buttonErinnerungen_Click);
            // 
            // buttonSonderfunktion
            // 
            this.buttonSonderfunktion.Enabled = false;
            this.buttonSonderfunktion.Location = new System.Drawing.Point(1479, 410);
            this.buttonSonderfunktion.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSonderfunktion.Name = "buttonSonderfunktion";
            this.buttonSonderfunktion.Size = new System.Drawing.Size(239, 144);
            this.buttonSonderfunktion.TabIndex = 4;
            this.buttonSonderfunktion.Text = "Sonderfunktion";
            this.buttonSonderfunktion.UseVisualStyleBackColor = true;
            this.buttonSonderfunktion.Click += new System.EventHandler(this.buttonSonderabfragen_Click);
            // 
            // groupMainKartonagen
            // 
            this.groupMainKartonagen.Controls.Add(this.buttonUebersichtKartons);
            this.groupMainKartonagen.Controls.Add(this.buttonMainKartonagenChange);
            this.groupMainKartonagen.Controls.Add(this.buttonMainKartonagenAdd);
            this.groupMainKartonagen.Location = new System.Drawing.Point(164, 386);
            this.groupMainKartonagen.Margin = new System.Windows.Forms.Padding(4);
            this.groupMainKartonagen.Name = "groupMainKartonagen";
            this.groupMainKartonagen.Padding = new System.Windows.Forms.Padding(4);
            this.groupMainKartonagen.Size = new System.Drawing.Size(755, 175);
            this.groupMainKartonagen.TabIndex = 4;
            this.groupMainKartonagen.TabStop = false;
            this.groupMainKartonagen.Text = "Kartonagen";
            // 
            // buttonUebersichtKartons
            // 
            this.buttonUebersichtKartons.Location = new System.Drawing.Point(501, 23);
            this.buttonUebersichtKartons.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUebersichtKartons.Name = "buttonUebersichtKartons";
            this.buttonUebersichtKartons.Size = new System.Drawing.Size(239, 144);
            this.buttonUebersichtKartons.TabIndex = 4;
            this.buttonUebersichtKartons.Text = "Übersicht Kartons";
            this.buttonUebersichtKartons.UseVisualStyleBackColor = true;
            this.buttonUebersichtKartons.Click += new System.EventHandler(this.buttonUebersichtKartons_Click);
            // 
            // buttonMainKartonagenChange
            // 
            this.buttonMainKartonagenChange.Location = new System.Drawing.Point(255, 23);
            this.buttonMainKartonagenChange.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainKartonagenChange.Name = "buttonMainKartonagenChange";
            this.buttonMainKartonagenChange.Size = new System.Drawing.Size(239, 144);
            this.buttonMainKartonagenChange.TabIndex = 1;
            this.buttonMainKartonagenChange.Text = "Suchen / Ändern";
            this.buttonMainKartonagenChange.UseVisualStyleBackColor = true;
            this.buttonMainKartonagenChange.Click += new System.EventHandler(this.buttonMainKartonagenChange_Click);
            // 
            // buttonMainKartonagenAdd
            // 
            this.buttonMainKartonagenAdd.Location = new System.Drawing.Point(8, 23);
            this.buttonMainKartonagenAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainKartonagenAdd.Name = "buttonMainKartonagenAdd";
            this.buttonMainKartonagenAdd.Size = new System.Drawing.Size(239, 144);
            this.buttonMainKartonagenAdd.TabIndex = 0;
            this.buttonMainKartonagenAdd.Text = "Anlegen";
            this.buttonMainKartonagenAdd.UseVisualStyleBackColor = true;
            this.buttonMainKartonagenAdd.Click += new System.EventHandler(this.buttonMainKartonagenAdd_Click);
            // 
            // groupMainUmzuege
            // 
            this.groupMainUmzuege.Controls.Add(this.buttonMainUmzuegeShow);
            this.groupMainUmzuege.Controls.Add(this.buttonMainUmzuegeChange);
            this.groupMainUmzuege.Controls.Add(this.buttonMainUmzuegeAdd);
            this.groupMainUmzuege.Location = new System.Drawing.Point(164, 204);
            this.groupMainUmzuege.Margin = new System.Windows.Forms.Padding(4);
            this.groupMainUmzuege.Name = "groupMainUmzuege";
            this.groupMainUmzuege.Padding = new System.Windows.Forms.Padding(4);
            this.groupMainUmzuege.Size = new System.Drawing.Size(755, 175);
            this.groupMainUmzuege.TabIndex = 3;
            this.groupMainUmzuege.TabStop = false;
            this.groupMainUmzuege.Text = "Umzüge";
            // 
            // buttonMainUmzuegeShow
            // 
            this.buttonMainUmzuegeShow.Location = new System.Drawing.Point(501, 23);
            this.buttonMainUmzuegeShow.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainUmzuegeShow.Name = "buttonMainUmzuegeShow";
            this.buttonMainUmzuegeShow.Size = new System.Drawing.Size(239, 144);
            this.buttonMainUmzuegeShow.TabIndex = 3;
            this.buttonMainUmzuegeShow.Text = "Übersichten";
            this.buttonMainUmzuegeShow.UseVisualStyleBackColor = true;
            this.buttonMainUmzuegeShow.Click += new System.EventHandler(this.buttonMainUmzuegeShow_Click);
            // 
            // buttonMainUmzuegeChange
            // 
            this.buttonMainUmzuegeChange.Location = new System.Drawing.Point(255, 23);
            this.buttonMainUmzuegeChange.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainUmzuegeChange.Name = "buttonMainUmzuegeChange";
            this.buttonMainUmzuegeChange.Size = new System.Drawing.Size(239, 144);
            this.buttonMainUmzuegeChange.TabIndex = 1;
            this.buttonMainUmzuegeChange.Text = "Suchen / Ändern";
            this.buttonMainUmzuegeChange.UseVisualStyleBackColor = true;
            this.buttonMainUmzuegeChange.Click += new System.EventHandler(this.buttonMainUmzuegeChange_Click);
            // 
            // buttonMainUmzuegeAdd
            // 
            this.buttonMainUmzuegeAdd.Location = new System.Drawing.Point(8, 23);
            this.buttonMainUmzuegeAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainUmzuegeAdd.Name = "buttonMainUmzuegeAdd";
            this.buttonMainUmzuegeAdd.Size = new System.Drawing.Size(239, 144);
            this.buttonMainUmzuegeAdd.TabIndex = 0;
            this.buttonMainUmzuegeAdd.Text = "Anlegen";
            this.buttonMainUmzuegeAdd.UseVisualStyleBackColor = true;
            this.buttonMainUmzuegeAdd.Click += new System.EventHandler(this.buttonMainUmzuegeAdd_Click);
            // 
            // groupMainKunden
            // 
            this.groupMainKunden.Controls.Add(this.buttonMainKundenShow);
            this.groupMainKunden.Controls.Add(this.buttonMainKundenChange);
            this.groupMainKunden.Controls.Add(this.buttonMainKundenAdd);
            this.groupMainKunden.Location = new System.Drawing.Point(164, 22);
            this.groupMainKunden.Margin = new System.Windows.Forms.Padding(4);
            this.groupMainKunden.Name = "groupMainKunden";
            this.groupMainKunden.Padding = new System.Windows.Forms.Padding(4);
            this.groupMainKunden.Size = new System.Drawing.Size(755, 175);
            this.groupMainKunden.TabIndex = 1;
            this.groupMainKunden.TabStop = false;
            this.groupMainKunden.Text = "Kunden";
            // 
            // buttonMainKundenShow
            // 
            this.buttonMainKundenShow.Location = new System.Drawing.Point(501, 23);
            this.buttonMainKundenShow.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainKundenShow.Name = "buttonMainKundenShow";
            this.buttonMainKundenShow.Size = new System.Drawing.Size(239, 144);
            this.buttonMainKundenShow.TabIndex = 2;
            this.buttonMainKundenShow.Text = "Übersichten";
            this.buttonMainKundenShow.UseVisualStyleBackColor = true;
            this.buttonMainKundenShow.Click += new System.EventHandler(this.buttonMainKundenShow_Click);
            // 
            // buttonMainKundenChange
            // 
            this.buttonMainKundenChange.Location = new System.Drawing.Point(255, 23);
            this.buttonMainKundenChange.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainKundenChange.Name = "buttonMainKundenChange";
            this.buttonMainKundenChange.Size = new System.Drawing.Size(239, 144);
            this.buttonMainKundenChange.TabIndex = 1;
            this.buttonMainKundenChange.Text = "Suchen / Ändern";
            this.buttonMainKundenChange.UseVisualStyleBackColor = true;
            this.buttonMainKundenChange.Click += new System.EventHandler(this.buttonMainKundenChange_Click);
            // 
            // buttonMainKundenAdd
            // 
            this.buttonMainKundenAdd.Location = new System.Drawing.Point(8, 23);
            this.buttonMainKundenAdd.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMainKundenAdd.Name = "buttonMainKundenAdd";
            this.buttonMainKundenAdd.Size = new System.Drawing.Size(239, 144);
            this.buttonMainKundenAdd.TabIndex = 0;
            this.buttonMainKundenAdd.Text = "Anlegen";
            this.buttonMainKundenAdd.UseVisualStyleBackColor = true;
            this.buttonMainKundenAdd.Click += new System.EventHandler(this.buttonMainKundenAdd_Click);
            // 
            // groupMainBenutzer
            // 
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerNora);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerSonst);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerJan);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerEva);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerJonas);
            this.groupMainBenutzer.Controls.Add(this.radioMainBenutzerRita);
            this.groupMainBenutzer.Location = new System.Drawing.Point(16, 15);
            this.groupMainBenutzer.Margin = new System.Windows.Forms.Padding(4);
            this.groupMainBenutzer.Name = "groupMainBenutzer";
            this.groupMainBenutzer.Padding = new System.Windows.Forms.Padding(4);
            this.groupMainBenutzer.Size = new System.Drawing.Size(140, 230);
            this.groupMainBenutzer.TabIndex = 0;
            this.groupMainBenutzer.TabStop = false;
            this.groupMainBenutzer.Text = "Benutzer";
            // 
            // radioMainBenutzerNora
            // 
            this.radioMainBenutzerNora.AutoSize = true;
            this.radioMainBenutzerNora.Location = new System.Drawing.Point(8, 138);
            this.radioMainBenutzerNora.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerNora.Name = "radioMainBenutzerNora";
            this.radioMainBenutzerNora.Size = new System.Drawing.Size(60, 21);
            this.radioMainBenutzerNora.TabIndex = 5;
            this.radioMainBenutzerNora.Text = "Nora";
            this.radioMainBenutzerNora.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerSonst
            // 
            this.radioMainBenutzerSonst.AutoSize = true;
            this.radioMainBenutzerSonst.Checked = true;
            this.radioMainBenutzerSonst.Location = new System.Drawing.Point(8, 166);
            this.radioMainBenutzerSonst.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerSonst.Name = "radioMainBenutzerSonst";
            this.radioMainBenutzerSonst.Size = new System.Drawing.Size(84, 21);
            this.radioMainBenutzerSonst.TabIndex = 4;
            this.radioMainBenutzerSonst.TabStop = true;
            this.radioMainBenutzerSonst.Text = "Sonstige";
            this.radioMainBenutzerSonst.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerJan
            // 
            this.radioMainBenutzerJan.AutoSize = true;
            this.radioMainBenutzerJan.Location = new System.Drawing.Point(8, 110);
            this.radioMainBenutzerJan.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerJan.Name = "radioMainBenutzerJan";
            this.radioMainBenutzerJan.Size = new System.Drawing.Size(52, 21);
            this.radioMainBenutzerJan.TabIndex = 3;
            this.radioMainBenutzerJan.Text = "Jan";
            this.radioMainBenutzerJan.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerEva
            // 
            this.radioMainBenutzerEva.AutoSize = true;
            this.radioMainBenutzerEva.Location = new System.Drawing.Point(8, 81);
            this.radioMainBenutzerEva.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerEva.Name = "radioMainBenutzerEva";
            this.radioMainBenutzerEva.Size = new System.Drawing.Size(53, 21);
            this.radioMainBenutzerEva.TabIndex = 2;
            this.radioMainBenutzerEva.Text = "Eva";
            this.radioMainBenutzerEva.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerJonas
            // 
            this.radioMainBenutzerJonas.AutoSize = true;
            this.radioMainBenutzerJonas.Location = new System.Drawing.Point(8, 53);
            this.radioMainBenutzerJonas.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerJonas.Name = "radioMainBenutzerJonas";
            this.radioMainBenutzerJonas.Size = new System.Drawing.Size(67, 21);
            this.radioMainBenutzerJonas.TabIndex = 1;
            this.radioMainBenutzerJonas.Text = "Jonas";
            this.radioMainBenutzerJonas.UseVisualStyleBackColor = true;
            // 
            // radioMainBenutzerRita
            // 
            this.radioMainBenutzerRita.AutoSize = true;
            this.radioMainBenutzerRita.Location = new System.Drawing.Point(8, 25);
            this.radioMainBenutzerRita.Margin = new System.Windows.Forms.Padding(4);
            this.radioMainBenutzerRita.Name = "radioMainBenutzerRita";
            this.radioMainBenutzerRita.Size = new System.Drawing.Size(54, 21);
            this.radioMainBenutzerRita.TabIndex = 0;
            this.radioMainBenutzerRita.Text = "Rita";
            this.radioMainBenutzerRita.UseVisualStyleBackColor = true;
            this.radioMainBenutzerRita.CheckedChanged += new System.EventHandler(this.radioMainBenutzerRita_CheckedChanged);
            // 
            // textMainLog
            // 
            this.textMainLog.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textMainLog.Location = new System.Drawing.Point(16, 4);
            this.textMainLog.Margin = new System.Windows.Forms.Padding(4);
            this.textMainLog.Multiline = true;
            this.textMainLog.Name = "textMainLog";
            this.textMainLog.ReadOnly = true;
            this.textMainLog.Size = new System.Drawing.Size(1600, 146);
            this.textMainLog.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1779, 774);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "mainForm";
            this.Text = "Hauptmenü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupMainKartonagen.ResumeLayout(false);
            this.groupMainUmzuege.ResumeLayout(false);
            this.groupMainKunden.ResumeLayout(false);
            this.groupMainBenutzer.ResumeLayout(false);
            this.groupMainBenutzer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupMainKartonagen;
        private System.Windows.Forms.Button buttonMainKartonagenChange;
        private System.Windows.Forms.Button buttonMainKartonagenAdd;
        private System.Windows.Forms.GroupBox groupMainUmzuege;
        private System.Windows.Forms.Button buttonMainUmzuegeChange;
        private System.Windows.Forms.Button buttonMainUmzuegeAdd;
        private System.Windows.Forms.GroupBox groupMainKunden;
        private System.Windows.Forms.Button buttonMainKundenChange;
        private System.Windows.Forms.Button buttonMainKundenAdd;
        private System.Windows.Forms.GroupBox groupMainBenutzer;
        private System.Windows.Forms.RadioButton radioMainBenutzerSonst;
        private System.Windows.Forms.RadioButton radioMainBenutzerJan;
        private System.Windows.Forms.RadioButton radioMainBenutzerEva;
        private System.Windows.Forms.RadioButton radioMainBenutzerJonas;
        private System.Windows.Forms.RadioButton radioMainBenutzerRita;
        private System.Windows.Forms.TextBox textMainLog;
        private System.Windows.Forms.Button buttonMainKundenShow;
        private System.Windows.Forms.Button buttonMainUmzuegeShow;
        private System.Windows.Forms.Button buttonSonderfunktion;
        private System.Windows.Forms.Button buttonLaufzettel;
        private System.Windows.Forms.Button buttonLaufKarton;
        private System.Windows.Forms.Button buttonErinnerungen;
        private System.Windows.Forms.Button buttonUebersichtKartons;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button PDFRead;
        private System.Windows.Forms.RadioButton radioMainBenutzerNora;
        private System.Windows.Forms.Button button1;
    }
}

