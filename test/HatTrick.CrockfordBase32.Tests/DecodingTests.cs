using HatTrick.CrockfordBase32;

namespace HatTrick.CrockfordBase32.Tests;

public class DecodingTests
{
    [Theory]
    [TestData]
    public void Can_decode_values_to_string(TestParameters parameters)
    {
        //given & when
        var result = parameters.Encoded.FromCrockfordBase32String();

        //then
        Assert.Equal(parameters.Decoded, result);
    }

    [Theory]
    [TestData]
    public void Can_encode_values_to_string_with_check_symbol(TestParameters parameters)
    {
        //given & when
        var result = parameters.Decoded.ToCrockfordBase32String(true);

        //then
        Assert.Equal($"{parameters.Encoded}{parameters.ValidCheckDigit}", result);
    }

    [Theory]
    [TestData]
    public void Can_decode_values_to_string_with_check_symbol(TestParameters parameters)
    {
        //given & when
        var result = $"{parameters.Encoded}{parameters.ValidCheckDigit}".FromCrockfordBase32String(true);

        //then
        Assert.Equal(parameters.Decoded, result);
    }

    [Theory]
    [TestData]
    public void Can_decode_values_with_hyphens_to_string(TestParameters parameters)
    {
        //given & when
        var result = parameters.EncodedWithHyphens.FromCrockfordBase32String(false);

        //then
        Assert.Equal(parameters.Decoded, result);
    }

    [Theory]
    [TestData]
    public void Can_decode_values_with_hyphens_and_check_digit_to_string(TestParameters parameters)
    {
        //given & when
        var result = $"{parameters.EncodedWithHyphens}{parameters.ValidCheckDigit}".FromCrockfordBase32String(true);

        //then
        Assert.Equal(parameters.Decoded, result);
    }

    [Theory]
    [TestData]
    public void Can_decode_values_with_hyphens_and_check_digit_and_ending_in_hyphen_to_string(TestParameters parameters)
    {
        //given & when
        var result = $"{parameters.EncodedWithHyphens}{parameters.ValidCheckDigit}-".FromCrockfordBase32String(true);

        //then
        Assert.Equal(parameters.Decoded, result);
    }

    [Theory]
    [TestData]
    public void Does_decoding_values_with_invalid_character_fail_as_expected(TestParameters parameters)
    {
        //given & when & then
        var ex = Assert.Throws<CrockfordBase32SymbolOutOfRangeException>(() => parameters.EncodedWithInvalidCharacter.FromCrockfordBase32String(false));
    }

    [Theory]
    [TestData]
    public void Does_decoding_values_with_invalid_check_symbol_fail_as_expected(TestParameters parameters)
    {
        //given & when & then
        Assert.Throws<CrockfordBase32FormatException>(() => $"{parameters.Encoded}{parameters.InvalidCheckDigit}-".FromCrockfordBase32String(true));
    }

    [Theory]
    [TestData]
    public void Does_decoding_values_with_invalid_character_as_check_symbol_fail_as_expected(TestParameters parameters)
    {
        //given & when & then
        Assert.Throws<CrockfordBase32FormatException>(() => $"{parameters.Encoded}U".FromCrockfordBase32String(true));
    }

    [Fact]
    public void Does_decoding_empty_string_fail_as_expected()
    {
        //given & when & then
        Assert.Throws<ArgumentException>(() => string.Empty.FromCrockfordBase32String(true));
    }
}