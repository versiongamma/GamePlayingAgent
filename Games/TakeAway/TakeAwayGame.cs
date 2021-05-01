using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.TakeAway {
    public class TakeAwayGame {
        public Table table;
        public bool turn = true;

        private int searchDepth, tableSize, maxTakeAway;
        private List<long> averageOperationTime = new List<long>();

        public TakeAwayGame(int tSize = 25, int mTakeAway = 3, int sDepth = 0, bool doUserInput = true) {
            if (sDepth == 0) { searchDepth = tSize;  }
            else { searchDepth = sDepth; }
            tableSize = tSize;
            maxTakeAway = mTakeAway;

            table = new Table(tableSize, maxTakeAway, turn);

            if (doUserInput) { UserPlay(); }
        }

        public void Play(int move) {
            if (table.isTermnial()) { return; }

            table.Remove(move);
            if (table.isTermnial()) { return; }
            turn = false;

            table.Remove(Search(searchDepth));
            if (table.isTermnial()) { return; }
            turn = true;
        }

        public void UserPlay() {
            var agentPlay = 0;
            var playerPlay = 0;
            var error = "";

            while (!table.isTermnial()) {

                Console.Clear();

                /** Method for writing game state messages before the table appears */
                if (error != "") {
                    Console.WriteLine(error + "\n\n");
                    error = "";
                } else {
                    // This is a terrible way of implementing this. But I'll be damned if it wasn't quick and easy
                    if (playerPlay > 0) { Console.WriteLine("You removed " + playerPlay + " chips"); } else { Console.Write("\n"); };
                    if (agentPlay > 0) { Console.WriteLine("Computer removed " + agentPlay + " chips\n"); } else { Console.WriteLine("\n"); };
                }

                /** End the error and game state messages */

                Console.WriteLine(table + " chips on the table");
                Console.Write("Amount (1-" + maxTakeAway + ") > ");
                string input = Console.ReadLine();

                try {
                    if (int.Parse(input[0] + "") > maxTakeAway || int.Parse(input[0] + "") < 1) {
                        error = "Input out of range! (1-" + maxTakeAway + ")";
                        Console.Clear();
                        continue;
                    }
                } catch (Exception e) {
                    error = "Input not a number!";
                    Console.Clear();
                    continue;
                }

                // I don't like this duplication, but this isn't part of any algorithm so who cares
                playerPlay = int.Parse(input); turn = false;
                table.Remove(playerPlay);
                if (table.isTermnial()) { break; }

                agentPlay = Search(searchDepth); turn = true;
                table.Remove(agentPlay);
                if (table.isTermnial()) { break; }

                
            }

            Console.Clear();
            if (turn) { Console.WriteLine("Computer removed " + agentPlay + " chips"); } else { Console.WriteLine("You removed " + playerPlay + " chips"); }

            Console.WriteLine("\n\nThere are 0 chips on the table");
            Console.WriteLine((turn ? "Computer" : "You") + " Won!");

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public int Search(int depth = Int32.MaxValue) {
            Stopwatch time = new Stopwatch();
            time.Start();
            TakeAwayTree tree = new TakeAwayTree(table, depth);
            time.Stop();
            averageOperationTime.Add(time.ElapsedMilliseconds);

            return tree.GetBestPlay();
        }

        public long GetAverageOperationTime() { return averageOperationTime.Sum() / averageOperationTime.Count(); }
    }
}
