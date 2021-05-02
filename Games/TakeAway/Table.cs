using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.TakeAway {
    public class Table : State {
        public int chips, maxTakeAway;
        public bool turn;

        public Table(int size, int maxTakeAway, bool turn) {
            chips = size;
            this.maxTakeAway = maxTakeAway;
            this.turn = turn;
        }

        public void Remove(int amount) { chips -= amount; }

        public int Get() { return chips;  }

        public override string ToString() { return chips.ToString(); }

        public int Eval() {
            if (chips <= 0 && turn) { return -10; }
            if ((chips + 1) % maxTakeAway == 0 && !turn) { return 5; }
            if (chips <= 0 && !turn) { return 10; }
            if ((chips + 1) % maxTakeAway == 0 && turn) { return -5; }

            return 0;
        }

        public bool isTerminal() { return chips <= 0; }
    }
}
