using Master_Pol.Database.Entity;

namespace Master_Pol;

public partial class PartnerSale : Form
{
    private int partnerId;
    
    public PartnerSale(int partnerId)
    {
        InitializeComponent();
        this.partnerId = partnerId;
        LoadSalesHistory();
    }
    
    private void LoadSalesHistory()
    {
        try
        {
            var db = new Database.Database(
                "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=Master_pol");
            List<Sales> salesHistory = db.GetSalesHistory(partnerId);

            if (salesHistory == null || salesHistory.Count == 0)
            {
                MessageBox.Show("История продаж пуста.", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = true
            };

            dataGridView.DataSource = salesHistory;

            if (dataGridView.Columns["ProductName"] != null)
                dataGridView.Columns["ProductName"].HeaderText = "Наименование продукции";
            if (dataGridView.Columns["Quantity"] != null) dataGridView.Columns["Quantity"].HeaderText = "Количество";
            if (dataGridView.Columns["SaleDate"] != null) dataGridView.Columns["SaleDate"].HeaderText = "Дата продажи";

            this.Controls.Add(dataGridView);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки истории продаж: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
            
    }
    
    
    
}