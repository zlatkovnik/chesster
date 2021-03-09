namespace Chesster
{
    public static class Piece
    {
        public const int Empty = 0;
        public const int wP = 1;
        public const int wN = 2;
        public const int wB = 3;
        public const int wR = 4;
        public const int wQ = 5;
        public const int wK = 6;
        public const int bP = 7;
        public const int bN = 8;
        public const int bB = 9;
        public const int bR = 10;
        public const int bQ = 11;
        public const int bK = 12;
    }
    public static class Color
    {
        public const int White = 0;
        public const int Black = 1;
        public const int Both = 2;
    }
    public static class File
    {
        public const int A = 0;
        public const int B = 1;
        public const int C = 2;
        public const int D = 3;
        public const int E = 4;
        public const int F = 5;
        public const int G = 6;
        public const int H = 7;
        public const int None = 8;
    }
    public static class Rank
    {
        public const int r1 = 0;
        public const int r2 = 1;
        public const int r3 = 2;
        public const int r4 = 3;
        public const int r5 = 4;
        public const int r6 = 5;
        public const int r7 = 6;
        public const int r8 = 7;
        public const int None = 8;
    }
    public static class Position
    {
        public const int A1 = 21;
        public const int B1 = 22;
        public const int C1 = 23;
        public const int D1 = 24;
        public const int E1 = 25;
        public const int F1 = 26;
        public const int G1 = 27;
        public const int H1 = 28;

        public const int A2 = 32;
        public const int B2 = 32;
        public const int C2 = 33;
        public const int D2 = 34;
        public const int E2 = 35;
        public const int F2 = 36;
        public const int G2 = 37;
        public const int H2 = 38;

        public const int A3 = 41;
        public const int B3 = 42;
        public const int C3 = 43;
        public const int D3 = 44;
        public const int E3 = 45;
        public const int F3 = 46;
        public const int G3 = 47;
        public const int H3 = 48;

        public const int A4 = 51;
        public const int B4 = 52;
        public const int C4 = 53;
        public const int D4 = 54;
        public const int E4 = 55;
        public const int F4 = 56;
        public const int G4 = 57;
        public const int H4 = 58;

        public const int A5 = 61;
        public const int B5 = 62;
        public const int C5 = 63;
        public const int D5 = 64;
        public const int E5 = 65;
        public const int F5 = 66;
        public const int G5 = 67;
        public const int H5 = 68;

        public const int A6 = 71;
        public const int B6 = 72;
        public const int C6 = 73;
        public const int D6 = 74;
        public const int E6 = 75;
        public const int F6 = 76;
        public const int G6 = 77;
        public const int H6 = 78;

        public const int A7 = 81;
        public const int B7 = 82;
        public const int C7 = 83;
        public const int D7 = 84;
        public const int E7 = 85;
        public const int F7 = 86;
        public const int G7 = 87;
        public const int H7 = 88;

        public const int A8 = 91;
        public const int B8 = 92;
        public const int C8 = 93;
        public const int D8 = 94;
        public const int E8 = 95;
        public const int F8 = 96;
        public const int G8 = 97;
        public const int H8 = 98;

        public const int NoSquare = 99;
        public const int OffBoard = 100;
    }

    public static class Castle
    {
        public const int WhiteKing = 1;
        public const int WhiteQueen = 2;
        public const int BlackKing = 4;
        public const int BlackQueen = 8;
    }
}
