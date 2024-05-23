public class LuhnCardValidationService : ICardValidationService, ICardDetailsValidationService
{
    /// <summary>
    /// Service class for validating credit card numbers using the Luhn algorithm.
    /// </summary>
    public LuhnCardValidationService()
    {
    }

    /// <summary>
    /// Validates the given credit card number using the Luhn algorithm.
    /// </summary>
    /// <param name="cardNumber">The credit card number to validate.</param>
    /// <returns>True if the card number is valid, otherwise false.</returns>
    public bool ValidateCard(string cardNumber)
    {
        int sum = 0;
        bool isAlternate = false;

        // Traverse the card number from right to left
        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (isAlternate)
            {
                digit *= 2;

                // If the doubled digit is greater than 9, subtract 9
                if (digit > 9)
                {
                    digit -= 9;
                }
            }

            sum += digit;
            isAlternate = !isAlternate;
        }

        // If the sum is divisible by 10, the card number is valid
        return sum % 10 == 0;
    }

    /// <summary>
    /// Validates the given card holder's name.
    /// </summary>
    /// <param name="cardHolderName">The card holder's name to validate.</param>
    /// <returns>True if the card holder's name is valid, otherwise false.</returns>
    public bool ValidateCardHolderName(string cardHolderName)
    {
        // Check that the card holder's name is not empty and contains only alphabetic characters and spaces
        return !string.IsNullOrWhiteSpace(cardHolderName) && cardHolderName.All(c => char.IsLetter(c) || c == ' ');
    }

    /// <summary>
    /// Validates the given expiry date.
    /// </summary>
    /// <param name="expiryMonth">The expiry month.</param>
    /// <param name="expiryYear">The expiry year.</param>
    /// <returns>True if the expiry date is valid, otherwise false.</returns>
    public bool ValidateExpiryDate(int expiryMonth, int expiryYear)
    {
        // Check that the expiry date is not in the past
        DateTime now = DateTime.Now;
        return expiryYear > now.Year || (expiryYear == now.Year && expiryMonth >= now.Month);
    }

    /// <summary>
    /// Validates the given CVV (Card Verification Value).
    /// </summary>
    /// <param name="cvv">The CVV to validate.</param>
    /// <returns>True if the CVV is valid, otherwise false.</returns>
    public bool ValidateCVV(int cvv)
    {
        // Check that the CVV is a 3 or 4 digit number
        return cvv >= 100 && cvv <= 9999;
    }
}