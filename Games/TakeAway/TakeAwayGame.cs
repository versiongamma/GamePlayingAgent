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
        private long compSearchTime = 0;

        private int searchDepth, tableSize, maxTakeAway;

        public TakeAwayGame(int tSize = 25, int mTakeAway = 3, int sDepth = 0, bool doUserInput = true, bool playerStart = true) {
            if (sDepth == 0) { searchDepth = tSize;  }
            else { searchDepth = sDepth; }
            tableSize = tSize;
            maxTakeAway = mTakeAway;

            table = new Table(tableSize, maxTakeAway, turn);

            if (!playerStart) {
                turn = false;
                Stopwatch time = new Stopwatch();
                time.Start();
                int play = Search(searchDepth);
                time.Stop();
                compSearchTime = time.ElapsedMilliseconds;
                table.Remove(play);
                if (doUserInput) { UserPlay(play); }
            } else {
                if (doUserInput) { UserPlay(); }
            }

            
        }

        public void Play(int move) {
            if (table.isTerminal()) { return; }

            table.Remove(move);
            if (table.isTerminal()) { return; }
            turn = false;

            table.Remove(Search(searchDepth));
            if (table.isTerminal()) { return; }
            turn = true;
        }

        public void UserPlay(int agentPlay = 0) {
            var playerPlay = 0;
            var error = "";

            turn = true;

            while (!table.isTerminal()) {

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

                if (compSearchTime > 0) {
                    Console.WriteLine("Computer took: " + compSearchTime + "ms to find this play");
                    compSearchTime = 0;
                } else {
                    Console.WriteLine();
                }

                /** End the error and game state messages */

                Console.WriteLine(table + " chips on the table");
                Console.Write("Amount (1-" + maxTakeAway + ") > ");
                string input = Console.ReadLine();

                try {
                    if (int.Parse(input) > maxTakeAway || int.Parse(input) < 1) {
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
                if (table.isTerminal()) { break; }

                agentPlay = Search(searchDepth); turn = true;
                table.Remove(agentPlay);
                if (table.isTerminal()) { break; }

                
            }

            Console.Clear();
            if (turn) { Console.WriteLine("Computer removed " + agentPlay + " chips"); } else { Console.WriteLine("You removed " + playerPlay + " chips"); }

            Console.WriteLine("\n\nThere are 0 chips on the table");
            Console.WriteLine((turn ? "Computer" : "You") + " Won!");

            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public string Compare() {
            string output = compSearchTime + "ms for first search";

            while (!table.isTerminal()) {
                int play = Search(searchDepth);
                Play(play);
                //output += table + " ";
            }

            return output; //+ (turn ? "First Agent" : "Second Agent") + " Won";
        }

        public int Search(int depth = Int32.MaxValue) {
            Stopwatch time = new Stopwatch();
            time.Start();
            TakeAwayTree tree = new TakeAwayTree(table, depth);
            time.Stop();
            compSearchTime = time.ElapsedMilliseconds;

            return tree.GetBestPlay();
        }
    }
}
