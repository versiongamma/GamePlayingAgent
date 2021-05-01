using COMP717.Algorithms;
using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.ConnectFour {
    class ConnectFourTree {
        public Node root;

        public ConnectFourTree(Grid start, char turn, bool minimax, int depth = Int32.MaxValue) {
            if (!minimax) { 
                root = GenerateTreeAlphaBeta(
                    new Node(0, new Grid((char[,])start.GetState().Clone()), new int[] { 0, 0 }),
                    turn, depth, Int32.MinValue, Int32.MaxValue
            ); } else {
                root = GenerateTree(
                    new Node(0, new Grid((char[,])start.GetState().Clone()), new int[] { 0, 0 }), 
                    turn, depth);
                root.value = Minimax.run(root, depth, true);
            }

            int max = Int32.MinValue;
            foreach (Node child in root.children) {
                if (child.value > max) {
                    max = child.value;
                    root.play = child.play;
                }
            }
        }

        public Node GenerateTreeAlphaBeta(Node node, char turn, int depth, int a, int b) {
            Grid board = node.state as Grid;
            for (int x = 0; x < 7; x++) {
                if (board.ColumnFull(x)) { continue; }
                if (board.isTermnial() || depth == 0) { return new Node(board.Eval(), new Grid((char[,])board.GetState().Clone()), node.play); }

                Grid newBoard = new Grid((char[,])board.GetState().Clone());
                newBoard.Add(x, turn);
                Node newNode = new Node(0, newBoard, new int[] { x, 0 });
                node.children.Add(GenerateTreeAlphaBeta(newNode, turn == 'X' ? 'O' : 'X', depth - 1, a, b));

                if (turn == 'X') {                  
                    int value = Minimax.run(node, depth, true);
                    if (value > a) { node.value = value; a = value; }
                } else {
                    int value = Minimax.run(node, depth, false);
                    if (value < b) { node.value = value; b = value; }
                }

                if (a >= b) { break; }
            }

            return node;
        }

        public Node GenerateTree(Node node, char turn, int depth) {
            Grid board = node.state as Grid;
            for (int x = 0; x < 7; x++) {
                if (board.ColumnFull(x)) { continue; }
                if (board.isTermnial() || depth == 0) { return new Node(board.Eval(), new Grid((char[,])board.GetState().Clone()), node.play); }

                Grid newBoard = new Grid((char[,])board.GetState().Clone());
                newBoard.Add(x, turn);
                Node newNode = new Node(0, newBoard, new int[] { x, 0 });
                node.children.Add(GenerateTree(newNode, turn == 'X' ? 'O' : 'X', depth - 1));
            }
            

            return node;

        }

        public int GetBestPlay() { return root.play[0]; }
    }
}
