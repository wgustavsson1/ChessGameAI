using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace ChessGame
{
   public class Bot
    {
       private IPiece toSquarePiece; //Used by moveBack() to save the original Piece at a square while reseting a move
       private List<IPiece> playerPieceSet;
       private List<IPiece> botPieceSet;
       private Square[,] squares;
 
       public void MakeMove(Square[,] squares, List<IPiece> playerPieceSet, List<IPiece> botPieceSet)
       {

        this.playerPieceSet = playerPieceSet; //Set field
        this.botPieceSet = botPieceSet; //Set field
        this.squares = squares; //Set field

        Move bestMove = CalculateBestMove();

        bestMove.MovedPiece.Move(bestMove);  //Execute best move

        if(bestMove.CapturedPiece != null)
        {
            playerPieceSet.Remove(bestMove.CapturedPiece);
        }
           
       }
       /// <summary>
       /// AI to calculate the best move
       /// </summary>
       private Move CalculateBestMove()
       {
           Move bestMove;
           double highestMoveScore = 0;

           List<Move> allPossibleMoves = allMyPossibleMoves();
           bestMove = allPossibleMoves[0]; //Assign default move to best move
           highestMoveScore = RateMove(bestMove); //Assign default value


           foreach (Move m in allPossibleMoves)
           {
               testMove(m);

               bool prohibitedMove = false;

               foreach (IPiece p in playerPieceSet)
               {
                   foreach (Move enemyMove in p.PossibleMoves(squares, playerPieceSet, botPieceSet))
                   {
                       if (enemyMove.CapturedPiece != null && enemyMove.CapturedPiece.PieceType == Pieces.KING)
                       {
                           prohibitedMove = true;
                       }
                   }
               }
               moveBack(m);


               if (RateMove(m) > highestMoveScore && !prohibitedMove)
               {
                   highestMoveScore = RateMove(m);
                   bestMove = m;
               }
           }

           return bestMove;
       }

       private List<Move> allMyPossibleMoves()
       {
           List<Move> allPossibleMoves = new List<Move>();

           //Add all possible moves to list
           foreach (IPiece p in botPieceSet)
           {
               allPossibleMoves.AddRange(p.PossibleMoves(squares,botPieceSet,playerPieceSet)); //this = player player = enemy
           }

           return allPossibleMoves;
       }
            
       private double RateMove(Move move)
       {
           return (threat(move) - risk(move)) + pieceValueProfit(move)  + reachabillity(move); 
       }

       private double risk(Move move)
       {
           testMove(move);
           double riskedPieceValue = 0; //how much piecevalue the bot would risk if it executed the move
           List<IPiece> alreadyCheckedPieces = new List<IPiece>();
           foreach(IPiece p in playerPieceSet)
           {
               foreach(Move m in p.PossibleMoves(squares,playerPieceSet,botPieceSet))
               {
                   if(m.CapturedPiece != null && !alreadyCheckedPieces.Contains(m.CapturedPiece))// If the move killed a piece
                   {
                       riskedPieceValue += m.CapturedPiece.PieceValue;
                   }
               }
           }
           moveBack(move);
           return riskedPieceValue;
       }

       private double threat(Move move)
       {
           List<IPiece> alreadyCheckedPieces = new List<IPiece>(); //Avoid counting threat against same piece twice
           testMove(move);

           double threatedPieceValue = 0; //How much PieceValue the bot would threat if it executed the move

           foreach(Move m in allMyPossibleMoves())
           {
              if(m.CapturedPiece != null && !alreadyCheckedPieces.Contains(m.CapturedPiece)) //If the move would kill a piece
              {
                  threatedPieceValue += m.CapturedPiece.PieceValue; // Add threat
                  alreadyCheckedPieces.Add(m.CapturedPiece);

                  foreach (IPiece p in playerPieceSet)
                  {
                      foreach (Move playerMove in p.PossibleMoves(squares,playerPieceSet,botPieceSet))
                      {
                          if(playerMove.CapturedPiece == m.MovedPiece) //If the player kill the piece
                          {
                              threatedPieceValue -= m.CapturedPiece.PieceValue; //Downgrade move
                          }

                      }
                  }

                 
              }
           }
           moveBack(move);

           return threatedPieceValue * 0.5; // * 0.5 to nerf threat in realation to attacks
       }

       private double pieceValueProfit(Move move)
       {
           if(move.CapturedPiece != null)
           {
               return move.CapturedPiece.PieceValue;
           }
           else
           {
               return 0;
           }
       }
       /// <summary>
       /// Test how much gameboard control a move leads to
       /// </summary>
       /// <param name="move"></param>
       /// <returns></returns>
       private double reachabillity(Move move)
       {
           testMove(move); //Simulate the move to test it
           int numberOfPossibleMoves = 0;
           foreach(Move m in allMyPossibleMoves())
           {
               
               numberOfPossibleMoves++;
           }

           moveBack(move);
           return numberOfPossibleMoves * 0.05;
       }

       private void testMove(Move move)
       {
           //Test a move without making it visible
           toSquarePiece = move.ToSquare.Piece; //Save piece to use later by moveBack()
           move.ToSquare.Piece = move.MovedPiece; //Add piece to new square
           move.FromSquare.Piece = null;
           move.MovedPiece.MySquare = move.ToSquare; //Add new square to Piece 

       }
       private void moveBack(Move move)
       {
           //Undo testMove

           move.FromSquare.Piece = move.MovedPiece;
           move.ToSquare.Piece = toSquarePiece;
           move.MovedPiece.MySquare = move.FromSquare; 
       }

    }
}
