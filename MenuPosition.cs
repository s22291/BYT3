using System;

public class MenuPosition
{
    // Properties
    public string Name { get; set; }           // Name of the menu item
    public decimal ProductionCost { get; set; } // Production cost of the item
    public decimal SellPrice { get; set; }      // Selling price of the item
    public string Temperature { get; set; }    // Temperature category (e.g., "Hot", "Cold")

    // Constant desired profit margin (9%)
    public const decimal DesiredProfitMargin = 0.09m;

    // Computed property to calculate actual profit margin
    public decimal ActualProfitMargin
    {
        get
        {
            if (SellPrice > 0)
            {
                return (SellPrice - ProductionCost) / SellPrice;
            }
            return 0; // Avoid division by zero
        }
    }

    // Constructor
    public MenuPosition(string name, decimal productionCost, decimal sellPrice, string temperature)
    {
        Name = name;
        ProductionCost = productionCost;
        SellPrice = sellPrice;
        Temperature = temperature;
    }

    // Method to display menu position information
    public void DisplayMenuPositionInfo()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Production Cost: {ProductionCost:C}");
        Console.WriteLine($"Sell Price: {SellPrice:C}");
        Console.WriteLine($"Temperature: {Temperature}");
        Console.WriteLine($"Desired Profit Margin: {DesiredProfitMargin:P}");
        Console.WriteLine($"Actual Profit Margin: {ActualProfitMargin:P}");
    }
}
