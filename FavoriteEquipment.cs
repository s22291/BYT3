public class FavoriteEquipment
{
    // Properties of the FavoriteEquipment class
    public string KnifeSize { get; set; }           // Preferred knife size (e.g., "6\"", "8\"", "10\"")
    public string CuttingBoardMaterial { get; set; } // Preferred cutting board material (e.g., "Plastic", "Wooden", "Glass")

    // Constructor for initializing the FavoriteEquipment class
    public FavoriteEquipment(string knifeSize, string cuttingBoardMaterial)
    {
        // Validate knife size
        if (knifeSize != "6\"" && knifeSize != "8\"" && knifeSize != "10\"")
        {
            throw new ArgumentException("Invalid knife size. Choose '6\"', '8\"', or '10\"'.");
        }

        // Validate cutting board material
        if (cuttingBoardMaterial != "Plastic" && cuttingBoardMaterial != "Wooden" && cuttingBoardMaterial != "Glass")
        {
            throw new ArgumentException("Invalid cutting board material. Choose 'Plastic', 'Wooden', or 'Glass'.");
        }

        KnifeSize = knifeSize;
        CuttingBoardMaterial = cuttingBoardMaterial;
    }

    // Method to display favorite equipment information
    public void DisplayEquipmentInfo()
    {
        Console.WriteLine($"Favorite Knife Size: {KnifeSize}");
        Console.WriteLine($"Favorite Cutting Board Material: {CuttingBoardMaterial}");
    }
}
