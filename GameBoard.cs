using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
   public class GameBoard : Panel
   {
       private Colors playerColor; //Used by initGameBoard() to determine color of the pieces at the players side of board
       private Square[,] squares = new Square[8,8];
       private List<IPiece> playerPieceSet;
       private List<IPiece> botPieceSet;
       private IPiece selectedPiece; // Piece pressed by player
       private int numberOfSelectedPieces = 0;

       public GameBoard(Point location , Colors playerColor)
       {
           this.Location = location;
           this.playerColor = playerColor;
           initGameBoard();
       }


       //***************************************************
       //Private methods
       //***************************************************

       /// <summary>
       /// Setup gameboard pieces
       /// </summary>
       private void initGameBoard()
       {
           //gameboard:  8*90px + 40px frame
           this.Size = new Size(760,760);
           //Woodcolor behind squares
           this.BackColor = Color.FromArgb(79, 36, 18);
           this.BorderStyle = BorderStyle.Fixed3D;
           this.Cursor = Cursors.Hand;
           setUpSquares();
           addSquaresToBoard();
           setUpPlayerPieceSet();
           setUpBotPieceSet();
       }
       /// <summary>
       /// Setup squares properties e.g color,location,size
       /// </summary>
       private void setUpSquares()
       {
           for(int x = 0; x <= 7; x++)
           {
               for(int y = 0; y<=7; y++)
               {
                   squares[x,y] = new Square();
                   squares[x,y].Size = new Size(90,90);
                   squares[x, y].Position = new Point(x, y); //Position on gameboard
                   squares[x, y].Location = new Point((20 + x*90) , (20 + y*90)); // +20 for wood border
                   squares[x, y].Click += square_click;
                   squares[x, y].SizeMode = PictureBoxSizeMode.CenterImage;

                   //Decide color of square
                   if(y % 2 == 0)
                   {
                       if (x % 2 == 0) { squares[x, y].BackColor = Color.FloralWhite; squares[x, y].Color = Colors.WHITE; }
                       else { squares[x, y].BackColor = Color.DimGray; squares[x, y].Color = Colors.BLACK; }
                   }
                   else
                   {
                       if (x % 2 == 0) { squares[x, y].BackColor = Color.DimGray; squares[x, y].Color = Colors.BLACK; }
                       else { squares[x, y].BackColor = Color.FloralWhite; squares[x, y].Color = Colors.WHITE; }
                   }
               }
           }
       }

       private void addSquaresToBoard()
       {
          if(squares != null)
          {
              this.SuspendLayout(); // Suspend panel(gameboard)

              foreach(Square square in squares)
              {
                  this.Controls.Add(square); // Add square to gameboard
              }
              this.ResumeLayout();
              this.Refresh();
          }
       }

       private void setUpPlayerPieceSet()
       {
           playerPieceSet = new List<IPiece>();

           //set up 8 pawns
           for(int i = 1; i<=8; i++)
           {
               Pawn p = new Pawn(playerColor , MovingDirections.UP);  //Players pawns move from  bottom to top of board
               p.MySquare = squares[i-1,6]; // MySquare the pawns on seventh row ([6])
               squares[i-1,6].Image = p.Image; // Set squares image to pieceimage
               squares[i - 1, 6].Piece = p;
               playerPieceSet.Add(p);
           }

           //set up 2 rooks
           Rook r1 = new Rook(playerColor);
           Rook r2 = new Rook(playerColor);

           r1.MySquare = squares[0, 7];
           squares[0, 7].Image = r1.Image;
           squares[0, 7].Piece = r1;
           r2.MySquare = squares[7, 7];
           squares[7, 7].Image = r2.Image;
           squares[7, 7].Piece = r2;

           playerPieceSet.Add(r1);
           playerPieceSet.Add(r2);
           
           //set up 2 knights
           Knight k1 = new Knight(playerColor);
           Knight k2 = new Knight(playerColor);

           k1.MySquare = squares[1, 7];
           squares[1, 7].Image = k1.Image;
           squares[1, 7].Piece = k1;
           k2.MySquare = squares[6, 7];
           squares[6, 7].Image = k2.Image;
           squares[6, 7].Piece = k2;

           playerPieceSet.Add(k1);
           PlayerPieceSet.Add(k2);
           
           //set up 2 bishops

           Bishop b1 = new Bishop(playerColor);
           Bishop b2 = new Bishop(playerColor);

           b1.MySquare = Squares[2, 7];
           squares[2, 7].Image = b1.Image;
           squares[2, 7].Piece = b1;
           b2.MySquare = Squares[5, 7];
           squares[5, 7].Image = b2.Image;
           squares[5, 7].Piece = b2;

           playerPieceSet.Add(b1);
           playerPieceSet.Add(b2);

           //set up queen
           Queen q = new Queen(playerColor);
           q.MySquare = Squares[3,7];
           squares[3, 7].Image = q.Image;
           squares[3, 7].Piece = q;

           playerPieceSet.Add(q);

           // set up king
           King k = new King(playerColor);
           k.MySquare = Squares[4, 7];
           Squares[4, 7].Image = k.Image;
           Squares[4, 7].Piece = k;

           playerPieceSet.Add(k);
           

       }

       private void setUpBotPieceSet()
       {
           botPieceSet = new List<IPiece>();
           Colors color = Colors.WHITE; // Color of the BOT's pieces

           if(playerColor == Colors.WHITE)
           {
               color = Colors.BLACK;
           }

           //set up 8 pawns
           for(int i = 1; i<=8; i++)
           {
               Pawn p = new Pawn(color,MovingDirections.DOWN); //BOT's pawns move from top of board to bottom
               p.MySquare = squares[i - 1, 1]; //Place along with second row
               squares[i - 1, 1].Image = p.Image;
               squares[i - 1, 1].Piece = p;
               botPieceSet.Add(p);
           }
           //set up 2 rooks
           Rook r1 = new Rook(color);
           Rook r2 = new Rook(color);

           r1.MySquare = squares[0, 0];
           squares[0, 0].Image = r1.Image;
           squares[0, 0].Piece = r1;
           r2.MySquare = squares[7, 0];
           squares[7, 0].Image = r2.Image;
           squares[7, 0].Piece = r2;

           botPieceSet.Add(r1);
           botPieceSet.Add(r2);

           //set up 2 knights
           Knight k1 = new Knight(color);
           Knight k2 = new Knight(color);

           k1.MySquare = squares[1, 0];
           squares[1, 0].Image = k1.Image;
           squares[1, 0].Piece = k1;
           k2.MySquare = squares[6, 0];
           squares[6, 0].Image = k2.Image;
           squares[6, 0].Piece = k2;

           botPieceSet.Add(k1);
           botPieceSet.Add(k2);

           //set up 2 bishops
           Bishop b1 = new Bishop(color);
           Bishop b2 = new Bishop(color);

           b1.MySquare = squares[2, 0];
           squares[2, 0].Image = b1.Image;
           squares[2, 0].Piece = b1;
           b2.MySquare = squares[5, 0];
           squares[5, 0].Image = b2.Image;
           squares[5, 0].Piece = b2;

           botPieceSet.Add(b1);
           botPieceSet.Add(b2);

           //set up queen
           Queen q = new Queen(color);
           q.MySquare = squares[3,0];
           squares[3, 0].Image = q.Image;
           squares[3, 0].Piece = q;
           botPieceSet.Add(q);

           // set up king
           King k = new King(color);
           k.MySquare = squares[4, 0];
           squares[4, 0].Image = k.Image;
           squares[4, 0].Piece = k;
           botPieceSet.Add(k);

       }

       /// <summary>
       /// Event handler for Square.Click
       /// </summary>
       private void square_click(object sender, EventArgs e)
       {
           if (PlayerInteractionEnabled) // If players turn
           {

               Square pressedSquare = (Square)sender;
               if(pressedSquare.Piece != null)
               MessageBox.Show(pressedSquare.Piece.ToString());

               if (playerPieceSet.Contains(pressedSquare.Piece)) //if the pressed piece belongs to player
               {
                   // TODO: Refresh square colors
                   selectedPiece = pressedSquare.Piece;
                   numberOfSelectedPieces++;

                   List<Move> moves = new List<Move>();
                   if (pressedSquare.Piece != null)
                   {
                       moves = pressedSquare.Piece.PossibleMoves(squares,playerPieceSet,botPieceSet);
                   }

                       foreach (Move m in moves)
                       {
                           
                               m.ToSquare.BackColor = Color.LightGreen; // Draw valid squared green;
                       }

               }
               else if(selectedPiece != null &&  !(playerPieceSet.Contains(pressedSquare.Piece))) //If piece is selected by player and pressed piece isn't the players
               {
                   foreach(Move m in selectedPiece.PossibleMoves(squares,playerPieceSet,botPieceSet))
                   {
                       if(m.ToSquare == pressedSquare) //If the press is a valid move
                       {
                           selectedPiece.Move(m); //Move selected piece
                           if(m.CapturedPiece != null) //If a bot piece got killed
                           {
                               botPieceSet.Remove(m.CapturedPiece);
                           }
                           PlayerInteractionEnabled = false; //Disable player interaction
                           selectedPiece = null; // reset selected piece variable
                           pressedSquare = null; //reset pressedSquare //TODO check if nesessary

                        }
                   }
              }

              
               if(selectedPiece == null || numberOfSelectedPieces >1) // If no piece is selected or too many is selected
               {
                   foreach(Square s in squares)
                   {
                       //reset all squares colors to black or white, green squares get reseted
                       if (s.Color == Colors.BLACK) s.BackColor = Color.DimGray;
                       else s.BackColor = Color.FloralWhite;

                       numberOfSelectedPieces = 0; //Reset
                   }
               }
                
              }

          }



       //***************************************************
       //Public methods
       //***************************************************


       /// <summary>
       /// Setup gameboard to default state
       /// </summary>
       /// <param name="playerColor"></param>
       public void Reset(Colors playerColor)
       {
           this.playerColor = playerColor;
           //Clear all squares
           foreach(Square s in squares)
           {
               s.Image = null;
               s.Piece = null;
               //Reset square colors
               if(s.Color == Colors.BLACK)
               {
                   s.BackColor = Color.DimGray;
               }
               else 
               {
                   s.BackColor = Color.FloralWhite;
               }
           }
           setUpPlayerPieceSet();
           setUpBotPieceSet();
       }

       //***************************************************
       //Public properties
       //***************************************************


       /// <summary>
       /// Read only ,returns 2d array of gameboard squares
       /// </summary>
       public Square[,] Squares {get { return squares; }}

       /// <summary>
       /// List of gameboards black pieces
       /// </summary>
       public List<IPiece> PlayerPieceSet { get { return playerPieceSet; } set { playerPieceSet = value; } }

       /// <summary>
       /// List of gameboards white pieces
       /// </summary>
       public List<IPiece> BotPieceSet { get{return botPieceSet;} set {botPieceSet = value;}}

       public bool PlayerInteractionEnabled { get; set; }

    }
}
