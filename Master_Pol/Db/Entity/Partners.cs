namespace Master_Pol.Database.Entity;

public class Partners
{
    public int Id { get; set; }
    public string Partner_Type { get; set; }
    public string Partner_Name { get; set; }
    public string Director { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public long Inn { get; set; }
    public int Rating { get; set; }
    
    public int Percentage { get; set; }
}