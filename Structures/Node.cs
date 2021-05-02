using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Structures {
    public class Node {
        public List<Node> children;
        public int value;
        public State state;
        public int[] play;

        public Node(int value, State state, int[] play) {
            this.value = value;
            this.state = state;
            this.children = new List<Node>();
            this.play = play;
        }
    }
}
