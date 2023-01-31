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

namespace HatTrick.CrockfordBase32;

public static class CrockfordBase32Extensions
{
    #region encode
    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="ReadOnlyMemory{char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static ReadOnlyMemory<char> ToCrockfordBase32ReadOnlyMemory(this long value)
        => CrockfordBase32.GetReadOnlyMemory(value);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <returns>A <see cref="ReadOnlyMemory{char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static ReadOnlyMemory<char> ToCrockfordBase32ReadOnlyMemory(this long value, bool useCheckSymbol)
        => CrockfordBase32.GetReadOnlyMemory(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="ReadOnlyMemory{char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32ReadOnlyMemory(this long value, out ReadOnlyMemory<char>? result)
        => CrockfordBase32.TryGetReadOnlyMemory(value, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <param name="result">A <see cref="ReadOnlyMemory{char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32ReadOnlyMemory(this long value, bool useCheckSymbol, out ReadOnlyMemory<char>? result)
        => CrockfordBase32.TryGetReadOnlyMemory(value, useCheckSymbol, out result);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="Memory{char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static Memory<char> ToCrockfordBase32Memory(this long value)
        => CrockfordBase32.GetMemory(value);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <returns>A <see cref="Memory{char}"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static Memory<char> ToCrockfordBase32Memory(this long value, bool useCheckSymbol)
        => CrockfordBase32.GetMemory(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="Memory{char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32Memory(this long value, out Memory<char>? result)
        => CrockfordBase32.TryGetMemory(value, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <param name="result">A <see cref="Memory{char}"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32Memory(this long value, bool useCheckSymbol, out Memory<char>? result)
        => CrockfordBase32.TryGetMemory(value, useCheckSymbol, out result);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="char"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static char[] ToCrockfordBase32CharArray(this long value)
        => CrockfordBase32.GetCharArray(value);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <returns>A <see cref="char"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static char[] ToCrockfordBase32CharArray(this long value, bool useCheckSymbol)
        => CrockfordBase32.GetCharArray(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="char"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32CharArray(this long value, out char[]? result)
        => CrockfordBase32.TryGetCharArray(value, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <param name="result">A <see cref="char"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32CharArray(this long value, bool useCheckSymbol, out char[]? result)
        => CrockfordBase32.TryGetCharArray(value, useCheckSymbol, out result);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="byte"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static byte[] ToCrockfordBase32ByteArray(this long value)
        => CrockfordBase32.GetByteArray(value);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <returns>A <see cref="byte"/>[] from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static byte[] ToCrockfordBase32ByteArray(this long value, bool useCheckSymbol)
        => CrockfordBase32.GetByteArray(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="byte"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32ByteArray(this long value, out  byte[]? result)
        => CrockfordBase32.TryGetByteArray(value, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <param name="result">A <see cref="byte"/>[] containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32ByteArray(this long value, bool useCheckSymbol, out  byte[]? result)
        => CrockfordBase32.TryGetByteArray(value, useCheckSymbol, out result);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <returns>A <see cref="string"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static string ToCrockfordBase32String(this long value)
        => CrockfordBase32.GetString(value);

    /// <summary>
    /// Converts a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <returns>A <see cref="string"/> from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is less than 0.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="long"/> <paramref name="value"/> cannot be encoded.</exception>
    public static string ToCrockfordBase32String(this long value, bool useCheckSymbol)
        => CrockfordBase32.GetString(value, useCheckSymbol);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="result">A <see cref="string"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32String(this long value, out string? result)
        => CrockfordBase32.TryGetString(value, out result);

    /// <summary>
    /// Tries to convert a signed <see cref="long"/> value into an equivalent array of symbols using Crockford Base32 encoding.
    /// </summary>
    /// <param name="value">The signed long value to convert.</param>
    /// <param name="useCheckSymbol">Indicates whether a computed check symbol is appended to the return value.</param>
    /// <param name="result">A <see cref="string"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryToCrockfordBase32String(this long value, bool useCheckSymbol, out string? result)
        => CrockfordBase32.TryGetString(value, useCheckSymbol, out result);
    #endregion

    #region decode
    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{char}"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is is empty.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="ReadOnlyMemory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater).</exception>
    public static long FromCrockfordBase32ReadOnlyMemory(this ReadOnlyMemory<char> value)
        => CrockfordBase32.GetInt64(value);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is is empty.</exception>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="ReadOnlyMemory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long FromCrockfordBase32ReadOnlyMemory(this ReadOnlyMemory<char> value, bool hasCheckSymbol)
        => CrockfordBase32.GetInt64(value, hasCheckSymbol);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{char}"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32ReadOnlyMemory(this ReadOnlyMemory<char> value, out long? result)
        => CrockfordBase32.TryGetInt64(value, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="ReadOnlyMemory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="ReadOnlyMemory{char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32ReadOnlyMemory(this ReadOnlyMemory<char> value, bool hasCheckSymbol, out long? result)
        => CrockfordBase32.TryGetInt64(value, hasCheckSymbol, out result);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{char}"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="Memory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater).</exception>
    public static long FromCrockfordBase32Memory(this Memory<char> value)
        => CrockfordBase32.GetInt64(value);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="Memory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long FromCrockfordBase32Memory(this Memory<char> value, bool hasCheckSymbol)
        => CrockfordBase32.GetInt64(value, hasCheckSymbol);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{char}"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32Memory(this Memory<char> value, out long? result)
        => CrockfordBase32.TryGetInt64(value, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="Memory{char}"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="Memory{char}"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32Memory(this Memory<char> value, bool hasCheckSymbol, out long? result)
        => CrockfordBase32.TryGetInt64(value, hasCheckSymbol, out result);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="char"/>[] <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater).</exception>
    public static long FromCrockfordBase32CharArray(this char[] value)
        => CrockfordBase32.GetInt64(value);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="ReadOnlyMemory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long FromCrockfordBase32CharArray(this char[] value, bool hasCheckSymbol)
        => CrockfordBase32.GetInt64(value, hasCheckSymbol);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32CharArray(this char[] value, out long? result)
        => CrockfordBase32.TryGetInt64(value, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="char"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="char"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32CharArray(this char[] value, bool hasCheckSymbol, out long? result)
        => CrockfordBase32.TryGetInt64(value, hasCheckSymbol, out result);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="byte"/>[] <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater).</exception>
    public static long FromCrockfordBase32ByteArray(this  byte[] value)
        => CrockfordBase32.GetInt64(value);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="ReadOnlyMemory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long FromCrockfordBase32ByteArray(this  byte[] value, bool hasCheckSymbol)
        => CrockfordBase32.GetInt64(value, hasCheckSymbol);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32ByteArray(this  byte[] value, out long? result)
        => CrockfordBase32.TryGetInt64(value, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="byte"/>[] value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32ByteArray(this  byte[] value, bool hasCheckSymbol, out long? result)
        => CrockfordBase32.TryGetInt64(value, hasCheckSymbol, out result);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="string"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater).</exception>
    public static long FromCrockfordBase32String(this string value)
        => CrockfordBase32.GetInt64(value);

    /// <summary>
    /// Converts a Crockford Base32 encoded set of symbols from the provided <see cref="byte"/>[] into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <returns>A signed <see cref="long"/> value from the conversion of the <paramref name="value"/>.</returns>
    /// <exception cref="InvalidDataException">Thrown when the <see cref="ReadOnlyMemory{char}"/> <paramref name="value"/> cannot be decoded.  Typically, the 
    /// value contains a symbol that is not a subset of the valid symbols for Crockford Base 32 (i.e. an extended ASCII charater) or the computed check symbol indicates
    /// wrong or transposed symbols.</exception>
    public static long FromCrockfordBase32String(this string value, bool hasCheckSymbol)
        => CrockfordBase32.GetInt64(value, hasCheckSymbol);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="string"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32String(this string value, out long? result)
        => CrockfordBase32.TryGetInt64(value, out result);

    /// <summary>
    /// Tries to convert a Crockford Base32 encoded set of symbols from the provided <see cref="string"/> into an equivalent signed <see cref="long"/> value.
    /// </summary>
    /// <param name="value">The <see cref="string"/> value to convert to a signed long value.</param>
    /// <param name="hasCheckSymbol">Indicates whether the <paramref name="value"/> includes a computed check symbol as the last symbol.</param>
    /// <param name="result">A <see cref="long"/> containing the converted symbols if the conversion was successful.</param>
    /// <returns>A <see cref="bool"/> indicating success (true) or failure (false) of the conversion.</returns>
    public static bool TryFromCrockfordBase32String(this string value, bool hasCheckSymbol, out long? result)
        => CrockfordBase32.TryGetInt64(value, hasCheckSymbol, out result);
    #endregion
}
