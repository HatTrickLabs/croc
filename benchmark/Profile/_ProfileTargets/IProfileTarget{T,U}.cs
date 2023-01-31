using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profile;

public interface IProfileTarget<in T, out U> : IProfileTarget
{
    U Execute(T input, bool useCheckSymbol);
}
