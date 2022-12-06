using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum MovingDirections {UP,DOWN}; // UP or DOWN along with gameboard
namespace ChessGame
{
   public class Pawn : Piece ,IPiece
    {
       public MovingDirections MovingDirection { get; set; }
       private bool firstMove = true;
       public bool FirstMove { get { return firstMove; } set { firstMove = value; } }

       /// <summary>
       /// Pawn class constructor
       /// </summary>
       /// <param name="color">Piececolor black ,white</param>
       /// <param name="movingDirection">up or down</param>
       public Pawn(Colors color, MovingDirections movingDirection) :base(Pieces.PAWN,color)
       {                    
           this.MovingDirection = movingDirection;
       }

       //**************************************************
       // Private methodes
       //**************************************************

       private List<Move> calculatePossibleMoves(Square[,] squares, List<IPiece> enemyPieceSet)
       {
           List<Move> moves = new List<Move>(); //Create list of moves to fill
           Move m;

           int direction;

           if (MovingDirection == MovingDirections.UP) direction = -1; else direction = 1;  // negative or positive movingdirection

               //1 step forward
               if(WithinBounds(MySquare,0,1*direction))
               {
                   Square toSquare = squares[MySquare.Position.X, MySquare.Position.Y + (1 * direction)];
                   if(toSquare.Empty) //If the square is empty
                   {
                       m = new Move(MySquare, toSquare, this, null);
                       moves.Add(m);
                   }
               }

              if (firstMove)
              {
               //2 steps forward
               if (WithinBounds(MySquare, 0, 2 * direction))
               {
                   Square toSquare = squares[MySquare.Position.X, MySquare.Position.Y + (2 * direction)];
                   if (toSquare.Empty) //If the square is empty
                   {
                       m = new Move(MySquare, toSquare, this, null);
                       moves.Add(m);
                   }
               }
           }
           

           return moves;
       }

       private List<Move> calculatePossibleAttackMoves (Square[,] squares, List<IPiece> enemyPieceSet)
       {
           List<Move> moves = new List<Move>();
           int ySteps;
           if (MovingDirection == MovingDirections.UP) ySteps = -1; else ySteps = 1;  // decide if piece attacks upwards or downwards

           //First possible attack
           if(WithinBounds(MySquare,-1,ySteps)) //If move is within gameboard bounds
           {
               Move m;
               Square toSquare = squares[(MySquare.Position.X - 1), (MySquare.Position.Y + ySteps)];
               if (enemyPieceSet.Contains(toSquare.Piece)) //If the square contains an enemy
               {
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }

           //Seconds possible attack
           if (WithinBounds(MySquare, +1, ySteps)) //If move is within gameboard bounds
           {
               Move m;
               Square toSquare = squares[(MySquare.Position.X + 1), (MySquare.Position.Y + ySteps)];
               if (enemyPieceSet.Contains(toSquare.Piece)) //If the square contains an enemy
               {
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }

           return moves;
       }

        //**************************************************
        // IPiece implementation
        //**************************************************

       /// <summary>
       /// returns pawn's possible moves, both attacks and moves
       /// </summary>
       /// <returns></returns>
       public List<Move> PossibleMoves(Square[,] squares, List<IPiece> playerPieceSet, List<IPiece> enemyPieceSet)
       {
           List<Move> calculatedMoves = calculatePossibleMoves(squares, enemyPieceSet);
           List<Move> calculatedAttackMoves = calculatePossibleAttackMoves(squares,enemyPieceSet);
           List<Move> allMoves = new List<Move>(); //All possible moves

           allMoves.AddRange(calculatedMoves); //add to list
           allMoves.AddRange(calculatedAttackMoves); // add to list
            
           return allMoves;
       }
       public List<Move> PossibleAttackMoves(Square[,] squares, List<IPiece> playerPieceSet, List<IPiece> enemyPieceSet)
       {
           return calculatePossibleAttackMoves(squares,enemyPieceSet);
       }

       public void Move(Move move) //Called from IPiece reference
       {
           MovePiece(move); //Call base class move method
           firstMove = false; //First move is done
       }

       /// <summary>
       /// square which piece is located in
       /// </summary>
      public Square MySquare { get; set; }


      /// <summary>
      /// How important the piece is
      /// </summary>
      public float PieceValue { get { return 1; } }

      public Pieces PieceType { get { return Pieces.PAWN; } }
    }
}
