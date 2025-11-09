using MiniShop.WFClient.Services;

using System.Text.Json;


namespace MiniShop.WFClient.Forms.Login
{
    public partial class FrmRegister : Form
    {
        private readonly ApiClient _api;

        public FrmRegister()
        {
            InitializeComponent();
            _api = new ApiClient();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = new
            {
                username = txtUser.Text.Trim(),
                password = txtPassword.Text.Trim()
            };

            try
            {
                var response = await _api.PostAsync<JsonElement>("api/auth/register", data);
                MessageBox.Show("Usuario registrado correctamente.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario:\n{ex.Message}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
