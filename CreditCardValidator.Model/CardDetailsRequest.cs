namespace CreditCardValidator.Model;

public class CardDetailsRequest
{
    public required string CardNumber { get; set; }
    public required string CardHolderName { get; set; }
    public int ExpiryMonth { get; set; }
    public int ExpiryYear { get; set; }
    public int CVV { get; set; }
}
