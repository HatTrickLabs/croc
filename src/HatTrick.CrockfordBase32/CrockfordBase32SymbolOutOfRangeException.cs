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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
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

    public static void ThrowIf(
        bool condition,
        ref char value,
        string message,
        [CallerMemberName] string? caller = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        if (condition)
        {
            Throw(ref value, message, caller, expression);
        }
    }

    [DoesNotReturn]
    private static void Throw(
        ref char value,
        string message,
        [CallerMemberName] string? caller = null,
        [CallerArgumentExpression("value")] string? expression = null)
    {
        throw new CrockfordBase32SymbolOutOfRangeException(value, message);
    }
}
