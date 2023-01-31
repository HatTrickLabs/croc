using HatTrick.CrockfordBase32;

namespace Profile;

public class EncodeToStringProfileTarget : IProfileTarget<long, string>
{
    public void Dispose() { }

    public string Execute(long input, bool useCheckSymbol)
        => input.ToCrockfordBase32String(useCheckSymbol);
}
