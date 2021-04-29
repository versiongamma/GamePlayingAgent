using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.ConnectFour {
    class ConnectFourGame {
        Grid board = new Grid();
        char turn = 'O';

        public ConnectFourGame() {


            UserPlay();

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public void Play(int x) {
            board.Add(x, turn);
            if (board.isTermnial()) { return; }
            turn = turn == 'X' ? 'O' : 'X';

            board.Add(Search(6), turn);
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
