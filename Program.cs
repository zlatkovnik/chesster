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
            //Display.PrintBitBoards(board.Pawns);

            ShowSqAtBySide(1, board);
        }

        public static void ShowSqAtBySide(int side, Board board)
        {


            int rank = 0;
            int file = 0;
            int sq = 0;

            Console.WriteLine(String.Format("\n\nSquares attacked by:{0}\n", Display.SideChar[side]));
            for (rank = Rank.r8; rank >= Rank.r1; --rank)
            {
                for (file = File.A; file <= File.H; ++file)
                {
                    sq = Util.FileRankToSquare(file, rank);
                    if (board.SquareAttacked(sq, side))
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("-");
                    }

                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
