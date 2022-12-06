﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
   public class Rook : Piece , IPiece
    {

       /// <summary>
       /// Rook class constructor
       /// </summary>
       /// <param name="color">color of the piece</param>
       public Rook(Colors color) :base(Pieces.ROOK,color)
       {

       }

       //******************************************
       //Priavate Methods
       //******************************************
       private List<Move> calcualteVerticalMoves(Square[,] squares)
       {
           List<Move> moves = new List<Move>();

           //max and min steps will be determined by whether pieces are blocking the moves
           int maxNumberOfYSteps = 7;
           int minNumberOfYSteps = 1;
           Move m; //New move to be added to list


           //Check for first piece occurance in upward moving direction
           for (int i = 1; i <= 7; i++) //Maximal steps for a rook move = 7
           {
               if (WithinBounds(MySquare, 0, -i)) //If square is within gameboard bounds
               {
                   Square toSquare = squares[MySquare.Position.X, (MySquare.Position.Y - i)];
                   if (toSquare.Piece != null) //If the square contains a blocking piece
                   {
                       maxNumberOfYSteps = i; //Calculate maximum moving direction
                       break;
                   }
                   
               }
           }

           //Create possible moves in upward direction
           for (int i = minNumberOfYSteps; i <= maxNumberOfYSteps; i++)
           {
               if (WithinBounds(MySquare, 0, -i))
               {
                   Square toSquare = squares[MySquare.Position.X, (MySquare.Position.Y - i)];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }


           //Check for first piece occurance in downward moving direction
           for (int i = 1; i <= 7; i++) //Maximal steps for a rook move = 7
           {
               maxNumberOfYSteps = 7; //Set default max moves

               if (WithinBounds(MySquare, 0, i)) //If square is within gameboard bounds
               {
                   Square toSquare = squares[MySquare.Position.X, (MySquare.Position.Y + i)];
                   if (toSquare.Piece != null) //If the square contains a blocking piece
                   {
                       maxNumberOfYSteps = i; //Calculate maximum steps to move
                       break;
                   }

               }

           }


           //create possible moves in downward direction
           for (int i = minNumberOfYSteps; i <= maxNumberOfYSteps; i++)
           {
               if (WithinBounds(MySquare, 0, i))
               {
                   Square toSquare = squares[MySquare.Position.X, (MySquare.Position.Y + i)];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }

           return moves;
       }

       private List<Move> calcualteHorizontalMoves(Square[,] squares)
       {
           List<Move> moves = new List<Move>();

           //max and min steps will be determined by whether pieces are blocking the moves
           int maxNumberOfXSteps = 7;
           int minNumberOfXSteps = 1;
           Move m; //New move to be added to list

           //
           //Check for first piece occurance in left moving direction
           for (int i = 1; i <= 7; i++) //Maximal steps for a rook move = 7
           {
               if (WithinBounds(MySquare, -i , 0)) //If square is within gameboard bounds
               {
                   Square toSquare = squares[MySquare.Position.X -i , MySquare.Position.Y];
                   if (toSquare.Piece != null) //If the square contains a blocking piece
                   {
                       maxNumberOfXSteps = i; //Calculate maximum moving direction
                       break;
                   }

               }
           }

           //Create possible moves in left direction
           for (int i = minNumberOfXSteps; i <= maxNumberOfXSteps; i++)
           {
               if (WithinBounds(MySquare, -i, 0))
               {
                   Square toSquare = squares[MySquare.Position.X -i, MySquare.Position.Y];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }


           //Check for first piece occurance in right moving direction
           for (int i = 1; i <= 7; i++) //Maximal steps for a rook move = 7
           {
               maxNumberOfXSteps = 7; //Set default max moves

               if (WithinBounds(MySquare, i,0)) //If square is within gameboard bounds
               {
                   Square toSquare = squares[MySquare.Position.X + i, MySquare.Position.Y];
                   if (toSquare.Piece != null) //If the square contains a blocking piece
                   {
                       maxNumberOfXSteps = i; //Calculate maximum steps to move
                       break;
                   }

               }

           }


           //create possible moves in right direction
           for (int i = minNumberOfXSteps; i <= maxNumberOfXSteps; i++)
           {
               if (WithinBounds(MySquare, i, 0))
               {
                   Square toSquare = squares[MySquare.Position.X + i, MySquare.Position.Y];
                   m = new Move(MySquare, toSquare, this, toSquare.Piece);
                   moves.Add(m);
               }
           }

           return moves;
       }

       //*******************************************
       //IPiece implementation
       //*******************************************

       public List<Move> PossibleMoves(Square[,] squares , List<IPiece> playerPieceSet , List<IPiece> enemyPieceSet)
       {
           List<Move> allMoves = new List<Move>();

           allMoves.AddRange(calcualteHorizontalMoves(squares)); //Add horizontal moves to allmoves
           allMoves.AddRange(calcualteVerticalMoves(squares));  //Add vertical moves to allmoves
           
           return removeInvalidMoves(allMoves, playerPieceSet); //Return corrected list
       }

       public void Move(Move move) //Called from IPiece reference
       {
           MovePiece(move); //Call base class move method
       }
       public Square MySquare { get; set; }
       public float PieceValue { get { return 5; } }
       public Pieces PieceType { get { return Pieces.ROOK; } }

       
    }
}
