namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewOpiskelijat = new DataGridView();
            comboBoxOpiskelijaryhmat = new ComboBox();
            button1 = new Button();
            textBoxEtunimi = new TextBox();
            textBoxSukunimi = new TextBox();
            buttonPoistaOpiskelija = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOpiskelijat).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewOpiskelijat
            // 
            dataGridViewOpiskelijat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOpiskelijat.Location = new Point(92, 103);
            dataGridViewOpiskelijat.Name = "dataGridViewOpiskelijat";
            dataGridViewOpiskelijat.Size = new Size(600, 293);
            dataGridViewOpiskelijat.TabIndex = 0;
            // 
            // comboBoxOpiskelijaryhmat
            // 
            comboBoxOpiskelijaryhmat.AccessibleName = " gaybblockgra+";
            comboBoxOpiskelijaryhmat.FormattingEnabled = true;
            comboBoxOpiskelijaryhmat.Location = new Point(279, 43);
            comboBoxOpiskelijaryhmat.Name = "comboBoxOpiskelijaryhmat";
            comboBoxOpiskelijaryhmat.Size = new Size(234, 23);
            comboBoxOpiskelijaryhmat.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(181, 43);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBoxEtunimi
            // 
            textBoxEtunimi.Location = new Point(581, 44);
            textBoxEtunimi.Name = "textBoxEtunimi";
            textBoxEtunimi.Size = new Size(100, 23);
            textBoxEtunimi.TabIndex = 3;
            // 
            // textBoxSukunimi
            // 
            textBoxSukunimi.Location = new Point(581, 74);
            textBoxSukunimi.Name = "textBoxSukunimi";
            textBoxSukunimi.Size = new Size(100, 23);
            textBoxSukunimi.TabIndex = 4;
            // 
            // buttonPoistaOpiskelija
            // 
            buttonPoistaOpiskelija.Location = new Point(181, 72);
            buttonPoistaOpiskelija.Name = "buttonPoistaOpiskelija";
            buttonPoistaOpiskelija.Size = new Size(75, 23);
            buttonPoistaOpiskelija.TabIndex = 5;
            buttonPoistaOpiskelija.Text = "Poista Opiskelija";
            buttonPoistaOpiskelija.UseVisualStyleBackColor = true;
            buttonPoistaOpiskelija.Click += buttonPoistaOpiskelija_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonPoistaOpiskelija);
            Controls.Add(textBoxSukunimi);
            Controls.Add(textBoxEtunimi);
            Controls.Add(button1);
            Controls.Add(comboBoxOpiskelijaryhmat);
            Controls.Add(dataGridViewOpiskelijat);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridViewOpiskelijat).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewOpiskelijat;
        private ComboBox comboBoxOpiskelijaryhmat;
        private Button button1;
        private TextBox textBoxEtunimi;
        private TextBox textBoxSukunimi;
        private Button buttonPoistaOpiskelija;
    }
}
