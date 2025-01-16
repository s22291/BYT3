/** \file     Mixologist.cs
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

public class Mixologist
{
    // -----------------------------------------------------
    // Private backing fields
    // -----------------------------------------------------
    private bool ownsUniform;
    private DateTime dateHired;

    // -----------------------------------------------------
    // Original properties (private setters).
    // Mark them [XmlIgnore] so the serializer won't try
    // to access the private setters directly.
    // -----------------------------------------------------

    [XmlIgnore]
    public bool OwnsUniform
    {
        get { return ownsUniform; }
        private set
        {
            // Validate that the mixologist has at least 10 years of experience
            // *We calculate "YearsOfExperience" from "dateHired" below.
            // *Careful: If dateHired is default(DateTime), this might matter,
            //  so see how you handle that in the parameterless constructor.
            if (value && YearsOfExperience < 10)
            {
                throw new InvalidOperationException("A mixologist cannot own a uniform unless they have at least 10 years of experience.");
            }
            ownsUniform = value;
        }
    }

    [XmlIgnore]
    public DateTime DateHired
    {
        get { return dateHired; }
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("DateHired cannot be in the future.");
            }
            dateHired = value;
        }
    }

    // -----------------------------------------------------
    // Derived Attribute
    // Typically, derived attributes have no setter, so
    // we can mark them [XmlIgnore] if you don't want them
    // serialized or if the serializer would break them.
    // -----------------------------------------------------
    [XmlIgnore]
    public int YearsOfExperience
    {
        get { return DateTime.Now.Year - DateHired.Year; }
    }

    // -----------------------------------------------------
    // Bridge properties for XML Serialization.
    // They have public setters so the serializer can set them.
    // They point to the same private fields but call your
    // existing property logic for validation.
    // -----------------------------------------------------

    [XmlElement("OwnsUniform")]
    public bool OwnsUniformForXml
    {
        get => ownsUniform;
        set => OwnsUniform = value;  // calls the private setter logic
    }

    [XmlElement("DateHired")]
    public DateTime DateHiredForXml
    {
        get => dateHired;
        set => DateHired = value;    // calls the private setter logic
    }

    // -----------------------------------------------------
    // Static attribute & class extent
    // -----------------------------------------------------
    public static int TotalMixologists { get; private set; } = 0;

    private static List<Mixologist> Mixologists = new List<Mixologist>();

    // -----------------------------------------------------
    // Parameterless constructor (REQUIRED by XML serializer).
    // Provide safe defaults that won't break validation.
    // Be careful with the uniform validation, since setting
    // ownsUniform = true might throw if dateHired isn't old enough.
    // DO NOT increment static count here (avoid double-counting).
    // -----------------------------------------------------
    public Mixologist()
    {
        // Provide defaults:
        dateHired = DateTime.Now.AddYears(-10); // so it doesn't fail if OwnsUniform is set to true
        ownsUniform = false;
    }

    // -----------------------------------------------------
    // Main Constructor
    // -----------------------------------------------------
    public Mixologist(DateTime dateHired, bool ownsUniform = false)
    {
        DateHired = dateHired;   // validates
        OwnsUniform = ownsUniform; // validates

        TotalMixologists++;
        Mixologists.Add(this);
    }

    // -----------------------------------------------------
    // Methods
    // -----------------------------------------------------
    public void UpdateOwnsUniform(bool ownsUniform)
    {
        OwnsUniform = ownsUniform; // re-validate
    }

    public void DisplayMixologistInfo()
    {
        Console.WriteLine($"Date Hired: {DateHired:yyyy-MM-dd}");
        Console.WriteLine($"Years of Experience: {YearsOfExperience}");
        Console.WriteLine($"Owns Uniform: {(OwnsUniform ? "Yes" : "No")}");
    }

    // -----------------------------------------------------
    // Static Methods
    // -----------------------------------------------------
    public static void DisplayAllMixologists()
    {
        Console.WriteLine("\n--- All Mixologists ---");
        foreach (var mixologist in Mixologists)
        {
            mixologist.DisplayMixologistInfo();
            Console.WriteLine();
        }
    }

    public static void SaveToFile(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Mixologist>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, Mixologists);
            }
            Console.WriteLine("Mixologists saved to file successfully.");
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Mixologist>));
            using (StreamReader reader = new StreamReader(filePath))
            {
                Mixologists = (List<Mixologist>?)serializer.Deserialize(reader) ?? new List<Mixologist>();
            }
            Console.WriteLine("Mixologists loaded from file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
        }
    }
}
