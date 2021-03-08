using System;

namespace Chesster
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.InitializeBoard();
            //Display.PrintBoard(board.Pieces);
            //Display.PrintBitBoards(board.Pawns);
            int count = Bitboard.CountBits(board.Pawns[0]);
            Console.WriteLine(count);
        }
    }
}
