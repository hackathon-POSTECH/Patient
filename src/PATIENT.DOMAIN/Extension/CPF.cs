namespace PATIENT.DOMAIN.Extension;

public static class CPF
{
    public static bool IsValid(string cpf)
    {
        // Remove caracteres não numéricos
        cpf = cpf.Replace(".", "").Replace("-", "");

        // Verifica se o CPF tem 11 dígitos
        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return false;

        // Verifica se todos os dígitos são iguais (caso: 111.111.111-11)
        if (cpf.Distinct().Count() == 1)
            return false;

        // Validação dos dígitos verificadores
        var digits = cpf.Substring(0, 9)
            .Select(c => int.Parse(c.ToString()))
            .ToArray();

        // Primeiro dígito verificador
        var firstVerifier = CalculateVerifier(digits, 10);
        if (firstVerifier != int.Parse(cpf[9].ToString()))
            return false;

        // Segundo dígito verificador
        var secondVerifier = CalculateVerifier(digits.Concat(new[] { firstVerifier }).ToArray(), 11);
        return secondVerifier == int.Parse(cpf[10].ToString());
    }

    private static int CalculateVerifier(int[] digits, int multiplier)
    {
        var sum = digits.Select((d, i) => d * (multiplier - i)).Sum();
        var remainder = sum % 11;
        return remainder < 2 ? 0 : 11 - remainder;
    }    
}