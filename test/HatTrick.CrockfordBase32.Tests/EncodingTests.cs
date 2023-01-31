using HatTrick.CrockfordBase32;
using Newtonsoft.Json.Linq;
using System.Buffers;

namespace HatTrick.CrockfordBase32.Tests;

public class EncodingTests
{
    [Theory]
    [TestData]
    public void Can_encode_values_to_string_using_ref_array(TestParameters parameters)
    {
        //given & when
        var rented = ArrayPool<char>.Shared.Rent(14);
        var start = CrockfordBase32.Encode((ulong)parameters.Decoded, false, ref rented);
        var result = string.Create(rented.Length - start, rented, (s, c) => c[start..].CopyTo(s));

        //then
        Assert.Equal(parameters.Encoded, result);
        ArrayPool<char>.Shared.Return(rented);
    }

    [Fact]
    public void Does_encoding_values_to_string_using_ref_array_with_small_array_fail_as_expected()
    {
        //given & when
        var rented = new char[10];

        //then
        Assert.Throws<ArgumentException>(() => CrockfordBase32.Encode(int.MaxValue, false, ref rented));
    }

    [Theory]
    [TestData]
    public void Can_encode_values_to_string(TestParameters parameters)
    {
        //given & when
        var result = parameters.Decoded.ToCrockfordBase32String();

        //then
        Assert.Equal(parameters.Encoded, result);
    }

    [Theory]
    [TestData]
    public void Does_encoding_with_check_digit_to_string_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32String(true);
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_to_string_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32String(out var _));
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_with_check_digit_to_string_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32String(true, out var _));
    }

    [Fact]
    public void Does_encoding_a_negative_value_fail_as_expected()
    {
        //given & when & then
        Assert.Throws<ArgumentException>(() => (-1L).ToCrockfordBase32String());
    }

    [Theory]
    [TestData]
    public void Does_encoding_to_read_only_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32ReadOnlyMemory();
    }

    [Theory]
    [TestData]
    public void Does_encoding_with_check_digit_to_read_only_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32ReadOnlyMemory(true);
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_to_read_only_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32ReadOnlyMemory(out var _));
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_with_check_digit_to_read_only_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32ReadOnlyMemory(true, out var _));
    }

    [Theory]
    [TestData]
    public void Does_encoding_to_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32Memory();
    }

    [Theory]
    [TestData]
    public void Does_encoding_with_check_digit_to_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32Memory(true);
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_to_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32Memory(out var _));
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_with_check_digit_to_memory_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32Memory(true, out var _));
    }

    [Theory]
    [TestData]
    public void Does_encoding_to_char_array_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32CharArray();
    }

    [Theory]
    [TestData]
    public void Does_encoding_with_check_digit_to_char_array_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32CharArray(true);
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_to_char_array_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32CharArray(out var _));
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_with_check_digit_to_char_array_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32CharArray(true, out var _));
    }

    [Theory]
    [TestData]
    public void Does_encoding_to_byte_array_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32ByteArray();
    }

    [Theory]
    [TestData]
    public void Does_encoding_with_check_digit_to_byte_array_succeed(TestParameters parameters)
    {
        //given & when & then
        parameters.Decoded.ToCrockfordBase32ByteArray(true);
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_to_byte_array_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32ByteArray(out var _));
    }

    [Theory]
    [TestData]
    public void Does_trying_to_encode_with_check_digit_to_byte_array_succeed(TestParameters parameters)
    {
        //given & when & then
        Assert.True(parameters.Decoded.TryToCrockfordBase32ByteArray(true, out var _));
    }
}