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

public class EncodeCommandHandler : CommandHandler<EncodeCommand>
{
    public override IEnumerable<string> Execute(EncodeCommand command)
    {
        if (!command.Value.HasValue)
            throw new ArgumentException("Could not encode the value.  Encoding requires a valid value of type 'long'.");
        return new string[] { command.Value!.Value.ToCrockfordBase32String(command.UseCheckSymbol) };
    }
}
