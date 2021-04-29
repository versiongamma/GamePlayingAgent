using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.ConnectFour {
    class Grid : State {
        char[,] state = new char[6, 7];

        public Grid() {
            for (var y = 0; y < 6; y++) {
                for (var x = 0; x < 7; x++) {
                    state[y, x] = ' ';
                }
            }

        }

        public Grid(char [,] state) { this.state = state; }

        public bool Add(int column, char turn) {
            if (state[5, column] != ' ') { return false; }
            for (var y = 0; y < 6; y++) { 
                if (state[y, column] == ' ') { 
                    state[y, column] = turn;
                    return true;
                } 
            }
            return false;
        }

        public bool ColumnFull(int x) {
            return state[5, x] != ' ';
        }

        public char Get(int x, int y) { return state[y, x]; }

        public char[,] GetState() { return state; }

        public override string ToString() {
            string output = "";
            for (var y = 5; y >= 0; y--) {
                output += "| ";
                for (var x = 0; x < 7; x++) {
                    output += state[y, x] + " | ";
                }
                output += "\n";
            }
            return output;
        }

        public int Eval() {
            var final = 0;
            /** Check all columns */
            for (var y = 0; y < 6; y++) {
                var result = 0;
                var chainX = 0;
                var chainO = 0;
                for (var x = 0; x < 7; x++) {
                    if (state[y,x] == 'X') {
                        chainO = 0;
                        if (chainX == 0) { result += 1; chainX++; continue; }
                        if (chainX == 1) { result += 3; chainX++; continue; }
                        if (chainX == 2) { result += 5; chainX++; continue; }
                        if (chainX == 3) { return 100; }
                    }
                    else if (state[y, x] == 'O') {
                        chainX = 0;
                        if (chainO == 0) { result -= 1; chainO++; continue; }
                        if (chainO == 1) { result -= 3; chainO++; continue; }
                        if (chainO == 2) { result -= 5; chainO++; continue; }
                        if (chainO == 3) { return -100; }
                    }

                    else {
                        chainX = 0;
                        chainO = 0;
                    }
                }

                final += result;
            }

            /** Check all rows */
            for (var x = 0; x < 7; x++) {
                var result = 0;
                var chainX = 0;
                var chainO = 0;
                for (var y = 0; y < 6; y++) {
                    if (state[y, x] == 'X') {
                        chainO = 0;
                        if (chainX == 0) { result += 1; chainX++; continue; }
                        if (chainX == 1) { result += 3; chainX++; continue; }
                        if (chainX == 2) { result += 5; chainX++; continue; }
                        if (chainX == 3) { return 100; }
                    } else if (state[y, x] == 'O') {
                        chainX = 0;
                        if (chainO == 0) { result -= 1; chainO++; continue; }
                        if (chainO == 1) { result -= 3; chainO++; continue; }
                        if (chainO == 2) { result -= 5; chainO++; continue; }
                        if (chainO == 3) { return -100; }
                    } else {
                        chainX = 0;
                        chainO = 0;
                    }
                }

                final += result;
            }

            /** Diagonals (From Left) */
            int i = 0, j = 2;
            while (i < 3) {
                int x = 0, y = 0;
                if (j > 0) {
                    y = 5 - j;
                    x = 0;

                    
                    j--;
                } else if (j == 0) {
                    i = 0;
                    j = Int32.MinValue;

                    y = 5;
                    x = i;
                } else {
                    y = 5;
                    x = i;
                }


                var result = 0;
                var chainX = 0;
                var chainO = 0;
                while (x < 6 && y >= 0) {
                    //Console.WriteLine("[" + y + "," + x + "]");
                    if (state[y, x] == 'X') {
                        chainO = 0;
                        y--; x++; 
                        if (chainX == 0) { result += 1; chainX++; continue; }
                        if (chainX == 1) { result += 3; chainX++; continue; }
                        if (chainX == 2) { result += 5; chainX++; continue; }
                        if (chainX == 3) { return 100; }
                    } else if (state[y, x] == 'O') {
                        chainX = 0;
                        y--; x++; 
                        if (chainO == 0) { result -= 1; chainO++; continue; }
                        if (chainO == 1) { result -= 3; chainO++; continue; }
                        if (chainO == 2) { result -= 5; chainO++; continue; }
                        if (chainO == 3) { return -100; }
                    }

                    chainO = 0;
                    chainX = 0;
                    y--; x++;
                }

                i++;
                final += result;
            }

            /** Diagonals (From Right) */
            i = 6; j = 2;
            while (i <= 6 && i >= 3) {
                int x = 0, y = 0;
                if (j > 0) {
                    y = 5 - j;
                    x = 6;


                    j--;
                } else if (j == 0) {
                    i = 6;
                    j = Int32.MinValue;

                    y = 5;
                    x = i;                
                } else {
                    y = 5;
                    x = i;
                }

                var result = 0;
                var chainX = 0;
                var chainO = 0;
                while (x >= 0 && y >= 0) {
                    //Console.WriteLine("[" + y + "," + x + "]");
                    if (state[y, x] == 'X') {
                        chainO = 0;
                        y--; x--;
                        if (chainX == 0) { result += 1; chainX++; continue; }
                        if (chainX == 1) { result += 3; chainX++; continue; }
                        if (chainX == 2) { result += 5; chainX++; continue; }
                        if (chainX == 3) { return 100; }
                    } else if (state[y, x] == 'O') {
                        chainX = 0;
                        y--; x--;
                        if (chainO == 0) { result -= 1; chainO++; continue; }
                        if (chainO == 1) { result -= 3; chainO++; continue; }
                        if (chainO == 2) { result -= 5; chainO++; continue; }
                        if (chainO == 3) { return -100; }
                    }

                    chainO = 0;
                    chainX = 0;
                    y--; x--;
                }

                i--;
                final += result;
            }

            return final;
        }

        public bool IsFull() {
            foreach (char space in state) {
                if (space == ' ') { return false; }
            }

            return true;
        }

        public bool isTermnial() { return Math.Abs(Eval()) == 100 || IsFull(); }
    }
}
