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
            while (code[stream] != ' ')
            {
                char c = code[stream];
                switch (c)
                {
                    case '/':
                        rank--;
                        file = File.A;
                        break;
                    case 'r':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bR;
                        file++;
                        break;
                    case 'n':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bN;
                        file++;
                        break;
                    case 'b':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bB;
                        file++;
                        break;
                    case 'q':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bQ;
                        file++;
                        break;
                    case 'k':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bK;
                        file++;
                        break;
                    case 'p':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.bP;
                        file++;
                        break;
                    case 'R':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wR;
                        file++;
                        break;
                    case 'N':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wN;
                        file++;
                        break;
                    case 'B':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wB;
                        file++;
                        break;
                    case 'Q':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wQ;
                        file++;
                        break;
                    case 'K':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wK;
                        file++;
                        break;
                    case 'P':
                        board.Pieces[Converter.FileRankToSquare(file, rank)] = Piece.wP;
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