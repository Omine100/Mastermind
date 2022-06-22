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

        static void Main(string[] args)
        {
            Console.WriteLine("QUADAX PROGRAMMING EXERCISE");
            Console.WriteLine("EXERCISE: Create a C# console application that is a simple version of Mastermind.");

            do
            {
                StartGame();
                Console.Write("Would you like to play again (y/n)? ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        private static void StartGame()
        {
            Console.WriteLine("Rules" + 
                $"\n\tYou have {mAttempts} attempts to guess a {mAnswerLength}-digit number" +
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
            while (guess.ToString().Length != 4 || guess < 0)
            {
                Console.Write($"Guess: ");
                int.TryParse(Console.ReadLine(), out guess);
                Console.WriteLine("Answer must be 4 digits and non-negative.");
            }
            Console.Write("\t");
        }

        public static bool Evaluate(int answer, int guess)
        {
            if (answer == guess) return true;
            for (int i = 0; i < mAnswerLength; i++)
            {
                if (answer.ToString().Substring(i, 1) == guess.ToString().Substring(i, 1)) Console.Write("+");
                else if (CheckElseWhere(answer, guess.ToString().Substring(i, 1))) Console.Write("-");
                else Console.Write("_");
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
