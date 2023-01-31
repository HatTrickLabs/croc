using System.Runtime.Serialization;

namespace HatTrick.CrockfordBase32;

public class CrockfordBase32SymbolOutOfRangeException : ArgumentOutOfRangeException
{
    public char Symbol { get; init; }

    public CrockfordBase32SymbolOutOfRangeException(char symbol, string message)
        : base(message)
    {
        Symbol = symbol;
    }

    public CrockfordBase32SymbolOutOfRangeException(char symbol, string message, Exception innerException)
        : base(message, innerException)
    {
        Symbol = symbol;
    }

    protected CrockfordBase32SymbolOutOfRangeException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Symbol = (char)info.GetValue("Symbol", typeof(char))!;
    }
}
