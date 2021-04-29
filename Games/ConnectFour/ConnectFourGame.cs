using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.ConnectFour {
    class ConnectFourGame {
        private Grid board = new Grid();
        private char turn = 'O';
        private int searchDepth;


        public ConnectFourGame(int searchDepth = Int32.MaxValue) {
            this.searchDepth = searchDepth;

            UserPlay();

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public void Play(int x) {
            board.Add(x, turn);
            if (board.isTermnial()) { return; }
            turn = turn == 'X' ? 'O' : 'X';

            board.Add(Search(searchDepth    ), turn);
            if (board.isTermnial()) { return; }
            turn = turn == 'X' ? 'O' : 'X';
        }

        public void UserPlay() {
           
            while (!board.isTermnial()) {
                Console.Clear();
                Console.WriteLine(board);
                Console.Write("> ");

                string input = Console.ReadLine();
                Play(int.Parse(input));
            }

            if (Math.Abs(board.Eval()) != 100) {
                Console.WriteLine("Tie!");
            } else {
                Console.WriteLine((turn == 'X' ? "Computer" : "You") + " Won!");
            }

            Console.Clear();
            Console.WriteLine(board);
        }

        public int Search(int depth) {
            ConnectFourTree tree = new ConnectFourTree(board, turn, 6);
            return tree.GetBestPlay();
        }
    }
}
