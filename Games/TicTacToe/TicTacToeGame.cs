using COMP717.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.TicTacToe {
    public class TicTacToeGame {
        private Board board = new Board();
        private char turn;
        private int searchDepth;

        private long compSearchTime = 0;

        public TicTacToeGame(bool playerStart = true, int searchDepth = 9, bool doUserInput = true) {
            turn = 'O';
            this.searchDepth = searchDepth;

            if (!playerStart) {
                int[] agentPlay = Search(searchDepth);
                board.GetState()[agentPlay[0], agentPlay[1]] = 'X';
                if (doUserInput) { UserPlay(new int[] { agentPlay[1], agentPlay[0] }); }
            } else {
                if (doUserInput) { UserPlay(new int[] { -1, -1 }); }
            }

            
        }

        // Come one man, why are you returning a 2d int array to represent the plays they made, not cool
        public int[][] Play(int x, int y) {
            if (!board.Add(x, y, turn)) { return new int[][] { new int[] { -1, -1}, new int[] { -1, -1 } }; }
            if (board.isTerminal()) { return new int[][] { new int[] { -1, -1 }, new int[] { -1, -1 } }; }
            turn = turn == 'X' ? 'O' : 'X';

            int[] agentPlay = Search(searchDepth);
            board.Add(agentPlay[1], agentPlay[0], turn);
            if (board.isTerminal()) { return new int[][] { new int[] { -1, -1 }, new int[] { -1, -1 } }; }
            turn = turn == 'X' ? 'O' : 'X';

            return new int[][] { new int[] { x, y }, agentPlay };
        }

        public void UserPlay(int[] compPlay) {
            var error = "";

            var playerPlay = new int[] { -1, -1 };

            
            Console.WriteLine();
            while (!board.isTerminal()) {
                Console.Clear();

                if (error != "") {
                    Console.WriteLine(error);
                    error = "";
                } else {
                    Console.WriteLine();
                }

                if (compPlay[0] != -1) {
                    Console.WriteLine("Computer (X) played [" + (compPlay[0] + 1) + "," + (compPlay[1] + 1) + "]");
                    compPlay = new int[] { -1, -1 };
                } else {
                    Console.WriteLine();
                }

                if (playerPlay[0] != -1) {
                    Console.WriteLine("You (O) played [" + (playerPlay[0] + 1) + ", " + (playerPlay[1] + 1) + "]");
                    playerPlay = new int[] { -1, -1 };
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
                Console.Write("Index (c,r)> ");
                string input = Console.ReadLine();

                // Catch Errors in input
                try {
                    if (int.Parse(input[0] + "") > 3 || int.Parse(input[2] + "") > 3 ||
                        int.Parse(input[0] + "") < 1 || int.Parse(input[2] + "") < 1) {
                        error = "Index out of range! Indices must be between 1 and 3!";
                        continue;
                    }

                    
                    if (board.GetState()[int.Parse(input[2] + "") - 1, int.Parse(input[0] + "") - 1] != ' ') {
                        error = "Space has already been played in!";
                        continue;
                    }
                } catch {
                    error = "Index not in the format (column,row)! (i.e. 3,2)";
                    continue;
                }

                int[][] plays = Play(int.Parse(input[0] + "") - 1, int.Parse(input[2] + "") - 1);
                playerPlay = plays[0];
                compPlay = plays[1];

                
            }
            
            Console.Clear();
            Console.Write("\n\n\n");
            Console.WriteLine(board);
            if (Math.Abs(board.Eval()) != 10) {
                Console.WriteLine("Tie!");
            } else {
                Console.WriteLine((turn == 'X' ? "Computer" : "You") + " Won!");
            }

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public bool IsComplete() { 
            return board.isTerminal(); 
        }

        public int[] Search(int depth = Int32.MaxValue) {
            Stopwatch time = new Stopwatch();
            time.Start();
            TicTacToeTree tree = new TicTacToeTree(new Board((char[,])this.board.GetState().Clone()), turn, depth);
            time.Stop();
            compSearchTime = time.ElapsedMilliseconds;
            return tree.GetBestPlay();
        }

        public override string ToString() { return board.ToString(); }


    }
}
