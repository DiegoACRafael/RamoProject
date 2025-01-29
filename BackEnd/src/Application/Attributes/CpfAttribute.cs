using System.ComponentModel.DataAnnotations;
using System.Linq;

public class CpfAttribute : ValidationAttribute
{
    public CpfAttribute() 
    {
        ErrorMessage = "CPF invalid.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        string cpf = value.ToString();

        if (!IsCpfValid(cpf))
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }

    private bool IsCpfValid(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        if (cpf.All(c => c == cpf[0]))
            return false;

        int[] primeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int digito1 = CalcularDigito(cpf.Substring(0, 9), primeiroDigito);
        if (digito1 != int.Parse(cpf[9].ToString()))
            return false;

        int[] segundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int digito2 = CalcularDigito(cpf.Substring(0, 10), segundoDigito);
        if (digito2 != int.Parse(cpf[10].ToString()))
            return false;

        return true;
    }

    private int CalcularDigito(string cpf, int[] pesos)
    {
        int soma = 0;
        for (int i = 0; i < cpf.Length; i++)
        {
            soma += int.Parse(cpf[i].ToString()) * pesos[i];
        }
        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
