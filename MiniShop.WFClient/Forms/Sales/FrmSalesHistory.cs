using MiniShop.WFClient.Services;
using System.Data;
using System.Text.Json;

namespace MiniShop.WFClient.Forms.Sales
{
    public partial class FrmSalesHistory : Form
    {
        private readonly ApiClient _api;

        public FrmSalesHistory(string token)
        {
            InitializeComponent();
            _api = new ApiClient();
            _api.SetToken(token);
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {

            string from = dtFrom.Value.ToString("yyyy-MM-dd");
            string to = dtTo.Value.ToString("yyyy-MM-dd");

            try
            {
                var report = await _api.GetAsync<JsonElement>($"api/reports/sales?from={from}&to={to}");

                if (report.ValueKind == JsonValueKind.Undefined)
                {
                    MessageBox.Show("No se pudo obtener el historial.");
                    return;
                }

                if (report.TryGetProperty("sales", out var sales))
                {
                    var list = sales.EnumerateArray()
                        .Select(s => new
                        {
                            ID = s.GetProperty("id").GetInt32(),
                            Fecha = s.GetProperty("date").GetDateTime().ToLocalTime(),
                            Usuario = s.GetProperty("user").GetString(),
                            Total = s.GetProperty("total").GetDecimal(),
                            Items = s.GetProperty("itemsCount").GetInt32()
                        }).ToList();

                    dgvSales.DataSource = list;

                    // Mostrar resumen
                    var totalGeneral = report.GetProperty("totalGeneral").GetDecimal();
                    var totalVentas = report.GetProperty("totalSales").GetInt32();

                    lblSummary.Text = $"Ventas: {totalVentas} | Total general: {totalGeneral:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial:\n{ex.Message}", "MiniShop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSalesHistory_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
        }

        private async void dgvSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int saleId = (int)dgvSales.Rows[e.RowIndex].Cells["ID"].Value;
            var sale = await _api.GetAsync<JsonElement>($"api/sales/{saleId}");

            if (sale.ValueKind != JsonValueKind.Undefined)
            {
                var detail = sale.GetProperty("items").EnumerateArray()
                    .Select(i => $"{i.GetProperty("productName").GetString()} x{i.GetProperty("quantity").GetInt32()} = {i.GetProperty("subtotal").GetDecimal():C}")
                    .ToList();

                MessageBox.Show(string.Join(Environment.NewLine, detail), $"Venta #{saleId}");
            }
        }
    }
}
