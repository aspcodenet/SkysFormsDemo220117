using System.ComponentModel.DataAnnotations;

namespace SkysFormsDemo.Infrastructure.Validation;

public class GoodYearAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        string year = value.ToString();
        if (year == "1972")
            return true;
        if (year == "1973")
            return true;
        return false;
    }
}