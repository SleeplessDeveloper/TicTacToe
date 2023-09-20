using System.Diagnostics.Metrics;
using System.Transactions;
using System.Linq;

namespace TIcTacToe
{
    internal class Program
    {
        static readonly string[,] BoardLayout = 
            {
                {"1","2","3"},
                {"4","5","6"}, 
                {"7","8","9"}
            };

        /*static int[,] IntBoardLayout =
            {
                {1,2,3},
                {4,5,6},
                {7,8,9}
            };*/
        static int NumPlayers = 2;
        static bool GameOver = false;
        static bool Player1 = false;
        static bool Player2 = false;

        static bool PlayAgain = false;

        static string[,] reuseableLayout = BoardLayout;
        static void Main(string[] args)
        {
            
            int NoOfPlayers = NumPlayers;
            
            TicTacToe(reuseableLayout);
            GameLogic(NoOfPlayers, reuseableLayout);
            Restart(PlayAgain,reuseableLayout, NoOfPlayers);

        }

        public static bool Checker(string[,] board , bool winner)
        {
            string valueCheck = "";
            int counter = 0;
            int rowCounter = 0;
            int horizontalCounter = 0;

            //foreach loop checks horizontal
            foreach(string a in board)
            {
                counter++;
                valueCheck += a;

                if (counter == 3)
                {
                    if (valueCheck == "XXX" || valueCheck == "OOO")
                    {
                        Player1 = valueCheck == "XXX" ? true : false;
                        Player2 = valueCheck == "OOO" ? true : false;
                        return winner = true;
                         
                    }else
                    {
                        valueCheck = "";
                        counter = 0;
                    }
                }
            }

            //while loop
            //check vertical
            foreach(string b in board)
            {
                counter++;

                valueCheck += board[horizontalCounter, rowCounter];
                horizontalCounter++;

                if (horizontalCounter == 3)
                {
                    rowCounter++;
                    horizontalCounter = 0;
                }

                if(counter == 3)
                {
                    if(valueCheck == "XXX" || valueCheck == "OOO")
                    {
                        Player1 = valueCheck == "XXX" ? true : false;
                        Player2 = valueCheck == "OOO" ? true : false;
                        return winner = true;
                    }
                    else
                    {
                        valueCheck = "";
                        counter = 0;
                    }
                }
            }

            //Nested for loop to check diagonal row(forward)
            for(int i = 0; i < board.GetLength(0); i++) 
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    if(i == j)
                    {
                        counter++;
                        valueCheck += board[i, j];

                        if(counter == 3)
                        {
                            if(valueCheck == "XXX" || valueCheck == "OOO")
                            {
                                Player1 = valueCheck == "XXX" ? true : false;
                                Player2 = valueCheck == "OOO" ? true : false;
                                return winner = true;
                            }
                            else
                            {
                                valueCheck = "";
                                counter = 0;
                            }
                        }
                    }
                }
            }

            // for loop to check diagonal backwards
            for(int i = 0 , j = 2; i < board.GetLength(0); i++, j--)
            {
                counter++;
                valueCheck += board[i, j];
                if (counter == 3)
                {
                    if(valueCheck == "XXX"|| valueCheck == "OOO")
                    {
                        Player1 = valueCheck == "XXX" ? true : false;
                        Player2 = valueCheck == "OOO" ? true : false;
                        return winner = true;
                    }
                    else
                    {
                        valueCheck = "";
                        counter = 0;
                    }
                }
            }

            foreach( string x in board)
            {
                if(x == "O" ||  x == "X")
                {
                    winner = true;
                }
                else { winner = false; }
            }

