using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    public class UserBoard
    {
        private int[,] physBoard;
        private bool[,] userBoard;
        private bool bingo = false;
        public UserBoard(int[,] x)
        {
            //initalize the physical board and userBoard
            physBoard = x;
            userBoard = new bool [5, 5];
            //initalize all values to be false in our userBoard
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    userBoard[j, i] = false;
                }
            }

            userBoard[2, 2] = true; //this is our star or free space it is always true

        }

        //record the value the user entered on our userBoard and check if this newly entered position is a bingo 
        public void record(int [] position)
        {
            //mark the userboard at position row and col to be true 
            int row = position[0];
            int col = position[1];
            userBoard[row, col] = true;

            //check for a bingo
            checkBingo(row, col);
        }

        //check if we have a bingo 
        public void checkBingo(int rows, int cols)
        {
            bool checkRowFalse = false; //check if there is a false value in the row

            //first check the entire row of the value, check every col in rows 
            for(int i = 0; i < 5; i++)
            {
                if (userBoard[rows, i] == false)
                {
                    checkRowFalse = true;
                }
            }
            //if we found no false values in the row we found a bingo 
            if(checkRowFalse == false)
            {
                bingo = true;
            }

            bool checkColFalse = false; //check if there is a false value in the column 
            //second check our entire column for a bingo, check every row in columns  
            for (int j = 0; j < 5; j++)
            {
                if(userBoard[j, cols] == false)
                {
                    checkColFalse = true;
                }

            }
            //if we found no false values in the row we found a bingo
            if(checkColFalse == false)
            {
                bingo = true;
            }

            //now we need to check for the special cases, ie diagonally
            //check first diagonal
            if(userBoard[4,0] && userBoard[3,1] && userBoard[2,2] && userBoard[1,3] && userBoard[0,4])
            {
                //if first diagonal equals to true we found a bingo 
                bingo = true;
            }

            //now we need to check for the other special case
            //check second diagonal 
            if(userBoard[0,0] && userBoard[1,1] && userBoard[2,2] && userBoard[3,3] && userBoard[4, 4]){
                //if this second diagonal equals to true we found a bingo 
                bingo = true;
            }

        }

        public bool getBingo()
        {
            return bingo;
        }
    }
}
