using OLNParserTestProject.Models;
using Sprache;

namespace OLNParserTestProject.Parsers
{
    public static class OLNLParser
    {
        static readonly Parser<string> DigitalParser =
            Parse.Digit.Many().Text().Token();
        static readonly Parser<string> LetterParser =
            Parse.Letter.Many().Text().Token();
        static readonly Parser<string> MultiSpacePattern =
            Parse.AnyChar.Until(Parse.String("  ").Text()).Text().Token();
        static readonly Parser<string> SingleSpacePattern =
            Parse.AnyChar.Until(Parse.Char(' ')).Text().Token();
        static readonly Parser<string> OLNPattern =
            Parse.String("OLN/").Text();
        static readonly Parser<string> CLASSPattern =
            Parse.String("CLASS/").Text();
        static readonly Parser<string> RACPattern =
            Parse.String("RAC/").Text();
        static readonly Parser<string> SEXPattern =
            Parse.String("SEX/").Text();
        static readonly Parser<string> EYEPattern =
            Parse.String("EYE/").Text();
        static readonly Parser<string> HAIPattern =
            Parse.String("HAI/").Text();
        static readonly Parser<string> HGTPattern =
            Parse.String("HGT/").Text();
        static readonly Parser<string> WGTPattern =
            Parse.String("WGT/").Text();
        static readonly Parser<string> SOCPattern =
            Parse.String("SOC/").Text();
        static readonly Parser<string> DOBPattern =
            Parse.String("DOB/").Text();
        static readonly Parser<string> ISSUPattern =
            Parse.String("ISSU/").Text();
        static readonly Parser<string> EXPPattern =
            Parse.String("EXP/").Text();
        static readonly Parser<string> RESTRICTIONSPattern =
            Parse.String("RESTRICTIONS/").Text();
        static readonly Parser<string> ENDORSEMENTSPattern =
            Parse.String("ENDORSEMENTS/").Text();
        static readonly Parser<string> ELIGDATEPattern =
            Parse.String("ELIG DATE/").Text();
        static readonly Parser<string> STATUSNONCDLPattern =
            Parse.String("STATUS NON-CDL:").Text();
        static readonly Parser<string> STATUSCDLPattern =
            Parse.String("STATUS CDL:").Text();
        static readonly Parser<string> RemainStringPattern =
            Parse.AnyChar.Many().Text().Token();
        private static readonly Parser<KeyValuePair<string, string>[]> OlnAndClassParser =
            from olnKey in OLNPattern
            from olnValue in MultiSpacePattern
            from classKey in CLASSPattern
            from classValue in RemainStringPattern
            select new[]
                {
                    new KeyValuePair<string, string>(olnKey, olnValue),
                    new KeyValuePair<string, string>(classKey, classValue)
                };
        private static readonly Parser<KeyValuePair<string, string>[]> BodyInfoParser =
            from racKey in RACPattern
            from racValue in MultiSpacePattern
            from sexKey in SEXPattern
            from sexValue in MultiSpacePattern
            from eyeKey in EYEPattern
            from eyeValue in MultiSpacePattern
            from haiKey in HAIPattern
            from haicValue in MultiSpacePattern
            from hgtKey in HGTPattern
            from hgtValue in MultiSpacePattern
            from wgtKey in WGTPattern
            from wgtValue in RemainStringPattern
            select new[]
            {
                new KeyValuePair<string, string>(racKey, racValue),
                new KeyValuePair<string, string>(sexKey, sexValue),
                new KeyValuePair<string, string>(eyeKey, eyeValue),
                new KeyValuePair<string, string>(haiKey, haicValue),
                new KeyValuePair<string, string>(hgtKey, hgtValue),
                new KeyValuePair<string, string>(wgtKey, wgtValue),
            };
        private static readonly Parser<KeyValuePair<string, string>[]> SOCParser =
           from socKey in SOCPattern
           from socValue in SingleSpacePattern
           from dobKey in DOBPattern
           from dobValue in SingleSpacePattern
           from issuKey in ISSUPattern
           from issuValue in SingleSpacePattern
           from expKey in EXPPattern
           from expValue in RemainStringPattern
           select new[]
           {
                new KeyValuePair<string, string>(socKey, socValue),
                new KeyValuePair<string, string>(dobKey, dobValue),
                new KeyValuePair<string, string>(issuKey, issuValue),
                new KeyValuePair<string, string>(expKey, expValue)
           };
        private static readonly Parser<KeyValuePair<string, string>[]> RestrictionsParser =
           from restrictionKey in RESTRICTIONSPattern
           from restrictionValue in SingleSpacePattern
           from endorsementsKey in ENDORSEMENTSPattern
           from endorsementsValue in SingleSpacePattern
           from eligdateKey in ELIGDATEPattern
           from eligdateValue in RemainStringPattern
           select new[]
           {
                new KeyValuePair<string, string>(restrictionKey, restrictionValue),
                new KeyValuePair<string, string>(endorsementsKey, endorsementsValue),
                new KeyValuePair<string, string>(eligdateKey, eligdateValue)
           };
        private static readonly Parser<KeyValuePair<string, string>[]> StatusParser =
           from statusnoncdlKey in STATUSNONCDLPattern
           from statusnoncdlValue in SingleSpacePattern
           from statuscdlKey in STATUSCDLPattern
           from statuscdlValue in RemainStringPattern
           select new[]
           {
                new KeyValuePair<string, string>(statusnoncdlKey, statusnoncdlValue),
                new KeyValuePair<string, string>(statuscdlKey, statuscdlValue)
           };
        public static readonly Parser<string[]> NameParser =
            from lastName in LetterParser
            from firstName in LetterParser
            from middleName in LetterParser
            select new[] { firstName, middleName, lastName };

