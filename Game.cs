namespace Hangman
{
    internal partial class Program
    {
        public static class Game
        {
            public static void ExitGame()
            {
                Console.WriteLine("\nThanks for playing! Goodbye :)");
                Environment.Exit(0);
            }
            public static int SelectOption()
            {
                Utils.DrawGameTitle();

                string decoration = new('=', 15);

                Console.WriteLine(decoration);

                Console.WriteLine(" 1. New game");
                Console.WriteLine(" 2. Add word");
                Console.WriteLine(" 3. Exit");

                Console.WriteLine(decoration + "\n");

                Console.Write("Select an option: ");

                return Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
