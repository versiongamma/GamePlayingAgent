using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717 {
    public interface State {
        int Eval();
        bool isTermnial();
    }
}
