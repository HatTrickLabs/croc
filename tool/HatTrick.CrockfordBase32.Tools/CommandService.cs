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

namespace HatTrick.CrockfordBase32.Tools;

public class CommandService : IDisposable
{
    private HelpCommand? _help = new HelpCommand();
    private IList<ICommandHandler>? _commandHandlers = new List<ICommandHandler>() { new HelpCommandHandler() };
    private bool disposedValue;

    public void Register<T>()
        where T : class, ICommandHandler, new()
    {
        Register(new T());
    }

    public void Register(ICommandHandler handler)
    {
        HelpCommand.AddCommand(handler);
        _commandHandlers!.Add(handler);
    }

    public IEnumerable<string> Execute(IEnumerable<string> args)
    {
        var results = _commandHandlers!.Select(ch =>
        {
            ICommand? c = null;
            try
            {
                c = ch.FromCommand(args.First(), args.Skip(1));
            }
            catch (Exception)
            {
                return new string[] { "Invalid command or options provided.", "" }.Concat(_help!.UsageInstructions);
            }
            if (c is not null)
                return ch.Execute(c, args);
            return Enumerable.Empty<string>();
        }).Aggregate((main, item) => main.Concat(item));

        return results.Any() ? results : new string[] { "Command not recognized.", "" }.Concat(_help!.UsageInstructions);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _commandHandlers = null;
                _help = null;
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
