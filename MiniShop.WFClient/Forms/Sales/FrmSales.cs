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

namespace MiniShop.WFClient.Forms.Sales
{
    public partial class FrmSales : Form
    {
        private readonly ApiClient _api;
        private readonly List<(int productId, string name, decimal price, int qty)> _items = new();

        public FrmSales(string token)
        {
            InitializeComponent();
            _api = new ApiClient();
            _api.SetToken(token);
        }

        private async void FrmSales_Load(object sender, EventArgs e)
        {
            var result = await _api.GetAsync<JsonElement>("api/products?page=1&pageSize=100");
            if (result.TryGetProperty("items", out var items))
            {
                var products = items.EnumerateArray()
                    .Select(p => new
                    {
                        Id = p.GetProperty("id").GetInt32(),
                        Name = p.GetProperty("name").GetString(),
                        Price = p.GetProperty("price").GetDecimal()
                    })
                    .ToList();

                cmbProducts.DataSource = products;
                cmbProducts.DisplayMember = "Name";
                cmbProducts.ValueMember = "Id";
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null) return;

            var selected = (dynamic)cmbProducts.SelectedItem;
            int id = selected.Id;
            string name = selected.Name;
            decimal price = selected.Price;
            int qty = (int)numQty.Value;

            if (qty <= 0) return;

            _items.Add((id, name, price, qty));
            dgvItems.DataSource = null;
            dgvItems.DataSource = _items.Select(i => new
            {
                Producto = i.name,
                Cantidad = i.qty,
                Precio = i.price,
                Subtotal = i.price * i.qty
            }).ToList();

            lblTotal.Text = $"Total: {_items.Sum(i => i.price * i.qty):C}";
        }

        private async void btnSaveSale_Click(object sender, EventArgs e)
        {
            if (_items.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.");
                return;
            }

            var sale = new
            {
                items = _items.Select(i => new { productId = i.productId, quantity = i.qty }).ToList()
            };

            var response = await _api.PostAsync<JsonElement>("api/sales", sale);

            MessageBox.Show("Venta registrada correctamente.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _items.Clear();
            dgvItems.DataSource = null;
            lblTotal.Text = "Total: $0";
        }

        private async void cmbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem is not null)
            {
                var selected = (dynamic)cmbProducts.SelectedItem;
                int id = selected.Id;
                var product = await _api.GetAsync<JsonElement>($"api/products/{id}");

                if (product.ValueKind != JsonValueKind.Undefined)
                {
                    int stock = product.GetProperty("stock").GetInt32();
                    numQty.Maximum = stock;
                }
            }
        }
    }
}
