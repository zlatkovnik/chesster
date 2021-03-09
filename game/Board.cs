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
        //Material value of the board
        public int[] Material { get; set; }
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
            BigPieces = new int[2];
            MajorPieces = new int[2];
            MinorPieces = new int[2];
            Material = new int[2];
            //Maximum number of moves 2048
            History = new Undo[2048];
            //10 because you can have 2 rooks, and 8 from pawns
            PieceList = new int[13, 10];
        }

        public void InitializeBoard()
        {
            Util.InitUtil();
            //FEN.CodeToBoard(FEN.StandardFEN, this);
            ResetBoard();
            UpdateListsMaterial();
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 120; i++)
            {
                Pieces[i] = Position.OffBoard;
            }
            for (int i = 0; i < 64; i++)
            {
                Pieces[Util.From64To120[i]] = Piece.Empty;
            }

            for (int i = 0; i < 2; ++i)
            {
                BigPieces[i] = 0;
                MajorPieces[i] = 0;
                MinorPieces[i] = 0;
                Pawns[i] = 0UL;
            }

            for (int i = 0; i < 13; ++i)
            {
                NumberOfPieces[i] = 0;
            }

            KingSquare[Color.White] = KingSquare[Color.Black] = Position.NoSquare;

            Side = Color.Both;
            EnPassant = Position.NoSquare;
            FiftyMove = 0;

            Ply = 0;
            HistoryPly = 0;

            CastlePermission = 0;

            PositionKey = 0UL;
        }

        public void UpdateListsMaterial()
        {

            int piece, sq, colour;

            for (int i = 0; i < 120; ++i)
            {
                sq = i;
                piece = Pieces[i];
                if (piece != Position.OffBoard && piece != Piece.Empty)
                {
                    colour = Util.PieceCol[piece];

                    if (Util.PieceBig[piece]) BigPieces[colour]++;
                    if (Util.PieceMin[piece]) MinorPieces[colour]++;
                    if (Util.PieceMaj[piece]) MajorPieces[colour]++;

                    Material[colour] += Util.PieceVal[piece];

                    PieceList[piece, NumberOfPieces[piece]] = sq;
                    NumberOfPieces[piece]++;

                    if (piece == Piece.wK) KingSquare[Color.White] = sq;
                    if (piece == Piece.bK) KingSquare[Color.Black] = sq;

                    if (piece == Piece.wP)
                    {
                        Pawns[Color.White] |= Util.SetMask[Util.From120To64[sq]];
                        Pawns[Color.Both] |= Util.SetMask[Util.From120To64[sq]];
                    }
                    else if (piece == Piece.bP)
                    {
                        Pawns[Color.Black] |= Util.SetMask[Util.From120To64[sq]];
                        Pawns[Color.Both] |= Util.SetMask[Util.From120To64[sq]];
                    }
                }
            }

        }
    }
}