using HatTrick.CrockfordBase32;

namespace Profile;

public class EncodeToCharArrayProfileTarget : IProfileTarget<long, char[]>
{
    public void Dispose() { }

    public char[] Execute(long input, bool useCheckSymbol)
        => input.ToCrockfordBase32CharArray(useCheckSymbol);
}
