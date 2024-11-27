public class Drink
{
    // Properties of the Drink class
    public string DrinkwareType { get; set; } // Type of drinkware (e.g., "Glass", "Mug", "Wine Glass")
    public bool IfSparkling { get; set; }     // Whether the drink is sparkling or not

    // Constructor to initialize the Drink class
    public Drink(string drinkwareType, bool ifSparkling)
    {
        DrinkwareType = drinkwareType;
        IfSparkling = ifSparkling;
    }

    // Method to display drink information
    public void DisplayDrinkInfo()
    {
        Console.WriteLine($"Drinkware Type: {DrinkwareType}");
        Console.WriteLine($"Is Sparkling: {(IfSparkling ? "Yes" : "No")}");
    }
}
