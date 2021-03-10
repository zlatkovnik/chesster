using System;

namespace Chesster
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.InitializeBoard();
            FEN.CodeToBoard(FEN.PawnFEN, board);
            Display.PrintBoard(board);
            //Display.PrintBitBoards(board.Pawns);

            Moves moves = new Moves();
            moves.Count = 0;
            moves.List = new Move[256];
            board.GenerateAllMoves(ref moves);

        }


    }
}
