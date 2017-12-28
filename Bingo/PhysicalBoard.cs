using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    public class PhysicalBoard
    {
        //this is a 2d array that is the physical board
        private int [,] phyBoard;
        private int [] history;
        private Random random;

        //constructor initalizes the board with values 
        public PhysicalBoard()
        {
            //initalize our arrays and vars
            phyBoard = new int[5, 5];
            history = new int[5];
            for(int i = 0; i < history.Length; i++)
            {
                history[i] = -1;
            }
            random= new Random();

        }

        //randomizes and initalizes the phyBoard 
        public void randomize()
        {

            /*            
             * "history" is an array that keeps track of all the values that we currently have
             *  if we come across a value from the random num generator that is in the array, skip and try again 
             *  
             * The bingo board is 5x5, however each column is between values min-max where min = (columnNumber * 15) + 1 and max = (min + 15)   
             * Example:
             * 
             *     column 0        where x = column number (in this case 0)
             *        w            min = (0 *15) + 1 = 1
             *        w            and max = (min + 15) = 15 
             *        w           
             *        w
             *        w
             *        
             *        
             *        so in this case (column 0) the value of w is between 1 and 15
             *        
             *        Note: that we start from 0 in our array, so we need to go (coulmnNumber + 1) to get our column number 
            */

            int randomNumber = 0;
            int maxColumns = 5; //5 columns in our array 
            int maxRows = 5; //5 rows in our array
            int min; //min range
            int max; //max range

            for(int col = 0; col < maxColumns; col++)
            {
                for(int row = 0; row < maxRows; row++)
                {
 
                    min = (col * 15) + 1;
                    max = (min + 15);

                    //get a random number between ranges min - max
                    randomNumber = random.Next(min, max);

                    for(int i = 0; i < 4; i++)
                    {
                        if(history[i] == randomNumber)
                        {
                            //if we find a duplicate re-roll for a new number and check the arry again
                            randomNumber = random.Next(min, max);
                            i = -1;
                        }
                    }

                    //if we didnt find a duplicate store the value into our history array and phyBoard
                    history[row] = randomNumber;
                    phyBoard[row, col] = randomNumber;

                }

            }

        }

        //gets the value of (row)(col) of phyBoard and returns the random number 
        public int getValue(int row, int col)
        {
            return (phyBoard[row, col]);
        }

        //returns the entire physical board 
        public int [,] getPhyBoard()
        {
            return phyBoard;
        }
    }
}
