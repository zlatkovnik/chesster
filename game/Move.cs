namespace Chesster
{
    public struct Move
    {
        /*
        0000 0000 0000 0000 0000 0111 1111 -> From 0x3f
        0000 0000 0000 0011 1111 1000 0000 -> To >> 7, 0x3f
        0000 0000 0011 1100 0000 0000 0000 -> Captured piece >> 14, 0xf
        0000 0000 0100 0000 0000 0000 0000 -> En passant 0x40000
        0000 0000 1000 0000 0000 0000 0000 -> Pawn start 0x80000
        0000 1111 0000 0000 0000 0000 0000 -> Promoted piece >> 20, 0xf
        0001 0000 0000 0000 0000 0000 0000 -> Castle 0x1000000
        */
        public int Code;
        //Score is for sorting
        public int Score;
    }
}