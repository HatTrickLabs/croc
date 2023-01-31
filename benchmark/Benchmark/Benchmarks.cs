using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using HatTrick.CrockfordBase32;

namespace Benchmark;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Method)]
[RankColumn]
public class Benchmarks
{
    public record BenchmarkParameter(long Value, string Encoded, string EncodedWithCheckSymbol)
    {
        public override string ToString()   
            => $"{Value}:{EncodedWithCheckSymbol[..^1]}[{EncodedWithCheckSymbol[^1..]}]";
    }

    public static List<BenchmarkParameter> BenchmarkParameters => new List<BenchmarkParameter>()
    {
        // ReSharper disable StringLiteralTypo
        new(0L,"0", "00"),
        new(byte.MaxValue / 2, "3Z", "3ZG"),
        new(byte.MaxValue, "7Z", "7Z~"),
        new(int.MaxValue /  2, "ZZZZZZ", "ZZZZZZA"),
        new(int.MaxValue, "1ZZZZZZ", "1ZZZZZZN"),
        new(long.MaxValue / 2,"3ZZZZZZZZZZZZ", "3ZZZZZZZZZZZZ2"),
        new(long.MaxValue, "7ZZZZZZZZZZZZ", "7ZZZZZZZZZZZZ5")
        // ReSharper enable StringLiteralTypo

    };

    [ParamsSource(nameof(BenchmarkParameters))]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public BenchmarkParameter? Parameter { get; set; }

    [Params(true, false)]
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnassignedField.Global
    public bool UseCheckSymbol;

    [Benchmark(Description = "Encode to read only memory")]
    public ReadOnlyMemory<char> EncodeToReadOnlyMemory()
    {
        return Parameter!.Value.ToCrockfordBase32ReadOnlyMemory(UseCheckSymbol);
    }

    [Benchmark(Description = "Encode to string")]
    public string EncodeToString()
    {
        return Parameter!.Value.ToCrockfordBase32String(UseCheckSymbol);
    }

    // [Benchmark(Description = "Encode to char array")]
    // public char[] EncodeToCharArray()
    // {
    //     return Parameter!.Value.ToCrockfordBase32CharArray(UseCheckSymbol);
    // }

    [Benchmark(Description = "Decode from string")]
    public long DecodeToLong()
    {
        return (UseCheckSymbol ? Parameter!.EncodedWithCheckSymbol : Parameter!.Encoded).FromCrockfordBase32String(UseCheckSymbol);
    }
}
