namespace LeetCode.Cracking.MathAndLogicPuzzles;

/// <summary>
/// There is an 8x8 chessboard in which two diagonally opposite corners have been cut off.
/// You are given 31 dominos, and a single domino can cover exactly two squares. Can you use the 31 dominos
/// to cover the entire board? Prove your answer by providing an example or showing why it's impossible.
/// 
/// Solution:
/// Each domino would take 1 black and 1 white cell. Originally, there are 32 black and 32 white cells.
/// Taking off 2 cells in opposite corners, it's either 2 black or 2 white.
/// Either way, the dominos cannot be anymore placed.
/// </summary>
