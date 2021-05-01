using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Algorithms {
    public class Minimax {
        public static int run(Node node, int depth, bool maxing) {
            if (depth == 0 || node.state.isTermnial()) { return node.value; }

            if (maxing) {
                int max = Int32.MinValue;
                foreach (Node child in node.children) {
                    int value = run(child, depth - 1, !maxing);
                    if (value > max) { node.value = value; max = value; }
                }

                return max;
            } else {
                int min = Int32.MaxValue;
                foreach (Node child in node.children) {
                    int value = run(child, depth - 1, !maxing);
                    if (value < min) { node.value = value; min = value; }
                }
                return min;
            }
        }
    }
    
}
