using MiniShop.WFClient.Forms.Productos;

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
    }
}
