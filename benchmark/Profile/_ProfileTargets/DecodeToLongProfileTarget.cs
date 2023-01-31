using HatTrick.CrockfordBase32;

namespace Profile;

public class DecodeToLongProfileTarget : IProfileTarget<string, long>
{
    public void Dispose() { }

    public long Execute(string input, bool useCheckSymbol)
        => input.FromCrockfordBase32String(useCheckSymbol);
}
