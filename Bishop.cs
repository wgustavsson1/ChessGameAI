using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
   public class Bishop : Piece , IPiece
    {

       /// <summary>
       /// Bishop class contructor
       /// </summary>
       /// <param name="color">the color of the piece</param>
       public Bishop(Colors color) : base(Pieces.BISHOP, color)
       {

       }

        //*******************************************
        //IPiece implementation
        //*******************************************

       public List<Move> PossibleMoves(Square[,] squares, List<IPiece> playerPieceSet, List<IPiece> enemyPieceSet)
       {
           List<Move> moves = new List<Move>();
           Move m; //new move

           //Add moves in up-left direction
           for (int i = 1; i <= 7; i++) //Max steps = 7
           {
               if (WithinBounds(MySquare, -i, -i))
               {
                   Square toSquare = squares[MySquare.Position.X - i, MySquare.Position.Y - i];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
                   if (toSquare.Piece != null) //If a piece is blocking moves
                   {
                       break; //Don't add more moves
                   }
               }
           }

           //Add moves in up-right direction
           for (int i = 1; i <= 7; i++)
           {
               if (WithinBounds(MySquare, i, -i))
               {
                   Square toSquare = squares[MySquare.Position.X + i, MySquare.Position.Y - i];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
                   if (toSquare.Piece != null) //If a piece is blocking moves
                   {
                       break; //Don't add more moves
                   }
               }
           }

           //Add moves in down-left direction
           for (int i = 1; i <= 7; i++)
           {
               if (WithinBounds(MySquare, -i, i))
               {
                   Square toSquare = squares[MySquare.Position.X - i, MySquare.Position.Y + i];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
                   if (toSquare.Piece != null) //If a piece is blocking moves
                   {
                       break; //Don't add more moves
                   }
               }
           }

           //Add moves in down-right direction
           for (int i = 1; i <= 7; i++)
           {
               if (WithinBounds(MySquare, i, i))
               {
                   Square toSquare = squares[MySquare.Position.X + i, MySquare.Position.Y + i];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
                   if (toSquare.Piece != null) //If a piece is blocking moves
                   {
                       break; //Don't add more moves
                   }
               }
           }


           return removeInvalidMoves(moves,playerPieceSet);
       }

       public void Move(Move move) //Called from IPiece reference
       {
           MovePiece(move); //Call base class move method
       }
        public Square MySquare { get; set; }
        public float PieceValue { get { return 3; } }
        public Pieces PieceType { get { return Pieces.BISHOP; } }
    }
}
