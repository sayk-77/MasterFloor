using Master_Pol.Database.Entity;
using Npgsql;


namespace Master_Pol.Database;

public class Database
{
    private string connectionString;

    public Database(string connectionString)
    {
        this.connectionString = connectionString;
    }

     public List<Partners> GetPartners()
        {
            List<Partners> partners = new List<Partners>();

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    
                    string query = @"
                        SELECT 
                            p.id, 
                            p.partner_type, 
                            p.partner_name, 
                            p.director, 
                            p.email, 
                            p.phone, 
                            p.address, 
                            p.inn, 
                            p.rating,
                            COALESCE(SUM(pp.quantity), 0) AS total_quantity
                        FROM 
                            partners p
                        LEFT JOIN 
                            partner_products pp ON p.id = pp.partner_id
                        GROUP BY 
                            p.id, p.partner_type, p.partner_name, p.director, p.email, p.phone, p.address, p.inn, p.rating";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int totalQuant = Convert.ToInt32(reader["total_quantity"]);
                                int percentage = CalculateDiscountPercentage(totalQuant);

                                Partners partner = new Partners
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Partner_Type = reader["partner_type"].ToString(),
                                    Partner_Name = reader["partner_name"].ToString(),
                                    Director = reader["director"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Phone = reader["phone"].ToString(),
                                    Address = reader["address"].ToString(),
                                    Inn = Convert.ToInt64(reader["inn"]),
                                    Rating = Convert.ToInt32(reader["rating"]),
                                    Percentage = percentage 
                                };

                                partners.Add(partner);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении партнеров: " + ex.Message);
                }
            }

            return partners;
        }
    
    private int CalculateDiscountPercentage(int totalQuantity)
    {
        if (totalQuantity < 10000)
            return 0; 
        else if (totalQuantity >= 10000 && totalQuantity < 50000)
            return 5; 
        else if (totalQuantity >= 50000 && totalQuantity < 300000)
            return 10; 
        else
            return 15; 
    }
    
    public void AddPartner(string name, string type, int rating, string address, string director, string phone, string email, long inn)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            string query = @"
            INSERT INTO partners (partner_name, partner_type, rating, address, director, phone, email, inn)
            VALUES (@name, @type, @rating, @address, @director, @phone, @email, @inn)";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("type", type);
                command.Parameters.AddWithValue("rating", rating);
                command.Parameters.AddWithValue("address", address);
                command.Parameters.AddWithValue("director", director);
                command.Parameters.AddWithValue("phone", phone);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("inn", inn);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при добавлении партнера " + ex.Message);
                }
            }
        }
    }
    
    
    public void UpdatePartner(int id, string name, string type, int rating, string address, string director, string phone, string email, long inn)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            string query = @"
            UPDATE partners 
            SET partner_name = @name, partner_type = @type, rating = @rating, 
                address = @address, director = @director, phone = @phone, email = @email, inn = @inn
            WHERE id = @id";

            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("type", type);
                command.Parameters.AddWithValue("rating", rating);
                command.Parameters.AddWithValue("address", address);
                command.Parameters.AddWithValue("director", director);
                command.Parameters.AddWithValue("phone", phone);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("inn", inn);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при обновлении партнера: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    
    public List<Sales> GetSalesHistory(int partnerId)
    {
        List<Sales> salesHistory = new List<Sales>();

        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            string query = @"
            SELECT 
                p.product_name AS product_name, 
                pp.quantity AS quantity, 
                pp.sale_date AS sale_date
            FROM 
                partner_products pp
            JOIN 
                products p ON pp.product_id = p.id
            WHERE 
                pp.partner_id = @partner_id";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@partner_id", partnerId);

                try
                {
                    connection.Open();
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
  
                            salesHistory.Add(new Sales
                            {
                                ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "Неизвестно" : reader["product_name"].ToString(),
                                Quantity = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : Convert.ToInt32(reader["quantity"]),
                                SaleDate = reader.IsDBNull(reader.GetOrdinal("sale_date")) ? DateTime.MinValue : Convert.ToDateTime(reader["sale_date"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении истории продаж: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            return salesHistory;
        }
    }
    
    public int CalculateMaterial(int productTypeId, int materialTypeId, int quantity, double param1, double param2)
    {
    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
    {
        try
        {
            connection.Open();
            
            string productQuery = "SELECT coefficient FROM product_types WHERE id = @product_type_id";
            using (NpgsqlCommand productCommand = new NpgsqlCommand(productQuery, connection))
            {
                productCommand.Parameters.AddWithValue("product_type_id", productTypeId);
                object productCoefficientObj = productCommand.ExecuteScalar();

                if (productCoefficientObj == null || !(productCoefficientObj is decimal))
                {
                    return -1;
                }

                decimal productCoefficient = Convert.ToDecimal(productCoefficientObj);
                
                string materialQuery = "SELECT defect_percentage FROM material_types WHERE id = @material_type_id";
                using (NpgsqlCommand materialCommand = new NpgsqlCommand(materialQuery, connection))
                {
                    materialCommand.Parameters.AddWithValue("material_type_id", materialTypeId);
                    object wastePercentageObj = materialCommand.ExecuteScalar();

                    if (wastePercentageObj == null || !(wastePercentageObj is decimal))
                    {
                        return -1; 
                    }

                    decimal wastePercentage = Convert.ToDecimal(wastePercentageObj);
                    
                    double materialPerUnit = param1 * param2 * (double)productCoefficient;
                    double totalMaterial = materialPerUnit * quantity / (1 - (double)wastePercentage);

                    return Convert.ToInt32(Math.Ceiling(totalMaterial));
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при расчете: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return -1;
        }
    }
}
    
    
}