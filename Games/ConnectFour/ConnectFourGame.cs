using System;
using COMP717.Algorithms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace COMP717.Game.ConnectFour {
    class ConnectFourGame {
        public Grid board = new Grid();
        public char turn = 'O';
        private int searchDepth;
        private bool minimax;
        private long compSearchTime = 0;


        public ConnectFourGame(int searchDepth = Int32.MaxValue, bool playerStart = true, bool minimax = false, bool doUIPlay = true) {
            this.searchDepth = searchDepth;
            this.minimax = minimax;

            if (!playerStart) {
                turn = 'X';
                int play = Search();
                board.Add(play, turn);
                turn = 'O';
                if (doUIPlay) { UserPlay(play); }
            } else {
                if (doUIPlay) { UserPlay(); }
            }
          
        }

        public int[] Play(int x) {
            board.Add(x, turn);
            if (board.isTerminal()) { return new int[] { -1, -1 }; }
            turn = turn == 'X' ? 'O' : 'X';

            int compPlay = Search();
            board.Add(compPlay, turn);
            if (board.isTerminal()) { return new int[] { -1, -1 }; }
            turn = turn == 'X' ? 'O' : 'X';

            return new int[] { x, compPlay };
        }

        public void ComputerPlay() {
            turn = 'X';
            int play = Search();
            board.Add(play, turn);
            turn = 'O';
        }

        public void UserPlay(int compStartingMove = -1) {
            string error = "";
            int compPlay = compStartingMove, playerPlay = -1;
           
            while (!board.isTerminal()) {
                //Console.Clear();

                // Is this better? I'm not sure, maybe it's more readable who knows
                if (error != "") {
                    Console.WriteLine(error);
                    error = "";
                } else {
                    Console.WriteLine();
                }

                if (compPlay > -1) {
                    Console.WriteLine("Computer (X) played in column: " + (compPlay + 1));
                    compPlay = 0;
                } else {
                    Console.WriteLine();
                }

                if (playerPlay > -1) {
                    Console.WriteLine("You (O) played in column: " + (playerPlay + 1));
                    playerPlay = 0;
                } else {
                    Console.WriteLine();
                }

                if (compSearchTime > 0) {
                    Console.WriteLine("Computer took: " + compSearchTime + "ms to find this play");
                    compSearchTime = 0;
                } else {
                    Console.WriteLine();
                }

                Console.WriteLine();

                Console.WriteLine(board);
                Console.WriteLine("  1   2   3   4   5   6   7\n");
                Console.Write("> ");

                string input = Console.ReadLine();

                try { 
                    if (int.Parse(input) > 7 || int.Parse(input) < 1) {
                        error = "Input out of range! Must be between 1 and 7!";
                        continue;
                    }
                } catch {
                    error = "Input not a number!";
                    continue;
                }
                int[] plays = Play(int.Parse(input) - 1);

                playerPlay = plays[0];
                compPlay = plays[1];
            }

            Console.Clear();
            Console.WriteLine();

            if (Math.Abs(board.Eval()) != 100) {
                Console.WriteLine("Tie!");
            } else {
                Console.WriteLine((turn == 'X' ? "Computer" : "You") + " Won!");
            }

            Console.WriteLine();

            Console.WriteLine(board);

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public int Search() {
            Stopwatch time = new Stopwatch();
            time.Start();
            ConnectFourTree tree = new ConnectFourTree(board, turn, minimax, searchDepth);
            //Console.WriteLine(tree.root);
            time.Stop();
            compSearchTime = time.ElapsedMilliseconds;
            return tree.GetBestPlay();
        }

        public override string ToString() { return board.ToString(); }

        public bool Complete() { return board.isTerminal(); }

        public Grid GetBoard() { return board; }

        public char Outcome() {
            if (board.Eval() == 100) { return 'X'; }
            if (board.Eval() == -100) { return 'O'; }
            if (board.IsFull()) { return ' '; }
            return 'n';
        }

        
    }
}
