// Elin Ericstam SUT21

using System;

namespace NumbersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runGame = true;
            bool continueGame = true;

            while (runGame == true)
            {
                int difficulty;
                int guessesLeft = 0;
                int totalGuesses = 0;
                bool guessedRight = false;
                int minNumberGuess = 1;
                int maxNumberGuess = 0;
                bool error = false;

                if (continueGame == true)
                {
                    Console.WriteLine("Välkommen! Välj svårighetsgrad mellan 1-5.");
                }

                try
                {
                    do
                    {
                        error = Int32.TryParse(Console.ReadLine(), out difficulty);
                        error = false;

                        if (error == true || (difficulty > 5 || difficulty < 1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Vänligen ange ett nummer mellan 1-5.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            error = true;
                        }

                    } while (error == true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

                //int guessesLeft = 0;
                //int totalGuesses = 0;
                //bool guessedRight = false;
                //int minNumberGuess = 1;
                //int maxNumberGuess = 0;

                Random random = new Random();
                int number = 0;

                switch (difficulty)
                {
                    case 1:
                        number = random.Next(1, 5);
                        guessesLeft = 4;
                        totalGuesses = 4;
                        maxNumberGuess = 5;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-5. Kan du gissa vilket? Du får fyra försök.");
                        break;

                    case 2:
                        number = random.Next(1, 10);
                        guessesLeft = 4;
                        totalGuesses = 4;
                        maxNumberGuess = 10;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-10. Kan du gissa vilket? Du får fyra försök.");
                        break;

                    case 3:
                        number = random.Next(1, 20);
                        guessesLeft = 5;
                        totalGuesses = 5;
                        maxNumberGuess = 20;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-20. Kan du gissa vilket? Du får fem försök.");
                        break;

                    case 4:
                        number = random.Next(1, 25);
                        guessesLeft = 5;
                        totalGuesses = 5;
                        maxNumberGuess = 25;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-25. Kan du gissa vilket? Du får fem försök.");
                        break;

                    case 5:
                        number = random.Next(1, 50);
                        guessesLeft = 6;
                        totalGuesses = 6;
                        maxNumberGuess = 50;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-50. Kan du gissa vilket? Du får sex försök.");
                        break;

                    default:
                        continueGame = false;
                        break;
                }

                //Random random = new Random();
                //int number = random.Next(1, 20);

                //int guessesLeft = 5;
                //bool guessedRight = false;


                if (continueGame == true)
                {
                    while (guessesLeft > 0 && guessedRight != true)
                    {
                        Program.CheckGuess(guessesLeft, number, guessedRight, maxNumberGuess, minNumberGuess, out guessesLeft, out guessedRight);
                    }

                    if (guessedRight == false)
                    {
                        Console.WriteLine($"Tyvärr du lyckades inte gissa talet på {totalGuesses} försök!");
                    }

                    Console.WriteLine("\nVill du spela igen? Svara Ja/Nej");
                    string replay = Console.ReadLine().ToUpper();

                    if (replay == "NEJ" /*|| continueGame == false*/)
                    {
                        Console.WriteLine("Tack för att du spelade!");
                        runGame = false;
                    }
                    else if (replay == "JA")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du svarade inte Ja/Nej och spelet börjar om.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
                    }
                }
            }
        }

        static void CheckGuess(int guessesLeft, int number, bool isGuessRight, int maxNumberGuess, int minNumberGuess, out int guessesLeft2, out bool isGuessRight2) 
        {
            int userGuess;
            bool noError = false;

            try
            {
                //userGuess = Int32.Parse(Console.ReadLine());
                do
                {
                    noError = Int32.TryParse(Console.ReadLine(), out userGuess); 
                    //error = false;

                    if (noError == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vänligen ange ett nummer.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        noError = false;
                    }
                                        
                } while (noError == false);
                //Console.ForegroundColor = ConsoleColor.Gray;
                //userGuess = Int32.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            //error = true;

            Random random = new Random();
            int answer = random.Next(0, 4);

            if (userGuess == number)
            {
                string[] outputGuessedRight = { "Woho! Du gjorde det!", "Grattis du klarade det!", "Du gissade rätt!", "Det var rätt!", "Du lyckades!" };
                Console.WriteLine(outputGuessedRight[answer]);
                guessesLeft--;
                isGuessRight = true;              
            }
            else if (userGuess > maxNumberGuess || userGuess < minNumberGuess && noError == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Du gissade inte inom {minNumberGuess}-{maxNumberGuess}.");
            }
            else if (userGuess < number && noError == true)
            {
                string[] outputGuessedTooLow = { "Tyvärr du gissade för lågt!", "Talet är högre!", "Det var för lågt!", "Gissa högre!", "Inte riktigt, talet är högre!" };
                Console.WriteLine(outputGuessedTooLow[answer]);

                if (userGuess >= number - 2 && isGuessRight == true)
                {
                    Console.WriteLine("Det bränns!");
                }
                else if (userGuess < number - (maxNumberGuess / 2) && isGuessRight == true) // TODO FIXA
                {
                    Console.WriteLine("Oj, det var långt ifrån!");
                }

                guessesLeft--;
            }
            else if (userGuess > number && noError == true)
            {
                string[] outputGuessedTooHigh = { "Tyvärr du gissade för högt!", "Talet är lägre!", "Det var för högt!", "Gissa lägre!", "Inte riktigt, talet är lägre!" }; 
                Console.WriteLine(outputGuessedTooHigh[answer]);

                if (userGuess <= number + 2 && isGuessRight == false)
                {
                    Console.WriteLine("Det bränns!");
                }
                else if (userGuess > number + (maxNumberGuess / 2) && isGuessRight == false) // TODO FIXA
                {
                    Console.WriteLine("Oj, det var långt ifrån!");
                }
                
                guessesLeft--;
            }

            isGuessRight2 = isGuessRight;
            guessesLeft2 = guessesLeft;

        }
    }
}
