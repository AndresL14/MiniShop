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

namespace MiniShop.WFClient.Forms.Productos
{
    public partial class FrmProducts : Form
    {
        private readonly ApiClient _api;

        public FrmProducts(string token)
        {
            InitializeComponent();
            _api = new ApiClient();
            _api.SetToken(token);
        }

        private async void btnLoad_Click_1(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var frm = new FrmProductEdit(_api);
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadProducts();
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para editar.");
                return;
            }

            var id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);
            var frm = new FrmProductEdit(_api, id);
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadProducts();
        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
                return;
            }

            var id = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);
            if (MessageBox.Show("¿Eliminar producto?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var ok = await _api.DeleteAsync($"api/products/{id}");
                if (ok)
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                    await LoadProducts();
                }
                else
                    MessageBox.Show("Error al eliminar producto.");
            }
        }


        private async Task LoadProducts()
        {
            try
            {
                var result = await _api.GetAsync<JsonElement>("api/products?page=1&pageSize=50");

                if (result.ValueKind != JsonValueKind.Undefined && result.TryGetProperty("items", out var items))
                {
                    var list = items.EnumerateArray()
                        .Select(p => new
                        {
                            Id = p.GetProperty("id").GetInt32(),
                            Nombre = p.GetProperty("name").GetString(),
                            Precio = p.GetProperty("price").GetDecimal(),
                            Stock = p.GetProperty("stock").GetInt32(),
                            Imagen = p.TryGetProperty("imageUrl", out var img) ? img.GetString() : ""
                        })
                        .ToList();

                    dgvProducts.DataSource = list;

                    // Ajustar columnas automáticamente
                    dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvProducts.Columns["Id"].Visible = true;
                    dgvProducts.Columns["Imagen"].HeaderText = "Imagen (URL)";
                }
                else
                {
                    dgvProducts.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar productos:\n{ex.Message}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
