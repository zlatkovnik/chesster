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
    }
}