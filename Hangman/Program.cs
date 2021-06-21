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
            Console.WriteLine("------WELLCOME TO HANGMAN GAME!------");
            Console.WriteLine("------GAME RULE-------");
            Console.WriteLine("------To Start Game Choose one Option-------");
            Console.WriteLine("1:  -Play By Entering The Whole Word" + " \n" +
                              "2:  -Play By Entering a Letter" + " \n" +
                              "0:  -To Exit Game");

            int UserInput = GetUserinput();
            switch(UserInput)
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
            //string[] ThinkWords = new string[] { "mangos", "animals", "world", "human", "computer", "birds", "programing", "technalogy","apple" };
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
            StringBuilder StrBuilder = new StringBuilder();
            char[] ToSaveRightGusse = new char[RandomString.Length];
            Console.Write("Unrevealed word is:");
            Console.Write(UnrevealedArray);
            int count = 10;
            int ToSaveLetterIndex = 0;
            while (count > 0)
            {
                Console.WriteLine("\n" + "Guess a Letter");
                string UserInput = Console.ReadLine().ToUpper();
                bool CheckForLetters = IsAllLetters(UserInput);
                if (CheckForLetters == false) // To check if user input is correct format. 
                {
                    Console.WriteLine("Entered letter is not in correct formate");
                    Console.WriteLine("Try Again");
                    Console.WriteLine("You still have" + " " + count + " " + "tries left");
                }
                if(CheckForLetters == true)
                {
                    // This if is to check if user write one letter or more.
                    if (UserInput.Length == 1)
                    {
                        char Gusscharacter;
                       
                        // When first time program starts there is no letters to compare just insert them.
                        if (count == 10)
                        {
                            Gusscharacter = char.Parse(UserInput);
                            if (RandomString.Contains<char>(Gusscharacter))
                            {
                               
                                for (int i = 0; i < RandomString.Length; i++)
                                {
                                    if (Gusscharacter == RandomString[i])
                                    {
                                        UnrevealedArray[i] = Gusscharacter;
                                        
                                    }
                                }
                                //ToSaveRightGusse[ToSaveLetterIndex] = Gusscharacter;
                                Array.Fill(ToSaveRightGusse, Gusscharacter);
                                ToSaveLetterIndex = ToSaveLetterIndex + 1;
                            }
                            else
                            {
                                StrBuilder.Append(Gusscharacter);

                            }
                            Console.Write("Unrevealed Word is" + "" + " :");
                            Console.WriteLine(UnrevealedArray);
                            Console.Write("Your Guess is:" + " ");
                            Console.WriteLine(StrBuilder);
                            count--;
                            Console.WriteLine("You have" + " " + count + " " + "tries remaining");

                        }
                        else
                        {
                            Gusscharacter = char.Parse(UserInput);
                            Boolean LetterExist = false;
                            for (int y = 0; y < UnrevealedArray.Length; y++)  //when user already entered the letter.
                            {
                               
                                if (Gusscharacter.Equals(UnrevealedArray[y]) || StrBuilder.ToString().Contains(Gusscharacter))
                                {
                                    int NumberOfCount = count;
                                    count = NumberOfCount;
                                    LetterExist = true;
                                }

                            }
                            if (LetterExist == true)
                            {
                                Console.WriteLine("You have already entered this letter:" + " " + Gusscharacter);
                                Console.WriteLine("You still have" + " " + count + " " + "tries remaining");
                            }
                            if (LetterExist == false)
                            {
                                
                                if (RandomString.Contains<char>(Gusscharacter))
                                {
                                    for (int z = 0; z < RandomString.Length; z++)
                                    {
                                        if (Gusscharacter == RandomString[z])
                                        {
                                            UnrevealedArray[z] = Gusscharacter;
                                        }
                                        
                                    }
                                    ToSaveRightGusse[ToSaveLetterIndex] = Gusscharacter;
                                    ToSaveLetterIndex = ToSaveLetterIndex +1;

                                }
                                else
                                {
                                    StrBuilder.Append(Gusscharacter);

                                }
                                count--;
                                Console.WriteLine("You have" + " " + count + " " + "tries remaining");
                            }
                            if (count != 0)
                            {
                                Console.Write("Unrevealed Word is:" + "");
                                Console.WriteLine(UnrevealedArray);
                                Console.Write("Your Guess is:" + " ");
                                Console.WriteLine(StrBuilder);
                            }
                        }

                    }     // End of if User enter one letter
                    else // User entered more than one letter
                    {
                        Console.WriteLine("You have enter more then one letter");
                        Console.WriteLine("Try Again");
                        Console.WriteLine("You have still" + " "+ count+" "+ "tries left");
                    }

                }
              
                // This if is when you have take 10 tries and can't gusse the right word.
               
                string RightGuess = new string(ToSaveRightGusse);
                if (string.Equals(RandomString,RightGuess)) // not working yet.
                {
                      
                        Console.WriteLine("Congratulation you guess is right");
                        Console.WriteLine("Unrevealed word is:" + " " + RandomString);
                        Console.WriteLine("Your guessword is:" + " " + RightGuess);
                        Console.WriteLine("You Won!");
                        break;
                }
                if (count == 0 && RandomString != RightGuess)
                {
                    Console.WriteLine("You Lose the Game ");
                    Console.Write("Unrevealed Word is:" + "");
                    Console.WriteLine(RandomString);
                    Console.Write("Your Guess word is:" + " ");
                    Console.WriteLine(StrBuilder);
                }

            } // Ends While
            
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
