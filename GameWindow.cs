using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace ChessGame
{
    public enum Players {PLAYER,BOT};

    public class GameWindow : Form
    {
        private bool gameOver = true; //False as default before player pressed new game button
        private GameBoard gameBoard;
        private Colors playerColor = Colors.WHITE; //The color of the players pieces, white as default
        private Players playersTurn;
        private Bot bot; 

        #region Create controls
        //Panel pnlGui
        Panel pnlGui;
        //Button btnExit
        Button btnExit;
        //Button btnSwitchColors
        Button btnSwitchColors;
        //Button btnNewGame
        Button btnNewGame;
        //Panel pnlFeedBack
        Label lblFeedBack;
        #endregion
        
        public GameWindow()
        {
            bot = new Bot(); //Create bot
            initGameWindow(); //Print graphics
        }

        private void initGameWindow()
        {

            #region set up controls
            //Panel pnlGui
            pnlGui = new Panel();
            pnlGui.Location = new Point(780, 0);
            pnlGui.Size = new Size(200, 780);
            pnlGui.BorderStyle = BorderStyle.Fixed3D;
            //Button btnExit
            btnExit = new Button();
            btnExit.Image = Image.FromFile(@"pics\exit.png");
            btnExit.Size = new Size(50, 50);
            btnExit.Location = new Point(145, 5);
            btnExit.BackColor = Color.White;
            btnExit.Click += btnExit_Click;
            //btnSwitchColors
            btnSwitchColors = new Button();
            btnSwitchColors.BackColor = Color.Brown;
            btnSwitchColors.Location = new Point(20, 500);
            btnSwitchColors.Size = new Size(160, 60);
            btnSwitchColors.Text = "SWITCH COLORS";
            btnSwitchColors.Font = new Font("Calibri", 14f, FontStyle.Bold, GraphicsUnit.Point);
            btnSwitchColors.Click += btnSwitchColors_Click;
            //btnNewGame
            btnNewGame = new Button();
            btnNewGame.BackColor = Color.Brown;
            btnNewGame.Location = new Point(20, 580);
            btnNewGame.Size = new Size(160, 60);
            btnNewGame.Text = "NEW GAME!";
            btnNewGame.Font = btnSwitchColors.Font;
            btnNewGame.Click += btnNewGame_Click;
            //lblFeedBack
            lblFeedBack = new Label();
            lblFeedBack.BackColor = Color.White;
            lblFeedBack.Location = new Point(20, 380);
            lblFeedBack.Size = new Size(160, 100);
            lblFeedBack.BorderStyle = BorderStyle.Fixed3D;
            lblFeedBack.Font = new Font("Calibri", 15f, FontStyle.Bold, GraphicsUnit.Point);
            lblFeedBack.Text = "Welcome!";
            lblFeedBack.TextAlign = ContentAlignment.MiddleCenter;
            

            #endregion

            #region Set up form
            this.ClientSize = new Size(980,780 ); //Width: 760 gameboard + 20 frame + 200 GUIpanel Height: 760 gameboard + 20 frame
            this.BackColor = Color.Ivory;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CancelButton = btnExit;
            
            #endregion

            #region set up gameboard
            gameBoard = new GameBoard(new Point(10, 10), playerColor);
            foreach(Square s in gameBoard.Squares) { s.Click += checkIfPlayerMoveIsDone; }
            gameBoard.PlayerInteractionEnabled = false; //Disable gameboard until btnNewgame is pressed

            #endregion

            #region Add controls
            //Add controls to form
            this.SuspendLayout();
            this.Controls.Add(gameBoard);
            this.Controls.Add(pnlGui);

            //Add controls to GUI panel
            pnlGui.Controls.Add(btnExit);
            pnlGui.Controls.Add(btnSwitchColors);
            pnlGui.Controls.Add(btnNewGame);
            pnlGui.Controls.Add(lblFeedBack);
            pnlGui.Refresh();
            this.ResumeLayout();
            this.Refresh();
            #endregion
        }

       private void checkIfPlayerMoveIsDone(object sender, EventArgs e)
        {
            if (gameBoard.PlayerInteractionEnabled == false) //If the player move is donw
            {
                playersTurn = Players.BOT;
                playMove();
            }
        }

       private void btnNewGame_Click(object sender, EventArgs e)
        {
            gameOver = false;
            gameBoard.Reset(playerColor);
            if (playerColor == Colors.WHITE) { playersTurn = Players.PLAYER; } else { playersTurn = Players.BOT; } //Decide who begin to play
            UpdateGraphics();
            playMove();
        }

       private void playMove()
        {    
            if(playersTurn == Players.BOT && gameOver == false)
            {
                bot.MakeMove(gameBoard.Squares,gameBoard.PlayerPieceSet,gameBoard.BotPieceSet); //get bot to make a move
                playersTurn = Players.PLAYER; 
            }
            CheckGameStatus();
            UpdateGraphics();
        }


        private void CheckGameStatus()
        {
            //True as default
            bool playerWon = true;
            bool botWon = true;

            lblFeedBack.Text = "Game is live"; //Reset lbl
            lblFeedBack.BackColor = Color.White;

            if(PlayerChecked())
            {
                lblFeedBack.Text = "Check!";
                lblFeedBack.BackColor = Color.Red;
            }
            if(BotChecked())
            {
                lblFeedBack.Text = "Check!";
                lblFeedBack.BackColor = Color.Green;
            }

            //Check if player won
            foreach(IPiece enemyPiece in gameBoard.BotPieceSet)
            {

               if(enemyPiece.PieceType == Pieces.KING)
               {
                   playerWon = false;
               }

            }

            //Check if BOT won
            foreach (IPiece playerPiece in gameBoard.PlayerPieceSet)
            {

                if (playerPiece.PieceType == Pieces.KING)
                {
                    botWon = false;
                }

            }

            if(playerWon)
            {
                lblFeedBack.Text = "Victory!";
                lblFeedBack.BackColor = Color.Green;
                gameOver = true;
            }
            else if(botWon)
            {
                lblFeedBack.Text = "You lost!";
                lblFeedBack.BackColor = Color.Red;
                gameOver = true;
            }

        }

        private bool PlayerChecked()
        {
            foreach(IPiece enemyPiece in gameBoard.BotPieceSet)
            {
                
                foreach(Move enemyMove in enemyPiece.PossibleMoves(gameBoard.Squares, gameBoard.BotPieceSet, gameBoard.PlayerPieceSet))
                {

                    if(enemyMove.CapturedPiece != null && enemyMove.CapturedPiece.PieceType == Pieces.KING) //If the move would kill the king
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private bool BotChecked()
        {
            foreach (IPiece playerPiece in gameBoard.PlayerPieceSet)
            {

                foreach (Move playerMove in playerPiece.PossibleMoves(gameBoard.Squares, gameBoard.PlayerPieceSet, gameBoard.BotPieceSet))
                {

                    if (playerMove.CapturedPiece != null && playerMove.CapturedPiece.PieceType == Pieces.KING) //If the move would kill the king
                    {
                        return true;
                    }
                }

            }

            return false;
        }


        private void UpdateGraphics()
        {
            if (gameOver == false) btnSwitchColors.Enabled = false; else btnSwitchColors.Enabled = true; //Disable switchcolor-button while game is live
            if (playersTurn == Players.PLAYER) { gameBoard.PlayerInteractionEnabled = true; } //Enable gameboard for player if it's players turn
        }
        /// <summary>
        /// Only available before game is started
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitchColors_Click(object sender, EventArgs e)
        {
           //Change playercolor
           if(playerColor == Colors.WHITE)
           {
               playerColor = Colors.BLACK;
           }
           else
           {
               playerColor = Colors.WHITE;
           }
            //Decide if bot or player begin
           if (playerColor == Colors.WHITE)
           {
               playersTurn = Players.PLAYER;
           }
           else
           {
               playersTurn = Players.BOT;
           }
           gameBoard.Reset(playerColor); //Setup from scratch
        }

        //***************************************
        //Private methodes
        //***************************************

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
