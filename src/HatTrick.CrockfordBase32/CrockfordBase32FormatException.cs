using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HatTrick.CrockfordBase32;

public class CrockfordBase32FormatException : FormatException
{
    public string Value { get; init; }

    public CrockfordBase32FormatException(string value, string message)
        : base(message)
    {
        Value = value;
    }

    public CrockfordBase32FormatException(string value, string message, Exception innerException)
        : base(message, innerException)
    {
        Value = value;
    }

    protected CrockfordBase32FormatException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Value = (string)info.GetValue("Value", typeof(string))!;
    }
}
