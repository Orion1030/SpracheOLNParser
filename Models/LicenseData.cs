using System.Text.Json.Serialization;

namespace OLNParserTestProject.Models
{
    public enum Races
    {
        [JsonPropertyName("Asian")]
        Asian,
        [JsonPropertyName("Black")]
        Black,
        [JsonPropertyName("Hispanic")]
        Hispanic,
        [JsonPropertyName("Native American")]
        NativeAmerican,
        [JsonPropertyName("White")]
        White,
        [JsonPropertyName("Other")]
        Other
    }
    public enum Sexs
    {
        [JsonPropertyName("Male")]
        Male,
        [JsonPropertyName("Female")]
        Female,
    }
    public class LicenseData
    {
        [JsonPropertyName("First name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("Last name")]
        public string? LastName { get; set; }

        [JsonPropertyName("Middle name")]
        public string? MiddleName { get; set; }

        [JsonPropertyName("Race")]
        public Races Race { get; set; } = Races.White;

        [JsonPropertyName("Sex")]
        public Sexs Sex { get; set; } = Sexs.Male;

        [JsonPropertyName("Date of Birth")]
        public string? DateOfBirth { get; set; }

        [JsonPropertyName("Height")]
        public  string? Height { get; set; }

        [JsonPropertyName("Weight")]
        public  string? Weight { get; set; }

        [JsonPropertyName("Hair color")]
        public string? HairColor { get; set; }

        [JsonPropertyName("Eye color")]
        public  string? EyeColor { get; set; }

        [JsonPropertyName("OLN")]
        public  string? OLN { get; set; }

        [JsonPropertyName("CLASS")]
        public  string? Class { get; set; }

        [JsonPropertyName("EXPIRATION DATE")]
        public string? ExpirationDate { get; set; }

        [JsonPropertyName("SOCIAL SECURITY NUMBER")]
        public  string? SocialSecurityNumber { get; set; }

        [JsonPropertyName("Physical Street address")]
        public  string? PhysicalStreetAddress { get; set; }

        [JsonPropertyName("City")]
        public  string? City { get; set; }

        [JsonPropertyName("State")]
        public  string? State { get; set; }

        private string? _zipCode;
        [JsonPropertyName("Zip Code")]
        public string? ZipCode
        {
            get => _zipCode;
            set
            {
                if (value != null && value.Length > 6)
                {
                    _zipCode = value.Trim().Substring(0, 6);
                }
                else
                {
                    _zipCode = value;
                }
            }
        }
    }
}
