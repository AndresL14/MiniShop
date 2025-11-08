namespace MiniShop.WFClient.Forms.Productos
{
    partial class FrmProductEdit
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
            label2 = new Label();
            label3 = new Label();
            txtName = new TextBox();
            numPrice = new NumericUpDown();
            numStock = new NumericUpDown();
            btnSave = new Button();
            button2 = new Button();
            picImage = new PictureBox();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picImage).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 22);
            label1.Name = "label1";
            label1.Size = new Size(140, 17);
            label1.TabIndex = 0;
            label1.Text = "Nombre del producto:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(47, 17);
            label2.TabIndex = 0;
            label2.Text = "Precio:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 90);
            label3.Name = "label3";
            label3.Size = new Size(42, 17);
            label3.TabIndex = 0;
            label3.Text = "Stock:";
            // 
            // txtName
            // 
            txtName.Location = new Point(158, 21);
            txtName.Name = "txtName";
            txtName.Size = new Size(226, 23);
            txtName.TabIndex = 1;
            // 
            // numPrice
            // 
            numPrice.Location = new Point(158, 57);
            numPrice.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(226, 23);
            numPrice.TabIndex = 2;
            // 
            // numStock
            // 
            numStock.Location = new Point(158, 90);
            numStock.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numStock.Name = "numStock";
            numStock.Size = new Size(226, 23);
            numStock.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(107, 286);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 3;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // button2
            // 
            button2.Location = new Point(215, 286);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // picImage
            // 
            picImage.BorderStyle = BorderStyle.FixedSingle;
            picImage.Location = new Point(107, 130);
            picImage.Name = "picImage";
            picImage.Size = new Size(183, 94);
            picImage.SizeMode = PictureBoxSizeMode.Zoom;
            picImage.TabIndex = 4;
            picImage.TabStop = false;
            // 
            // button3
            // 
            button3.Location = new Point(131, 230);
            button3.Name = "button3";
            button3.Size = new Size(140, 23);
            button3.TabIndex = 3;
            button3.Text = "Seleccionar imagen";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnSelectImage_Click;
            // 
            // FrmProductEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(396, 354);
            Controls.Add(picImage);
            Controls.Add(button2);
            Controls.Add(button3);
            Controls.Add(btnSave);
            Controls.Add(numStock);
            Controls.Add(numPrice);
            Controls.Add(txtName);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FrmProductEdit";
            Text = "Añadir producto";
            Load += FrmProductEdit_Load;
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)picImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private NumericUpDown numPrice;
        private NumericUpDown numStock;
        private Button btnSave;
        private Button button2;
        private PictureBox picImage;
        private Button button3;
        public TextBox txtName;
    }
}