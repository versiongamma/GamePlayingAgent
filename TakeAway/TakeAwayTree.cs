using COMP717.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.TakeAway {
    class TakeAwayTree {
        public Node root;
        public int depth;
        public int maxTakeAway;

        public TakeAwayTree(Table table, int depth) {
            maxTakeAway = table.maxTakeAway;
            this.depth = depth;
            root = GenerateTree(new Node(0, new Table(table.Get(), maxTakeAway, true), new int[] { 0, 0 }), depth);

            root.value = Minimax.run(root, depth, true);

            int max = Int32.MinValue;
            foreach (Node child in root.children) {
                if (child.value > max) {
                    max = child.value;
                    root.play = child.play;
                }
            }
        }

        public Node GenerateTree(Node node, int depth) {
            Table table = node.state as Table;

            for (int i = 1; i <= maxTakeAway; i++) {
                if (depth == 0 || table.Get() <= 0) {
                    Table terminalTable = new Table(table.Get(), maxTakeAway, table.turn);
                    return new Node(terminalTable.Eval(), terminalTable, new int[] { node.play[0], 0 }); 
                }

                Table newTable = new Table(table.Get() - i, maxTakeAway, !table.turn);
                node.children.Add(GenerateTree(new Node(0, newTable, new int[] { i, 0 }), depth - 1));
            }
            
            return node;
        }

        public int GetBestPlay() { return root.play[0]; }
    }
}
