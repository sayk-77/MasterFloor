using Master_Pol.Database.Entity;

namespace Master_Pol;

public partial class MasterFloorPartnerEditForm : Form
{
    private Partners partner;
    private bool isEditMode;
    private int id;

    private TextBox nameTextBox;
    private ComboBox typeComboBox;
    private NumericUpDown ratingNumericUpDown;
    private TextBox addressTextBox;
    private TextBox directorTextBox;
    private TextBox phoneTextBox;
    private TextBox emailTextBox;
    private TextBox InnTextBox;

    public MasterFloorPartnerEditForm()
    {
        InitializeComponents();
        isEditMode = false;
    }

    public MasterFloorPartnerEditForm(Partners partnerEdit)
    {
        InitializeComponents();
        this.partner = partnerEdit;
        isEditMode = true;
        this.id = partnerEdit.Id;
        LoadPartnerData();
    }

    private void InitializeComponents()
    {
        this.Text = isEditMode ? "Редактирование партнера" : "Добавление партнера";
        this.Size = new System.Drawing.Size(400, 380);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Icon = new Icon("MasterFloor.ico");
        
        Label nameLabel = new Label { Text = "Наименование:", Location = new System.Drawing.Point(20, 20) };
        nameTextBox = new TextBox { Location = new System.Drawing.Point(120, 20), Width = 200 };

        Label typeLabel = new Label { Text = "Тип партнера:", Location = new System.Drawing.Point(20, 50) };
        typeComboBox = new ComboBox { Location = new System.Drawing.Point(120, 50), Width = 200 };
        typeComboBox.Items.AddRange(new string[] { "ЗАО", "ООО", "ПАО", "ОАО" });

        Label ratingLabel = new Label { Text = "Рейтинг:", Location = new System.Drawing.Point(20, 80) };
        ratingNumericUpDown = new NumericUpDown { Location = new System.Drawing.Point(120, 80), Minimum = 0, Maximum = int.MaxValue };

        Label addressLabel = new Label { Text = "Адрес:", Location = new System.Drawing.Point(20, 110) };
        addressTextBox = new TextBox { Location = new System.Drawing.Point(120, 110), Width = 200 };

        Label directorLabel = new Label { Text = "ФИО директора:", Location = new System.Drawing.Point(20, 140) };
        directorTextBox = new TextBox { Location = new System.Drawing.Point(120, 140), Width = 200 };

        Label phoneLabel = new Label { Text = "Телефон:", Location = new System.Drawing.Point(20, 170) };
        phoneTextBox = new TextBox { Location = new System.Drawing.Point(120, 170), Width = 200 };

        Label emailLabel = new Label { Text = "Email:", Location = new System.Drawing.Point(20, 200) };
        emailTextBox = new TextBox { Location = new System.Drawing.Point(120, 200), Width = 200 };
        
        Label innLabel = new Label { Text = "ИНН:", Location = new System.Drawing.Point(20, 230) };
        InnTextBox = new TextBox { Location = new System.Drawing.Point(120, 230), Width = 200 };

        Button saveButton = new Button { Text = "Сохранить", Location = new System.Drawing.Point(120, 280), Width = 100, Height = 30, BackColor = Color.FromArgb(103, 186, 128), ForeColor = Color.White};
        Button cancelButton = new Button { Text = "Отмена", Location = new System.Drawing.Point(230, 280), Width = 100, Height = 30, BackColor = Color.FromArgb(103, 186, 128), ForeColor = Color.White};
        
        this.Controls.Add(nameLabel);
        this.Controls.Add(nameTextBox);
        this.Controls.Add(typeLabel);
        this.Controls.Add(typeComboBox);
        this.Controls.Add(ratingLabel);
        this.Controls.Add(ratingNumericUpDown);
        this.Controls.Add(addressLabel);
        this.Controls.Add(addressTextBox);
        this.Controls.Add(directorLabel);
        this.Controls.Add(directorTextBox);
        this.Controls.Add(phoneLabel);
        this.Controls.Add(phoneTextBox);
        this.Controls.Add(emailLabel);
        this.Controls.Add(emailTextBox);
        this.Controls.Add(innLabel);
        this.Controls.Add(InnTextBox);
        this.Controls.Add(saveButton);
        this.Controls.Add(cancelButton);
        
        saveButton.Click += (sender, e) => SavePartner();
        cancelButton.Click += (sender, e) => this.Close();
    }

    private void LoadPartnerData()
    {
        if (partner != null)
        {
            nameTextBox.Text = partner.Partner_Name;
            typeComboBox.SelectedItem = partner.Partner_Type;
            ratingNumericUpDown.Value = partner.Rating;
            addressTextBox.Text = partner.Address;
            directorTextBox.Text = partner.Director;
            phoneTextBox.Text = partner.Phone;
            emailTextBox.Text = partner.Email;
            InnTextBox.Text = partner.Inn.ToString();
        }
    }

    private void SavePartner()
    {
        string name = nameTextBox.Text.Trim();
        string type = typeComboBox.SelectedItem?.ToString() ?? "";
        int rating;
        
        if (!int.TryParse(ratingNumericUpDown.Value.ToString(), out rating) || rating < 0)
        {
            MessageBox.Show("Рейтинг должен быть целым неотрицательным числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        string address = addressTextBox.Text.Trim();
        string director = directorTextBox.Text.Trim();
        string phone = phoneTextBox.Text.Trim();
        string email = emailTextBox.Text.Trim();
        long inn = long.Parse(InnTextBox.Text.Trim());

        if (string.IsNullOrEmpty(name))
        {
            MessageBox.Show("Введите наименование партнера.");
            return;
        }

        if (isEditMode)
        {
            UpdatePartner();
        }
        else
        {
            AddPartner(name, type, rating, address, director, phone, email, inn);
        }

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void AddPartner(string name, string type, int rating, string address, string director, string phone, string email, long inn)
    {
        var db = new Database.Database("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=MasterFloorDB");
        db.AddPartner(name, type, rating, address, director, phone, email, inn);
    }

    private void UpdatePartner()
    {
        var db = new Database.Database("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=MasterFloorDB");
        
        if (!long.TryParse(InnTextBox.Text, out long inn))
        {
            MessageBox.Show("Неверный формат ИНН", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return; 
        }
        
        db.UpdatePartner(id, 
            nameTextBox.Text.Trim(), 
            typeComboBox.SelectedItem?.ToString() ?? "", 
            (int)ratingNumericUpDown.Value, 
            addressTextBox.Text.Trim(), 
            directorTextBox.Text.Trim(), 
            phoneTextBox.Text.Trim(), 
            emailTextBox.Text.Trim(),
            inn);
            
    }
}