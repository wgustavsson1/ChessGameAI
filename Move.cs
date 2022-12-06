using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ChessGame
{
   public class Move
    {
       private Square fromSquare;
       private Square toSquare;
       private IPiece movedPiece;
       private IPiece capturedPiece;
        
       public Move(Square fromSquare , Square toSquare , IPiece movedPiece , IPiece capturedPiece)
       {
           this.fromSquare = fromSquare;
           this.toSquare = toSquare;
           this.movedPiece = movedPiece;
           this.capturedPiece = capturedPiece;
       }

       

       //****************************************
       //Public properties
       //****************************************

       /// <summary>
       /// Moved from
       /// </summary>
       public Square FromSquare { get { return fromSquare; } }
       /// <summary>
       /// Moved to
       /// </summary>
       public Square ToSquare { get { return toSquare; } }


       public IPiece MovedPiece { get { return movedPiece; } }


       /// <summary>
       /// eliminated piece
       /// </summary>
       public IPiece CapturedPiece { get { return capturedPiece; } }
    }
}
