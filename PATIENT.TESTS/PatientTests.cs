using PATIENT.DOMAIN;

namespace PATIENT.TESTS;

public class PatientTests
{
    [Fact]
    public void CreatePatient_ShouldThrowException_WhenCpfIsInvalid()
    {
        // Arrange
        var name = "John Doe";
        var invalidCpf = "999.999.999-99";
        var email = "john.doe@example.com";
        var userId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            Patient.CreatePatient(name, invalidCpf, email, userId));

        Assert.Equal("CPF inválido.", exception.Message);
    }

    [Fact]
    public void CreatePatient_ShouldInitializePropertiesCorrectly_WhenCpfIsValid()
    {
        // Arrange
        var name = "Jane Doe";
        var validCpf = "123.456.789-09";
        var email = "jane.doe@example.com";
        var userId = Guid.NewGuid();

        // Act
        var patient = Patient.CreatePatient(name, validCpf, email, userId);

        // Assert
        Assert.Equal(name, patient.Name);
        Assert.Equal(validCpf, patient.Cpf);
        Assert.Equal(email, patient.Email);
        Assert.Equal(userId, patient.UserId);
    }
}