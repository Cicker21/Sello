namespace Sello
{
    partial class Estructura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Estructura));
            reset_b = new Button();
            confirm_b = new Button();
            aplicar_b = new Button();
            dataGridView1 = new DataGridView();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // reset_b
            // 
            reset_b.Anchor = AnchorStyles.Right;
            reset_b.Location = new Point(96, 90);
            reset_b.Name = "reset_b";
            reset_b.Size = new Size(119, 30);
            reset_b.TabIndex = 1;
            reset_b.Text = "Reset";
            reset_b.UseVisualStyleBackColor = true;
            reset_b.Click += reset;
            // 
            // confirm_b
            // 
            confirm_b.Anchor = AnchorStyles.Right;
            confirm_b.Enabled = false;
            confirm_b.Location = new Point(96, 54);
            confirm_b.Name = "confirm_b";
            confirm_b.Size = new Size(119, 30);
            confirm_b.TabIndex = 2;
            confirm_b.Text = "Confirmar";
            confirm_b.UseVisualStyleBackColor = true;
            confirm_b.Click += confirmar;
            // 
            // aplicar_b
            // 
            aplicar_b.Anchor = AnchorStyles.Right;
            aplicar_b.Location = new Point(96, 18);
            aplicar_b.Name = "aplicar_b";
            aplicar_b.Size = new Size(119, 30);
            aplicar_b.TabIndex = 3;
            aplicar_b.Text = "Crear Tabla";
            aplicar_b.UseVisualStyleBackColor = true;
            aplicar_b.Click += crear;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 147);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.ShowCellErrors = false;
            dataGridView1.ShowCellToolTips = false;
            dataGridView1.Size = new Size(627, 291);
            dataGridView1.TabIndex = 0;
            dataGridView1.TabStop = false;
            dataGridView1.CellMouseDown += cellDown;
            dataGridView1.CellMouseUp += cellUp;
            dataGridView1.CellValidating += dataGridView1_CellValidating;
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
            dataGridView1.RowsAdded += dataGridView1_RowsAdded;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left;
            button1.BackColor = Color.LightCoral;
            button1.Location = new Point(3, 54);
            button1.Name = "button1";
            button1.Size = new Size(119, 30);
            button1.TabIndex = 4;
            button1.Text = "Eliminar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += borrarFila;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Left;
            button2.BackColor = Color.YellowGreen;
            button2.Location = new Point(3, 18);
            button2.Name = "button2";
            button2.Size = new Size(119, 30);
            button2.TabIndex = 5;
            button2.Text = "Añadir";
            button2.UseVisualStyleBackColor = false;
            button2.Click += nuevaFila;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 6;
            label1.Text = "Última fila:";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(413, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(226, 92);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(label2);
            flowLayoutPanel2.Controls.Add(aplicar_b);
            flowLayoutPanel2.Controls.Add(confirm_b);
            flowLayoutPanel2.Controls.Add(reset_b);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(12, 12);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.RightToLeft = RightToLeft.Yes;
            flowLayoutPanel2.Size = new Size(218, 129);
            flowLayoutPanel2.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(96, 0);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.No;
            label2.Size = new Size(119, 15);
            label2.TabIndex = 7;
            label2.Text = "Nueva Tabla:";
            // 
            // Estructura
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(651, 450);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(490, 489);
            Name = "Estructura";
            Text = "Estructura";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button reset_b;
        private Button confirm_b;
        private Button aplicar_b;
        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label2;
    }
}