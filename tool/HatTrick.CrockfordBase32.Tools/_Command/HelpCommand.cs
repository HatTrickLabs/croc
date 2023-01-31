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

public class HelpCommand : ICommand
{
    private static List<string> _instructions = new List<string>()
    {
        {  "Usage:" },
        {  $"  {Constants.ToolName} [command] [options]" },
        {  "Commands:" },
    };

    public IEnumerable<string> Commands => new List<string>() { "", "help" };
    public IEnumerable<string> Options => new List<string>() { };
    public IEnumerable<string> UsageInstructions => _instructions.Concat(new string[] { "Run the following for specific command help:", $"  {Constants.ToolName} [command] -?"});

    public HelpCommand()
    {

    }

    public static void AddCommand(ICommandHandler handler)
    {
        _instructions.Add("  " + string.Join('|', handler.Commands));
    }

    public ICommand? FromCommand(string command, IEnumerable<string> args)
        => Commands.Any(c => string.Compare(c, command, true) == 0) ? new HelpCommand() : null;
}
