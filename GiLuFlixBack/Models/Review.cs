

namespace GiLuFlixBack.Models;

public class Review
{
    public int userID { get; set; }
    public int itemID { get; set; }
    public int rating { get; set; }
    public string reviewText { get; set; }
    
}