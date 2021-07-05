using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            ManueSelection();
        }

        static void ManueSelection()
        {
            Console.WriteLine("------WELLCOME TO HANGMAN GAME!------");
            Console.WriteLine("------GAME RULE-------");
            Console.WriteLine("------To Start Game Choose one Option-------");
            Console.WriteLine("1:  -Play By Entering The Whole Word" + " \n" +
                              "2:  -Play By Entering a Letter" + " \n" +
                              "0:  -To Exit Game");

            int UserInput = GetUserinput();
            switch (UserInput)
            {
                case 1:
                    GuessWord();
                    break;
                case 2:
                    GuessLetter();
                    break;
                case 0:
                    break;
            }

        }
        static string GetRandomWord()
        {
            //string[] ThinkWords = new string[] { "Animal","human", "computer", "programing", "technalogy","apple", "mangos","world", "birds","Sweden","Paris" };
           
            string[] ThinkWords = new string[14];
            string PathToFile = @"E:\New C# Proj\console ouput\mytext.txt";
            ThinkWords = File.ReadAllLines(PathToFile);
            Random Rnd = new Random();
            string RandomString = ThinkWords[Rnd.Next(0, ThinkWords.Length)].ToUpper();
            return RandomString;
        }   
        static (char[],string) Hidecharacters()
        {
            string RandomString = GetRandomWord();
            char[] CharArray = RandomString.ToCharArray();
            for (int i = 0; i  < CharArray.Length; i++)
            {
                CharArray[i] = '_';
               
            }
        
            return (CharArray,RandomString);
        }
        static void GuessLetter()
        {
            (char[] UnrevealedArray, string RandomString) = Hidecharacters();
            Console.WriteLine(RandomString);
            StringBuilder StrBuilder = new StringBuilder();
            char[] ToSaveRightGusse = new char[RandomString.Length];
            Console.Write("Unrevealed word is:");
            Console.Write(UnrevealedArray);
            bool isAlive = true;
            int count = 10;
            
            
            while (isAlive)
            {
                Console.WriteLine("\n" + "Guess a Letter");
                string UserInput = Console.ReadLine().ToUpper();
                bool CheckFormate = IsAllLetters(UserInput);
                Boolean LetterExist = false;
                // To check if user input is correct format.
                if (CheckFormate == false)  
                {
                    Console.WriteLine("Entered chracter is not in correct formate");
                    Console.WriteLine("Try Again");
                    Console.WriteLine("You still have" + " " + count + " " + "tries left");
                }
                if (CheckFormate == true && UserInput.Length == 1)
                {
                    char Gusscharacter = char.Parse(UserInput);
                    // Seting letter exist to true.
                    if(UnrevealedArray.Contains(Gusscharacter) || StrBuilder.ToString().Contains(Gusscharacter))
                    {
                        LetterExist = true;
                    }

                    //Adding right guss on the right place of hidden word.
                    if (RandomString.Contains<char>(Gusscharacter) && LetterExist == false)
                    {
                        
                        for (int i = 0; i < RandomString.Length; i++)
                            {
                                if (Gusscharacter == RandomString[i])
                                {
                                    UnrevealedArray[i] = Gusscharacter;
                                    ToSaveRightGusse[i] = Gusscharacter;
                            }
                            }
                           
                        
                        Console.Write("Unrevealed Word is" + "" + " :");
                        Console.WriteLine(UnrevealedArray);
                        Console.Write("Your Guess is:" + " ");
                        Console.WriteLine(StrBuilder);
                        count--;
                        Console.WriteLine("You have" + " " + count + " " + "tries remaining");
                    }
                    // If random word not contain the given chracter
                    if(!RandomString.Contains<char>(Gusscharacter) && LetterExist == false)
                    {
                            StrBuilder.Append(Gusscharacter);
                            Console.Write("Unrevealed Word is" + "" + " :");
                            Console.WriteLine(UnrevealedArray);
                            Console.Write("Your Guess is:" + " ");
                            Console.WriteLine(StrBuilder);
                            count--;
                            Console.WriteLine("You have" + " " + count + " " + "tries remaining");

                    }
                    // If you enter same letter second time
                    if (LetterExist == true)
                        {
                            Console.WriteLine("You have already entered this letter:" + " " + Gusscharacter);
                            Console.WriteLine("You still have" + " " + count + " " + "tries remaining");
                        }
                  
                }

                // User entered more than one letter   
                if (UserInput.Length > 1) 
                {
                    Console.WriteLine("You have enter more then one letter");
                    Console.WriteLine("Try Again");
                    Console.WriteLine("You have still" + " "+ count+" "+ "tries left");
                }

                string RightGuess = new string(ToSaveRightGusse);
                // Comparing Guss word and Random Word
                if (RightGuess.Equals(RandomString)) 
                {
                    
                        Console.WriteLine("Congratulation you guess is right");
                        Console.WriteLine("Unrevealed word is:" + " " + RandomString);
                        Console.WriteLine("Your guessword is:" + " " + RightGuess);
                        Console.WriteLine("You Won!");
                        break;
                }
                // This if is when you have take 10 tries and can't gusse the right word.
                if ( RightGuess!= RandomString && count == 0)
                {
                    Console.WriteLine("You Lose the Game ");
                    Console.Write("Unrevealed Word is:" + "");
                    Console.WriteLine(RandomString);
                    Console.Write("Your Guess word is:" + " ");
                    Console.WriteLine(StrBuilder);
                    break;
                }

            } // Ends While
            Console.WriteLine("To restart game press y or press enter to exit game");
            if(Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                ManueSelection();
            }
            
             
            
        }
        static void GuessWord()
        {
          
                (char[] UnrevealedArray, string RandomString) = Hidecharacters();
                Console.Write("Unrealved word is:");
                Console.Write(UnrevealedArray);
                int count = 10;
                while (count > 0)
                {
                    Console.WriteLine("\n" + "Guss a word");
                    string UserInput = Console.ReadLine().ToUpper();
                    bool CheckForLetters = IsAllLetters(UserInput);
                    if (CheckForLetters == false)
                    {
                        Console.WriteLine("Entered word is not in correct formate");
                        Console.WriteLine("Try Again");
                        Console.WriteLine("You still have" + " " + count + " "+ "tries left");
                    }
                    else
                    {
                        if (string.Equals(UserInput, RandomString))
                        {
                            Console.WriteLine("Congratulation you guess is right");
                            Console.WriteLine("Unrevealed word is:" + " " + RandomString);
                            Console.WriteLine("Your guessword is:" + " " + UserInput);
                            Console.WriteLine("You Won!");
                            break;
                        }
                        else
                        { 
                            count--;
                            if (count >= 1)
                            {
                                Console.WriteLine("Your guess word is:" + " " + UserInput);
                                Console.WriteLine("Your guess is wrong!" + " " + "You have" + " " + count + " " + "Tries Left");

                            }
                            else
                            {
                                    Console.WriteLine("You have no Try Left You Lose the game!");
                            }
                        }
                                           
                    }
                    
                }  // End of while loop.
            Console.WriteLine("To restart game press y or press enter to exit game");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                ManueSelection();
            }
        } // End of GuessWord Method.
        static int GetUserinput()
        {
            string userInput = Console.ReadLine();

            int number = 0;
            int.TryParse(userInput, out number);

            return number;
        }
        static bool IsAllLetters(string str)
        {
            foreach (char letter in str)
            {
                if (!Char.IsLetter(letter))
                    return false;
            }
            return true;
        }

    }
}
