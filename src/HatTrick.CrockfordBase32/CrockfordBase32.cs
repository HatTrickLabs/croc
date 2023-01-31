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

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace HatTrick.CrockfordBase32;

public static class CrockfordBase32
{
    #region internals
    private const int DecodeBase = 32;
    private const int CheckSymbolBase = 37;
    private const int TargetBitSize = 5;
    private const int ShiftBitSize = 59;
    private const byte ArraySize = 13;
    private const byte ArraySizeWithCheckSymbol = 14;
    private const char Hyphen = '-';

    #region maps
    private static readonly Dictionary<byte, char> EncodeMap = new()
    {
            { 0, '0' },
            { 1, '1' },
            { 2, '2' },
            { 3, '3' },
            { 4, '4' },
            { 5, '5' },
            { 6, '6' },
            { 7, '7' },
            { 8, '8' },
            { 9, '9' },
            { 10, 'A' },
            { 11, 'B' },
            { 12, 'C' },
            { 13, 'D' },
            { 14, 'E' },
            { 15, 'F' },
            { 16, 'G' },
            { 17, 'H' },
            { 18, 'J' },
            { 19, 'K' },
            { 20, 'M' },
            { 21, 'N' },
            { 22, 'P' },
            { 23, 'Q' },
            { 24, 'R' },
            { 25, 'S' },
            { 26, 'T' },
            { 27, 'V' },
            { 28, 'W' },
            { 29, 'X' },
            { 30, 'Y' },
            { 31, 'Z' }
        };

    private static readonly Dictionary<byte, char> EncodeCheckSymbolMap = new(EncodeMap)
        {
            { 32, '*' },
            { 33, '~' },
            { 34, '$' },
            { 35, '=' },
            { 36, 'U' }
        };

    private static readonly Dictionary<char, byte> DecodeMap = new()
    {
            { '0', 0 },
            { 'O', 0 },
            { 'o', 0 },
            { '1', 1 },
            { 'I', 1 },
            { 'i', 1 },
            { 'L', 1 },
            { 'l', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'A', 10 },
            { 'a', 10 },
            { 'B', 11 },
            { 'b', 11 },
            { 'C', 12 },
            { 'c', 12 },
            { 'D', 13 },
            { 'd', 13 },
            { 'E', 14 },
            { 'e', 14 },
            { 'F', 15 },
            { 'f', 15 },
            { 'G', 16 },
            { 'g', 16 },
            { 'H', 17 },
            { 'h', 17 },
            { 'J', 18 },
            { 'j', 18 },
            { 'K', 19 },
            { 'k', 19 },
            { 'M', 20 },
            { 'm', 20 },
            { 'N', 21 },
            { 'n', 21 },
            { 'P', 22 },
            { 'p', 22 },
            { 'Q', 23 },
            { 'q', 23 },
            { 'R', 24 },
            { 'r', 24 },
            { 'S', 25 },
            { 's', 25 },
            { 'T', 26 },
            { 't', 26 },
            { 'V', 27 },
            { 'v', 27 },
            { 'W', 28 },
            { 'w', 28 },
            { 'X', 29 },
            { 'x', 29 },
            { 'Y', 30 },
            { 'y', 30 },
            { 'Z', 31 },
            { 'z', 31 }
        };

    private static readonly Dictionary<char, byte> DecodeCheckSymbolMap = new(DecodeMap)
        {
            { '*', 32 },
            { '~', 33 },
            { '$', 34 },
            { '=', 35 },
            { 'U', 36 },
            { 'u', 36 }
        };
    #endregion
    #endregion

