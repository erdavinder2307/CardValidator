namespace CreditCardValidator.Tests;

public class LuhnCardValidationServiceTest
{
    [Fact]
    public void ValidateCard_WhenCardIsValid_ReturnsTrue()
    {
        // Arrange
        var cardNumber = "4111111111111111";
        var luhnCardValidationService = new LuhnCardValidationService();

        // Act
        var result = luhnCardValidationService.ValidateCard(cardNumber);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ValidateCard_WhenCardIsInvalid_ReturnsFalse()
    {
        // Arrange
        var cardNumber = "4111111111111112";
        var luhnCardValidationService = new LuhnCardValidationService();

        // Act
        var result = luhnCardValidationService.ValidateCard(cardNumber);

        // Assert
        Assert.False(result);
    }

}
