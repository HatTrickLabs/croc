namespace HatTrick.CrockfordBase32.Tests;

public class TestParameters
{ 
    public long Decoded { get; set; }
    public string Encoded { get; set; }
    public string EncodedWithHyphens { get; set; }
    public string EncodedWithInvalidCharacter { get; set; }
    public string ValidCheckDigit { get; set; }
    public string InvalidCheckDigit { get; set; }

    public TestParameters(long decoded, string encoded, string encodedWithHyphens, string encodedWithInvalidCharacter, string validCheckDigit, string invalidCheckDigit)
    {
        Decoded = decoded;
        Encoded = encoded;
        EncodedWithHyphens = encodedWithHyphens;
        EncodedWithInvalidCharacter = encodedWithInvalidCharacter;
        ValidCheckDigit = validCheckDigit;
        InvalidCheckDigit = invalidCheckDigit;
    }
}