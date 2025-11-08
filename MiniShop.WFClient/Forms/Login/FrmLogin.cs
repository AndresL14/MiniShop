using MiniShop.WFClient.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniShop.WFClient.Forms.Login
{
    public partial class FrmLogin : Form
    {
        private readonly ApiClient _api;

        public string? Token { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            _api = new ApiClient();
        }

        private async void btnLogin_Click_1(object sender, EventArgs e)
        {
            var loginData = new
            {
                username = txtUsername.Text.Trim(),
                password = txtPassword.Text.Trim()
            };

            try
            {
                var response = await _api.PostAsync<JsonElement>("api/auth/login", loginData);
                if (response.TryGetProperty("token", out var tokenProp))
                {
                    Token = tokenProp.GetString();
                    MessageBox.Show("Inicio de sesión exitoso.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la API:\n{ex.Message}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new FrmRegister();
            frm.ShowDialog();
        }
    }
}
