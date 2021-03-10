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

        public bool SquareAttacked(int sq, int side)
        {

            //int pce, index, t_sq, dir;

            // pawns
            if (side == Color.White)
            {
                if (Pieces[sq - 11] == Piece.wP || Pieces[sq - 9] == Piece.wP)
                {
                    return true;
                }
            }
            else
            {
                if (Pieces[sq + 11] == Piece.bP || Pieces[sq + 9] == Piece.bP)
                {
                    return true;
                }
            }

            //Knights
            for (int i = 0; i < 8; i++)
            {
                int piece = Pieces[sq + Util.KnightDirection[i]];
                if (piece != Position.OffBoard && Util.PieceKnight[piece] && Util.PieceCol[piece] == side)
                {
                    return true;
                }
            }

            //Rooks Queens
            for (int i = 0; i < 4; ++i)
            {
                int dir = Util.RookDirection[i];
                int tempSq = sq + dir;
                int piece = Pieces[tempSq];
                while (piece != Position.OffBoard)
                {
                    if (piece != Piece.Empty)
                    {
                        if (piece != Position.OffBoard && Util.PieceRookQueen[piece] && Util.PieceCol[piece] == side)
                        {
                            return true;
                        }
                        break;
                    }
                    tempSq += dir;
                    piece = Pieces[tempSq];
                }
            }

            // bishops, queens
            for (int i = 0; i < 4; ++i)
            {
                int dir = Util.BishopDirection[i];
                int tempSq = sq + dir;
                int piece = Pieces[tempSq];
                while (piece != Position.OffBoard)
                {
                    if (piece != Piece.Empty)
                    {
                        if (piece != Position.OffBoard && Util.PieceBishopQueen[piece] && Util.PieceCol[piece] == side)
                        {
                            return true;
                        }
                        break;
                    }
                    tempSq += dir;
                    piece = Pieces[tempSq];
                }
            }

            // kings
            for (int i = 0; i < 8; ++i)
            {
                int piece = Pieces[sq + Util.KingDirection[i]];
                if (piece != Position.OffBoard && Util.PieceKing[piece] && Util.PieceCol[piece] == side)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddQuietMove(int move, ref Moves moves)
        {
            moves.List[moves.Count].Code = move;
            moves.List[moves.Count].Score = 0;
            moves.Count++;
        }

        public void AddCaptureMove(int move, ref Moves moves)
        {
            moves.List[moves.Count].Code = move;
            moves.List[moves.Count].Score = 0;
            moves.Count++;
        }

        public void AddEnPassantMove(int move, ref Moves moves)
        {
            moves.List[moves.Count].Code = move;
            moves.List[moves.Count].Score = 0;
            moves.Count++;
        }

        public void GenerateAllMoves(ref Moves moves)
        {
            moves.Count = 0;
            if (Side == Color.White)
            {
                for (int pceNum = 0; pceNum < NumberOfPieces[Piece.wP]; ++pceNum)
                {
                    int sq = PieceList[Piece.wP, pceNum];
                    //ASSERT(SqOnBoard(sq));

                    if (Pieces[sq + 10] == Piece.Empty)
                    {
                        AddWhitePawnMove(sq, sq + 10, ref moves);
                        if (Util.SquareToRank[sq] == Rank.r2 && Pieces[sq + 20] == Piece.Empty)
                        {
                            AddQuietMove(MoveToCode(sq, (sq + 20), Piece.Empty, Piece.Empty, Move.PawnStartFlag), ref moves);
                        }
                    }
                    if (Pieces[sq + 9] != Position.OffBoard && Util.PieceCol[Pieces[sq + 9]] == Color.Black)
                    {
                        AddWhitePawnCaptureMove(sq, sq + 9, Pieces[sq + 9], ref moves);
                    }
                    if (Pieces[sq + 11] != Position.OffBoard && Util.PieceCol[Pieces[sq + 11]] == Color.Black)
                    {
                        AddWhitePawnCaptureMove(sq, sq + 11, Pieces[sq + 11], ref moves);
                    }
                    if (sq + 9 == EnPassant)
                    {
                        AddCaptureMove(MoveToCode(sq, sq + 9, Piece.Empty, Piece.Empty, Move.EnPassantFlag), ref moves);
                    }
                    if (sq + 11 == EnPassant)
                    {
                        AddCaptureMove(MoveToCode(sq, sq + 11, Piece.Empty, Piece.Empty, Move.EnPassantFlag), ref moves);
                    }

                }
            }
            else
            {
                for (int pceNum = 0; pceNum < NumberOfPieces[Piece.bP]; ++pceNum)
                {
                    int sq = PieceList[Piece.bP, pceNum];
                    //ASSERT(SqOnBoard(sq));

                    if (Pieces[sq - 10] == Piece.Empty)
                    {
                        AddBlackPawnMove(sq, sq - 10, ref moves);
                        if (Util.SquareToRank[sq] == Rank.r7 && Pieces[sq - 20] == Piece.Empty)
                        {
                            AddQuietMove(MoveToCode(sq, (sq - 20), Piece.Empty, Piece.Empty, Move.PawnStartFlag), ref moves);
                        }
                    }
                    if (Pieces[sq - 9] != Position.OffBoard && Util.PieceCol[Pieces[sq - 9]] == Color.White)
                    {
                        AddBlackPawnCaptureMove(sq, sq - 9, Pieces[sq - 9], ref moves);
                    }
                    if (Pieces[sq - 11] != Position.OffBoard && Util.PieceCol[Pieces[sq - 11]] == Color.White)
                    {
                        AddBlackPawnCaptureMove(sq, sq - 11, Pieces[sq - 11], ref moves);
                    }
                    if (sq - 9 == EnPassant)
                    {
                        AddCaptureMove(MoveToCode(sq, sq - 9, Piece.Empty, Piece.Empty, Move.EnPassantFlag), ref moves);
                    }
                    if (sq - 11 == EnPassant)
                    {
                        AddCaptureMove(MoveToCode(sq, sq - 11, Piece.Empty, Piece.Empty, Move.EnPassantFlag), ref moves);
                    }

                }
            }
        }

        private void AddWhitePawnCaptureMove(int from, int to, int capture, ref Moves moves)
        {
            if (Util.SquareToRank[from] == Rank.r7)
            {
                AddCaptureMove(MoveToCode(from, to, capture, Piece.wQ, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.wR, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.wB, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.wN, 0), ref moves);
            }
            else
            {
                AddCaptureMove(MoveToCode(from, to, capture, Piece.Empty, 0), ref moves);
            }
        }

        private void AddWhitePawnMove(int from, int to, ref Moves moves)
        {
            if (Util.SquareToRank[from] == Rank.r7)
            {
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.wQ, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.wR, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.wB, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.wN, 0), ref moves);
            }
            else
            {
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.Empty, 0), ref moves);
            }
        }

        private void AddBlackPawnCaptureMove(int from, int to, int capture, ref Moves moves)
        {
            if (Util.SquareToRank[from] == Rank.r2)
            {
                AddCaptureMove(MoveToCode(from, to, capture, Piece.bQ, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.bR, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.bB, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, capture, Piece.bN, 0), ref moves);
            }
            else
            {
                AddCaptureMove(MoveToCode(from, to, capture, Piece.Empty, 0), ref moves);
            }
        }

        private void AddBlackPawnMove(int from, int to, ref Moves moves)
        {
            if (Util.SquareToRank[from] == Rank.r2)
            {
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.bQ, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.bR, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.bB, 0), ref moves);
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.bN, 0), ref moves);
            }
            else
            {
                AddCaptureMove(MoveToCode(from, to, Piece.Empty, Piece.Empty, 0), ref moves);
            }
        }

        private int MoveToCode(int from, int to, int capture, int promotion, int flag)
        {
            return from | (to << 7) | (capture << 14) | (promotion << 20) | flag;
        }
    }
}