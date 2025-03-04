
using Master_Pol.Database.Entity;

namespace Master_Pol;

public partial class Form1 : Form
{
    private Panel mainPanel;
    private Database.Database dbHelper;
    string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin;Database=Master_pol";
    public Form1()
    {
        InitializeComponent();
        InitializeComponents();
        
        dbHelper = new Database.Database(connectionString);
        
        this.Load += PartnerCardsForm_Load;
    }
    
    private void PartnerCardsForm_Load(object sender, EventArgs e)
    {
        LoadPartners();
    }

    private void LoadPartners()
    {
        mainPanel.Controls.Clear();
            
        try
        {
            List<Partners> partners = dbHelper.GetPartners();
                
           
            for (int i = 0; i < partners.Count; i++)
            {
                CreatePartnerCard(mainPanel, i, partners[i]);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при загрузке данных: " + ex.Message, "Ошибка", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void InitializeComponents()
    {
        this.Text = "Партнеры";
        this.Size = new Size(650, 530);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;
        this.Font = new Font("Segoe UI", 9F);

       
        mainPanel = new Panel
        {
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(20, 20),
            Size = new Size(590, 420),
            Padding = new Padding(10),
            AutoScroll = true 
        };
        this.Controls.Add(mainPanel);
        
        Button refreshButton = new Button
        {
            Text = "Обновить",
            Location = new Point(20, 450),
            Size = new Size(100, 30)
        };
        refreshButton.Click += (sender, e) => LoadPartners();
        this.Controls.Add(refreshButton);
        
        Button addButton = new Button
        {
            Text = "Добавить",
            Location = new Point(130, 450),
            Size = new Size(100, 30)
        };
        addButton.Click += (sender, e) => ShowPartnerEditForm(null); 
        this.Controls.Add(addButton);
        
        
    }
    
    private void ShowPartnerEditForm(Partners partner)
    {
        PartnerEdit editForm = partner == null 
            ? new PartnerEdit() 
            : new PartnerEdit(partner); 

        if (editForm.ShowDialog() == DialogResult.OK)
        {
            LoadPartners();
        }
    }
    
    private void ShowSalesHistory(int partnerId)
    {
        PartnerSale salesHistoryForm = new PartnerSale(partnerId);
        salesHistoryForm.ShowDialog();
    }
    
    
  private void CreatePartnerCard(Panel parent, int index, Partners partner)
        {
            int yPos = 20 + (index * 130);
            
            Panel cardPanel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(20, yPos),
                Size = new Size(530, 110),
                BackColor = Color.White
            };
            
            cardPanel.Click += (sender, e) => ShowPartnerEditForm(partner);
            
            parent.Controls.Add(cardPanel);
            
            Label titleLabel = new Label
            {
                Text = $"{partner.Partner_Type} | {partner.Partner_Name}",
                Location = new Point(20, 15),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            cardPanel.Controls.Add(titleLabel);
            
            Button historyButton = new Button
            {
                Text = "История продаж",
                Location = new Point(400, 50),
                Size = new Size(120, 30)
            };
            historyButton.Click += (sender, e) => ShowSalesHistory(partner.Id);
            cardPanel.Controls.Add(historyButton);
            
            Label percentLabel = new Label
            {
                Text = $"{partner.Percentage}%",
                Location = new Point(450, 15),
                Size = new Size(50, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            cardPanel.Controls.Add(percentLabel);
            
            Label positionLabel = new Label
            {
                Text = partner.Director,
                Location = new Point(20, 40),
                Size = new Size(300, 20)
            };
            cardPanel.Controls.Add(positionLabel);
            
            Label phoneLabel = new Label
            {
                Text = partner.Phone,
                Location = new Point(20, 60),
                Size = new Size(300, 20)
            };
            cardPanel.Controls.Add(phoneLabel);
            
            Label ratingLabel = new Label
            {
                Text = $"Рейтинг: {partner.Rating}",
                Location = new Point(20, 80),
                Size = new Size(300, 20)
            };
            cardPanel.Controls.Add(ratingLabel);
        }
  
  
}