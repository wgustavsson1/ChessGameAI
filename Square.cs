using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace ChessGame
{
   public class Square : PictureBox
    {

       //
       //Public properties
       //
       /// <summary>
       /// Square's position in the gameboard
       /// </summary>
       public Point Position { get; set;}

       public Colors Color { get; set; }
       /// <summary>
       /// The piece that the square is occupied by
       /// </summary>
       public IPiece Piece { get; set; }
       /// <summary>
       /// Occupied by piece or not , read only
       /// </summary>
       public bool Empty { get { if (Piece == null){ return true; } else { return false; } } } //The square is empty if there isn't a piece in it

    }
}
