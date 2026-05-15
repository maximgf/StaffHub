using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StaffHub.Application.Validation;

public class InnValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var inn = value as string;
        if (string.IsNullOrWhiteSpace(inn))
        {
            return new ValidationResult("ИНН не может быть пустым.");
        }

        if (!inn.All(char.IsDigit))
        {
            return new ValidationResult("ИНН должен содержать только цифры.");
        }

        if (inn.Length == 10)
        {
            int d10 = GetChecksum(inn, new[] { 2, 4, 10, 3, 5, 9, 4, 6, 8 });
            if (inn[9] - '0' != d10)
            {
                return new ValidationResult("Неверное контрольное число ИНН (10 знаков).");
            }
        }
        else if (inn.Length == 12)
        {
            int d11 = GetChecksum(inn, new[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 });
            int d12 = GetChecksum(inn, new[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 });
            
            if (inn[10] - '0' != d11 || inn[11] - '0' != d12)
            {
                return new ValidationResult("Неверное контрольное число ИНН (12 знаков).");
            }
        }
        else
        {
            return new ValidationResult("ИНН должен состоять из 10 или 12 цифр.");
        }

        return ValidationResult.Success;
    }

    private int GetChecksum(string inn, int[] coefficients)
    {
        int sum = 0;
        for (int i = 0; i < coefficients.Length; i++)
        {
            sum += (inn[i] - '0') * coefficients[i];
        }
        return (sum % 11) % 10;
    }
}
