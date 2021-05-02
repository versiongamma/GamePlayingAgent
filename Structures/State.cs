using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Structures
{
    public interface State {
        int Eval();
        bool isTerminal();
    }
}
