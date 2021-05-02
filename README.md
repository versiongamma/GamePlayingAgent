# Game Playing Agent - COMP717

#### An implementation of Minimax and Alpha Beta Pruning to solve TicTacToe, Take Away, and Connect Four

---

This repository contains the code base for a .NET console application that allows the user to play against game playing agents, and adjust the settings that the agent uses for a specified game.

## TicTacToe

## Take Away

In this game, there is a pile of X chips on the table. Two players take turns to remove 1 to n chips from the table, with player 1 starting first, and the player removing the last chip(s) wins the game.

For this game, the user can specify the same parameters as in TicTacToe, while also adding:

- The number of chips that start on the table (X)
- The amount each player is allowed to remove (n)

## Connect 4

Connect 4 has an updated search algorithm, Alpha Beta Pruning, which acts as a superset of Minimax, wherein it has the same basic function, but will prune nodes based on specific conditions. In this implementation, αβ pruning is done during the generation of the game tree, unlike Minimax which is run after the tree is created