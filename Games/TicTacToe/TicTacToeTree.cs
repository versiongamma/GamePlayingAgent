using COMP717.Algorithms;
using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.TicTacToe {
    public class TicTacToeTree {
        public Node root;

        public TicTacToeTree(Board start, char turn, int depth = Int32.MaxValue) {

            root = GenerateTree(new Node(0, new Board((char[,])start.GetState().Clone()), new int[] { 0, 0 }), turn, depth);

            root.value = Minimax.run(root, depth, true);
            int max = Int32.MinValue;
            foreach (Node child in root.children) {
                if (child.value > max) {
                    max = child.value;
                    root.play = child.play;
                }
            }
        }

        public Node GenerateTree(Node node, char turn, int depth) {
            Board board = node.state as Board;

            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    if (board.Get(x, y) != ' ') { continue; }

                    if (board.isTerminal() || depth == 0) { return new Node(board.Eval(), new Board((char[,])board.GetState().Clone()), node.play); }

                    Board newBoard = new Board((char[,])board.GetState().Clone());
                    newBoard.Add(x, y, turn);
                    Node newNode = new Node(0, newBoard, new int[] { y, x });
                    node.children.Add(GenerateTree(newNode, turn == 'X' ? 'O' : 'X', depth - 1));
                }
            }

            return node;

        }

        public int[] GetBestPlay() { return root.play; }
    }
}
