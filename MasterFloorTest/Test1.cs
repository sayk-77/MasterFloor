using Master_Pol.Database;

namespace MasterFloorTest;

[TestClass]
public sealed class MasterFloorTests
{
    public const string connString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=MasterFloorDB";
    public Database db = new Database(connString);
    
    [TestMethod]
    public void TestCalculateDiscountPercentage1()
    {
        int totalSum = 1000;
        int discountPercentage = db.CalculateDiscountPercentage(totalSum);
        
        Assert.AreEqual(0, discountPercentage);
    }

    [TestMethod]
    public void TestCalculateDiscountPercentage2()
    {
        int totalSum = 40000;
        int discountPercentage = db.CalculateDiscountPercentage(totalSum);

        Assert.AreEqual(5, discountPercentage);
    }

    [TestMethod]
    public void TestCalculateDiscountPercentage3()
    {
        int totalSum = 60000;
        int discountPercentage = db.CalculateDiscountPercentage(totalSum);

        Assert.AreEqual(10, discountPercentage);
    }

    [TestMethod]
    public void TestCalculateDiscountPercentage4()
    {
        int totalSum = 400000;
        int discountPercentage = db.CalculateDiscountPercentage(totalSum);

        Assert.AreEqual(15, discountPercentage);
    }

    [TestMethod]
    public void TestAddPartnerSuccess()
    {
        string name = "Паркет9112";
        string type = "ЗАО";
        int rating = 10;
        string address = "652050, Кемеровская область, город Юрга, ул. Лесная, 15";
        string director = "Петров Петр Петрович";
        string phone = "987 123 56 78";
        string email = "parket9112@email.com";
        long inn = 1722455172;

        int result = db.AddPartner(name, type, rating, address, director, phone, email, inn);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestAddPartnerFailed()
    {
        string name = null;
        string type = "ЗАО";
        int rating = 10;
        string address = null;
        string director = "Петров Петр Петрович";
        string phone = "987 123 56 78";
        string email = "petr@email.com";
        long inn = 2922455172;

        int result = db.AddPartner(name, type, rating, address, director, phone, email, inn);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TestUpdatePartnerSuccess()
    {
        int id = 5;
        string name = "Ламинат911";
        string type = "ЗАО";
        int rating = 10;
        string address = "652050, Кемеровская область, город Юрга, ул. Лесная, 15";
        string director = "Петров Петр Петрович";
        string phone = "987 123 56 78";
        string email = "laminat911@email.com";
        long inn = 2922655072;

        int result = db.UpdatePartner(5, name, type, rating, address, director, phone, email, inn);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void TestUpdatePartnerFailed()
    {
        int id = 5;
        string name = null;
        string type = "ЗАО";
        int rating = 10;
        string address = null;
        string director = "Петров Петр Петрович";
        string phone = "987 123 56 78";
        string email = "petr@email.com";
        long inn = 2922455172;

        int result = db.UpdatePartner(5, name, type, rating, address, director, phone, email, inn);
        Assert.AreEqual(0, result);
    }
}