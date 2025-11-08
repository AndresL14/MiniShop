using MiniShop.WFClient.Services;
using System.Text.Json;

namespace MiniShop.WFClient.Forms.Productos
{
    public partial class FrmProductEdit : Form
    {
        private readonly ApiClient _api;
        private readonly int? _productId;
        private string? _imagePath;

        public bool Editar = false;

        public FrmProductEdit(ApiClient api, int? productId = null)
        {
            InitializeComponent();
            _api = api;
            _productId = productId;

            if (_productId.HasValue)
                Text = "Editar producto";
            else
                Text = "Agregar producto";
        }

        private async void FrmProductEdit_Load(object sender, EventArgs e)
        {
            if (_productId.HasValue)
                await LoadProductData(_productId.Value);
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Imágenes|*.jpg;*.png;*.jpeg",
                Title = "Seleccionar imagen"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imagePath = ofd.FileName;
                picImage.Image = Image.FromFile(_imagePath);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validadatos())
                    return;

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(txtName.Text.Trim()), "Name");
                content.Add(new StringContent(numPrice.Value.ToString()), "Price");
                content.Add(new StringContent(numStock.Value.ToString()), "Stock");

                if (!string.IsNullOrEmpty(_imagePath))
                {
                    var fileStream = File.OpenRead(_imagePath);
                    content.Add(new StreamContent(fileStream), "Image", Path.GetFileName(_imagePath));
                }

                HttpResponseMessage? response;

                if (_productId == null)
                    response = await _api.PostMultipartAsync("api/products", content);
                else
                    response = await _api.PutMultipartAsync($"api/products/{_productId}", content);

                if (response!.IsSuccessStatusCode)
                {
                    MessageBox.Show("Producto guardado correctamente.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    var err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error al guardar: {err}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el producto:\n{ex.Message}");
            }
        }

        private bool Validadatos()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numPrice.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (numStock.Value < 0)
            {
                MessageBox.Show("El stock no puede ser negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private async Task LoadProductData(int id)
        {
            try
            {
                var product = await _api.GetAsync<JsonElement>($"api/products/{id}");
                if (product.ValueKind == JsonValueKind.Undefined)
                {
                    MessageBox.Show("No se pudo cargar el producto.", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                txtName.Text = product.GetProperty("name").GetString();
                numPrice.Value = Convert.ToDecimal(product.GetProperty("price").GetDouble());
                numStock.Value = product.GetProperty("stock").GetInt32();

                if (product.TryGetProperty("imageUrl", out var imgProp))
                {
                    var imageUrl = imgProp.GetString();
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        try
                        {
                            using var client = new HttpClient();
                            var imgData = await client.GetByteArrayAsync($"{AppConfig.ApiBaseUrl.TrimEnd('/')}{imageUrl}");
                            using var ms = new MemoryStream(imgData);
                            picImage.Image = Image.FromStream(ms);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos del producto:\n{ex.Message}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
