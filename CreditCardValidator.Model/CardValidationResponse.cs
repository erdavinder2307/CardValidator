namespace CreditCardValidator.Model;

public class CardValidationResponse
{
    public bool IsValid { get; set; }
    public required string CardNumber { get; set; }
    public required string Message { get; set; }
}
