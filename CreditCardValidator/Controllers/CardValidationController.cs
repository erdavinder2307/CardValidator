using CreditCardValidator.Model;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardValidator;

[Route("api/[controller]")]
[ApiController]
public class CardValidationController : ControllerBase
{
    private readonly ICardValidationService _luhnCardValidationService;
    private readonly ICardDetailsValidationService _cardDetailsValidationService;
    public CardValidationController(ICardValidationService luhnCardValidationService,

    ICardDetailsValidationService cardDetailsValidationService)
    {
        _luhnCardValidationService = luhnCardValidationService;
        _cardDetailsValidationService = cardDetailsValidationService;
    }


    [HttpPost]
    public IActionResult LuhnCardValidation(CardDetailsRequest cardDetails)
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
}
