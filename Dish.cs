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
    // -----------------------------------------------------
    // Enum for Course Type
    // -----------------------------------------------------
    public enum CourseTypeEnum
    {
        Starter,
        Main,
        Dessert
    }

    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private CourseTypeEnum courseType;
    private string serviceCutleryType;

    // -----------------------------------------------------
    // Original Properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to call these private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public CourseTypeEnum CourseType
    {
        get => courseType;
        private set => courseType = value; 
    }

    [XmlIgnore]
    public string ServiceCutleryType
    {
        get => serviceCutleryType;
        private set
        {
            // Reuse your Validator logic
            serviceCutleryType = Validator.ValidateNonEmptyString(value, nameof(ServiceCutleryType));
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML serialization.
    // They have public getters/setters so the serializer
    // can set them. They delegate to the original
    // properties, preserving all validation logic.
    // -----------------------------------------------------

    [XmlElement("CourseType")]
    public CourseTypeEnum CourseTypeForXml
    {
        get => courseType;
        set => CourseType = value;  // triggers the private setter
    }

    [XmlElement("ServiceCutleryType")]
    public string ServiceCutleryTypeForXml
    {
        get => serviceCutleryType;
        set => ServiceCutleryType = value; // triggers the private setter
    }

    // -----------------------------------------------------
    // Static attribute & class extent
    // -----------------------------------------------------
    public static int TotalDishes { get; private set; } = 0;
    private static List<Dish> Dishes = new List<Dish>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't break logic.
    // Do NOT increment TotalDishes here (avoid double-count).
    // -----------------------------------------------------
    public Dish()
    {
        // Safe defaults
        courseType = CourseTypeEnum.Main;
        serviceCutleryType = "Fork";
    }

    // -----------------------------------------------------
    // Main constructor
    // -----------------------------------------------------
    public Dish(CourseTypeEnum courseType, string serviceCutleryType)
    {
        CourseType = courseType;  // calls private setter
        ServiceCutleryType = serviceCutleryType; // calls private setter

        TotalDishes++;
        Dishes.Add(this);
    }

    // -----------------------------------------------------
    // Method to display dish information
    // -----------------------------------------------------
    public void DisplayDishInfo()
    {
        Console.WriteLine($"Course Type: {CourseType}");
        Console.WriteLine($"Service Cutlery Type: {ServiceCutleryType}");
    }

    // -----------------------------------------------------
    // Static method to display all dishes
    // -----------------------------------------------------
    public static void DisplayAllDishes()
    {
        Console.WriteLine("\n--- All Dishes ---");
        foreach (var dish in Dishes)
        {
            dish.DisplayDishInfo();
            Console.WriteLine();
        }
    }

    // -----------------------------------------------------
    // Serialization / Deserialization
    // -----------------------------------------------------
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
