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
// The latest version of this file can be found at https://github.com/HatTrickLabs/db-ex
#endregion

namespace HatTrick.CrockfordBase32.Tools;

public class EncodeCommand : ICommand
{
    public IEnumerable<string> Commands => new List<string>() { "e", "encode" };
    public IEnumerable<string> Options => new List<string>() { "-?", "-cs", "--check-symbol" };
    public IEnumerable<string> UsageInstructions => new List<string>()
    {
        "Usage:",
        "  e|encode [options]",
        "Instructions:",
        "  Use the command 'e' or 'encode' to encode a long value using the Crockford Base 32 encoding scheme.",
        "  Use the options '-cs' or '--check-symbol' to indicate the encoded string ends with a check symbol.",
        "Example:",
        $"  {Constants.ToolName} e 1234 -cs"
    };

    public bool UseCheckSymbol { get; set; }
    public long? Value { get; set; }

    public EncodeCommand()
    {

    }

    public EncodeCommand(IEnumerable<string> args)
    {
        UseCheckSymbol = Options.Any(a => args.Contains(a));
        var value = args.SingleOrDefault(a => !Options.Contains(a));
        if (value is not null)
        {
            if (long.TryParse(value, out var v))
                Value = v;
        }
    }

    public ICommand? FromCommand(string command, IEnumerable<string> args)
        => Commands.Any(c => string.Compare(c, command, true) == 0) ? new EncodeCommand(args) : null;
}
