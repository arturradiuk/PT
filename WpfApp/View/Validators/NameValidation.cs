using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace View
{
    public class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = value as string;
            bool isValid = Regex.IsMatch(name ?? string.Empty, @"[\w\s]{2,50}");
            if (!isValid)
                return new ValidationResult(false, "value must contain of 2-50 non-special characters only");
            return ValidationResult.ValidResult;;
        }
    }
}