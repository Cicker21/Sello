namespace Sello
{
    partial class contras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(contras));
            t_pwd = new TextBox();
            label1 = new Label();
            c_mayus = new CheckBox();
            c_minus = new CheckBox();
            c_nums = new CheckBox();
            c_espec = new CheckBox();
            trackBar1 = new TrackBar();
            label3 = new Label();
            numericUpDown1 = new NumericUpDown();
            noAleatorio_tb = new TextBox();
            button1 = new Button();
            noaleatorio = new CheckBox();
            button2 = new Button();
            notEngh_lbl = new Label();
            invalidChar_lbl = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // t_pwd
            // 
            t_pwd.Location = new Point(12, 27);
            t_pwd.Name = "t_pwd";
            t_pwd.Size = new Size(237, 23);
            t_pwd.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 1;
            label1.Text = "Contraseña";
            // 
            // c_mayus
            // 
            c_mayus.AutoSize = true;
            c_mayus.Checked = true;
            c_mayus.CheckState = CheckState.Checked;
            c_mayus.Location = new Point(364, 31);
            c_mayus.Name = "c_mayus";
            c_mayus.Size = new Size(88, 19);
            c_mayus.TabIndex = 2;
            c_mayus.Text = "Mayúsculas";
            c_mayus.UseVisualStyleBackColor = true;
            c_mayus.Click += alMenosUno;
            // 
            // c_minus
            // 
            c_minus.AutoSize = true;
            c_minus.Checked = true;
            c_minus.CheckState = CheckState.Checked;
            c_minus.Location = new Point(364, 56);
            c_minus.Name = "c_minus";
            c_minus.Size = new Size(86, 19);
            c_minus.TabIndex = 3;
            c_minus.Text = "Minúsculas";
            c_minus.UseVisualStyleBackColor = true;
            c_minus.Click += alMenosUno;
            // 
            // c_nums
            // 
            c_nums.AutoSize = true;
            c_nums.Checked = true;
            c_nums.CheckState = CheckState.Checked;
            c_nums.Location = new Point(364, 81);
            c_nums.Name = "c_nums";
            c_nums.Size = new Size(75, 19);
            c_nums.TabIndex = 4;
            c_nums.Text = "Números";
            c_nums.UseVisualStyleBackColor = true;
            c_nums.Click += alMenosUno;
            // 
            // c_espec
            // 
            c_espec.AutoSize = true;
            c_espec.Checked = true;
            c_espec.CheckState = CheckState.Checked;
            c_espec.Location = new Point(364, 106);
            c_espec.Name = "c_espec";
            c_espec.Size = new Size(75, 19);
            c_espec.TabIndex = 6;
            c_espec.Text = "Simbolos";
            c_espec.UseVisualStyleBackColor = true;
            c_espec.Click += alMenosUno;
            // 
            // trackBar1
            // 
            trackBar1.BackColor = SystemColors.Control;
            trackBar1.Location = new Point(12, 112);
            trackBar1.Maximum = 40;
            trackBar1.Minimum = 1;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(246, 45);
            trackBar1.TabIndex = 2;
            trackBar1.TickStyle = TickStyle.Both;
            trackBar1.Value = 12;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 85);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 9;
            label3.Text = "Longitud";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(73, 83);
            numericUpDown1.Maximum = new decimal(new int[] { 40, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 10;
            numericUpDown1.Value = new decimal(new int[] { 12, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // noAleatorio_tb
            // 
            noAleatorio_tb.Enabled = false;
            noAleatorio_tb.Location = new Point(12, 204);
            noAleatorio_tb.Name = "noAleatorio_tb";
            noAleatorio_tb.PlaceholderText = "Texto Base";
            noAleatorio_tb.Size = new Size(246, 23);
            noAleatorio_tb.TabIndex = 11;
            noAleatorio_tb.TextChanged += noAleatorio_tb_TextChanged;
            noAleatorio_tb.KeyPress += noAleatorio_tb_KeyPress;
            // 
            // button1
            // 
            button1.Location = new Point(283, 31);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 13;
            button1.Text = "Copiar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // noaleatorio
            // 
            noaleatorio.AutoSize = true;
            noaleatorio.Location = new Point(12, 179);
            noaleatorio.Name = "noaleatorio";
            noaleatorio.Size = new Size(151, 19);
            noaleatorio.TabIndex = 14;
            noaleatorio.Text = "Contraseña no aleatoria";
            noaleatorio.UseVisualStyleBackColor = true;
            noaleatorio.CheckedChanged += noaleatorio_CheckedChanged;
            // 
            // button2
            // 
            button2.BackgroundImageLayout = ImageLayout.None;
            button2.FlatStyle = FlatStyle.System;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.ForeColor = SystemColors.ActiveCaption;
            button2.Location = new Point(252, 27);
            button2.Margin = new Padding(0);
            button2.Name = "button2";
            button2.Size = new Size(28, 27);
            button2.TabIndex = 10;
            button2.Text = "🔄️";
            button2.TextAlign = ContentAlignment.TopLeft;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // notEngh_lbl
            // 
            notEngh_lbl.AutoSize = true;
            notEngh_lbl.BackColor = Color.FromArgb(255, 255, 192);
            notEngh_lbl.BorderStyle = BorderStyle.FixedSingle;
            notEngh_lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            notEngh_lbl.Location = new Point(35, 235);
            notEngh_lbl.Name = "notEngh_lbl";
            notEngh_lbl.Padding = new Padding(8, 4, 4, 4);
            notEngh_lbl.Size = new Size(223, 25);
            notEngh_lbl.TabIndex = 16;
            notEngh_lbl.Text = "¡Cantidad de carácteres insuficiente!";
            notEngh_lbl.Visible = false;
            // 
            // invalidChar_lbl
            // 
            invalidChar_lbl.AutoSize = true;
            invalidChar_lbl.BackColor = Color.LightCyan;
            invalidChar_lbl.BorderStyle = BorderStyle.FixedSingle;
            invalidChar_lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            invalidChar_lbl.Location = new Point(264, 202);
            invalidChar_lbl.Name = "invalidChar_lbl";
            invalidChar_lbl.Padding = new Padding(8, 4, 4, 4);
            invalidChar_lbl.Size = new Size(128, 25);
            invalidChar_lbl.TabIndex = 17;
            invalidChar_lbl.Text = "Carácter inválido ' '";
            invalidChar_lbl.Visible = false;
            // 
            // contras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 299);
            Controls.Add(invalidChar_lbl);
            Controls.Add(notEngh_lbl);
            Controls.Add(button2);
            Controls.Add(noaleatorio);
            Controls.Add(button1);
            Controls.Add(noAleatorio_tb);
            Controls.Add(numericUpDown1);
            Controls.Add(label3);
            Controls.Add(trackBar1);
            Controls.Add(c_espec);
            Controls.Add(c_nums);
            Controls.Add(c_minus);
            Controls.Add(c_mayus);
            Controls.Add(label1);
            Controls.Add(t_pwd);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "contras";
            Text = "contras";
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox t_pwd;
        private Label label1;
        private CheckBox c_mayus;
        private CheckBox c_minus;
        private CheckBox c_nums;
        private CheckBox c_espec;
        private TrackBar trackBar1;
        private Label label3;
        private NumericUpDown numericUpDown1;
        private TextBox noAleatorio_tb;
        private Button button1;
        private CheckBox noaleatorio;
        private Button button2;
        private Label notEngh_lbl;
        private Label invalidChar_lbl;
    }
}