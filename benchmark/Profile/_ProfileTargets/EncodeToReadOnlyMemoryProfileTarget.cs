using HatTrick.CrockfordBase32;

namespace Profile;

public class EncodeToReadOnlyMemoryProfileTarget : IProfileTarget<long, ReadOnlyMemory<char>>
{
    public void Dispose() { }

    public ReadOnlyMemory<char> Execute(long input, bool useCheckSymbol)
        => input.ToCrockfordBase32ReadOnlyMemory(useCheckSymbol);
}
