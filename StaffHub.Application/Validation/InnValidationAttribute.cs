using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StaffHub.Application.Validation;

/// <summary>
/// Атрибут для проверки правильности ИНН (10 или 12 цифр с проверкой контрольной суммы).
/// </summary>
public class InnValidationAttribute : ValidationAttribute
{
    /// <summary>
    /// Проверяет строку ИНН: длина, набор цифр и контрольные разряды для 10- или 12-значного номера.
    /// </summary>
    /// <param name="value">Проверяемое значение (строка ИНН).</param>
    /// <param name="validationContext">Контекст валидации данных.</param>
    /// <returns>Результат проверки или <c>null</c>, если значение допустимо.</returns>
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

    /// <summary>
    /// Вычисляет контрольную цифру по переданным коэффициентам (алгоритм ФНС для ИНН).
    /// </summary>
    /// <param name="inn">Строка ИНН.</param>
    /// <param name="coefficients">Весовые коэффициенты для соответствующих разрядов.</param>
    /// <returns>Значение контрольного разряда.</returns>
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
