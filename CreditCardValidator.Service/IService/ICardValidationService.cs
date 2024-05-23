
public interface ICardValidationService
{
    bool ValidateCard(string cardNumber);

}

public interface ICardDetailsValidationService
{
    bool ValidateCardHolderName(string cardHolderName);
    public bool ValidateExpiryDate(int expiryMonth, int expiryYear);
    public bool ValidateCVV(int cvv);
}