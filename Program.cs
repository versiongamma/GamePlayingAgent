using COMP717.Algorithms;
using COMP717.Game.ConnectFour;
using COMP717.Game.TakeAway;
using COMP717.Game.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717 {
    class Program {
        static void Main(string[] args) {
            bool exit = false;

            var tccSearchDepth = 9;
            var taSearchDepth = 15;
            var c4SearchDepth = 6;

            var taCards = 15;
            var taRemoveAmount = 3;

            var c4minimax = false;

            var tccStarter = true;
            var taStarter = true;
            var c4Starter = true;
            
            while (!exit) {
                Console.Clear();

                Console.WriteLine("" +
"   _____ ____  __  __ _____ ______ __ ______ \n" +
"  / ____/ __ \\|  \\/  |  __ \\____  /_ |____  |\n" +
" | |   | |  | | \\  / | |__) |  / / | |   / / \n" +
" | |   | |  | | |\\/| |  ___/  / /  | |  / /  \n" +
" | |___| |__| | |  | | |     / /   | | / /   \n" +
"  \\_____\\____/|_|  |_|_|    /_/    |_|/_/  v1.1");

                Console.WriteLine("\n -- Assignment One - Game Playing Agents --");
                Console.WriteLine("       -- Matt Tribble - 19076935 --\n");

                Console.WriteLine("1) Tic Tac Toe");
                Console.WriteLine("2) Take Away");
                Console.WriteLine("3) Connect 4");
                Console.WriteLine("4) Settings");
                Console.WriteLine("x) Exit\n");

                Console.Write("> ");
                string input = Console.ReadLine();

                switch (input.ToLower()) {
                    case "1":
                        Console.Clear();
                        TicTacToeGame tcc = new TicTacToeGame(tccStarter, tccSearchDepth);
                        break;
                    case "2":
                        Console.Clear();
                        TakeAwayGame ta = new TakeAwayGame(taCards, taRemoveAmount, taSearchDepth, true, taStarter);
                        break;
                    case "3":
                        Console.Clear();
                        ConnectFourGame c4 = new ConnectFourGame(c4SearchDepth, c4Starter, c4minimax);
                        break;
                    case "4":
                        input = "";
                        while (input != "x") {
                            Console.Clear();
                            Console.WriteLine("" +
"  ___ ___ _____ _____ ___ _  _  ___ ___ \n" +
" / __| __|_   _|_   _|_ _| \\| |/ __/ __| \n" +
" \\__ \\ _|  | |   | |  | || .` | (_ \\__ \\ \n" +
" |___/___| |_|   |_| |___|_|\\_|\\___|___/\n\n");
                            Console.WriteLine("1) Tic Tac Toe");
                            Console.WriteLine("2) Take Away");
                            Console.WriteLine("3) Connect 4");
                            Console.WriteLine("x) Exit\n");

                            Console.Write("> ");
                            input = Console.ReadLine();

                            if (input == "x") { break; }
                            switch(input.ToLower()) {
                                /* Settings for Tic Tac Toe */
                                case "1":
                                    input = "";
                                    while (input != "x") {
                                        Console.Clear();
                                        Console.WriteLine("Tic Tac Toe Settings\n");
                                        Console.WriteLine("1) Search Depth (" + tccSearchDepth + ")");
                                        Console.WriteLine("2) Starting Player (" + (tccStarter ? "Player" : "Agent") + ")");
                                        Console.WriteLine("x) Exit\n");

                                        Console.Write("> ");
                                        input = Console.ReadLine();

                                        if (input == "x") { break; }

                                        if (input == "1") {
                                            var set = false;
                                            while (!set) {
                                                try {
                                                    Console.Write("\nSearch Depth> ");
                                                    input = Console.ReadLine();
                                                    if (int.Parse(input) <= 0 || int.Parse(input) > 9) {
                                                        Console.WriteLine("\nSearch depth must be between 1 and 9\n");
                                                        continue;
                                                    }
                                                    tccSearchDepth = int.Parse(input);
                                                    input = "";
                                                    set = true;
                                                } catch { Console.WriteLine("\nNot a number!\n"); }
                                            }
                                        }

                                        if (input == "2") {
                                            var set = false;
                                            while (!set) {
                                                Console.Write("\nStarting Player (1: Player, 2: Agent)> ");
                                                input = Console.ReadLine();
                                                if (input != "1" && input != "2") {
                                                    Console.WriteLine("\nMust be 1 or 2!\n");
                                                    continue;
                                                }
                                                tccStarter = input == "1" ? true : false;
                                                input = "";
                                                set = true;
                                            }
                                        }
                                    }
                                break;

                                /* Settings for Take Away */
                                case "2":
                                    input = "";
                                    while (input != "x") {
                                        Console.Clear();
                                        Console.WriteLine("Take Away Settings\n");
                                        Console.WriteLine("1) Search Depth (" + taSearchDepth + ")");
                                        Console.WriteLine("2) Number of Chips (" + taCards + ")");
                                        Console.WriteLine("3) Chip Removal Limit (" + taRemoveAmount + ")");
                                        Console.WriteLine("4) Starting Player (" + (taStarter ? "Player" : "Agent") + ")");
                                        Console.WriteLine("x) Exit\n");

                                        Console.Write("> ");
                                        input = Console.ReadLine();

                                        if (input == "x") { break; }

                                        if (input == "1") {
                                            var set = false;
                                            while (!set) {
                                                try {
                                                    Console.Write("\nSearch Depth> ");
                                                    input = Console.ReadLine();
                                                    if (int.Parse(input) <= 0 || int.Parse(input) > taCards) {
                                                        Console.WriteLine("\nSearch depth must be between 1 and " + taCards + "\n");
                                                        continue;
                                                    }
                                                    taSearchDepth = int.Parse(input);
                                                    input = "";
                                                    set = true;
                                                } catch { Console.WriteLine("\nNot a number!\n"); }
                                            }
                                        }

                                        if (input == "2") {
                                            var set = false;
                                            while (!set) {
                                                try {
                                                    Console.Write("\nChips> ");
                                                    input = Console.ReadLine();
                                                    if (int.Parse(input) <= 0) {
                                                        Console.WriteLine("\nNumber must be greater than 0\n");
                                                        continue;
                                                    }
                                                    taCards = int.Parse(input);
                                                    input = "";
                                                    set = true;
                                                } catch { Console.WriteLine("\nNot a number!\n"); }
                                            }
                                        }

                                        if (input == "3") {
                                            var set = false;
                                            while (!set) {
                                                try {
                                                    Console.Write("\nRemoval Limit> ");
                                                    input = Console.ReadLine();
                                                    if (int.Parse(input) <= 0) {
                                                        Console.WriteLine("\nNumber must be greater than 0\n");
                                                        continue;
                                                    }
                                                    taRemoveAmount = int.Parse(input);
                                                    input = "";
                                                    set = true;
                                                } catch { Console.WriteLine("\nNot a number!\n"); }
                                            }
                                        }

                                        if (input == "4") {
                                            var set = false;
                                            while (!set) {
                                                Console.Write("\nStarting Player (1: Player, 2: Agent)> ");
                                                input = Console.ReadLine();
                                                if (input != "1" && input != "2") {
                                                    Console.WriteLine("\nMust be 1 or 2!\n");
                                                    continue;
                                                }
                                                taStarter = input == "1" ? true : false;
                                                input = "";
                                                set = true;
                                            }
                                        }
                                    }
                                    break;

                                /* Settings for Connect 4 */
                                case "3":
                                    input = "";
                                    while (input != "x") {
                                        Console.Clear();
                                        Console.WriteLine("Connect 4 Settings\n");
                                        Console.WriteLine("1) Search Depth (" + c4SearchDepth + ")");
                                        Console.WriteLine("2) Search Algoritm (" + (c4minimax ? "Minimax" : "Alpha Beta Pruning") + ")");
                                        Console.WriteLine("3) Starting Player (" + (c4Starter ? "Player" : "Agent") + ")");
                                        Console.WriteLine("x) Exit\n");

                                        Console.Write("> ");
                                        input = Console.ReadLine();

                                        if (input == "x") { break; }

                                        if (input == "1") {
                                            var set = false;
                                            while (!set) {
                                                try {
                                                    Console.Write("\nSearch Depth> ");
                                                    input = Console.ReadLine();
                                                    if (int.Parse(input) <= 0 || int.Parse(input) > 42) {
                                                        Console.WriteLine("\nSearch depth must be between 1 and 42\n");
                                                        continue;
                                                    }
                                                    c4SearchDepth = int.Parse(input);
                                                    input = "";
                                                    set = true;
                                                } catch { Console.WriteLine("\nNot a number!\n"); }
                                            }
                                        }

                                        if (input == "2") {
                                            var set = false;
                                            while (!set) {
                                                Console.Write("\nSearch Algorithm (1: Minimax, 2: Alpha Beta Pruning)> ");
                                                input = Console.ReadLine();
                                                if (input != "1" && input != "2") {
                                                    Console.WriteLine("\nMust be 1 or 2!\n");
                                                    continue;
                                                }
                                                c4minimax = input == "1" ? true : false;
                                                input = "";
                                                set = true;
                                            }
                                        }

                                        if (input == "3") {
                                            var set = false;
                                            while (!set) {
                                                Console.Write("\nStarting Player (1: Player, 2: Agent)> ");
                                                input = Console.ReadLine();
                                                if (input != "1" && input != "2") {
                                                    Console.WriteLine("\nMust be 1 or 2!\n");
                                                    continue;
                                                }
                                                c4Starter = input == "1" ?  true : false;
                                                set = true;
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        break;
                    case "x":
                        exit = true;
                        break;
                }              
            }   
        }
    }
}
