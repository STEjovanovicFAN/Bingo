using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bingo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String [] mainPage = { "BingoPNG", "StartPic", "Start", "AuthorLabel" };
        public String[] gamePage = { "Pause", "Exit1", "BingoButton", "BingoBoard", "CallBoard", "OutCir1", "OutCir2", "OutCir3", "OutCir4", "OutCir5",
                                     "InCir1", "InCir2", "InCir3", "InCir4", "InCir5", "BallNum1", "BallNum2", "BallNum3", "BallNum4", "BallNum5"};
        public int mode = 0;
        public DispatcherTimer dispatcherTimer;
        public RandomHistory randHis;
        public int numCalls;
        public VirtualBoard virtBoard;
        public UserBoard userBoard;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        //after user presses start: initalize the physical board, initalize the virtual board and start the timer class for the board call
        public void initalizeGame()
        {
            //create the physical board 
            PhysicalBoard pb = new PhysicalBoard();
            pb.randomize();
            //show the player the new board layout 
            initalizeBoard(pb);

            //pass the phyiscal board to the virtual board and userBoard 
            virtBoard = new VirtualBoard(pb.getPhyBoard());
            userBoard = new UserBoard(pb.getPhyBoard());


            //initalize our random history for out call board and initalize our num calls 
            randHis = new RandomHistory();
            numCalls = 0;
            //create and start our timer for our call board
            initalizeTimer();



        }

        //shows the user the state of the new physical board 
        public void initalizeBoard(PhysicalBoard board)
        {
            button00.Content = board.getValue(0, 0);
            button10.Content = board.getValue(1, 0);
            button20.Content = board.getValue(2, 0);
            button30.Content = board.getValue(3, 0);
            button40.Content = board.getValue(4, 0);

            button01.Content = board.getValue(0, 1);
            button11.Content = board.getValue(1, 1);
            button21.Content = board.getValue(2, 1);
            button31.Content = board.getValue(3, 1);
            button41.Content = board.getValue(4, 1);

            button02.Content = board.getValue(0, 2);
            button12.Content = board.getValue(1, 2);
            //button22.Content = board.getValue(2, 2); //this is our free space aka star
            button32.Content = board.getValue(3, 2);
            button42.Content = board.getValue(4, 2);

            button03.Content = board.getValue(0, 3);
            button13.Content = board.getValue(1, 3);
            button23.Content = board.getValue(2, 3);
            button33.Content = board.getValue(3, 3);
            button43.Content = board.getValue(4, 3);

            button04.Content = board.getValue(0, 4);
            button14.Content = board.getValue(1, 4);
            button24.Content = board.getValue(2, 4);
            button34.Content = board.getValue(3, 4);
            button44.Content = board.getValue(4, 4);


        }
        
        //initalizes and starts the timer every 4 seconds 
        //calls the handleTimerTimeOut method to handle the timeout 
        public void initalizeTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(handelTimerTimeOut);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }

        //handels the timeout 
        public void handelTimerTimeOut(object sender, EventArgs e)
        {
            /* whenever 4 seconds pass this method will be called 
             *
             * this method should:
             *  -call a class to generate a random number 
             *  -pass this number on to the virtual board (do this later)
             *  -Update the new number for the user to see (this will be in the bingo ball) 
             */

            //call for a random generated number that has not been repeated before
            if (numCalls < 74)
            {
                int [] getArray = randHis.call(); //gets an array with {col, value}
                numCalls++;

                //display the bingo ball with these values 
                displayBall(getArray);
                //send these values to our virtual board 
                virtBoard.newCall(getArray);
            }
            else //we have reached the end, stop the timer 
            {
                dispatcherTimer.Stop();
            }

        }

        //displays the newly drawn ball 
        public void displayBall(int [] arrayValues)
        {
            //organize the colors into order in the array 
            string [] colorArray = {"#FFB72121","#FF27CAE7", "#FF75C518", "#FFDDAE2E", "#FFE150E1" }; //red, blue, green, orange, pink 
            //Color color = (Color)ColorConverter.ConvertFromString(colorArray[arrayValues[0]]);

            //for first time 
            if (numCalls == 1)
            {
                
                OutCir1.Visibility = Visibility.Visible;
                InCir1.Visibility = Visibility.Visible;
                BallNum1.Visibility = Visibility.Visible;

                BallNum1.Content = arrayValues[1];
                OutCir1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorArray[arrayValues[0]]));
            }

            else if(numCalls == 2)
            {
                OutCir2.Visibility = Visibility.Visible;
                InCir2.Visibility = Visibility.Visible;
                BallNum2.Visibility = Visibility.Visible;

                BallNum2.Content = BallNum1.Content;
                OutCir2.Fill = OutCir1.Fill;

                BallNum1.Content = arrayValues[1];
                OutCir1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorArray[arrayValues[0]]));

            }

            else if(numCalls == 3)
            {
                OutCir3.Visibility = Visibility.Visible;
                InCir3.Visibility = Visibility.Visible;
                BallNum3.Visibility = Visibility.Visible;

                BallNum3.Content = BallNum2.Content;
                OutCir3.Fill = OutCir2.Fill;

                BallNum2.Content = BallNum1.Content;
                OutCir2.Fill = OutCir1.Fill;

                BallNum1.Content = arrayValues[1];
                OutCir1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorArray[arrayValues[0]]));


            }
            else if(numCalls == 4)
            {
                OutCir4.Visibility = Visibility.Visible;
                InCir4.Visibility = Visibility.Visible;
                BallNum4.Visibility = Visibility.Visible;

                BallNum4.Content = BallNum3.Content;
                OutCir4.Fill = OutCir3.Fill;

                BallNum3.Content = BallNum2.Content;
                OutCir3.Fill = OutCir2.Fill;

                BallNum2.Content = BallNum1.Content;
                OutCir2.Fill = OutCir1.Fill;

                BallNum1.Content = arrayValues[1];
                OutCir1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorArray[arrayValues[0]]));

            }

            else     //(numCalls >= 5)
            {
                OutCir5.Visibility = Visibility.Visible;
                InCir5.Visibility = Visibility.Visible;
                BallNum5.Visibility = Visibility.Visible;

                BallNum5.Content = BallNum4.Content;
                OutCir5.Fill = OutCir4.Fill;

                BallNum4.Content = BallNum3.Content;
                OutCir4.Fill = OutCir3.Fill;

                BallNum3.Content = BallNum2.Content;
                OutCir3.Fill = OutCir2.Fill;

                BallNum2.Content = BallNum1.Content;
                OutCir2.Fill = OutCir1.Fill;

                BallNum1.Content = arrayValues[1];
                OutCir1.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorArray[arrayValues[0]]));

            }

        }

        //User Click on a bingo Button 
        private void bingoButton_Click(object sender, RoutedEventArgs e)
        {
            //get the content of the button
            string content = (sender as Button).Content.ToString();
            int intContent = Convert.ToInt32(content);

            //find the row and col of the number 
            int [] rowCol = virtBoard.findRowCol(intContent);

            //now that we have the position of the content of the button, we need to check if this button can be checked off
            //to do this ask the truth table, if it returns true disable button, else do nothing
            bool check = virtBoard.checkTrue(rowCol);
            if(check == true)
            {
                (sender as Button).IsEnabled = false;
                //if this is true record this on our Userboard as what the user documented 
                userBoard.record(rowCol);
                //now that we recorded this value on our userBoard see if we have a bingo
                bool bingo = userBoard.getBingo();
                if(bingo == true)
                {
                    BingoButton.IsEnabled = true;
                }

            }

        }

        //Exit button 
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //Exit application if pressed 
            App.Current.Shutdown();
        }

        //pause button, pauses the callboard 
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            string content = (string)Pause.Content;
            if (content.Equals("Pause"))
            {
                //change the content to resume and stop the timer 
                Pause.Content = "Resume";
                dispatcherTimer.Stop();
            }
            else
            {
                //change the content to pause and resume the timer
                Pause.Content = "Pause";
                dispatcherTimer.Start();
            }
        }

        //Minimize program 
        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            //Minimize application if pressed 
            WindowState = WindowState.Minimized;
        }

        //Start Screen and exit screen  
        private void StartEnd(object sender, RoutedEventArgs e)
        {

            if(mode == 0)
            {
                //When the user presses the start button hide all ui elements from main page and load new elements   
                mode = 1;

                //current UI elements needing to be hid: "BingoPNG", "StartPic", "Start", "AuthorLabel"
                BingoPNG.Visibility = Visibility.Hidden;
                StartPic.Visibility = Visibility.Hidden;
                Start.Visibility = Visibility.Hidden;
                AuthorLabel.Visibility = Visibility.Hidden;

                //UI elements that need to be shown: "Pause", "Exit1", "BingoButton", "BingoBoard"
                Pause.Visibility = Visibility.Visible;
                Exit1.Visibility = Visibility.Visible;
                BingoButton.Visibility = Visibility.Visible;
                BingoBoard.Visibility = Visibility.Visible;
                CallBoard.Visibility = Visibility.Visible;
                //OutCir1.Visibility = Visibility.Visible;
                //OutCir2.Visibility = Visibility.Visible;
                //OutCir3.Visibility = Visibility.Visible;
                //OutCir4.Visibility = Visibility.Visible;
                //OutCir5.Visibility = Visibility.Visible;
                //InCir1.Visibility = Visibility.Visible;
                //InCir2.Visibility = Visibility.Visible;
                //InCir3.Visibility = Visibility.Visible;
                //InCir4.Visibility = Visibility.Visible;
                //InCir5.Visibility = Visibility.Visible;
                //BallNum1.Visibility = Visibility.Visible;
                //BallNum2.Visibility = Visibility.Visible;
                //BallNum3.Visibility = Visibility.Visible;
                //BallNum4.Visibility = Visibility.Visible;
                //BallNum5.Visibility = Visibility.Visible;
                ButtonGrid.Visibility = Visibility.Visible;
                star.Visibility = Visibility.Visible;

                //initalize the game
                initalizeGame();

            }
            else
            {
                //When the user presses the Exit button on the game page hide all ui elements from main page and load new elements 
                mode = 0;

                //current UI elements needing to be hid: "Pause", "Exit1", "BingoButton", "BingoBoard"
                Pause.Visibility = Visibility.Hidden;
                Exit1.Visibility = Visibility.Hidden;
                BingoButton.Visibility = Visibility.Hidden;
                BingoBoard.Visibility = Visibility.Hidden;
                CallBoard.Visibility = Visibility.Hidden;
                OutCir1.Visibility = Visibility.Hidden;
                OutCir2.Visibility = Visibility.Hidden;
                OutCir3.Visibility = Visibility.Hidden;
                OutCir4.Visibility = Visibility.Hidden;
                OutCir5.Visibility = Visibility.Hidden;
                InCir1.Visibility = Visibility.Hidden;
                InCir2.Visibility = Visibility.Hidden;
                InCir3.Visibility = Visibility.Hidden;
                InCir4.Visibility = Visibility.Hidden;
                InCir5.Visibility = Visibility.Hidden;
                BallNum1.Visibility = Visibility.Hidden;
                BallNum2.Visibility = Visibility.Hidden;
                BallNum3.Visibility = Visibility.Hidden;
                BallNum4.Visibility = Visibility.Hidden;
                BallNum5.Visibility = Visibility.Hidden;
                ButtonGrid.Visibility = Visibility.Hidden;
                star.Visibility = Visibility.Hidden;
                YouWon.Visibility = Visibility.Hidden;

                BingoButton.IsEnabled = false;
                dispatcherTimer.Stop();

                button00.IsEnabled = true;
                button01.IsEnabled = true;
                button02.IsEnabled = true;
                button03.IsEnabled = true;
                button04.IsEnabled = true;
                button10.IsEnabled = true;
                button11.IsEnabled = true;
                button12.IsEnabled = true;
                button13.IsEnabled = true;
                button14.IsEnabled = true;
                button20.IsEnabled = true;
                button21.IsEnabled = true;
                button23.IsEnabled = true;
                button24.IsEnabled = true;
                button30.IsEnabled = true;
                button31.IsEnabled = true;
                button32.IsEnabled = true;
                button33.IsEnabled = true;
                button34.IsEnabled = true;
                button40.IsEnabled = true;
                button41.IsEnabled = true;
                button42.IsEnabled = true;
                button43.IsEnabled = true;
                button44.IsEnabled = true;

                //UI elements that need to be shown:
                BingoPNG.Visibility = Visibility.Visible;
                StartPic.Visibility = Visibility.Visible;
                Start.Visibility = Visibility.Visible;
                AuthorLabel.Visibility = Visibility.Visible;

            }
        }

        //Bingo button
        private void BingoButton_Click_1(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            YouWon.Visibility = Visibility.Visible;

        }
    }
}