            return winner;
        }

        //tic tac toe interface method
        public static void TicTacToe(string[,] board)
        {
            int counter = 0;
            //loop created to simultaneously create the layout and display the values of the int array
            for(int a = 0; a < board.GetLength(0); a++)
            {
                
                for(int b = 0; b < board.GetLength(1); b++)
                {
                    counter++;
                    //condition statements used to input values while creating the layout of the game
                    if (counter == 3 || counter == 6)
                    {

                        Console.WriteLine("________|__________|_______");

                    }
                    else if (counter == 2 || counter == 5 || counter == 8)
                    {
                        Console.WriteLine($"    {board[a,b-1]}   |     {board[a,b]}    |   {board[a,b+1]}  ");
                    }
                    else
                    {
                        Console.WriteLine("        |          |      ");
                    }
                }

                


            }
            
        }

        public static void GameLogic(int players, string[,] boards)
        {
            int userInput;
            string userPlayAgain;
            bool notValid = true;
            do
            {
                
                if (players == 2)
                {
                    players -= 1;
                    Console.WriteLine($"\nPlayer {players}: Choose your field!");
                    if(int.TryParse(Console.ReadLine(), out userInput))
                    {
                        switch(userInput)
                        {
                            case 1:
                                Console.Clear();
                                boards[0, 0] = boards[0, 0] != "O" ? "X"
                                    : (boards[0,0] != "O" && boards[0, 0] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 2:
                                Console.Clear();
                                boards[0, 1] = boards[0, 1] != "O" ? "X"
                                    : (boards[0, 1] != "O" && boards[0, 1] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 3:
                                Console.Clear();
                                boards[0, 2] = boards[0, 2] != "O" ? "X" 
                                    : (boards[0, 2] != "O" && boards[0, 2] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 4:
                                Console.Clear();
                                boards[1, 0] = boards[1, 0] != "O" ? "X"
                                    : (boards[1, 0] != "O" && boards[1, 0] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 5:
                                Console.Clear();
                                boards[1, 1] = boards[1, 1] != "O" ? "X" : (boards[1, 1] != "O" && boards[1, 1] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 6:
                                Console.Clear();
                                boards[1, 2] = boards[1, 2] != "O" ? "X" 
                                    : (boards[1, 2] != "O" && boards[1, 2] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 7:
                                Console.Clear();
                                boards[2, 0] = boards[2, 0] != "O" ? "X" 
                                    : (boards[2, 0] != "O" && boards[2, 0] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 8:
                                Console.Clear();
                                boards[2, 1] = boards[2, 1] != "O" ? "X" 
                                    : (boards[2, 1] != "O" && boards[2, 1] != "O" && boards[2, 1] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 9:
                                Console.Clear();
                                boards[2, 2] = boards[2, 2] != "O" ? "X" 
                                    : (boards[2, 2] != "O" && boards[2, 2] != "X") ? "X" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            default:
                                Console.Clear();
                                TicTacToe(boards);
                                break;

                        }
                    }
                    else
                    {
                        Console.Clear();
                        TicTacToe(boards);
                        Console.WriteLine("Please enter a number!");
                        players += 1;
                    }

                }
                else if(players == 1)
                {
                    players += 1;
                    Console.WriteLine($"\nPlayer {players}: Choose your field!");
                    if (int.TryParse(Console.ReadLine(),out userInput))
                    {
                        switch (userInput)
                        {
                            case 1:
                                Console.Clear();
                                boards[0, 0] = boards[0, 0] != "X" ? "O" 
                                    : (boards[0, 0] != "X" && boards[0, 0] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 2:
                                Console.Clear();
                                boards[0, 1] = boards[0, 1] != "X" ? "O" 
                                    : (boards[0, 1] != "X" && boards[0, 1] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 3:
                                Console.Clear();
                                boards[0, 2] = boards[0, 2] != "X" ? "O" 
                                    : (boards[0, 2] != "X" && boards[0, 2] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 4:
                                Console.Clear();
                                boards[1, 0] = boards[1, 0] != "X" ? "O"
                                    : (boards[1, 0] != "X" && boards[1, 0] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 5:
                                Console.Clear();
                                boards[1, 1] = boards[1, 1] != "X" ? "O" 
                                    : (boards[1, 1] != "X" && boards[1, 1] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 6:
                                Console.Clear();
                                boards[1, 2] = boards[1, 2] != "X" ? "O" 
                                    : (boards[1, 2] != "X" && boards[1, 2] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 7:
                                Console.Clear();
                                boards[2, 0] = boards[2, 0] != "X" ? "O"
                                    : (boards[2, 0] != "X" && boards[2, 0] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 8:
                                Console.Clear();
                                boards[2, 1] = boards[2, 1] != "X" ? "O" 
                                    : (boards[2, 1] != "X" && boards[2, 1] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            case 9:
                                Console.Clear();
                                boards[2, 2] = boards[2, 2] != "X" ? "O" 
                                    : (boards[2, 2] != "X" && boards[2, 2] != "O") ? "O" : "You cant play in an occupied space";
                                TicTacToe(boards);
                                GameOver = Checker(boards, GameOver);
                                break;
                            default:
                                Console.Clear();
                                TicTacToe(boards);
                                break;
                        }
                     }
                    else
                    {
                        Console.Clear();
                        TicTacToe(boards);
                        Console.WriteLine("Please enter a number!");
                        players -= 1;
                    }
                }
                
            } while (!GameOver);

            //if statement to determine who won or if its a draw
            if (Player1==true)
            {
                Console.WriteLine("Player 1 has won!");
            }else if(Player2==true)
            {
                Console.WriteLine("Player 2 has won");
            }else { Console.WriteLine("Draw"); }

            do
            {
                
                Console.WriteLine("Do you want to play again? y/n");
                userPlayAgain = Console.ReadLine().ToLower();

                if (userPlayAgain == "y")
                {
                    PlayAgain = true;
                    notValid = false;
                    Console.Clear() ;
                    reuseableLayout = BoardLayout;
                }
                else if (userPlayAgain == "n")
                {
                    PlayAgain = false;
                    notValid = true;
                    Console.Clear();
                    reuseableLayout = BoardLayout;
                    
                }
                else { Console.WriteLine("Please enter a valid response"); }

            } while (notValid);
            
        }  
        
        public static void Restart(bool reset, string[,] boards, int players)
        {
            if (reset)
            {
                TicTacToe(boards);
                GameLogic(players, boards);
            }
            else
            {
                Console.WriteLine("Goodbye!");
            }
        }
            
    }
}