using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    public class RandomHistory
    {
        private Random random;
        private int[,] history;
        private int numCalls;

        //constructor that initalizes the random number generator and the history array
        public RandomHistory()
        {
            //since we have 5 columns on our bingo sheet with values ranging from (x * 15) to (x * 15) + 15, where x = column number
            //initalize array to be: [15][5], Note: 15*5 = 75 which is all the bingo numbers possible 
            history = new int[15, 5];

            //initalize the array with null values (in this case -1)
            int maxRows = 15;
            int maxCol = 5;
            for(int col = 0; col < maxCol; col++)
            {
                for(int row = 0; row < maxRows; row++)
                {
                    history[row, col] = -1;
                }

            }

            //initalize random 
            random = new Random();
            numCalls = 0;

        }

        //generates and returns a random value AS LONG AS IT HASNT BEEN CALLED BEFORE
        public int [] call()
        {
            //repeat is a boolean value to keep looping if it is a duplicate number 
            Boolean repeat = true;
            int value = -1;
            int col = -1;
            while (repeat == true /*&& numCalls < 75*/)
            {
                //generate random value 
                value = random.Next(1, 75);

                //our history array is sorted by columns, check this values column to find a duplicate 
                //if value is in column 0
                if(value >= 1 && value <= 15)
                {
                    col = 0;
                }

                //if value is in column 1
                else if (value >= 16 && value <= 30)
                {
                    col = 1;
                }

                //if value is in column 2
                else if (value >= 31 && value <= 45)
                {
                    col = 2;
                }

                //if value is in column 3
                else if (value >= 46 && value <= 60)
                {
                    col = 3;
                }

                //if value is in column 4
                else //values 61 - 75
                {
                    col = 4;
                }

                Boolean findDup = false; 
                //now since we have the column of the value check through the column to find a duplicate 
                for(int i = 0; i < 15; i++)
                {
                    //first check if the value at history[i] is a null value (ie. -1)
                    if(history[i,col] == -1)
                    {
                        //we have reached the end of the history
                        //if our value of findDup did not change to true, store the value and break out of the loop
                        if(findDup == false)
                        {
                            history[i, col] = value;
                            repeat = false;
                            i = 15;
                        }
                        else
                        {
                            //otherwise do nothing and let the value fall through and repeat the process from the top
                            i = 15;
                        }
                    }

                    //check if it is a duplicate 
                    else //(value == history[i,col])
                    {
                        if(value == history[i, col])
                        {
                            //if it is a duplicate, change our variable to true 
                            findDup = true;
                        }

                    }
                }


            }

            numCalls++;
            int[] retArray = { col, value };
            return retArray;
        }


    }
}
