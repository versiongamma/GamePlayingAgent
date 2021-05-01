using COMP717.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717.Game.TicTacToe {
    public class Board : State {

        private char[,] state = new char[3, 3];

        public Board() {
            for (int y = 0; y < 3; y++) {
                for (int x = 0; x < 3; x++) {
                    state[y, x] = ' ';
                }
            }
        }

        public Board(char[,] state) {
            this.state = state;
        }

        public char Get(int x, int y) { return state[y, x]; }

        public char[,] GetState() { return state; }

        public bool Add(int x, int y, char turn) {
            if (state[y, x] == ' ') { state[y, x] = turn; return true; }
            return false;
        }
        

        public override string ToString() {
            string output = "    1   2   3\n";
            for (int y = 0; y < 3; y++) {
            output += (y + 1) +" | ";
                for (int x = 0; x < 3; x++) {
                    output += this.state[y, x] + " | ";
                }
            output += "\n";
            }
            return output;
        }

        public string ToString(Boolean singleLine) {
            if (!singleLine) { return ToString(); }

            string output = "| ";
            foreach (char tile in state) {
                output += tile + " | ";
            }

            return output;
        }

        public Boolean IsFull() {
            foreach(char tile in state) {
                if (tile == ' ') { return false;  }
            }
            return true;
        }

        public bool isTermnial() { return (Math.Abs(Eval()) == 10 || IsFull()); }

        public int Eval() {
            int result = 0;
            int xCount = 0, oCount = 0;


            for (int y = 0; y < 3; ++y) {
                xCount = 0;
                oCount = 0;
                for (int x = 0; x < 3; ++x) {
                    if (state[y, x] == 'X') { ++xCount; }
                    if (state[y, x] == 'O') { ++oCount; }
                }

                if (xCount == 2 && oCount == 0) { result += 3; }
                if (xCount == 1 && oCount == 0) { ++result; }

                if (oCount == 2 && xCount == 0) { result -= 3; }
                if (oCount == 1 && xCount == 0) { --result; }

                if (xCount == 3) { return 10; }
                if (oCount == 3) { return -10; }
            }

            for (int x = 0; x < 3; ++x) {
                xCount = 0;
                oCount = 0;
                for (int y = 0; y < 3; ++y) {
                    if (state[y, x] == 'X') { ++xCount; }
                    if (state[y, x] == 'O') { ++oCount; }
                }

                if (xCount == 2 && oCount == 0) { result += 3; }
                if (xCount == 1 && oCount == 0) { ++result; }

                if (oCount == 2 && xCount == 0) { result -= 3; }
                if (oCount == 1 && xCount == 0) { --result; }

                if (xCount == 3) { return 10; }
                if (oCount == 3) { return -10; }
            }

            xCount = 0;
            oCount = 0;
            for (int i = 0; i < 3; ++i) {
                if (state[i, i] == 'X') { ++xCount; }
                if (state[i, i] == 'O') { ++oCount; }
            }

            if (xCount == 2 && oCount == 0) { result += 3; }
            if (xCount == 1 && oCount == 0) { ++result; }

            if (oCount == 2 && xCount == 0) { result -= 3; }
            if (oCount == 1 && xCount == 0) { --result; }

            if (xCount == 3) { return 10; }
            if (oCount == 3) { return -10; }

            xCount = 0;
            oCount = 0;
            for (int i = 0; i < 3; ++i) {

                int col = i == 0 ? 2 : i == 2 ? 0 : i;
                if (state[i, col] == 'X') { ++xCount; }
                if (state[i, col] == 'O') { ++oCount; }
            }

            if (xCount == 2 && oCount == 0) { result += 3; }
            if (xCount == 1 && oCount == 0) { ++result; }

            if (oCount == 2 && xCount == 0) { result -= 3; }
            if (oCount == 1 && xCount == 0) { --result; }

            if (xCount == 3) { return 10; }
            if (oCount == 3) { return -10; }

            return result;
        }

        
    }
}
