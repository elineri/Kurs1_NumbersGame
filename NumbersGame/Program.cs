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
                string replay;

                if (continueGame == true) 
                {
                    Console.WriteLine("Välkommen! Välj svårighetsgrad mellan 1-5.");
                }

                try // User input for difficulty
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

                Random random = new Random();
                int number = 0;

                switch (difficulty) // Difficulty
                {
                    case 1:
                        number = random.Next(1, 6);
                        guessesLeft = 4;
                        totalGuesses = 4;
                        maxNumberGuess = 5;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-5. Kan du gissa vilket? Du får fyra försök.");
                        break;

                    case 2:
                        number = random.Next(1, 11);
                        guessesLeft = 4;
                        totalGuesses = 4;
                        maxNumberGuess = 10;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-10. Kan du gissa vilket? Du får fyra försök.");
                        break;

                    case 3:
                        number = random.Next(1, 21);
                        guessesLeft = 5;
                        totalGuesses = 5;
                        maxNumberGuess = 20;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-20. Kan du gissa vilket? Du får fem försök.");
                        break;

                    case 4:
                        number = random.Next(1, 26);
                        guessesLeft = 5;
                        totalGuesses = 5;
                        maxNumberGuess = 25;
                        continueGame = true;
                        Console.WriteLine("Jag tänker på ett nummer mellan 1-25. Kan du gissa vilket? Du får fem försök.");
                        break;

                    case 5:
                        number = random.Next(1, 51);
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

                if (continueGame == true)
                {
                    while (guessesLeft > 0 && guessedRight != true) // Guess + control
                    {
                        Program.CheckGuess(guessesLeft, number, guessedRight, maxNumberGuess, minNumberGuess, out guessesLeft, out guessedRight);
                    }

                    if (guessedRight == false)
                    {
                        Console.WriteLine($"Tyvärr du lyckades inte gissa talet på {totalGuesses} försök!");
                    }

                    // Restart game?
                    Console.WriteLine("\nVill du spela igen? Svara Ja/Nej");

                    try
                    {
                        replay = Console.ReadLine().ToUpper();

                        while (replay != "JA" && replay != "NEJ")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Du svarade inte Ja/Nej.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            replay = Console.ReadLine().ToUpper();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }

                    if (replay == "NEJ")
                    {
                        Console.WriteLine("Tack för att du spelade!");
                        runGame = false;
                    }
                    else if (replay == "JA")
                    {
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
                do
                {
                    noError = Int32.TryParse(Console.ReadLine(), out userGuess); // Guess

                    if (noError == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Vänligen ange ett nummer.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        noError = false;
                    }
                                        
                } while (noError == false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            Random random = new Random(); // Random output
            int answer = random.Next(0, 4);

            if (userGuess == number) // Correct guess
            {
                string[] outputGuessedRight = { "Woho! Du gjorde det!", "Grattis du klarade det!", "Du gissade rätt!", "Det var rätt!", "Du lyckades!" };
                Console.WriteLine(outputGuessedRight[answer]);
                guessesLeft--;
                isGuessRight = true;              
            }
            else if (userGuess > maxNumberGuess || userGuess < minNumberGuess && noError == true) // Guess outside intervall
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Du gissade inte inom {minNumberGuess}-{maxNumberGuess}.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (userGuess < number && noError == true) // Guess too low
            {
                string[] outputGuessedTooLow = { "Tyvärr du gissade för lågt!", "Talet är högre!", "Det var för lågt!", "Gissa högre!", "Inte riktigt, talet är högre!" };
                Console.WriteLine(outputGuessedTooLow[answer]);

                if (userGuess >= number - 2 && isGuessRight == false)
                {
                    Console.WriteLine("Det bränns!");
                }
                else if (userGuess < number - (maxNumberGuess / 2) && isGuessRight == false)
                {
                    Console.WriteLine("Oj, det var långt ifrån!");
                }

                guessesLeft--;
            }
            else if (userGuess > number && noError == true) // Guess too high
            {
                string[] outputGuessedTooHigh = { "Tyvärr du gissade för högt!", "Talet är lägre!", "Det var för högt!", "Gissa lägre!", "Inte riktigt, talet är lägre!" }; 
                Console.WriteLine(outputGuessedTooHigh[answer]);

                if (userGuess <= number + 2 && isGuessRight == false)
                {
                    Console.WriteLine("Det bränns!");
                }
                else if (userGuess > number + (maxNumberGuess / 2) && isGuessRight == false)
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
