using COMP717.Algorithms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.TicTacToe {
    public class TicTacToeGame {
        private Board board = new Board();
        private char turn;
        private int searchDepth;
        public Statistics stats = new Statistics();

        public int[] LastMove = new int[2];

        public TicTacToeGame(bool playerStart = true, int searchDepth = 9, bool doUserInput = true) {
            turn = playerStart ? 'O' : 'X';
            this.searchDepth = searchDepth;

            if (!playerStart) {
                int[] agentPlay = Search(searchDepth);
                Play(agentPlay[1], agentPlay[0]);
                LastMove = agentPlay;
            }

            if (doUserInput) { UserPlay(playerStart); }
        }

        public void Play(int x, int y) {
            if (board.isTermnial()) { return; }
            if (!board.Add(x, y, turn)) { LastMove = new int[] { x, y }; }
            if (board.isTermnial()) { return; }
            turn = turn == 'X' ? 'O' : 'X';

            int[] agentPlay = Search(searchDepth);
            board.Add(agentPlay[1], agentPlay[0], turn);
            LastMove = agentPlay;
            if (board.isTermnial()) { return; }
            turn = turn == 'X' ? 'O' : 'X';
        }

        public void UserPlay(bool playerStart) {
            while (!board.isTermnial()) {

                Console.WriteLine(board);
                Console.Write("Index (c,r) > ");
                string input = Console.ReadLine();

                try {
                    if (int.Parse(input[0] + "") > 3 || int.Parse(input[2] + "") > 3 ||
                        int.Parse(input[0] + "") < 1 || int.Parse(input[2] + "") < 1) {
                        Console.WriteLine("Index out of range! Indices must be between 1 and 3!");
                        continue;
                    }
                } catch (Exception e) {
                    Console.WriteLine("Index not in the format (column,row)! (i.e. 3,2)");
                    continue;
                }

                Play(int.Parse(input[0] + "") - 1, int.Parse(input[2] + "") - 1);

                Console.Clear();
            }

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
            if (board.Eval() == 10) { stats.Wins++; }
            if (board.Eval() == -10) { stats.Losses++; }
            if (board.isTermnial() && Math.Abs(board.Eval()) != 10) { stats.Ties++; } 
            return board.isTermnial(); 
        }

        public int[] Search(int depth = Int32.MaxValue) {
            Stopwatch time = new Stopwatch();
            time.Start();
            TicTacToeTree tree = new TicTacToeTree(new Board((char[,])this.board.GetState().Clone()), turn, depth);
            time.Stop();
            stats.OperationTimes.Add(time.ElapsedMilliseconds);
            //Console.WriteLine(tree.root);
            //System.Threading.Thread.Sleep(5);
            return tree.GetBestPlay();
        }

        public override string ToString() { return board.ToString(); }


    }
}
