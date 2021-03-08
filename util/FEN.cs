namespace Chesster
{
    public static class FEN
    {
        //Black is lower letter
        //FORMAT {PIECES} {SIDE} {CASTLE} {EN PASSANT} {50 TURN RULE} {MOVES (increment after black)}
        public const string StandardFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        public static void CodeToBoard(string code, Board board)
        {
            //For board
            int file = File.A;
            int rank = Rank.r8;
            int stream = 0;
            int sq;
            while (code[stream] != ' ')
            {
                char c = code[stream];
                sq = Converter.FileRankToSquare(file, rank);
                switch (c)
                {
                    case '/':
                        rank--;
                        file = File.A;
                        break;
                    case 'r':
                        board.Pieces[sq] = Piece.bR;
                        file++;
                        break;
                    case 'n':
                        board.Pieces[sq] = Piece.bN;
                        file++;
                        break;
                    case 'b':
                        board.Pieces[sq] = Piece.bB;
                        file++;
                        break;
                    case 'q':
                        board.Pieces[sq] = Piece.bQ;
                        file++;
                        break;
                    case 'k':
                        board.Pieces[sq] = Piece.bK;
                        file++;
                        break;
                    case 'p':
                        board.Pieces[sq] = Piece.bP;
                        ulong bp = 1UL << Converter.From120To64[sq];
                        board.Pawns[Color.Black] |= bp;
                        board.Pawns[Color.Both] |= bp;
                        file++;
                        break;
                    case 'R':
                        board.Pieces[sq] = Piece.wR;
                        file++;
                        break;
                    case 'N':
                        board.Pieces[sq] = Piece.wN;
                        file++;
                        break;
                    case 'B':
                        board.Pieces[sq] = Piece.wB;
                        file++;
                        break;
                    case 'Q':
                        board.Pieces[sq] = Piece.wQ;
                        file++;
                        break;
                    case 'K':
                        board.Pieces[sq] = Piece.wK;
                        file++;
                        break;
                    case 'P':
                        board.Pieces[sq] = Piece.wP;
                        ulong wp = 1UL << Converter.From120To64[sq];
                        board.Pawns[Color.White] |= wp;
                        board.Pawns[Color.Both] |= wp;
                        file++;
                        break;
                    default:
                        file += c - '0';
                        break;
                }
                stream++;
            }
            stream++;
            board.Side = (code[stream] == 'w') ? Color.White : Color.Black;
            stream++;
            while (code[stream] != ' ')
            {
                switch (code[stream])
                {
                    case 'K':
                        board.CastlePermission = Castle.WhiteKing;
                        break;
                    case 'Q':
                        board.CastlePermission = Castle.WhiteQueen;
                        break;
                    case 'k':
                        board.CastlePermission = Castle.BlackKing;
                        break;
                    case 'q':
                        board.CastlePermission = Castle.BlackQueen;
                        break;
                    default:
                        break;
                }
                stream++;
            }
            stream++;
            if (code[stream] == '-')
            {
                board.EnPassant = -1;
            }
            else
            {
                string s = "";
                while (code[stream] != ' ')
                {
                    s += code[stream];
                    stream++;
                }
                board.EnPassant = StringToNumber(s);
            }
            stream++;
            string str = "";
            while (code[stream] != ' ')
            {
                str += code[stream];
                stream++;
            }
            board.FiftyMove = StringToNumber(str);
        }

        private static int StringToNumber(string str)
        {
            int num = 0;
            int exp = 1;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                num += exp * (int)(str[i] - '0');
                exp *= 10;
            }
            return num;
        }
    }
}