# croc

![Build Status](https://github.com/hattricklabs/croc/actions/workflows/main.yml/badge.svg?branch=main)

**Supported TFMs:  `net6.0`+**

`croc` is a highly performant and opinionated implementation of the [Crockford Base 32](http://www.crockford.com/base32.html) encoding scheme.  `croc` was designed for use in business settings, where there is value in using an encoding scheme to obfuscate data.

What are some of the benefits to using Crockford Base 32 encoding?
- Enables obfuscation of data sent through an API or to a web frontend, like database ids.
- Creates human readable and translatable values.  Think of trying to communicate "21000005003" correctly with support personnel, when "KHV5-9CB" is so much easier (and definitely easier than some UUID).
- Supports inclusion of hyphens in encoded data to further support readability ("KH-V59C-B" is valid).

`croc` is opinionated - it works with values of type `long`.  Most implementations work with `ulong`, but it's rare to work with this type in business settings.  So, the methods provided by `croc` work with `long` values, saving you the hassle of conversion/boxing (but there is support for `ulong` using some "low-level" methods in `croc`).

3 primary goals of `croc`:
- No memory allocation
- Fast
- Easy to use

The first two are easily achieved using `croc`.  `croc's` decode methods result in **0** allocations and the encode methods result in **only the memory allocations required for the return value**.  `croc's` speed is ensured by single loops through the bits and bytes and using bit shifting.  There are numerous overloads to encoding/decoding to ensure you can use the data format that best suits your needs.

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

*(Yes, `croc` supports hyphens in encoded strings)*

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


BenchmarkDotNet=v0.13.4, OS=Windows 11 (10.0.22000.1455/21H2)
Intel Core i9-9980HK CPU 2.40GHz, 1 CPU, 8 logical and 8 physical cores
.NET SDK=7.0.101
[Host]     : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
DefaultJob : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2


|                       Method | UseCheckSymbol |                            Parameter |      Mean |     Error |    StdDev |    Median | Rank |   Gen0 | Allocated |
|----------------------------- |--------------- |------------------------------------- |----------:|----------:|----------:|----------:|-----:|-------:|----------:|
|         'Decode from string' |          False |                               0:0[0] |  31.04 ns |  2.949 ns |  8.696 ns |  28.74 ns |    3 |      - |         - |
|         'Decode from string' |          False |                 1073741823:ZZZZZZ[A] | 127.14 ns |  4.415 ns | 13.016 ns | 127.14 ns |   19 |      - |         - |
|         'Decode from string' |          False |                            127:3Z[G] |  48.12 ns |  5.232 ns | 15.427 ns |  44.70 ns |    6 |      - |         - |
|         'Decode from string' |          False |                2147483647:1ZZZZZZ[N] |  99.87 ns |  7.353 ns | 21.565 ns | 107.56 ns |   16 |      - |         - |
|         'Decode from string' |          False |                            255:7Z[~] |  25.09 ns |  0.407 ns |  0.597 ns |  24.92 ns |    2 |      - |         - |
|         'Decode from string' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 110.54 ns |  1.767 ns |  1.566 ns | 110.92 ns |   17 |      - |         - |
|         'Decode from string' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 107.71 ns |  1.411 ns |  1.251 ns | 107.64 ns |   17 |      - |         - |
|         'Decode from string' |           True |                               0:0[0] |  24.45 ns |  0.527 ns |  0.923 ns |  24.18 ns |    1 |      - |         - |
|         'Decode from string' |           True |                 1073741823:ZZZZZZ[A] |  61.63 ns |  1.241 ns |  1.570 ns |  61.41 ns |    9 |      - |         - |
|         'Decode from string' |           True |                            127:3Z[G] |  31.10 ns |  0.365 ns |  0.341 ns |  31.02 ns |    3 |      - |         - |
|         'Decode from string' |           True |                2147483647:1ZZZZZZ[N] |  72.42 ns |  1.416 ns |  1.255 ns |  72.59 ns |   11 |      - |         - |
|         'Decode from string' |           True |                            255:7Z[~] |  32.00 ns |  0.656 ns |  0.730 ns |  32.02 ns |    4 |      - |         - |
|         'Decode from string' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 109.34 ns |  2.160 ns |  2.219 ns | 109.68 ns |   17 |      - |         - |
|         'Decode from string' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 110.18 ns |  1.532 ns |  1.358 ns | 110.66 ns |   17 |      - |         - |
| 'Encode to read only memory' |          False |                               0:0[0] |  60.29 ns |  2.083 ns |  6.140 ns |  61.88 ns |    9 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False |                 1073741823:ZZZZZZ[A] |  74.04 ns |  3.294 ns |  9.712 ns |  76.44 ns |   11 | 0.0048 |      40 B |
| 'Encode to read only memory' |          False |                            127:3Z[G] |  65.05 ns |  1.976 ns |  5.826 ns |  66.50 ns |   10 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False |                2147483647:1ZZZZZZ[N] |  58.19 ns |  1.205 ns |  2.516 ns |  57.37 ns |    7 | 0.0048 |      40 B |
| 'Encode to read only memory' |          False |                            255:7Z[~] |  46.33 ns |  6.750 ns | 19.902 ns |  34.11 ns |    6 | 0.0038 |      32 B |
| 'Encode to read only memory' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] |  93.11 ns |  1.825 ns |  2.029 ns |  92.76 ns |   14 | 0.0067 |      56 B |
| 'Encode to read only memory' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] |  91.06 ns |  1.854 ns |  2.831 ns |  90.72 ns |   13 | 0.0067 |      56 B |
| 'Encode to read only memory' |           True |                               0:0[0] |  25.02 ns |  0.466 ns |  0.413 ns |  24.91 ns |    2 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True |                 1073741823:ZZZZZZ[A] |  54.35 ns |  0.957 ns |  1.102 ns |  53.99 ns |    6 | 0.0048 |      40 B |
| 'Encode to read only memory' |           True |                            127:3Z[G] |  30.97 ns |  0.596 ns |  0.529 ns |  30.92 ns |    3 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True |                2147483647:1ZZZZZZ[N] |  59.28 ns |  1.192 ns |  1.325 ns |  58.94 ns |    8 | 0.0048 |      40 B |
| 'Encode to read only memory' |           True |                            255:7Z[~] |  32.01 ns |  0.467 ns |  0.437 ns |  32.13 ns |    4 | 0.0038 |      32 B |
| 'Encode to read only memory' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] |  96.88 ns |  1.888 ns |  3.049 ns |  95.82 ns |   15 | 0.0067 |      56 B |
| 'Encode to read only memory' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] |  93.80 ns |  1.405 ns |  1.315 ns |  93.38 ns |   14 | 0.0067 |      56 B |
|           'Encode to string' |          False |                               0:0[0] |  84.81 ns |  2.802 ns |  8.217 ns |  85.91 ns |   12 | 0.0067 |      56 B |
|           'Encode to string' |          False |                 1073741823:ZZZZZZ[A] | 136.90 ns |  4.522 ns | 13.332 ns | 135.18 ns |   20 | 0.0095 |      80 B |
|           'Encode to string' |          False |                            127:3Z[G] |  55.72 ns |  4.679 ns | 12.967 ns |  59.57 ns |    7 | 0.0076 |      64 B |
|           'Encode to string' |          False |                2147483647:1ZZZZZZ[N] | 170.28 ns | 13.756 ns | 40.344 ns | 185.15 ns |   21 | 0.0095 |      80 B |
|           'Encode to string' |          False |                            255:7Z[~] |  40.61 ns |  0.750 ns |  0.702 ns |  40.66 ns |    5 | 0.0076 |      64 B |
|           'Encode to string' |          False | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 106.88 ns |  1.245 ns |  1.164 ns | 106.43 ns |   17 | 0.0124 |     104 B |
|           'Encode to string' |          False | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 101.59 ns |  1.807 ns |  1.602 ns | 102.09 ns |   16 | 0.0124 |     104 B |
|           'Encode to string' |           True |                               0:0[0] |  40.00 ns |  0.843 ns |  1.477 ns |  40.32 ns |    5 | 0.0076 |      64 B |
|           'Encode to string' |           True |                 1073741823:ZZZZZZ[A] |  66.82 ns |  1.124 ns |  1.051 ns |  67.02 ns |   10 | 0.0095 |      80 B |
|           'Encode to string' |           True |                            127:3Z[G] |  43.84 ns |  0.601 ns |  0.591 ns |  43.77 ns |    6 | 0.0076 |      64 B |
|           'Encode to string' |           True |                2147483647:1ZZZZZZ[N] |  72.02 ns |  0.785 ns |  0.696 ns |  72.03 ns |   11 | 0.0095 |      80 B |
|           'Encode to string' |           True |                            255:7Z[~] |  43.69 ns |  0.847 ns |  0.751 ns |  43.44 ns |    6 | 0.0076 |      64 B |
|           'Encode to string' |           True | 4611686018427387903:3ZZZZZZZZZZZZ[2] | 115.65 ns |  2.352 ns |  2.310 ns | 114.67 ns |   18 | 0.0134 |     112 B |
|           'Encode to string' |           True | 9223372036854775807:7ZZZZZZZZZZZZ[5] | 109.78 ns |  2.092 ns |  1.855 ns | 110.34 ns |   17 | 0.0134 |     112 B |