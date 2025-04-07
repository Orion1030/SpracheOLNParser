using OLNParserTestProject.Models;
using OLNParserTestProject.Parsers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OLNParserTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string? inputFile = "OLN Response - Sanitized.txt";
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            string? inputData = File.ReadAllText(inputFile);

            try
            {
                LicenseData parsedLicense = OLNLParser.ParseLicense(inputData);
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    WriteIndented = true
                };
                string? jsonOutput = JsonSerializer.Serialize(parsedLicense, options);

                Console.WriteLine($"Parsed JSON Output:\n{jsonOutput}");

                if (File.Exists("ParsedLicenseData.json"))
                {
                    File.Delete("ParsedLicenseData.json");
                }
                File.WriteAllText("ParsedLicenseData.json", jsonOutput);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing input data: " + ex.Message);
            }
            Console.ReadKey();
        }
    }
}