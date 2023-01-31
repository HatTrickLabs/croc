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

public abstract class CommandHandler<T> : ICommandHandler<T>, IDisposable
    where T : class, ICommand, new()
{
    private T? _command = new();
    private bool disposedValue;

    public IEnumerable<string> Commands => _command!.Commands;

    public ICommand? FromCommand(string command, IEnumerable<string> args)
    {
        return _command!.FromCommand(command, args);
    }

    public IEnumerable<string> Execute(ICommand command, IEnumerable<string> args)
    {
        if (args.Any(o => o == Constants.HelpOption))
            return _command!.UsageInstructions;

        try
        {
            return Execute((command as T)!);
        }
        catch (Exception ex)
        {
            return new string[] { ex.Message };
        }
    }

    public abstract IEnumerable<string> Execute(T command);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _command = null;
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

