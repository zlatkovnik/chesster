namespace Chesster
{
    public class Board
    {
        //Status of each square on board
        public int[] Pieces { get; set; }
        //Bitboards for pawns
        public ulong[] Pawns { get; set; }
        //Squares for kings
        public int[] KingSquare { get; set; }
        //Whose turn it is
        public int Side { get; set; }
        //En passant square
        public int EnPassant { get; set; }
        //50 move counter after last take
        public int FiftyMove { get; set; }
        //Ply counter for current search
        public int Ply { get; set; }
        //How many plys have been played
        public int HistoryPly { get; set; }
        //Zobrist key for current state of board
        public ulong PositionKey { get; set; }
        //Is castle possible?
        public int CastlePermission { get; set; }
        //How many pieces are on the board for each type
        public int[] NumberOfPieces { get; set; }
        //Not pawn
        public int[] BigPieces { get; set; }
        //Rook and Queen
        public int[] MajorPieces { get; set; }
        //Knight and Bishop
        public int[] MinorPieces { get; set; }
        //Move history
        public Undo[] History { get; set; }
        //Position for every piece, First index is piece, second is which
        public int[,] PieceList { get; set; }

        public Board()
        {
            Pieces = new int[120];
            Pawns = new ulong[3];
            KingSquare = new int[2];
            NumberOfPieces = new int[13];
            BigPieces = new int[3];
            MajorPieces = new int[3];
            MinorPieces = new int[3];
            //Maximum number of moves 2048
            History = new Undo[2048];
            //10 because you can have 2 rooks, and 8 from pawns
            PieceList = new int[13, 10];
        }

        public void InitializeBoard()
        {
            FEN.CodeToBoard(FEN.StandardFEN, this);
        }
    }
}