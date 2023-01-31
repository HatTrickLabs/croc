using System.Reflection;

namespace HatTrick.CrockfordBase32.Tests;

public sealed class TestDataAttribute : ClassDataAttribute
{
    public TestDataAttribute() : base(typeof(TestDataAttribute)) { }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { new TestParameters(0L, "0", "0-0", "0U", "0", "$") };
        yield return new object[] { new TestParameters(127L, "3Z", "3-Z", "3U", "G", "$") }; //byte.MaxValue / 2
        yield return new object[] { new TestParameters(255L, "7Z", "7-Z", "UZ", "~", "$") }; //byte.MaxValue
        yield return new object[] { new TestParameters(256L, "80", "8-0", "8U", "$", "~") }; //byte.MaxValue + 1
        yield return new object[] { new TestParameters(1073741823L, "ZZZZZZ", "-ZZ-ZZZ-Z", "ZZUZZZ", "A", "$") }; //int.MaxValue / 2
        yield return new object[] { new TestParameters(2147483647L, "1ZZZZZZ", "1-ZZZZZZ-", "1ZZUZZZ", "N", "$") }; //int.MaxValue
        yield return new object[] { new TestParameters(2147483648L, "2000000", "2-0-0-0-0-0-0", "20000U0", "P", "$") }; //int.MaxValue + 1
        yield return new object[] { new TestParameters(4611686018427387903, "3ZZZZZZZZZZZZ", "-3-Z-Z-Z-Z-Z-Z-Z-Z-Z-Z-Z-Z-", "3ZUZZZZZZZZZZ", "2", "$") }; //long.MaxValue / 2
        yield return new object[] { new TestParameters(9223372036854775806, "7ZZZZZZZZZZZY", "-7ZZZZZZZZZZZY-", "7ZZZZZZZZZZUY", "4", "$") }; //long.MaxValue - 1
        yield return new object[] { new TestParameters(9223372036854775807, "7ZZZZZZZZZZZZ", "7ZZZ-ZZZZZ-ZZZZ", "7UZZZZZZZZZZZ", "5", "$") }; //long.MaxValue
    }
}