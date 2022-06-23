using System;

namespace Mastermind
{
    class Program
    {
        #region Variables
        const int mAnswerLength = 4;
        const int mAttempts = 10;
        const int mUpperLimit = 6;
        #endregion

        /// <summary>
        /// Initial application method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            do
            {
                StartGame();
                Console.Write("Would you like to play again (y/n)? ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        /// <summary>
        /// Recursively starts the game for the user
        /// </summary>
        private static void StartGame()
        {
            Console.WriteLine("QUADAX PROGRAMMING EXERCISE");
            Console.WriteLine("EXERCISE: Create a C# console application that is a simple version of Mastermind.");
            Console.WriteLine("Rules" + 
                $"\n\tYou have {mAttempts} attempts to guess a {mAnswerLength}-digit number" +
                $"\n\t- Digits must be between 1 and {mUpperLimit}" +
                "\n\t- If the number is in the right spot, there will be a '+' sign" +
                "\n\t- If the number is in the wrong spot, there will be a '-' sign");

            int answer = CreateAnswer();
            bool won = false;
            for (int i = 0; i < mAttempts; i++)
            {
                UserGuess(out int guess);
                if (Evaluate(answer, guess))
                {
                    won = true;
                    break;
                }
            }

            if (won)
                Console.WriteLine("You won.");
            else
                Console.WriteLine($"You lost, the answer was: {answer}\n");
        }

        /// <summary>
        /// Answer creation. I figured arrays might be faster, but thought
        /// the code looked cleaner and easier to understand parsing
        /// a 4-digit integer. Since efficiency is not a consideration
        /// here I went this method.
        /// </summary>
        /// <returns></returns>
        public static int CreateAnswer()
        {
            int answer = 0;
            Random rnd = new Random();
            for (int i = 0; i < mAnswerLength; i++)
            {
                if (answer == 0)
                {
                    answer = rnd.Next(1, mUpperLimit);
                    continue;
                }
                answer = int.Parse(answer.ToString() + rnd.Next(1, mUpperLimit));
            }
            return answer;
        }

        public static void UserGuess(out int guess)
        {
            Console.Write($"Guess: ");
            int.TryParse(Console.ReadLine(), out guess);
            while (guess.ToString().Length != 4 || guess < 0 || !CheckGuessConstraints(guess))
            {
                Console.WriteLine($"Answer must be 4 digits, non-negative, and have digits between 1 and {mUpperLimit}.");
                Console.Write($"Guess: ");
                int.TryParse(Console.ReadLine(), out guess);
            }
            Console.Write("\t");
        }

        public static bool CheckGuessConstraints(int guess)
        {
            for (int i = 0; i < guess.ToString().Length; i++)
                if (int.Parse(guess.ToString().Substring(i, 1)) > mUpperLimit) return false;
            return true;
        }

        public static bool Evaluate(int answer, int guess)
        {
            if (answer == guess) return true;
            for (int i = 0; i < mAnswerLength; i++)
            {
                if (answer.ToString().Substring(i, 1) == guess.ToString().Substring(i, 1)) Console.Write("+");
                else if (CheckElseWhere(answer, guess.ToString().Substring(i, 1))) Console.Write("-");
                else Console.Write("/");
            }
            Console.WriteLine();
            return false;
        }

        public static bool CheckElseWhere(int answer, string value)
        {
            for (int i = 0; i < mAnswerLength; i++)
                if (answer.ToString().Substring(i, 1) == value) return true;
            return false;
        }
    }
}
