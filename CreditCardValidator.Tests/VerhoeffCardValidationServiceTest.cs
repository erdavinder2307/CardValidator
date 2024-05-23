namespace CreditCardValidator.Tests;

public class VerhoeffCardValidationServiceTest
{
    [Fact]
    public void ValidateCard_WhenCardIsValid_ReturnsTrue()
    {
        // Arrange
        var cardNumber = "4166432103099989";
        var verhoeffCardValidationService = new VerhoeffCardValidationService();

        // Act
        var result = verhoeffCardValidationService.ValidateCard(cardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateCard_WhenCardIsInvalid_ReturnsFalse()
    {
        // Arrange
        var cardNumber = "4111111111111112";
        var verhoeffCardValidationService = new VerhoeffCardValidationService();

        // Act
        var result = verhoeffCardValidationService.ValidateCard(cardNumber);

        // Assert
        Assert.False(result);
    }

}