        public static readonly Parser<string[]> AddressLineParser =
            from addressline in MultiSpacePattern
            from city in LetterParser
            from state in LetterParser
            from postalCode in DigitalParser
            select new[] { addressline, city, state, postalCode };

        public static bool CheckEndLine(string? line)
        {
            return line != null && line.Contains("** END OF DRIVERS HISTORY **");
        }
        public static LicenseData ParseLicense(string? input)
        {
            LicenseData driver = new LicenseData();

            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            var pairs = new List<KeyValuePair<string, string>>();
            var olnAndClassParsed = OlnAndClassParser.TryParse(lines[2]);
            if (olnAndClassParsed.WasSuccessful)
            {
                pairs.AddRange(olnAndClassParsed.Value);
            }
            var nameParsed = NameParser.TryParse(lines[3]);
            if (nameParsed.WasSuccessful)
            {
                driver.FirstName = ToCapitalCase(nameParsed.Value[0]);
                driver.MiddleName = ToCapitalCase(nameParsed.Value[1]);
                driver.LastName = ToCapitalCase(nameParsed.Value[2]);
            }
            var addressParsed = AddressLineParser.TryParse(lines[4]);
            if (addressParsed.WasSuccessful)
            {
                driver.PhysicalStreetAddress = addressParsed.Value[0];
                driver.City = addressParsed.Value[1];
                driver.State = addressParsed.Value[2];
                driver.ZipCode = addressParsed.Value[3];
            }
            var bodyInfoParsed = BodyInfoParser.TryParse(lines[5]);
            if (bodyInfoParsed.WasSuccessful)
            {
                pairs.AddRange(bodyInfoParsed.Value);
            }
            var socParsed = SOCParser.TryParse(lines[6]);
            if (socParsed.WasSuccessful)
            {
                pairs.AddRange(socParsed.Value);
            }
            var restrictionsParsed = RestrictionsParser.TryParse(lines[7]);
            if (restrictionsParsed.WasSuccessful)
            {
                pairs.AddRange(restrictionsParsed.Value);
            }
            var statusParsed = StatusParser.TryParse(lines[8]);
            if (statusParsed.WasSuccessful)
            {
                pairs.AddRange(statusParsed.Value);
            }

            foreach ( (string key, string value) in pairs)
            {
                switch (key)
                {
                    case "OLN/": driver.OLN = value; break;
                    case "CLASS/": driver.Class = value; break;
                    case "DOB/": driver.DateOfBirth = value; break;
                    case "SOC/": driver.SocialSecurityNumber = value; break;
                    case "EXP/": driver.ExpirationDate = value; break;
                    case "SEX/": driver.Sex = value == "M" ? Sexs.Male : Sexs.Female;  break;
                    case "RAC/": driver.Race = ParseRace(value); break;
                    case "EYE/": driver.EyeColor = value; break;
                    case "HAI/": driver.HairColor = value; break;
                    case "HGT/": driver.Height = GetHeightRange(value); break;
                    case "WGT/": driver.Weight = value; break;
                    default: break;
                }
            }
            return driver;
        }
        public static string ToCapitalCase(string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            str = str.Trim();
            string firstChar = str.Substring(0, 1).ToUpper();
            string rest = str.Substring(1).ToLower();
            return firstChar + rest;
        }
        public static string GetHeightRange(string? str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;
            if (str.Contains('-'))
            {
                return str;
            }
            else
            {
                return $"{str.Substring(0, 1)}-{str.Substring(1)}";
            }
        }
        public static Races ParseRace(string? race)
        {
            return race switch
            {
                "W" => Races.White,
                "B" => Races.Black,
                "I" => Races.NativeAmerican,
                "H" => Races.Hispanic,
                "A" => Races.Asian,
                "O" => Races.Other,
                _ => Races.Other
            };
        }
    }
}
