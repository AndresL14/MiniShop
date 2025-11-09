using MiniShop.WFClient.Services;
using System.Data;
using System.Text.Json;

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
            await CargarDatos();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null)
                return;

            var selected = (dynamic)cmbProducts.SelectedItem;
            int id = selected.Id;
            string name = selected.Name;
            decimal price = selected.Price;
            int qty = (int)numQty.Value;

            if (qty <= 0)
            {
                MessageBox.Show("Debe ingresar una cantidad válida.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = _items.FirstOrDefault(i => i.productId == id);

            if (existing.productId != 0)
            {
                int nuevaCantidad = existing.qty + qty;

                int stockDisponible = selected.Stock ?? 0;

                if (stockDisponible > 0 && nuevaCantidad > stockDisponible)
                {
                    MessageBox.Show($"Stock insuficiente. Solo hay {stockDisponible} unidades disponibles.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _items.Remove(existing);
                _items.Add((id, name, price, nuevaCantidad));
            }
            else
            {
                int stockDisponible = selected.Stock ?? 0;

                if (stockDisponible > 0 && qty > stockDisponible)
                {
                    MessageBox.Show($"Stock insuficiente. Solo hay {stockDisponible} unidades disponibles.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _items.Add((id, name, price, qty));
            }

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

            await CargarDatos();
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
                numQty.Value = 0;
            }
        }

        private async Task CargarDatos()
        {

            var result = await _api.GetAsync<JsonElement>("api/products?page=1&pageSize=100");
            if (result.TryGetProperty("items", out var items))
            {
                var products = items.EnumerateArray()
                    .Select(p => new
                    {
                        Id = p.GetProperty("id").GetInt32(),
                        Name = p.GetProperty("name").GetString(),
                        Price = p.GetProperty("price").GetDecimal(),
                        Stock = p.GetProperty("stock").GetInt32()
                    })
                    .ToList();

                cmbProducts.DataSource = products;
                cmbProducts.DisplayMember = "Name";
                cmbProducts.ValueMember = "Id";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener el nombre del producto seleccionado
            string productName = dgvItems.CurrentRow.Cells["Producto"].Value.ToString() ?? "";

            // Confirmar eliminación
            if (MessageBox.Show($"¿Desea eliminar '{productName}' del carrito?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            // Obtener el ID del producto
            var selectedProduct = _items.FirstOrDefault(i => i.name == productName);

            if (selectedProduct.productId != 0)
                _items.Remove(selectedProduct);

            // Refrescar la grid
            dgvItems.DataSource = null;
            dgvItems.DataSource = _items.Select(i => new
            {
                Producto = i.name,
                Cantidad = i.qty,
                Precio = i.price,
                Subtotal = i.price * i.qty
            }).ToList();

            // Recalcular total
            lblTotal.Text = $"Total: {_items.Sum(i => i.price * i.qty):C}";
        }
    }
}
