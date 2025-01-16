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
    // -----------------------------------------------------
    // Enums (unchanged)
    // -----------------------------------------------------
    public enum KnifeSizeType
    {
        SixInch,
        EightInch,
        TenInch
    }

    public enum CuttingBoardMaterialType
    {
        Plastic,
        Wooden,
        Glass
    }

    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private KnifeSizeType knifeSize;
    private CuttingBoardMaterialType cuttingBoardMaterial;

    // -----------------------------------------------------
    // Original properties with private setters.
    // Mark them [XmlIgnore] so the serializer won't try
    // to use these private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public KnifeSizeType KnifeSize
    {
        get => knifeSize;
        private set => knifeSize = value; 
    }

    [XmlIgnore]
    public CuttingBoardMaterialType CuttingBoardMaterial
    {
        get => cuttingBoardMaterial;
        private set => cuttingBoardMaterial = value;
    }

    // -----------------------------------------------------
    // Derived attribute
    // We typically mark derived properties [XmlIgnore]
    // (unless we want to serialize them too).
    // -----------------------------------------------------
    [XmlIgnore]
    public string EquipmentDescription
    {
        get
        {
            return $"{KnifeSize} knife with a {CuttingBoardMaterial.ToString().ToLower()} cutting board";
        }
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // They have public setters so the serializer can set them.
    // They point to the same backing fields but call your
    // existing property logic (within this class).
    // -----------------------------------------------------

    [XmlElement("KnifeSize")]
    public KnifeSizeType KnifeSizeForXml
    {
        get => knifeSize;
        set => KnifeSize = value;  // calls the private setter
    }

    [XmlElement("CuttingBoardMaterial")]
    public CuttingBoardMaterialType CuttingBoardMaterialForXml
    {
        get => cuttingBoardMaterial;
        set => CuttingBoardMaterial = value;  // calls the private setter
    }

    // -----------------------------------------------------
    // Private static counter (remains private)
    // Class extent
    // -----------------------------------------------------
    private static int TotalFavoriteEquipments = 0;
    private static List<FavoriteEquipment> Equipments = new List<FavoriteEquipment>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't break anything.
    // DO NOT increment the static counter here.
    // -----------------------------------------------------
    public FavoriteEquipment()
    {
        // Provide defaults (choose any valid enum members)
        knifeSize = KnifeSizeType.SixInch;
        cuttingBoardMaterial = CuttingBoardMaterialType.Plastic;
    }

    // -----------------------------------------------------
    // Simplified Constructor (no optional params in your code)
    // -----------------------------------------------------
    public FavoriteEquipment(KnifeSizeType knifeSize, CuttingBoardMaterialType cuttingBoardMaterial)
    {
        KnifeSize = knifeSize;  // private setter
        CuttingBoardMaterial = cuttingBoardMaterial; // private setter

        TotalFavoriteEquipments++;
        Equipments.Add(this);
    }

    // -----------------------------------------------------
    // Method to display favorite equipment information
    // -----------------------------------------------------
    public void DisplayEquipmentInfo()
    {
        Console.WriteLine($"Knife Size: {KnifeSize}");
        Console.WriteLine($"Cutting Board Material: {CuttingBoardMaterial}");
        Console.WriteLine($"Equipment Description: {EquipmentDescription}");
    }

    // -----------------------------------------------------
    // Static Method to display all equipment
    // -----------------------------------------------------
    public static void DisplayAllEquipments()
    {
        Console.WriteLine("\n--- All Favorite Equipment ---");
        foreach (var equipment in Equipments)
        {
            equipment.DisplayEquipmentInfo();
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
