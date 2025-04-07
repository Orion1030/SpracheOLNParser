# OLNParserTestProject

## Overview
OLNParserTestProject is a .NET 8 application designed to parse and process driver's license data. The project includes a parser that extracts various fields from a given input string and maps them to a `LicenseData` object.

## Features
- Parses driver's license information including OLN, class, race, sex, eye color, hair color, height, weight, social security number, date of birth, issue date, expiration date, restrictions, endorsements, eligibility date, and status.
- Converts parsed data into a structured `LicenseData` object.
- Supports parsing of names and addresses.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022

### Installation
1. Clone the repository:

```bash
git clone https://github.com/yourusername/OLNParserTestProject.git
```
2. Open the solution in Visual Studio 2022.

### Usage
1. Build the solution.
2. Run the project.

### Example
Here is an example of how to use the parser:
```csharp
using OLNParserTestProject.Parsers;
string input = "Your input string here"; 
LicenseData licenseData = OLNLParser.ParseLicense(input);
Console.WriteLine($"First Name: {licenseData.FirstName}"); 
Console.WriteLine($"Last Name: {licenseData.LastName}"); 
```


## Project Structure

### Parsers/OLNParser.cs
This file contains the main parsing logic. It uses the `Sprache` library to define various parsers for different fields in the driver's license data. The parsed data is then mapped to a `LicenseData` object.

Key methods and properties:
- `ParseLicense(string? input)`: Parses the input string and returns a `LicenseData` object.
- `ToCapitalCase(string? str)`: Converts a string to capital case.
- `GetHeightRange(string? str)`: Formats the height string.
- `ParseRace(string? race)`: Converts a race string to a `Races` enum.

### Models/LicenseData.cs
This file defines the `LicenseData` class, which holds the parsed driver's license information.

### Program.cs
This file contains the entry point of the application. It demonstrates how to use the `OLNLParser` to parse an input string and output the parsed data.

```csharp
using OLNParserTestProject.Parsers;
class Program { static void Main(string[] args) { string input = "Your input string here"; LicenseData licenseData = OLNLParser.ParseLicense(input);
    Console.WriteLine($"First Name: {licenseData.FirstName}");
    Console.WriteLine($"Last Name: {licenseData.LastName}");
    // Output other fields as needed
}
}
```

## Contributing
Contributions are welcome! Please open an issue or submit a pull request.

## License
This project is licensed under the MIT License.

## Acknowledgments
This README file provides an overview of the project, installation instructions, usage examples, and details about the key files and their functionalities. You can customize it further based on your specific project requirements.
I used the active document because you have the checkmark checked. You can include additional context using # references. Typing # opens a completion list of available context.