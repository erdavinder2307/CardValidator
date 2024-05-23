using CreditCardValidator.Model;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator;

[Route("api/[controller]")]
[ApiController]
public class CardValidationController : ControllerBase
{
    private readonly ICardValidationService _luhnCardValidationService;
    private readonly ICardDetailsValidationService _cardDetailsValidationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CardValidationController"/> class.
    /// </summary>
    /// <param name="luhnCardValidationService">The service for Luhn card validation.</param>
    /// <param name="cardDetailsValidationService">The service for card details validation.</param>
    public CardValidationController(ICardValidationService luhnCardValidationService,
                                    ICardDetailsValidationService cardDetailsValidationService)
    {
        _luhnCardValidationService = luhnCardValidationService;
        _cardDetailsValidationService = cardDetailsValidationService;
    }

    /// <summary>
    /// Validates a credit card using the Luhn algorithm and additional card details.
    /// </summary>
    /// <param name="cardDetails">The card details to validate.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the validation.</returns>
    /// [HttpPost]
    public IActionResult LuhnCardValidation(CardDetailsRequest cardDetails)
    {
        try
        {
            bool isValid = _luhnCardValidationService.ValidateCard(cardDetails.CardNumber) &&
                           _cardDetailsValidationService.ValidateCardHolderName(cardDetails.CardHolderName) &&
                           _cardDetailsValidationService.ValidateCVV(cardDetails.CVV) &&
                           _cardDetailsValidationService.ValidateExpiryDate(cardDetails.ExpiryMonth, cardDetails.ExpiryYear);

            if (isValid)
            {
                var response = new CardValidationResponse
                {
                    IsValid = true,
                    Message = "Card is valid.",
                    CardNumber = cardDetails.CardNumber,
                };

                return Ok(response);
            }
            else
            {
                var response = new CardValidationResponse
                {
                    IsValid = false,
                    Message = "Card is invalid.",
                    CardNumber = cardDetails.CardNumber,
                };

                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
