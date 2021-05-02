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
            var final = 0;
            
            for (var y = 0; y < 6; y++) {
                for (var x = 0; x < 4; x++) {
                    var result = 0;
                    var chainX = 0;
                    var chainO = 0;
                    for (var xShift = 0; xShift < 4; xShift++) {
                        if (state[y, x + xShift] == 'X') {
                            if (chainO > 0) {
                                result = 0;
                                break; 
                            }

                            if (chainX == 3) { return 100; }

                            chainX++;
                            result += chainX;
                            
                        }

                        if (state[y, x + xShift] == 'O') {
                            if (chainX > 0) {
                                result = 0;
                                break; 
                            }

                            if (chainO == 3) { return -100; }
                            chainO++;
                            result -= chainO;
                        }

                        if (state[y, x + xShift] == ' ') {
                            chainX = 0;
                            chainO = 0;
                        }
                    }

                    final += result;
                }
            }

            for (var x = 0; x < 7; x++) {
                for (var y = 0; y < 3; y++) {
                    var result = 0;
                    var chainX = 0;
                    var chainO = 0;
                    for (var yShift = 0; yShift < 4; yShift++) {                 
                        if (state[y + yShift, x] == 'X') {
                            if (chainO > 0) {
                                result = 0;
                                break;
                            }

                            if (chainX == 3) { return 100; }

                            chainX++;
                            result += chainX;

                        }

                        if (state[y + yShift, x] == 'O') {
                            if (chainX > 0) {
                                result = 0;
                                break;
                            }

                            if (chainO == 3) { return -100; }
                            chainO++;
                            result -= chainO;
                        }

                        if (state[y + yShift, x] == ' ') {
                            chainX = 0;
                            chainO = 0;
                        }
                    }

                    final += result;
                }
            }

            for (var x = 0; x < 4; x++) {
                for (var y = 3; y < 6; y++) {
                    var result = 0;
                    var chainX = 0;
                    var chainO = 0;
                    for (var i = 0; i < 4; i++) {
                        if (state[y - i, x + i] == 'X') {
                            if (chainO > 0) {
                                result = 0;
                                break;
                            }

                            if (chainX == 3) { return 100; }

                            chainX++;
                            result += chainX;

                        }

                        if (state[y - i, x + i] == 'O') {
                            if (chainX > 0) {
                                result = 0;
                                break;
                            }

                            if (chainO == 3) { return -100; }
                            chainO++;
                            result -= chainO;
                        }

                        if (state[y - i, x + i] == ' ') {
                            chainX = 0;
                            chainO = 0;
                        }
                    }

                    final += result;
                }
            }

            for (var x = 6; x >= 3; x--) {
                for (var y = 3; y < 6; y++) {
                    var result = 0;
                    var chainX = 0;
                    var chainO = 0;
                    for (var i = 0; i < 4; i++) {
                        if (state[y - i, x - i] == 'X') {
                            if (chainO > 0) {
                                result = 0;
                                break;
                            }

                            if (chainX == 3) { return 100; }

                            chainX++;
                            result += chainX;

                        }

                        if (state[y - i, x - i] == 'O') {
                            if (chainX > 0) {
                                result = 0;
                                break;
                            }

                            if (chainO == 3) { return -100; }
                            chainO++;
                            result -= chainO;
                        }

                        if (state[y - i, x - i] == ' ') {
                            chainX = 0;
                            chainO = 0;
                        }
                    }

                    final += result;
                }
            }

            return final;
        }

            public bool IsFull() {
            foreach (char space in state) {
                if (space == ' ') { return false; }
            }

            return true;
        }

        public bool isTerminal() { return Math.Abs(Eval()) == 100 || IsFull(); }
    }
}
