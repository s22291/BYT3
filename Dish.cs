using System;

class Program
{
    static void Main()
    {
        // Create a menu position with valid data
        MenuPosition item1 = new MenuPosition("Burger", 4.50m, 6.99m, "Hot");
        item1.DisplayMenuPositionInfo();

        Console.WriteLine();

        // Create another menu position
        MenuPosition item2 = new MenuPosition("Ice Cream", 1.25m, 3.50m, "Cold");
        item2.DisplayMenuPositionInfo();
    }
}
