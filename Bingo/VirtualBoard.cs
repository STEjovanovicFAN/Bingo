using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    public class VirtualBoard
    {
        private int[,] physBoard;
        private bool[,] truthBoard;

        //initalize variables in the constructor 
        public VirtualBoard(int [,] pb)
        { 
            physBoard = pb; //values of our bingo board 
            truthBoard = new Boolean[5, 5];

            //initalize all values to be false in our truth board 
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    truthBoard[j, i] = false;
                }
            }
            //initalize value 2,2 to be true (Note this is our star free space)
            truthBoard[2, 2] = true;
        }

        /*
         * Whenever our timer makes and calls a value, we also need to make a call to our virtual board
         * 
         * The virtual board plays itself:
         *      Has a physical board to know what the numbers there are 
         *      Has a truth board to mark off what positions on the bingo board to mark off 
         *      HOWEVER the virtual board does not know if it has bingo 
         */
        public void newCall(int [] values) //values contains an array with {col, value}
        {
            //when we get a new call check if we have it on our physical board 
            Boolean check = false;
            int ballNum = values[1];
            int col = values[0];
            int foundRow = -1;
            for(int rows = 0; rows < 5; rows++)
            {
                //if we find it in the row change our check to true
                if (physBoard[rows, col] == ballNum)
                {
                    check = true;
                    foundRow = rows;
                }
                    //otherwise keep looping
            }

            //check if we found the ballNum on our physical board
            if(check == true)
            {
                //mark the position on our truth board 
                truthBoard[foundRow, col] = true;
            }

        }

        //find the rows and column of this number on the bingo sheet 
        public int [] findRowCol(int value)
        {
            int findRow = -1;
            int findCol = -1;

            //go through the physical board to find the value 
            for(int col = 0; col < 5; col++)
            {
                for(int row = 0; row < 5; row++)
                {
                    if(value == physBoard[row, col])
                    {
                        findCol = col;
                        findRow = row;
                        break;
                    }

                }
            }
            //number position is findRow and findCol, return these 
            int[] rowCol = { findRow, findCol };

            return rowCol;
        }

        //User presses a button on the userBoard we need to know if its a valid press, return the truth board at that position  
        public Boolean checkTrue(int [] inputs)
        {
            int row = inputs[0];
            int col = inputs[1];

            return truthBoard[row, col];
        }

    }
}