    #region methods
    #region encode
    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="ReadOnlyMemory{Char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static ReadOnlyMemory<char> GetReadOnlyMemory(long value)
        => GetReadOnlyMemory(value, false);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <returns>A <see cref="ReadOnlyMemory{Char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static ReadOnlyMemory<char> GetReadOnlyMemory(long value, bool useCheckSymbol)
        => Encode(value, useCheckSymbol)!;

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="ReadOnlyMemory{Char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetReadOnlyMemory(long value, out ReadOnlyMemory<char>? result)
        => TryGetReadOnlyMemory(value, false, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="result">A <see cref="ReadOnlyMemory{Char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetReadOnlyMemory(long value, bool useCheckSymbol, out ReadOnlyMemory<char>? result)
    {
        result = null;
        try
        {
            result = Encode(value, useCheckSymbol);
            if (result is not null)
                return true;
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="Memory{Char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static Memory<char> GetMemory(long value)
        => GetMemory(value, false);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <returns>A <see cref="Memory{Char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static Memory<char> GetMemory(long value, bool useCheckSymbol)
        => Encode(value, useCheckSymbol)!;

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="Memory{Char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetMemory(long value, out Memory<char>? result)
        => TryGetMemory(value, false, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="result">A <see cref="Memory{Char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetMemory(long value, bool useCheckSymbol, out Memory<char>? result)
    {
        result = null;
        try
        {
            result = Encode(value, useCheckSymbol);
            if (result is not null)
                return true;
        }
        catch (Exception)
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="char"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static char[] GetCharArray(long value)
        => GetCharArray(value, false);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <returns>A <see cref="char"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static char[] GetCharArray(long value, bool useCheckSymbol)
        => Encode(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="char"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetCharArray(long value, out char[]? result)
        => TryGetCharArray(value, false, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="result">A <see cref="char"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetCharArray(long value, bool useCheckSymbol, out char[]? result)
    {
        result = null;
        try
        {
            result = Encode(value, useCheckSymbol);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="byte"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static byte[] GetByteArray(long value)
        => GetByteArray(value, false);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <returns>A <see cref="byte"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static byte[] GetByteArray(long value, bool useCheckSymbol)
        => Encode(value, useCheckSymbol).Select(x => (byte)x).ToArray();

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="byte"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetByteArray(long value, out byte[]? result)
        => TryGetByteArray(value, false, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="result">A <see cref="byte"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetByteArray(long value, bool useCheckSymbol, out byte[]? result)
    {
        result = null;
        try
        {
            result = Encode(value, useCheckSymbol).Select(x => (byte)x).ToArray();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="string"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static string GetString(long value)
        => GetString(value, false);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <returns>A <see cref="string"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    public static string GetString(long value, bool useCheckSymbol)
    {
        var chars = Encode(value, useCheckSymbol);
        return string.Create(chars.Length, chars, (s, c) => c.CopyTo(s));
    }

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="string"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetString(long value, out string? result)
        => TryGetString(value, false, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="result">A <see cref="string"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetString(long value, bool useCheckSymbol, out string? result)
    {
        result = null;
        try
        {
            var chars = Encode(value, useCheckSymbol);
            if (chars.Length == 0)
                return false;
            result = string.Create(chars.Length, chars, (s, c) => c.CopyTo(s));
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private static char[] Encode(long value, bool useCheckSymbol)
    {
        if (value < 0)
            throw new ArgumentException("Value must be greater than 0.", nameof(value));

        return Encode((ulong)value, useCheckSymbol);
    }

    private static char[] Encode(ulong value, bool useCheckSymbol)
    {
        var size = useCheckSymbol ? ArraySizeWithCheckSymbol : ArraySize;
        Span<char> encoded = stackalloc char[size];

        if (useCheckSymbol)
        {
            ref var resolvedCheckSymbol = ref CollectionsMarshal.GetValueRefOrNullRef(EncodeCheckSymbolMap, (byte)(value % CheckSymbolBase));
            encoded[ArraySize] = resolvedCheckSymbol;
        }

        var rangeStart = useCheckSymbol ? ArraySizeWithCheckSymbol - 1 : ArraySize;
        for (byte index = 1; index <= size; index++)
        {
            rangeStart--;
            ref var resolvedValue = ref CollectionsMarshal.GetValueRefOrNullRef(EncodeMap, (byte)(value << ShiftBitSize >> ShiftBitSize));
            encoded[ArraySize - index] = resolvedValue;
            value = value >> TargetBitSize;
            if (value <= 0)
                break;
        }
        return encoded[rangeStart..].ToArray();
    }

    /// <summary>
    /// Encodes a signed <see cref="long"/> value into the provided array of symbols using Crockford Base32 encoding.  This 
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="encodedSymbolReceiver">The destination array for encoded bytes.</param>
    /// <returns>The start index in the array where the encoded symbols start.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided encoded symbol receiver length is less than the required length (14).</exception>
    /// <remarks>This implementation is only useful in cases where the <paramref name="encodedSymbolReceiver"/> is managed and used in a performant way (i.e. sparse use and using Rent/Return pattern of <see cref="ArrayPool{T}"/>).</remarks>
    public static int Encode(long value, bool useCheckSymbol, ref char[] encodedSymbolReceiver)
    {
        if (value < 0)
            throw new ArgumentException("Value must be greater than 0.", nameof(value));

        return Encode((ulong)value, useCheckSymbol, ref encodedSymbolReceiver);
    }

    /// <summary>
    /// Encodes an unsigned <see cref="ulong"/> value into the provided array of symbols using Crockford Base32 encoding.  This 
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Compute and append a check symbol to the return value.</param>
    /// <param name="encodedSymbolReceiver">The destination array for encoded bytes.</param>
    /// <returns>The start index in the array where the encoded symbols start.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided encoded symbol receiver length is less than the required length (14).</exception>
    /// <remarks>This implementation is only useful in cases where the <paramref name="encodedSymbolReceiver"/> is managed and used in a performant way (i.e. sparse use and using Rent/Return pattern of <see cref="ArrayPool{T}"/>).</remarks>
    public static int Encode(ulong value, bool useCheckSymbol, ref char[] encodedSymbolReceiver)
    {
        if (encodedSymbolReceiver.Length < 14)
            throw new ArgumentException($"The receiving array must have a length greater than or equal to {ArraySizeWithCheckSymbol}", nameof(encodedSymbolReceiver));

        var size = useCheckSymbol ? encodedSymbolReceiver.Length : encodedSymbolReceiver.Length - 1;

        if (useCheckSymbol)
        {
            ref var resolvedCheckSymbol = ref CollectionsMarshal.GetValueRefOrNullRef(EncodeCheckSymbolMap, (byte)(value % CheckSymbolBase));
            encodedSymbolReceiver[^1] = resolvedCheckSymbol;
        }

        var rangeStart = useCheckSymbol ? encodedSymbolReceiver.Length - 1 : encodedSymbolReceiver.Length;
        for (byte index = 1; index <= size; index++)
        {
            rangeStart--;
            ref var resolvedValue = ref CollectionsMarshal.GetValueRefOrNullRef(EncodeMap, (byte)(value << ShiftBitSize >> ShiftBitSize));
            encodedSymbolReceiver[^index] = resolvedValue;
            value = value >> TargetBitSize;
            if (value <= 0)
                break;
        }

        return rangeStart;
    }
    #endregion

    #region decode
    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(ReadOnlyMemory<char> value)
        => Decode(value, false, true)!.Value;

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(ReadOnlyMemory<char> value, bool hasCheckSymbol)
        => Decode(value, hasCheckSymbol, true)!.Value;

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(ReadOnlyMemory<char> value, out long? result)
        => TryGetInt64(value, false, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(ReadOnlyMemory<char> value, bool hasCheckSymbol, out long? result)
    {
        result = null;
        try
        {
            result = Decode(value, hasCheckSymbol, false);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(Memory<char> value)
        => GetInt64(value, false);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(Memory<char> value, bool hasCheckSymbol)
        => Decode(value, hasCheckSymbol, true)!.Value;

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(Memory<char> value, out long? result)
        => TryGetInt64(value, false, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{Char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{Char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(Memory<char> value, bool hasCheckSymbol, out long? result)
    {
        result = null;
        try
        {
            result = Decode(value, hasCheckSymbol, false);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(char[] value)
        => GetInt64(value, false);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(char[] value, bool hasCheckSymbol)
        => Decode(value, hasCheckSymbol, true)!.Value;

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(char[] value, out long? result)
        => TryGetInt64(value, false, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(char[] value, bool hasCheckSymbol, out long? result)
    {
        result = null;
        try
        {
            result = Decode(value, hasCheckSymbol, false);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(byte[] value)
        => GetInt64(value, false);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character).</exception>
    /// <exception cref="CrockfordBase32FormatException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, 
    /// the computed check symbol indicates wrong or transposed symbols.</exception>
    public static long GetInt64(byte[] value, bool hasCheckSymbol)
        => Decode(Encoding.UTF8.GetChars(value.ToArray()), hasCheckSymbol, true)!.Value;

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(byte[] value, out long? result)
        => TryGetInt64(value, false, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(byte[] value, bool hasCheckSymbol, out long? result)
    {
        result = null;
        try
        {
            result = Decode(Encoding.UTF8.GetChars(value.ToArray()), hasCheckSymbol, false);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long GetInt64(string value)
        => GetInt64(value, false);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="CrockfordBase32SymbolOutOfRangeException">Thrown when the <see cref="ReadOnlyMemory{Char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII character) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long GetInt64(string value, bool hasCheckSymbol)
        => Decode(value.AsMemory(), hasCheckSymbol, true)!.Value;

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="string"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(string value, out long? result)
        => TryGetInt64(value, false, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="string"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryGetInt64(string value, bool hasCheckSymbol, out long? result)
    {
        result = null;
        try
        {
            result = Decode(value.AsMemory(), hasCheckSymbol, false);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    private static long? Decode(ReadOnlyMemory<char> value, bool hasCheckSymbol, bool throwOnError)
    {
        if (value.IsEmpty)
            return throwOnError ? throw new ArgumentException("The provided value is empty and cannot be decoded") : null;

        long result = 0;
        long currentBase = 1;
        int? checkSymbol = null;
        for (var index = (value.Length - 1); index >= 0; index--)
        {
            var current = value.Span[index];

            if (current == Hyphen)
                continue;

            if (hasCheckSymbol && !checkSymbol.HasValue)
            {
                ref var resolvedCheckSymbol = ref CollectionsMarshal.GetValueRefOrNullRef(DecodeCheckSymbolMap, current);
                if (Unsafe.IsNullRef(ref resolvedCheckSymbol))
                    return throwOnError ? throw new CrockfordBase32SymbolOutOfRangeException(current, $"The provided value '{value}' cannot be decoded, the character at {index + 1} is an invalid check symbol ({value[..index]} » {current} « {(index == value.Length - 1 ? "" : value[(index + 1)..])}).") : null;
                checkSymbol = resolvedCheckSymbol;
                continue;
            }

            ref var resolvedNumber = ref CollectionsMarshal.GetValueRefOrNullRef(DecodeMap, current);
            if (Unsafe.IsNullRef(ref resolvedNumber))
                return throwOnError ? throw new CrockfordBase32SymbolOutOfRangeException(current, $"The provided value '{value}' cannot be decoded, the character at {index + 1} is invalid ({value[..index]} » {current} « {(index == value.Length - 1 ? "" : value[(index + 1)..])}).") : null;

            result += resolvedNumber * currentBase;
            currentBase *= DecodeBase;
        }

        if (hasCheckSymbol && (result % CheckSymbolBase) != checkSymbol!.Value)
            return throwOnError ? throw new CrockfordBase32FormatException(new string(value.ToArray()), $"The provided value '{value}' has a check symbol value of {checkSymbol}, {result % CheckSymbolBase} was expected.") : null;

        return result;
    }
    #endregion
    #endregion
}
