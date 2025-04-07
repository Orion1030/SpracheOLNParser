# OLN Parser Coding Test

## Objective

Build a C# parser using the Sprache library to parse structured driver's license data from a text format into a JSON object.

## Project Structure

- **/Parsers**: Place your parsing logic here.
- **/Models**: Define your output model classes here.
- **/Input**: Contains sample OLN response input.
- **/Instructions**: Contains this README and supporting files.

## Getting Started

1. Clone or open this project in your IDE.
2. Install the Sprache NuGet package:
   ```
   dotnet add package Sprache
   ```
3. Open the file in `/Input/OLN Response - Sanitized.txt` to see the data you'll be parsing.
4. Use the field structure shown in `Example License Data Fields.txt` as your reference output.
5. Implement your parser to extract all relevant fields and serialize them as JSON.

## Deliverables

- Your parser code
- Your data model class(es)
- Sample output as JSON
- Optional: Unit tests or helper utilities

Good luck!
