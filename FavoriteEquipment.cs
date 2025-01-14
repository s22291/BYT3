/** \file     FavoriteEquipment.cs
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

public class FavoriteEquipment
{
    // Enum for Knife Sizes
    public enum KnifeSizeType
    {
        SixInch,   // 6"
        EightInch, // 8"
        TenInch    // 10"
    }

    // Enum for Cutting Board Materials
    public enum CuttingBoardMaterialType
    {
        Plastic,
        Wooden,
        Glass
    }

    // Basic Attribute (now strictly required)
    public KnifeSizeType KnifeSize { get; private set; }

    // Cutting board is no longer optional
    public CuttingBoardMaterialType CuttingBoardMaterial { get; private set; }

    // Private static counter (no longer accessible publicly)
    private static int TotalFavoriteEquipments = 0;

    // Class Extent
    private static List<FavoriteEquipment> Equipments = new List<FavoriteEquipment>();

    // Derived Attribute
    public string EquipmentDescription
    {
        get
        {
            return $"{KnifeSize} knife with a {CuttingBoardMaterial.ToString().ToLower()} cutting board";
        }
    }

    // Simplified Constructor (no optional parameter)
    public FavoriteEquipment(KnifeSizeType knifeSize, CuttingBoardMaterialType cuttingBoardMaterial)
    {
        KnifeSize = knifeSize;
        CuttingBoardMaterial = cuttingBoardMaterial;

        // Increment private static count and add to extent
        TotalFavoriteEquipments++;
        Equipments.Add(this);
    }

    // Method to display favorite equipment information
    public void DisplayEquipmentInfo()
    {
        Console.WriteLine($"Knife Size: {KnifeSize}");
        Console.WriteLine($"Cutting Board Material: {CuttingBoardMaterial}");
        Console.WriteLine($"Equipment Description: {EquipmentDescription}");
    }

    // Static Method to Display All FavoriteEquipment Objects
    public static void DisplayAllEquipments()
    {
        Console.WriteLine("\n--- All Favorite Equipment ---");
        foreach (var equipment in Equipments)
        {
            equipment.DisplayEquipmentInfo();
            Console.WriteLine();
        }
    }

    // Serialization Method
    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<FavoriteEquipment>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Equipments);
            }
            Console.WriteLine("FavoriteEquipment saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    // Deserialization Method
    public static void LoadFromFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<FavoriteEquipment>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Equipments = (List<FavoriteEquipment>?)serializer.Deserialize(reader) 
                             ?? new List<FavoriteEquipment>();
            }
            Console.WriteLine("FavoriteEquipment loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
