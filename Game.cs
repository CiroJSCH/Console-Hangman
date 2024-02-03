using System.Reflection.Metadata;

namespace Hangman
{
    internal partial class Program
    {
        public static class Game
        {
            public static void StartGame()
            {
                Console.Clear();

                string randomWord = Utils.GetRandomWord();
                char[] hiddenWord = new string('_', randomWord.Length).ToCharArray();

                int attemps = 0;
                List<char> guessedLetters = [];

                string decoration = new string('=', 50);

                while (true)
                {
                    Console.Clear();
                    char guessedLetter;

                    Console.WriteLine(decoration);
                    Console.WriteLine($" Guessed letters: {string.Join('-', guessedLetters)}");
                    Console.WriteLine(decoration);

                    DrawHangman(attemps);

                    Console.WriteLine("\n" + decoration);
                    Console.WriteLine($"Word: {string.Join(' ', hiddenWord)}");
                    Console.WriteLine(decoration + "\n");

                    if (randomWord == new string(hiddenWord))
                    {
                        Utils.DrawGameResult(isWin: true);
                        return;
                    }

                    if (attemps == 7) break;

                    Console.Write("Pick a letter: ");
                    guessedLetter = Console.ReadKey().KeyChar.ToString().ToLower().ToCharArray()[0];

                    if (!char.IsLetter(guessedLetter))
                    {
                        Console.WriteLine(
                            "\nThat is not valid. Use only letters from aA to zZ. Press any key to retry"
                        );
                        Console.ReadKey();
                    }
                    else
                    {
                        if (!randomWord.Contains(guessedLetter.ToString().ToLower()))
                        {
                            if (guessedLetters.Contains(guessedLetter))
                            {
                                Console.WriteLine(
                                    "\nYou have already chosen that letter. Press any key to try again."
                                );
                                Console.ReadKey();
                            }
                            else
                            {
                                attemps++;
                                guessedLetters.Add(guessedLetter);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < randomWord.Length; i++)
                            {
                                if (randomWord[i] == guessedLetter)
                                    hiddenWord[i] = guessedLetter;
                            }
                        }
                    }                 
                }

                Console.WriteLine($"The word was: {randomWord}");
                Utils.DrawGameResult(isWin: false);
            }

            public static void ExitGame()
            {
                Console.WriteLine("\nThanks for playing! Goodbye :)");
                Environment.Exit(0);
            }

            public static int SelectOption()
            {
                Console.Clear();

                Utils.DrawGameTitle();

                string decoration = new('=', 15);

                Console.WriteLine(decoration);

                Console.WriteLine(" 1. New game");
                Console.WriteLine(" 2. Add word");
                Console.WriteLine(" 3. Exit");

                Console.WriteLine(decoration + "\n");

                Console.Write("Select an option: ");

                if (!int.TryParse(Console.ReadLine(), out int option) || option < 1 || option > 3)
                {
                    Console.WriteLine("\n|- Invalid option. Press any key to retry -|");
                    Console.ReadLine();

                    option = SelectOption();
                }

                return option;
            }

            public static void AddWord()
            {
                Console.Clear();

                Utils.DrawGameTitle();

                Console.Write("\nType the word you want to add. Max 20 chars.\n");

                string decoration = new('=', 44);

                Console.WriteLine(decoration);

                Console.Write("Word: ");

                string word = Console.ReadLine() ?? "";
                bool isValidWord = Utils.IsValidWord(word);

                if (isValidWord)
                {
                    string filePath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        "Words.csv"
                    );

                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.Write($"{word.ToLower()};");
                    }

                    Console.WriteLine("\n|- Word added successfully! Press any key to continue -|");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\n|- Invalid word. Press any key to retry -|");
                    Console.ReadLine();

                    AddWord();
                }
            }

            public static void DrawHangman(int attemps)
            {
                Console.WriteLine("   __________");
                Console.WriteLine("   |        ¿");
                Console.WriteLine($"   |        {(attemps >= 1 ? "O" : "")}");

                if (attemps == 7)
                    Console.WriteLine("   |       ---");

                Console.WriteLine(
                    $"   |       {(attemps >= 3 ? "\\" : " ")}{(attemps >= 2 ? "|" : "")}{(attemps >= 4 ? "/" : "")}"
                );
                Console.WriteLine($"   |        {(attemps >= 2 ? "|" : "")}");
                Console.WriteLine(
                    $"   |       {(attemps >= 5 ? "/" : "")} {(attemps >= 6 ? "\\" : "")}"
                );
                Console.WriteLine("   |");
                Console.WriteLine(" __|__");
            }
        }
    }
}
