public class Cook
{
    // Properties of the Cook class
    public string Role { get; set; }        // The role or specialization of the cook (e.g., "Pastry Chef", "Grill Cook")
    public bool OwnsUniform { get; set; }   // Whether the cook owns a uniform

    // Constructor for initializing the Cook class
    public Cook(string role, bool ownsUniform)
    {
        Role = role;
        OwnsUniform = ownsUniform;
    }

    // Method to display cook information
    public void DisplayCookInfo()
    {
        Console.WriteLine($"Role: {Role}");
        Console.WriteLine($"Owns Uniform: {(OwnsUniform ? "Yes" : "No")}");
    }
}
