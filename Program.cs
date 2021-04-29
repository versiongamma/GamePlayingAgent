using COMP717.ConnectFour;
using COMP717.TakeAway;
using COMP717.TicTacToe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717 {
    class Program {
        static void Main(string[] args) {
            bool exit = false;

            

            while (!exit) {
                Console.Clear();
                Console.Write("> ");
                string input = Console.ReadLine();

                switch (input.ToLower()) {
                    case "1":
                        Console.Clear();
                        TicTacToeGame tcc = new TicTacToeGame(searchDepth: 1);
                        break;
                    case "2":
                        Console.Clear();
                        TakeAwayGame ta = new TakeAwayGame();
                        break;
                    case "3":
                        Console.Clear();
                        ConnectFourGame c4 = new ConnectFourGame();
                        break;
                    case "x":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Not Valid!");
                        break;
                }

                
            }
        }
    }
}
