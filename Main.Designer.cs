namespace Sello
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            dataGridView1 = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            textBox1 = new TextBox();
            button5 = new Button();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button6 = new Button();
            button4 = new Button();
            label3 = new Label();
            label4 = new Label();
            label2 = new Label();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 111);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(721, 290);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += toast;
            dataGridView1.CellMouseDown += edit;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 5, 1);
            tableLayoutPanel1.Controls.Add(button5, 5, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(button1, 0, 1);
            tableLayoutPanel1.Controls.Add(button2, 0, 2);
            tableLayoutPanel1.Controls.Add(button6, 2, 2);
            tableLayoutPanel1.Controls.Add(button4, 2, 1);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 3, 0);
            tableLayoutPanel1.Controls.Add(label2, 5, 0);
            tableLayoutPanel1.Controls.Add(button3, 3, 1);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 29.3103447F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70.68965F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.Size = new Size(721, 93);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel2.Location = new Point(455, 18);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 36.9863F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 63.0137F));
            tableLayoutPanel2.Size = new Size(263, 32);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(3, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(257, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button5
            // 
            button5.BackColor = Color.Lavender;
            button5.BackgroundImage = Properties.Resources.refresh_149;
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.Location = new Point(455, 56);
            button5.Name = "button5";
            button5.Size = new Size(36, 34);
            button5.TabIndex = 7;
            button5.UseVisualStyleBackColor = false;
            button5.Click += update;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 0;
            label1.Text = "Filas:";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(128, 255, 128);
            button1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(3, 18);
            button1.Name = "button1";
            button1.Size = new Size(85, 32);
            button1.TabIndex = 3;
            button1.Text = "Añadir";
            button1.UseVisualStyleBackColor = false;
            button1.Click += añadir;
            // 
            // button2
            // 
            button2.BackColor = Color.LightCoral;
            button2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(3, 56);
            button2.Name = "button2";
            button2.Size = new Size(85, 34);
            button2.TabIndex = 4;
            button2.Text = "Borrar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += borrar;
            // 
            // button6
            // 
            button6.BackColor = Color.NavajoWhite;
            button6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(94, 56);
            button6.Name = "button6";
            button6.Size = new Size(141, 34);
            button6.TabIndex = 8;
            button6.Text = "Modificar";
            button6.UseVisualStyleBackColor = false;
            button6.Click += modificar;
            // 
            // button4
            // 
            button4.BackColor = Color.LightSeaGreen;
            button4.BackgroundImage = Properties.Resources.Google_Drive_logo;
            button4.BackgroundImageLayout = ImageLayout.Zoom;
            button4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(94, 18);
            button4.Name = "button4";
            button4.Size = new Size(141, 32);
            button4.TabIndex = 6;
            button4.Text = "Google Drive";
            button4.UseVisualStyleBackColor = false;
            button4.MouseDown += drive;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(94, 0);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 9;
            label3.Text = "Datos:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(241, 0);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 10;
            label4.Text = "Heramientas:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(455, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 1;
            label2.Text = "Buscador:";
            // 
            // button3
            // 
            button3.BackColor = Color.CornflowerBlue;
            button3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(241, 18);
            button3.Name = "button3";
            button3.Size = new Size(158, 32);
            button3.TabIndex = 5;
            button3.Text = "Generar Contraseñas";
            button3.UseVisualStyleBackColor = false;
            button3.Click += generador;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(745, 413);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(603, 276);
            Name = "Main";
            Text = "Sello";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label label3;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel2;
    }
}