using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Structures {
    /* Represents the state of a game, for running the Minimax algorithm */
    public interface State {
        int Eval();
        bool isTerminal();
    }
}
