using MiniShop.WFClient.Forms.Login;
using MiniShop.WFClient.Forms.Productos;
using MiniShop.WFClient.Forms.Sales;

namespace MiniShop.WFClient
{
    public partial class FrmMain : Form
    {

        private string _token;

        public FrmMain(string token)
        {
            InitializeComponent();
            _token = token;
        }

        private void productosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            var frm = new FrmProducts(_token);
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new FrmSales(_token);
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var frm = new FrmSalesHistory(_token);
            frm.MdiParent = this;
            frm.Dock = DockStyle.Fill;
            frm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar sesión?", "MiniShop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Cierra todos los formularios abiertos
                foreach (Form frm in MdiChildren)
                    frm.Close();

                // Vuelve al login
                Hide();
                var login = new FrmLogin();
                var result = login.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrEmpty(login.Token))
                {
                    _token = login.Token;
                    Show();
                }
                else
                {
                    // Si canceló el login, cerrar aplicación
                    Application.Exit();
                }
            }
        }
    }
}
