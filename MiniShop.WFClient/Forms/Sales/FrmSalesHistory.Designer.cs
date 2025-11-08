namespace MiniShop.WFClient.Forms.Sales
{
    partial class FrmSalesHistory
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
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            panel2 = new Panel();
            lblSummary = new Label();
            btnLoad = new Button();
            dtTo = new DateTimePicker();
            dtFrom = new DateTimePicker();
            label3 = new Label();
            dgvSales = new DataGridView();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 16);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Desde:";
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(988, 60);
            panel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(368, 9);
            label2.Name = "label2";
            label2.Size = new Size(256, 37);
            label2.TabIndex = 0;
            label2.Text = "Historial de ventas";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnLoad);
            panel2.Controls.Add(dtTo);
            panel2.Controls.Add(dtFrom);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(988, 47);
            panel2.TabIndex = 2;
            // 
            // lblSummary
            // 
            lblSummary.AutoSize = true;
            lblSummary.Location = new Point(12, 12);
            lblSummary.MinimumSize = new Size(15, 15);
            lblSummary.Name = "lblSummary";
            lblSummary.Size = new Size(15, 15);
            lblSummary.TabIndex = 4;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(534, 10);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(116, 23);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Buscar";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dtTo
            // 
            dtTo.Location = new Point(314, 10);
            dtTo.Name = "dtTo";
            dtTo.Size = new Size(200, 23);
            dtTo.TabIndex = 2;
            // 
            // dtFrom
            // 
            dtFrom.Location = new Point(51, 10);
            dtFrom.Name = "dtFrom";
            dtFrom.Size = new Size(200, 23);
            dtFrom.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(268, 16);
            label3.Name = "label3";
            label3.Size = new Size(40, 15);
            label3.TabIndex = 1;
            label3.Text = "Hasta:";
            // 
            // dgvSales
            // 
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSales.Location = new Point(0, 113);
            dgvSales.Name = "dgvSales";
            dgvSales.ReadOnly = true;
            dgvSales.RowHeadersVisible = false;
            dgvSales.Size = new Size(988, 286);
            dgvSales.TabIndex = 3;
            dgvSales.CellDoubleClick += dgvSales_CellDoubleClick;
            // 
            // panel3
            // 
            panel3.Controls.Add(lblSummary);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 405);
            panel3.Name = "panel3";
            panel3.Size = new Size(988, 45);
            panel3.TabIndex = 5;
            // 
            // FrmSalesHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 450);
            Controls.Add(panel3);
            Controls.Add(dgvSales);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmSalesHistory";
            Text = "FrmSalesHistory";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSales).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Label label2;
        private Panel panel2;
        private DateTimePicker dtTo;
        private DateTimePicker dtFrom;
        private Label label3;
        private Button btnLoad;
        private DataGridView dgvSales;
        private Label lblSummary;
        private Panel panel3;
    }
}