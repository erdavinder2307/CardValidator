public class LuhnCardValidationService : ICardValidationService, ICardDetailsValidationService
{
    public LuhnCardValidationService()
    {
    }

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
    public bool ValidateCardHolderName(string cardHolderName)
    {
        // Check that the card holder's name is not empty and contains only alphabetic characters and spaces
        return !string.IsNullOrWhiteSpace(cardHolderName) && cardHolderName.All(c => char.IsLetter(c) || c == ' ');
    }

    public bool ValidateExpiryDate(int expiryMonth, int expiryYear)
    {
        // Check that the expiry date is not in the past
        DateTime now = DateTime.Now;
        return expiryYear > now.Year || (expiryYear == now.Year && expiryMonth >= now.Month);
    }

    public bool ValidateCVV(int cvv)
    {
        // Check that the CVV is a 3 or 4 digit number
        return cvv >= 100 && cvv <= 9999;
    }
}