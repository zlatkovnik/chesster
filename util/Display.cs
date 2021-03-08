using System;

namespace Chesster
{
    public static class Display
    {
        public static void PrintBoard(int[] pieces)
        {
            for (int i = 0; i < 64; i++)
            {
                if (i % 8 == 0)
                {
                    Console.WriteLine();
                }
                int index = Converter.From64To120[i];
                string symbol = GetPieceSymbol(pieces[index]);
                Console.Write(symbol);

            }
            Console.WriteLine();
        }

        public static string GetPieceSymbol(int piece)
        {
            switch (piece)
            {
                case Piece.Empty:
                    return "--";
                case Piece.wP:
                    return "♙";
                case Piece.wN:
                    return "♘";
                case Piece.wR:
                    return "♖";
                case Piece.wB:
                    return "♗";
                case Piece.wQ:
                    return "♕";
                case Piece.wK:
                    return "♔";
                case Piece.bP:
                    return "♟";
                case Piece.bR:
                    return "♜";
                case Piece.bN:
                    return "♞";
                case Piece.bB:
                    return "♝";
                case Piece.bQ:
                    return "♛";
                case Piece.bK:
                    return "♚";
                default:
                    return "er";
            }
        }

        public static void PrintBitBoards(ulong[] pawns)
        {
            Console.WriteLine("Bit boards:");
            ulong shift = 1UL;
            int rank = 0;
            int file = 0;
            int sq = 0;
            int sq64 = 0;

            for (int i = 0; i < 3; i++)
            {
                for (rank = Rank.r8; rank >= Rank.r1; --rank)
                {
                    for (file = File.A; file <= File.H; file++)
                    {
                        sq = Converter.FileRankToSquare(file, rank);
                        sq64 = Converter.From120To64[sq];
                        if (((shift << sq64) & pawns[i]) != 0UL)
                            Console.Write('X');
                        else
                            Console.Write('-');
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}