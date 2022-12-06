using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    public interface IPiece // All piece classes should implement the IPiece interface.
    {
        /// <summary>
        /// Get all the piece's' possible moves
        /// </summary>
        /// <param name="squares"></param>
        /// <param name="playerPieceSet"> ´Bot or players piece set</param>
        /// <param name="enemyPieceSet">The opponents set of pieces eg. bot or playerparam>
        /// <returns></returns>
        List<Move> PossibleMoves(Square[,] squares, List<IPiece> playerPieceSet , List<IPiece> enemyPieceSet);
        Square MySquare { get; set; }
        float PieceValue { get; }
        void Move(Move move);
        Pieces PieceType { get; }
    }
}
