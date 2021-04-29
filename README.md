# Game Playing Agent - COMP717

#### An implementation of Minimax and Alpha Beta Pruning to solve TicTacToe, Take Away, and Connect Four

---

## Minimax

>TicTacToe and Take Away are both solved with a Minimax algorithm, either depth limited or with a full tree search

## Alpha Beta Pruning

>Connect Four, alongside the Minimax algorithm, has a αβ pruned tree search.

The evalutation function of Connect Four is as follows:

`for (all columns, rows, diagonals) eval += X1 + 3*X2 + 5*X3 - (O1 + 3*O2 + 5*O3)`

`if (X4 == 1) eval = 100`

`if (O4 == 1) eval = -100`

Where X1-04 is the number of chips in succession of a specific type, i.e. X2 is 2 X chips in succession 