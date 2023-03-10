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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HatTrick.CrockfordBase32
{
    internal static class Throw
    {
        public static void ArgumentExceptionIf(
            bool condition,
            string message,
            [CallerMemberName] string? paramName = null)
        {
            if (condition)
            {
                ThrowArgumentException(message, paramName);
            }
        }

        [DoesNotReturn]
        private static void ThrowArgumentException(
            string message,
            string? paramName
        )
        {
            throw new ArgumentException(message, paramName);
        }
    }
}
