namespace MiniShop.WFClient.Forms.Sales
{
    partial class FrmSales
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
            cmbProducts = new ComboBox();
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            btnAddItem = new Button();
            label3 = new Label();
            numQty = new NumericUpDown();
            panel3 = new Panel();
            btnSaveSale = new Button();
            lblTotal = new Label();
            dgvItems = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // cmbProducts
            // 
            cmbProducts.FormattingEnabled = true;
            cmbProducts.Location = new Point(73, 15);
            cmbProducts.Name = "cmbProducts";
            cmbProducts.Size = new Size(206, 23);
            cmbProducts.TabIndex = 0;
            cmbProducts.SelectedIndexChanged += cmbProducts_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(912, 48);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(358, 9);
            label1.Name = "label1";
            label1.Size = new Size(210, 37);
            label1.TabIndex = 0;
            label1.Text = "Registrar venta";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 18);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 2;
            label2.Text = "Producto:";
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAddItem);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(numQty);
            panel2.Controls.Add(cmbProducts);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 48);
            panel2.Name = "panel2";
            panel2.Size = new Size(912, 52);
            panel2.TabIndex = 3;
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(493, 15);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(75, 23);
            btnAddItem.TabIndex = 5;
            btnAddItem.Text = "Agregar";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(295, 18);
            label3.Name = "label3";
            label3.Size = new Size(58, 15);
            label3.TabIndex = 4;
            label3.Text = "Cantidad:";
            // 
            // numQty
            // 
            numQty.Location = new Point(359, 15);
            numQty.Name = "numQty";
            numQty.Size = new Size(120, 23);
            numQty.TabIndex = 3;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnSaveSale);
            panel3.Controls.Add(lblTotal);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 409);
            panel3.Name = "panel3";
            panel3.Size = new Size(912, 41);
            panel3.TabIndex = 4;
            // 
            // btnSaveSale
            // 
            btnSaveSale.Location = new Point(761, 9);
            btnSaveSale.Name = "btnSaveSale";
            btnSaveSale.Size = new Size(119, 23);
            btnSaveSale.TabIndex = 1;
            btnSaveSale.Text = "Registrar venta";
            btnSaveSale.UseVisualStyleBackColor = true;
            btnSaveSale.Click += btnSaveSale_Click;
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(12, 13);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(0, 15);
            lblTotal.TabIndex = 0;
            // 
            // dgvItems
            // 
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Dock = DockStyle.Bottom;
            dgvItems.Location = new Point(0, 106);
            dgvItems.Name = "dgvItems";
            dgvItems.ReadOnly = true;
            dgvItems.RowHeadersVisible = false;
            dgvItems.Size = new Size(912, 303);
            dgvItems.TabIndex = 5;
            // 
            // FrmSales
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(912, 450);
            Controls.Add(dgvItems);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmSales";
            Text = "FrmSales";
            Load += FrmSales_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numQty).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbProducts;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private Panel panel2;
        private Label label3;
        private NumericUpDown numQty;
        private Panel panel3;
        private Button btnAddItem;
        private Label lblTotal;
        private DataGridView dgvItems;
        private Button btnSaveSale;
    }
}