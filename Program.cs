using System;

namespace Chesster
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.InitializeBoard();
            FEN.CodeToBoard(FEN.TestFEN, board);
            Display.PrintBoard(board);
            Display.PrintBitBoards(board.Pawns);
        }
    }
}
