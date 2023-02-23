# croc

![Build Status](https://github.com/hattricklabs/croc/actions/workflows/main.yml/badge.svg?branch=main)

**Supported TFMs:  `net6.0`+**

`croc` is a highly performant and opinionated implementation of the [Crockford Base 32](http://www.crockford.com/base32.html) encoding scheme.  `croc` was designed for use in business settings, where there is value in using an encoding scheme to obfuscate data.

What are some of the benefits to using Crockford Base 32 encoding?
- Enables obfuscation of data sent through an API or to a web frontend, like database ids.
- Creates human readable and translatable values.  Trying to communicate "21000005003" is more difficult and error prone than "KHV5-9CB".
- Supports inclusion of hyphens in encoded data to further support readability ("KH-V59C-B" is valid).

`croc` is opinionated - it works with values of type `long`.  Most implementations work with `ulong`, but it's rare to work with `ulong` in business settings.  So, the methods provided by `croc` work with `long` values, saving you the hassle of conversion/boxing (but there is support for `ulong` using some "low-level" methods in `croc`).

3 primary goals of `croc`:
- No memory allocation
- Fast
- Easy to use

`croc's` decode methods result in **0** allocations and the encode methods result in **only the memory allocations required for the return value**.  `croc's` speed is ensured by single loops through the bits and bytes and using bit shifting.  There are numerous overloads to encoding/decoding to ensure you can use the data format that best suits your needs.

---

***Where all of these encoding schemes fail is the practical day to day use.***  When support  pings you and asks:

> "Hey, can you look into order no. KHV5-9CB for me?  The customer hasn't received a confirmation of the order."
>   
> 	-- That Guy

Great, you have an obfuscated id ("KHV5-9CB")... now you have to spin up some console app or hit that clunky internal website to decode the value so you can look it up in the back-end system(s)....

We've got you covered!  `croc` also includes a dotnet tool that you can easily use to encode/decode values right from your IDE or shell.  With the dotnet tool, decoding is as simple as:

```bash

C:\> croc d KHV5-9CB

```

## Usage

Similar to the methods in the `System.Convert` namespace, `croc` provides methods to encode and decode values.

### Using static `CrockfordBase32`

```csharp
using HatTrick.CrockfordBase32;

long value = 12L;
char[]? charArrayResult = null;  
string? stringResult = null;
bool success;

//encode the value to a string
stringResult = CrockfordBase32.GetString(value);  

//encode the value to a string and include a check symbol at the end
stringResult = CrockfordBase32.GetString(value, true); 

//try and encode the value and let me know if it succeeds
success = CrockfordBase32.TryGetCharArray(value, out charArrayResult);  

//try and encode the value with a check symbol and let me know if it succeeds
success = CrockfordBase32.TryGetCharArray(value, true, out charArrayResult);

long? longResult = null;
string otherValue = "3ZG";

//decode a string
longResult = CrockfordBase32.GetInt64(otherValue);  

//decode a string with a check symbol and validate the check symbol
longResult = CrockfordBase32.GetInt64(otherValue, true); 

//try and decode a string and let me know if it succeeds
success = CrockfordBase32.TryGetInt64(otherValue, out longResult);  

//try and decode a string with a check symbol and let me know if it succeeds
success = CrockfordBase32.TryGetInt64(otherValue, true, out longResult);

```

### Using extension methods


```csharp
using HatTrick.CrockfordBase32;

long value = 12L;
ReadOnlyMemory<char>? memoryResult = null;  
string? stringResult = null;
bool success;

//encode the value
memoryResult = value.ToCrockfordBase32ReadOnlyMemory();  

//encode the value and include a check symbol at the end
memoryResult = value.ToCrockfordBase32ReadOnlyMemory(true);   

//try and encode the value and let me know if it succeeds
success = value.TryToCrockfordBase32ReadOnlyMemory(out memoryResult);  

//try and encode the value with a check symbol and let me know if it succeeds
success = value.TryToCrockfordBase32ReadOnlyMemory(true, out memoryResult);

long? longResult = null;
string otherValue = "3ZG";

//decode a string
longResult = otherValue.FromCrockfordBase32String();  

//decode a string with a check symbol and validate the check symbol
longResult = otherValue.FromCrockfordBase32String(true); 

//try and decode a string and let me know if it succeeds
success = otherValue.TryFromCrockfordBase32String(otherValue, out longResult);  

//try and decode a string with a check symbol and let me know if it succeeds
success = otherValue.TryFromCrockfordBase32String(otherValue, true, out longResult);

```

Methods following these patterns are available to work with all of the following types:
- `ReadOnlyMemory<char>`
- `Memory<char>`
- `char[]`
- `byte[]`
- `string`


## Get `croc`

| NuGet Packages                                |                     |
| :--------------------------------------| :-------------------|
| [HatTrick.CrockfordBase32](https://www.nuget.org/packages/HatTrick.CrockfordBase32/)             | ![Nuget](https://img.shields.io/nuget/v/HatTrick.CrockfordBase32)    |
| [HatTrick.CrockfordBase32.Tools](https://www.nuget.org/packages/HatTrick.CrockfordBase32.Tools/)       | ![Nuget](https://img.shields.io/nuget/v/HatTrick.CrockfordBase32.Tools)    |

### To use `croc` in your project

Simply create a reference to [HatTrick.CrockfordBase32](https://www.nuget.org/packages/HatTrick.CrockfordBase32/) in your project.


### To use the `croc` dotnet tool

To use the `croc` tool, fire up terminal and execute the following:

```bash
C:\> dotnet tool install HatTrick.CrockfordBase32.Tools --global
```

This will install the tool globally, so it's available in your shell from any path.  Once installed, you can encode and decode right from the command line using `croc`.  Use the `help` command to provide usage instructions:

```bash
C:\> croc help
```

This will respond with:

```txt
Usage:
  croc [command] [options]
Commands:
  e|encode
  d|decode
Run the following for specific command help:
  croc [command] -?
```

Executing:

```bash
C:\> croc encode -?
```

Will respond with:

```txt
Usage:
  e|encode [options]
Instructions:
  Use the command 'e' or 'encode' to encode a long value using the Crockford Base 32 encoding scheme.
  Use the options '-cs' or '--check-symbol' to indicate the encoded string ends with a check symbol.
Example:
  croc e 1234 -cs
```

## Performance

The following shows how we're doing.  The benchmarks used are included in the [HatTrick.CrockfordBase32.Benchmark](https://github.com/HatTrickLabs/croc/tree/master/benchmark/Benchmark) project.

BenchmarkDotNet=v0.13.4, OS=Windows 11 (10.0.22000.1574/21H2)
Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.103
  [Host]     : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.3 (7.0.323.6910), X64 RyuJIT AVX2


|                       Method | UseCheckSymbol |                            Parameter |      Mean |     Error |    StdDev |    Median | Rank |   Gen0 | Allocated |
|----------------------------- |--------------- |------------------------------------- |----------:|----------:|----------:|----------:|-----:|-------:|----------:|
|         'Decode from string' |          False |                               0:0[0] |  15.44 ns |  0.340 ns |  0.334 ns |  15.42 ns |    1 |      - |         - |
|         'Decode from string' |          False |                 1073741823:ZZZZZZ[A] |  51.27 ns |  1.032 ns |  0.915 ns |  51.09 ns |   11 |      - |         - |
|         'Decode from string' |          False |                            127:3Z[G] |  24.65 ns |  0.527 ns |  0.820 ns |  24.58 ns |    3 |      - |         - |
|         'Decode from string' |          False |                2147483647:1ZZZZZZ[N] |  63.21 ns |  1.282 ns |  2.760 ns |  62.48 ns |   14 |      - |         - |
|         'Decode from string' |          False |                            255:7Z[~] |  22.83 ns |  0.481 ns |  0.830 ns |  22.65 ns |    2 |      - |         - |
|         'Decode from string' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 105.58 ns |  2.152 ns |  2.873 ns | 105.77 ns |   20 |      - |         - |
|         'Decode from string' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 106.88 ns |  2.111 ns |  2.819 ns | 106.52 ns |   20 |      - |         - |
|         'Decode from string' |           True |                               0:0[0] |  27.23 ns |  0.421 ns |  0.373 ns |  27.20 ns |    4 |      - |         - |
|         'Decode from string' |           True |                 1073741823:ZZZZZZ[A] |  65.76 ns |  1.327 ns |  2.105 ns |  65.76 ns |   15 |      - |         - |
|         'Decode from string' |           True |                            127:3Z[G] |  34.00 ns |  0.718 ns |  1.789 ns |  34.04 ns |    6 |      - |         - |
|         'Decode from string' |           True |                2147483647:1ZZZZZZ[N] |  71.99 ns |  1.471 ns |  2.013 ns |  71.10 ns |   17 |      - |         - |
|         'Decode from string' |           True |                            255:7Z[~] |  31.53 ns |  0.661 ns |  0.708 ns |  31.36 ns |    5 |      - |         - |
|         'Decode from string' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 122.24 ns |  2.301 ns |  2.463 ns | 122.79 ns |   21 |      - |         - |
|         'Decode from string' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 158.46 ns | 10.811 ns | 31.878 ns | 139.91 ns |   22 |      - |         - |
| 'Encode to read only memory' |          False |                               0:0[0] |  25.19 ns |  0.274 ns |  0.256 ns |  25.14 ns |    3 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False |                 1073741823:ZZZZZZ[A] |  46.36 ns |  0.831 ns |  0.694 ns |  46.28 ns |   10 | 0.0048 |      40 B |
| 'Encode to read only memory' |          False |                            127:3Z[G] |  25.09 ns |  0.516 ns |  0.889 ns |  24.98 ns |    3 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False |                2147483647:1ZZZZZZ[N] |  58.31 ns |  1.185 ns |  1.268 ns |  58.24 ns |   13 | 0.0048 |      40 B |
| 'Encode to read only memory' |          False |                            255:7Z[~] |  25.39 ns |  0.535 ns |  0.923 ns |  25.24 ns |    3 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] |  96.69 ns |  1.957 ns |  3.675 ns |  95.83 ns |   19 | 0.0067 |      56 B |
| 'Encode to read only memory' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] |  91.40 ns |  1.839 ns |  4.114 ns |  91.43 ns |   18 | 0.0067 |      56 B |
| 'Encode to read only memory' |           True |                               0:0[0] |  26.57 ns |  0.550 ns |  0.676 ns |  26.64 ns |    4 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True |                 1073741823:ZZZZZZ[A] |  56.48 ns |  1.129 ns |  1.300 ns |  56.57 ns |   12 | 0.0048 |      40 B |
| 'Encode to read only memory' |           True |                            127:3Z[G] |  31.51 ns |  0.659 ns |  1.171 ns |  31.41 ns |    5 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True |                2147483647:1ZZZZZZ[N] |  60.63 ns |  1.124 ns |  1.939 ns |  60.83 ns |   14 | 0.0048 |      40 B |
| 'Encode to read only memory' |           True |                            255:7Z[~] |  31.47 ns |  0.660 ns |  1.543 ns |  31.11 ns |    5 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] |  92.16 ns |  1.868 ns |  3.222 ns |  91.40 ns |   18 | 0.0067 |      56 B |
| 'Encode to read only memory' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] |  90.62 ns |  1.838 ns |  3.542 ns |  89.71 ns |   18 | 0.0067 |      56 B |
|           'Encode to string' |          False |                               0:0[0] |  34.64 ns |  0.748 ns |  0.972 ns |  34.46 ns |    6 | 0.0067 |      56 B |
|           'Encode to string' |          False |                 1073741823:ZZZZZZ[A] |  61.84 ns |  1.296 ns |  1.685 ns |  61.80 ns |   14 | 0.0095 |      80 B |
|           'Encode to string' |          False |                            127:3Z[G] |  41.18 ns |  1.157 ns |  3.410 ns |  41.34 ns |    8 | 0.0076 |      64 B |
|           'Encode to string' |          False |                2147483647:1ZZZZZZ[N] |  70.09 ns |  1.469 ns |  3.317 ns |  69.99 ns |   17 | 0.0095 |      80 B |
|           'Encode to string' |          False |                            255:7Z[~] |  37.81 ns |  0.819 ns |  1.477 ns |  37.43 ns |    7 | 0.0076 |      64 B |
|           'Encode to string' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 100.98 ns |  2.071 ns |  2.385 ns | 100.47 ns |   20 | 0.0124 |     104 B |
|           'Encode to string' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 102.15 ns |  2.021 ns |  2.247 ns | 101.92 ns |   20 | 0.0124 |     104 B |
|           'Encode to string' |           True |                               0:0[0] |  41.35 ns |  0.818 ns |  1.475 ns |  41.80 ns |    8 | 0.0076 |      64 B |
|           'Encode to string' |           True |                 1073741823:ZZZZZZ[A] |  68.28 ns |  1.413 ns |  3.218 ns |  68.41 ns |   16 | 0.0095 |      80 B |
|           'Encode to string' |           True |                            127:3Z[G] |  47.09 ns |  1.010 ns |  1.382 ns |  47.38 ns |   10 | 0.0076 |      64 B |
|           'Encode to string' |           True |                2147483647:1ZZZZZZ[N] |  71.00 ns |  0.868 ns |  0.678 ns |  71.21 ns |   17 | 0.0095 |      80 B |
|           'Encode to string' |           True |                            255:7Z[~] |  43.29 ns |  0.926 ns |  1.784 ns |  42.79 ns |    9 | 0.0076 |      64 B |
|           'Encode to string' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 105.53 ns |  2.163 ns |  5.849 ns | 103.85 ns |   20 | 0.0134 |     112 B |
|           'Encode to string' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 102.82 ns |  2.111 ns |  2.593 ns | 102.78 ns |   20 | 0.0134 |     112 B |