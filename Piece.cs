using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessGame
{
  public enum Pieces {PAWN,KNIGHT,BISHOP,ROOK,QUEEN,KING}
  public enum Colors {WHITE,BLACK}
  public  enum MovingDirections {UP,DOWN}



   public class Piece
    {
    

       private Image image;

       /// <summary>
       /// Piece class contructor
       /// </summary>
       /// <param name="color">color of piece</param>
       /// <param name="piece">Type of piece e.g pawn,queen,king</param>
       public Piece(Pieces piece , Colors color)
       {
           //Set correct pieceimage
           image = Image.FromFile(@"pics\chess_piece_" + color + "_" + piece + ".png" );
       }

       //*******************************************
       //Protected methodes
       //*******************************************

       /// <summary>
       /// Check if a move is within the gameboard bounds
       /// </summary>
       /// <param name="startSquare">move from</param>
       /// <param name="xSteps">steps moved in x-axis</param>
       /// <param name="ySteps">steps moved in y-axis</param>
       /// <returns></returns>
       protected bool WithinBounds(Square startSquare, int xSteps, int ySteps)
       {
           if ((startSquare.Position.X + xSteps) <= 7 && (startSquare.Position.X + xSteps) >= 0 && (startSquare.Position.Y + ySteps) <= 7 && (startSquare.Position.Y + ySteps) >= 0)
           {
               return true;
           }
           else return false;
       }

       /// <summary>
       /// Returns a corrected list of moves
       /// </summary>
       /// <returns></returns>
       protected List<Move> removeInvalidMoves(List<Move> moves,List<IPiece> playerPieceSet)
       {
           List<Move> invalidMoves = new List<Move>();

           foreach(Move m in moves)
           {
               if(playerPieceSet.Contains(m.ToSquare.Piece)) //If the destination square is occupied by one of the player's own piece
               {
                   invalidMoves.Add(m); //Remove that move
               }
           }
           foreach(Move m in invalidMoves)
           {
               moves.Remove(m);
           }

           return moves; //Return list of valid moves
       }

       /// <summary>
       /// Move the piece along the gameboard
       /// </summary>
       /// <param name="m">Move to be made</param>
       protected void MovePiece(Move m)
       {
           m.ToSquare.Image = m.FromSquare.Image; //Move image to new square
           m.FromSquare.Image = null; //Remove piece image from old square
           m.ToSquare.Piece = m.MovedPiece; //Add piece to new square
           m.MovedPiece.MySquare = m.ToSquare; //Add new square to Piece
           m.FromSquare.Piece = null; //Remove pice reference from old square
      
       }

       //*******************************************
       //Public properties
       //*******************************************
       public Colors Color { get; set; }

       /// <summary>
       /// chesspiece image, read only
       /// </summary>
       public Image Image { get { return image; } }

    }
}
