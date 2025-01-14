/** \file     Dish.cs
*  \author    Aslan,Julio,Mohammad,Wiktor
*  \version   Final
*  \date      2024
*  \bug       No bugs so far
*  \copyright Polish and Japanies Information Technology 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Dish
{
    // Enum for Course Type
    public enum CourseTypeEnum
    {
        Starter,
        Main,
        Dessert
    }

    // Private fields
    private CourseTypeEnum courseType; // The course type (Starter, Main, Dessert)
    private string serviceCutleryType; // The type of service cutlery (e.g., Fork, Spoon, Knife)

    // Public read-only properties
    public CourseTypeEnum CourseType
    {
        get { return courseType; }
    }

    public string ServiceCutleryType
    {
        get { return serviceCutleryType; }
    }

    // Static attribute to count total dishes
    public static int TotalDishes { get; private set; } = 0;

    // Class extent to store all instances
    private static List<Dish> Dishes = new List<Dish>();

    // Constructor
    public Dish(CourseTypeEnum courseType, string serviceCutleryType)
    {
        // Validate inputs
        this.courseType = courseType; // Enum ensures only valid CourseType values are used
        this.serviceCutleryType = Validator.ValidateNonEmptyString(serviceCutleryType, nameof(ServiceCutleryType));

        // Increment static count and add to class extent
        TotalDishes++;
        Dishes.Add(this);
    }

    // Method to display dish information
    public void DisplayDishInfo()
    {
        Console.WriteLine($"Course Type: {CourseType}");
        Console.WriteLine($"Service Cutlery Type: {ServiceCutleryType}");
    }

    // Static method to display all dishes
    public static void DisplayAllDishes()
    {
        Console.WriteLine("\n--- All Dishes ---");
        foreach (var dish in Dishes)
        {
            dish.DisplayDishInfo();
            Console.WriteLine();
        }
    }

    // Serialization method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dish>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Dishes);
            }
            Console.WriteLine("Dishes saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // Deserialization method
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Dish>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Dishes = (List<Dish>?)serializer.Deserialize(reader) ?? new List<Dish>();
            }
            Console.WriteLine("Dishes loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}