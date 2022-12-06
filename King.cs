using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
  public class King : Piece , IPiece
    {

      /// <summary>
      /// King class constructor
      /// </summary>
      /// <param name="color">the color of the piece</param>
      public King(Colors color) : base(Pieces.KING, color)
      {

      }


        //*******************************************
        //IPiece implementation
        //*******************************************

      public List<Move> PossibleMoves(Square[,] squares, List<IPiece> playerPieceSet , List<IPiece> enemyPieceSet)
      {
          List<Move> moves = new List<Move>();
          Move m; //new move

          //Forward move
          if(WithinBounds(MySquare,0,-1))
          {
              Square toSquare = squares[MySquare.Position.X, MySquare.Position.Y - 1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Backward move
          if (WithinBounds(MySquare, 0, +1))
          {
              Square toSquare = squares[MySquare.Position.X, MySquare.Position.Y + 1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Left move   
          if(WithinBounds(MySquare, -1, 0))
          {
              Square toSquare = squares[MySquare.Position.X -1, MySquare.Position.Y];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Right move   
          if (WithinBounds(MySquare, +1, 0))
          {
              Square toSquare = squares[MySquare.Position.X + 1, MySquare.Position.Y];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
             
          //////////////////////Diagnonal moves ///////////////////////////////////////

          //Forward-left
          if (WithinBounds(MySquare, -1, -1))
          {
              Square toSquare = squares[MySquare.Position.X - 1, MySquare.Position.Y -1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Forward-right
          if (WithinBounds(MySquare, 1, -1))
          {
              Square toSquare = squares[MySquare.Position.X + 1, MySquare.Position.Y - 1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Backward-left
          if (WithinBounds(MySquare, -1, 1))
          {
              Square toSquare = squares[MySquare.Position.X - 1, MySquare.Position.Y + 1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }
          //Backward-right
          if (WithinBounds(MySquare, 1, 1))
          {
              Square toSquare = squares[MySquare.Position.X + 1, MySquare.Position.Y +1];
              m = new Move(MySquare, toSquare, this, toSquare.Piece);
              moves.Add(m);
          }

          return removeInvalidMoves(moves,playerPieceSet);
      }

      public void Move(Move move) //Called from IPiece reference
      {
          MovePiece(move); //Call base class move method
      }
        public Square MySquare { get; set; }
        public float PieceValue { get { return 1000; } } //Infinite value
        public Pieces PieceType { get { return Pieces.KING; } }
    }
}
