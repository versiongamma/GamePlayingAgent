using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.ConnectFour {
    class Grid : State {
        private char[,] state = new char[6, 7];

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
        public void SetState(char[,] state) { this.state = state; }

        public void Flip() {
            for (var y = 0; y < 6; y++) {
                for (var x = 0; x < 7; x++) {
                    if (state[y, x] == ' ') { continue; }
                    if (state[y, x] == 'X') { state[y, x] = 'O'; continue; }
                    if (state[y, x] == 'O') { state[y, x] = 'X'; continue; }
                }
            }
        }

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
            /**
            var final = 0;

            string[] terminals = new string[] { 
                "00 01 02 03", "01 02 03 04", "02 03 04 05", "03 04 05 06",
                "10 11 12 13", "11 12 13 04", "12 13 14 15", "13 14 15 16",
                "20 21 22 23", "21 22 23 04", "22 23 24 25", "23 24 25 26",
                "30 31 32 33", "31 32 33 04", "32 33 34 35", "33 34 35 36",
                "40 41 42 43", "41 42 43 04", "42 43 44 45", "43 44 45 46",
                "50 51 52 53", "51 52 53 04", "52 53 54 55", "53 54 55 56",

                "00 10 20 30", "10 20 30 40", "20 30 40 50", 
                "01 11 21 31", "11 21 31 41", "21 31 41 51", 
                "02 12 22 32", "12 22 32 42", "22 32 42 52", 
                "03 13 23 33", "13 23 33 43", "23 33 43 53", 
                "04 14 24 34", "14 24 34 44", "24 34 44 54", 
                "05 15 25 35", "15 25 35 45", "25 35 45 55", 
                "06 16 26 36", "16 26 36 46", "26 36 46 56", 

                "30 21 12 03", "40 31 22 13", "50 41 32 23",
                "31 22 13 04", "41 32 23 14", "51 42 33 24",
                "32 23 14 05", "42 33 24 15", "52 43 34 25",
                "33 24 15 06", "43 34 25 16", "53 44 35 26",

                "36 25 14 03", "46 35 24 13", "56 45 34 23",
                "35 24 13 02", "45 34 23 12", "55 44 33 22",
                "34 23 12 01", "44 33 22 11", "54 43 32 21",
                "33 22 11 00", "43 32 21 10", "53 42 31 20",
            };

            foreach (string t in terminals) {
                var result = 0;
                if (state[int.Parse(t[0] + ""), int.Parse(t[1] + "")] == 'X') { result++; }
                if (state[int.Parse(t[3] + ""), int.Parse(t[4] + "")] == 'X') { result++; }
                if (state[int.Parse(t[6] + ""), int.Parse(t[7] + "")] == 'X') { result++; }
                if (state[int.Parse(t[9] + ""), int.Parse(t[10] + "")] == 'X') { result++; }

                if (state[int.Parse(t[0] + ""), int.Parse(t[1] + "")] == 'O') { result--; }
                if (state[int.Parse(t[3] + ""), int.Parse(t[4] + "")] == 'O') { result--; }
                if (state[int.Parse(t[6] + ""), int.Parse(t[7] + "")] == 'O') { result--; }
                if (state[int.Parse(t[9] + ""), int.Parse(t[10] + "")] == 'O') { result--; }

                if (result == 4) { return Int32.MaxValue; }
                if (result == -4) { return Int32.MinValue; }

                final += result * 2;
            }

            return final;
            */

            var final = 0;
            /** Check all columns */
            for (var y = 0; y < 6; y++) {
                var result = 0;
                var chainX = 0;
                var chainO = 0;
                for (var x = 0; x < 7; x++) {
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
