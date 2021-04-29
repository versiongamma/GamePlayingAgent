using COMP717.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/** TODO: fucking kill myself */

namespace COMP717 {
    public class Compare {
        public static void CompareTicTacToe(int searchDepth, int iterations = 10, bool depthLimitedStarts = true) {
            

            List<long> timesG1 = new List<long>();
            List<long> timesG2 = new List<long>();

            int winsG1 = 0, ties = 0;

            for (int i = 0; i < iterations; i++) {

                TicTacToeGame g1 = new TicTacToeGame(searchDepth: depthLimitedStarts ? searchDepth : 9, doUserInput: false, playerStart: false);
                TicTacToeGame g2 = new TicTacToeGame(searchDepth: depthLimitedStarts ? 9 : searchDepth, doUserInput: false);
                while (!g1.IsComplete()) {
                    g2.Play(g1.LastMove[0], g1.LastMove[1]);
                    g1.Play(g2.LastMove[0], g2.LastMove[0]);
                }

                timesG1.Add(g1.stats.AverageOperationTime);
                timesG2.Add(g2.stats.AverageOperationTime);

                winsG1 += g1.stats.Wins;
                ties += g1.stats.Ties;
            }

            Console.WriteLine((depthLimitedStarts ? "Depth Limited Search: " : "Full Tree Search: ") + timesG1.Sum() / timesG1.Count() + "ms per search");
            Console.WriteLine((depthLimitedStarts ? "Full Tree Search: " : "Depth Limited Search: ") + timesG2.Sum() / timesG2.Count() + "ms per search");

            Console.WriteLine();

            Console.WriteLine(winsG1 + " Wins to " + (depthLimitedStarts ? "Depth Limited Search" : "Full Tree Search"));
            Console.WriteLine(-winsG1 + " Losses to " + (depthLimitedStarts ? "Depth Limited Search" : "Full Tree Search"));
            Console.WriteLine(ties + " Ties");

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }
    }
}
