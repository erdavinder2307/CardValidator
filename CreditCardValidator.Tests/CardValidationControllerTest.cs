using CreditCardValidator.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CreditCardValidator.Tests;

public class CreditCardValidatorTests
{
    [Fact]
    public void LuhnCardValidation_WhenCardIsValid_ReturnsOk()
    {
        // Arrange
        var cardDetails = new CardDetailsRequest
        {
            CardNumber = "4111111111111111",
            CardHolderName = "John Doe",
            ExpiryMonth = 12,
            ExpiryYear = 2023,
            CVV = 123
        };

        var mockLuhnCardValidationService = new Mock<ICardValidationService>();
        mockLuhnCardValidationService.Setup(x => x.ValidateCard(cardDetails.CardNumber)).Returns(true);

        var mockCardDetailsValidationService = new Mock<ICardDetailsValidationService>();
        mockCardDetailsValidationService.Setup(x => x.ValidateCardHolderName(cardDetails.CardHolderName)).Returns(true);
        mockCardDetailsValidationService.Setup(x => x.ValidateCVV(cardDetails.CVV)).Returns(true);
        mockCardDetailsValidationService.Setup(x => x.ValidateExpiryDate(cardDetails.ExpiryMonth, cardDetails.ExpiryYear)).Returns(true);

        var controller = new CardValidationController(mockLuhnCardValidationService.Object, mockCardDetailsValidationService.Object);

        // Act
        var result = controller.LuhnCardValidation(cardDetails);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<CardValidationResponse>(okResult.Value);
        Assert.True(response.IsValid);
        Assert.Equal("Card is valid.", response.Message);
        Assert.Equal(cardDetails.CardNumber, response.CardNumber);
    }

    [Fact]
    public void LuhnCardValidation_WhenCardIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var cardDetails = new CardDetailsRequest
        {
            CardNumber = "4111111111111111",
            CardHolderName = "John Doe",
            ExpiryMonth = 12,
            ExpiryYear = 2023,
            CVV = 123
        };

        var mockLuhnCardValidationService = new Mock<ICardValidationService>();
        mockLuhnCardValidationService.Setup(x => x.ValidateCard(cardDetails.CardNumber)).Returns(false);

        var mockCardDetailsValidationService = new Mock<ICardDetailsValidationService>();
        mockCardDetailsValidationService.Setup(x => x.ValidateCardHolderName(cardDetails.CardHolderName)).Returns(true);
        mockCardDetailsValidationService.Setup(x => x.ValidateCVV(cardDetails.CVV)).Returns(true);
        mockCardDetailsValidationService.Setup(x => x.ValidateExpiryDate(cardDetails.ExpiryMonth, cardDetails.ExpiryYear)).Returns(true);

    }
}