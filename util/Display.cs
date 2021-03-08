using System;

namespace Chesster
{
    public static class Display
    {
        public static void PrintBoard(int[] pieces)
        {
            for (int i = 0; i < 64; i++)
            {
                int index = Converter.From64To120[i];
                string symbol = GetPieceSymbol(pieces[index]);
                Console.Write(symbol);
                if (i % 7 == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        public static string GetPieceSymbol(int piece)
        {
            switch (piece)
            {
                case Piece.Empty:
                    return " ";
                case Piece.wP:
                    return "wP";
                case Piece.wN:
                    return "wN";
                case Piece.wB:
                    return "wB";
                case Piece.wQ:
                    return "wQ";
                case Piece.wK:
                    return "wK";
                case Piece.bP:
                    return "bP";
                case Piece.bN:
                    return "bN";
                case Piece.bB:
                    return "bB";
                case Piece.bQ:
                    return "bQ";
                case Piece.bK:
                    return "bK";
                default:
                    return "err";
            }
        }
    }
}