using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
   public class Knight : Piece , IPiece
    {

       /// <summary>
       /// Knight class constructor
       /// </summary>
       /// <param name="color">color of the piece</param>
       public Knight(Colors color) : base(Pieces.KNIGHT, color)
       {

       }


        //*******************************************
        //IPiece implementation
        //*******************************************

       public List<Move> PossibleMoves(Square[,] squares, List<IPiece> playerPieceSet ,List<IPiece> enemyPieceSet)
       {
           List<Move> moves = new List<Move>();

           //2 steps forward and 1 step left
           if (WithinBounds(MySquare, -1, -2))
           {
               Square toSquare = squares[MySquare.Position.X -1, MySquare.Position.Y -2];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //2 steps forward and 1 step right
           if (WithinBounds(MySquare,1,-2))
           {
               Square toSquare = squares[MySquare.Position.X + 1, MySquare.Position.Y - 2];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //1 step forward and 2 step left
           if (WithinBounds(MySquare, -2, -1))
           {
               Square toSquare = squares[MySquare.Position.X -2, MySquare.Position.Y -1];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //1 step forward and 2 step right
           if (WithinBounds(MySquare, 2, -1))
           {
               Square toSquare = squares[MySquare.Position.X + 2, MySquare.Position.Y - 1];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }

           //2 steps backward and 1 step left
           if (WithinBounds(MySquare, -1, +2))
           {
               Square toSquare = squares[MySquare.Position.X -1, MySquare.Position.Y +2];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //2 steps backward and 1 step right
           if (WithinBounds(MySquare, +1, +2))
           {
               Square toSquare = squares[MySquare.Position.X +1, MySquare.Position.Y + 2];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //1 step backward and 2 steps left
           if (WithinBounds(MySquare, -2, +1))
           {
               Square toSquare = squares[MySquare.Position.X - 2, MySquare.Position.Y +1];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           //1 step backward and 2 steps right
           if (WithinBounds(MySquare, +2, +1))
           {
               Square toSquare = squares[MySquare.Position.X + 2, MySquare.Position.Y + 1];
               Move m = new Move(MySquare, toSquare, this, toSquare.Piece);
               moves.Add(m);
           }
           return removeInvalidMoves(moves, playerPieceSet);
       }

       public void Move(Move move) //Called from IPiece reference
       {
           MovePiece(move); //Call base class move method
       }

       public Square MySquare { get; set; }
       public float PieceValue { get { return 3; } }
       public Pieces PieceType { get { return Pieces.KNIGHT; } }
    }

}
