using System.Text.RegularExpressions;

/// <summary>
/// Provides methods to validate credit card numbers using the Verhoeff algorithm.
/// </summary>
public class VerhoeffCardValidationService : ICardValidationService
{
    private static int[,] d = new int[,] {
        {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
        {1, 2, 3, 4, 0, 6, 7, 8, 9, 5},
        {2, 3, 4, 0, 1, 7, 8, 9, 5, 6},
        {3, 4, 0, 1, 2, 8, 9, 5, 6, 7},
        {4, 0, 1, 2, 3, 9, 5, 6, 7, 8},
        {5, 9, 8, 7, 6, 0, 4, 3, 2, 1},
        {6, 5, 9, 8, 7, 1, 0, 4, 3, 2},
        {7, 6, 5, 9, 8, 2, 1, 0, 4, 3},
        {8, 7, 6, 5, 9, 3, 2, 1, 0, 4},
        {9, 8, 7, 6, 5, 4, 3, 2, 1, 0}
    };

    private static int[,] p = new int[,] {
        {0, 1, 2, 3, 4, 5, 6, 7, 8, 9},
        {1, 5, 7, 6, 2, 8, 3, 0, 9, 4},
        {5, 8, 0, 3, 7, 9, 6, 1, 4, 2},
        {8, 9, 1, 6, 0, 4, 3, 5, 2, 7},
        {9, 4, 5, 3, 1, 2, 6, 8, 7, 0},
        {4, 2, 8, 6, 5, 7, 3, 9, 0, 1},
        {2, 7, 9, 3, 8, 0, 6, 4, 1, 5},
        {7, 0, 4, 6, 9, 1, 3, 2, 5, 8}
    };

    private static int[] inv = new int[] { 0, 4, 3, 2, 1, 5, 6, 7, 8, 9 };

    /// <summary>
    /// Validates a credit card number using the Verhoeff algorithm.
    /// </summary>
    /// <param name="cardNumber">The credit card number to validate.</param>
    /// <returns>True if the credit card number is valid, otherwise false.</returns>
    public bool ValidateCard(string cardNumber)
    {
        int c = 0;
        int[] myArray = StringToReversedIntArray(cardNumber);
        for (int i = 0; i < myArray.Length; i++)
        {
            c = d[c, p[(i % 8), myArray[i]]];
        }

        return (c == 0);
    }

    private int[] StringToReversedIntArray(string num)
    {
        int[] myArray = new int[num.Length];
        for (int i = 0; i < num.Length; i++)
        {
            myArray[i] = int.Parse(num.Substring(i, 1));
        }

        Array.Reverse(myArray);
        return myArray;
    }
}