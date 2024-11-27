public class Mixologist
{
    // Property for whether the mixologist owns a uniform
    public bool OwnsUniform { get; set; }

    // Constructor to initialize the Mixologist class
    public Mixologist(bool ownsUniform)
    {
        OwnsUniform = ownsUniform;
    }

    // Method to display mixologist information
    public void DisplayMixologistInfo()
    {
        Console.WriteLine($"Owns Uniform: {(OwnsUniform ? "Yes" : "No")}");
    }
}
