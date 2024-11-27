public class Waiter
{
    // Properties of the Waiter class
    public decimal AvgTipsValuePerDay { get; set; }           // Average value of tips per day
    public int AverageCustomersServedDaily { get; set; }      // Average number of customers served daily

    // Constructor to initialize the Waiter class
    public Waiter(decimal avgTipsValuePerDay, int averageCustomersServedDaily)
    {
        AvgTipsValuePerDay = avgTipsValuePerDay;
        AverageCustomersServedDaily = averageCustomersServedDaily;
    }

    // Method to display waiter information
    public void DisplayWaiterInfo()
    {
        Console.WriteLine($"Average Tips Value Per Day: {AvgTipsValuePerDay:C}"); // Format as currency
        Console.WriteLine($"Average Customers Served Daily: {AverageCustomersServedDaily}");
    }
}
