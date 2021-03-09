using System;

namespace Chesster
{
    public static class Display
    {
        public static char[] PieceChar =
        {
            '.',
            '♙', '♘', '♗', '♖', '♕', '♔',
            '♟', '♞', '♝', '♜', '♛', '♚'
        };

        public static char[] SideChar = { 'w', 'b', '-' };
        public static char[] RankChar = { '1', '2', '3', '4', '5', '6', '7', '8' };
        public static char[] FileChar = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

        public static void PrintBoard(Board board)
        {
            Console.WriteLine("Board: ");
            for (int rank = Rank.r8; rank >= Rank.r1; rank--)
            {
                Console.WriteLine();
                Console.Write("{0,3}", RankChar[rank]);
                for (int file = File.A; file <= File.H; file++)
                {
                    int sq = Util.FileRankToSquare(file, rank);
                    int piece = board.Pieces[sq];
                    Console.Write(String.Format("{0,3}", PieceChar[piece]));
                }
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int file = File.A; file <= File.H; file++)
            {
                Console.Write(String.Format("{0,3}", FileChar[file]));
            }
            Console.WriteLine();
            Console.WriteLine(String.Format("Side: {0}", SideChar[board.Side]));
            Console.WriteLine(String.Format("En passant: {0}", board.EnPassant));
            Console.WriteLine(String.Format("Castle: {0}{1}{2}{3}",
            ((board.CastlePermission & Castle.WhiteKing) != 0) ? 'K' : '-',
            ((board.CastlePermission & Castle.WhiteQueen) != 0) ? 'Q' : '-',
            ((board.CastlePermission & Castle.BlackKing) != 0) ? 'k' : '-',
            ((board.CastlePermission & Castle.BlackQueen) != 0) ? 'q' : '-'
            ));
            Console.WriteLine(String.Format("Key: {0}", board.PositionKey));
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
                        sq = Util.FileRankToSquare(file, rank);
                        sq64 = Util.From120To64[sq];
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