#region license
// Copyright (c) HatTrick Labs, LLC.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// The latest version of this file can be found at https://github.com/HatTrickLabs/croc
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
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

    public static void ThrowIf(
        bool condition,
        string value,
        string message,
        [CallerMemberName] string? caller = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (condition)
        {
            Throw(value, message, caller, expression);
        }
    }

    [DoesNotReturn]
    private static void Throw(
        string value,
        string message,
        [CallerMemberName] string? caller = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        throw new CrockfordBase32FormatException(value, message);
    }
}
