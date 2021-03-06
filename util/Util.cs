using System.Diagnostics;

namespace Chesster
{
    public static class Util
    {
        public static void InitUtil()
        {
            Util.InitBitMasks();
            Util.InitHashKeys();
        }
        #region Conversion
        public static int[] From120To64 = {
            65, 65, 65, 65, 65, 65, 65, 65, 65, 65,
            65, 65, 65, 65, 65, 65, 65, 65, 65, 65,
            65, 0,  1,  2,  3,  4,  5,  6,  7,  65,
            65, 8,  9,  10, 11, 12, 13, 14, 15, 65,
            65, 16, 17, 18, 19, 20, 21, 22, 23, 65,
            65, 24, 25, 26, 27, 28, 29, 30, 31, 65,
            65, 32, 33, 34, 35, 36, 37, 38, 39, 65,
            65, 40, 41, 42, 43, 44, 45, 46, 47, 65,
            65, 48, 49, 50, 51, 52, 53, 54, 55, 65,
            65, 56, 57, 58, 59, 60, 61, 62, 63, 65,
            65, 65, 65, 65, 65, 65, 65, 65, 65, 65,
            65, 65, 65, 65, 65, 65, 65, 65, 65, 65
        };
        public static int[] From64To120 = {
            21, 22, 23, 24, 25, 26, 27, 28,
            31, 32, 33, 34, 35, 36, 37, 38,
            41, 42, 43, 44, 45, 46, 47, 48,
            51, 52, 53, 54, 55, 56, 57, 58,
            61, 62, 63, 64, 65, 66, 67, 68,
            71, 72, 73, 74, 75, 76, 77, 78,
            81, 82, 83, 84, 85, 86, 87, 88,
            91, 92, 93, 94, 95, 96, 97, 98
        };

        public static int[] SquareToFile = {
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100,   0,   1,   2,   3,   4,   5,   6,   7, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100
        };

        public static int[] SquareToRank = {
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100,   0,   0,   0,   0,   0,   0,   0,   0, 100,
            100,   1,   1,   1,   1,   1,   1,   1,   1, 100,
            100,   2,   2,   2,   2,   2,   2,   2,   2, 100,
            100,   3,   3,   3,   3,   3,   3,   3,   3, 100,
            100,   4,   4,   4,   4,   4,   4,   4,   4, 100,
            100,   5,   5,   5,   5,   5,   5,   5,   5, 100,
            100,   6,   6,   6,   6,   6,   6,   6,   6, 100,
            100,   7,   7,   7,   7,   7,   7,   7,   7, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100, 100, 100
        };

        public static int FileRankToSquare(int file, int rank)
        {
            return 21 + file + rank * 10;
        }
        #endregion

        #region Bitmaps
        public static ulong[] SetMask;
        public static ulong[] ClearMask;

        public static void InitBitMasks()
        {
            SetMask = new ulong[64];
            ClearMask = new ulong[64];
            int index = 0;

            for (index = 0; index < 64; index++)
            {
                SetMask[index] = 0UL;
                ClearMask[index] = 0UL;
            }

            for (index = 0; index < 64; index++)
            {
                SetMask[index] |= (1UL << index);
                ClearMask[index] = ~SetMask[index];
            }
        }

        public static int[] BitTable =
        {
            63, 30, 3, 32, 25, 41, 22, 33, 15, 50, 42, 13, 11, 53, 19, 34, 61, 29, 2,
            51, 21, 43, 45, 10, 18, 47, 1, 54, 9, 57, 0, 35, 62, 31, 40, 4, 49, 5, 52,
            26, 60, 6, 23, 44, 46, 27, 56, 16, 7, 39, 48, 24, 59, 14, 12, 55, 38, 28,
            58, 20, 37, 17, 36, 8
        };

        public static int PopBit(ref ulong bb)
        {
            ulong b = bb ^ (bb - 1);
            uint fold = (uint)((b & 0xffffffff) ^ (b >> 32));
            bb &= bb - 1;
            return BitTable[(fold * 0x783a9b23) >> 26];
        }

        public static int CountBits(ulong b)
        {
            int r;
            for (r = 0; b != 0; r++, b &= b - 1) ;
            return r;
        }
        #endregion

        #region HashKeys
        public static ulong[,] PieceKeys;
        public static ulong SideKey;
        public static ulong[] CastleKeys;

        public static ulong GeneratePositionKey(Board board)
        {

            ulong finalKey = 0;
            int piece = Piece.Empty;

            // Key for pieces
            for (int sq = 0; sq < 120; ++sq)
            {
                piece = board.Pieces[sq];
                if (piece != Piece.Empty && piece != Position.OffBoard)
                {
                    Debug.Assert(piece >= Piece.wP && piece <= Piece.bK);
                    finalKey ^= PieceKeys[piece, sq];
                }
            }

            if (board.Side == Color.White)
            {
                finalKey ^= SideKey;
            }

            if (board.EnPassant != Position.NoSquare)
            {
                Debug.Assert(board.EnPassant >= 0 && board.EnPassant < 120);
                finalKey ^= PieceKeys[Piece.Empty, board.EnPassant];
            }

            Debug.Assert(board.CastlePermission >= 0 && board.CastlePermission <= 15);

            finalKey ^= CastleKeys[board.CastlePermission];

            return finalKey;
        }

        private static void InitHashKeys()
        {
            PieceKeys = new ulong[13, 120];
            CastleKeys = new ulong[16];
            for (int i = 0; i < 13; ++i)
            {
                for (int j = 0; j < 120; ++j)
                {
                    PieceKeys[i, j] = RandomLong();
                }
            }
            SideKey = RandomLong();
            for (int i = 0; i < 16; ++i)
            {
                CastleKeys[i] = RandomLong();
            }
        }

        private static ulong RandomLong()
        {
            System.Random random = new System.Random();
            ulong result = (ulong)random.Next();
            result = (result << 32);
            result |= (ulong)(uint)random.Next();
            return result;
        }
        #endregion

        #region Pieces
        public static bool[] PieceBig = { false, false, true, true, true, true, true, false, true, true, true, true, true };
        public static bool[] PieceMaj = { false, false, false, false, true, true, true, false, false, false, true, true, true };
        public static bool[] PieceMin = { false, false, true, true, false, false, false, false, true, true, false, false, false };
        public static int[] PieceVal = { 0, 100, 325, 325, 550, 1000, 50000, 100, 325, 325, 550, 1000, 50000 };
        public static int[] PieceCol = {
            Color.Both, Color.White, Color.White, Color.White, Color.White, Color.White, Color.White,
            Color.Black, Color.Black, Color.Black, Color.Black, Color.Black, Color.Black
        };


        #endregion

        #region Attack
        public static int[] KnightDirection = { -8, -19, -21, -12, 8, 19, 21, 12 };
        public static int[] RookDirection = { -1, -10, 1, 10 };
        public static int[] BishopDirection = { -9, -11, 11, 9 };
        public static int[] KingDirection = { -1, -10, 1, 10, -9, -11, 11, 9 };

        public static bool[] PieceKnight = { false, false, true, false, false, false, false, false, true, false, false, false, false };
        public static bool[] PieceKing = { false, false, false, false, false, false, true, false, false, false, false, false, true };
        public static bool[] PieceRookQueen = { false, false, false, false, true, true, false, false, false, false, true, true, false };
        public static bool[] PieceBishopQueen = { false, false, false, true, false, true, false, false, false, true, false, true, false };


        #endregion
    }
}