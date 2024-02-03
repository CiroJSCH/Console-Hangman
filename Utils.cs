namespace Hangman
{
    internal partial class Program
    {
        public static class Utils
        {
            public static void DrawGameTitle()
            {
                Console.WriteLine("\r\n  _   _    _    _   _  ____ __  __    _    _   _ \r\n | | | |  / \\  | \\ | |/ ___|  \\/  |  / \\  | \\ | |\r\n | |_| | / _ \\ |  \\| | |  _| |\\/| | / _ \\ |  \\| |\r\n |  _  |/ ___ \\| |\\  | |_| | |  | |/ ___ \\| |\\  |\r\n |_| |_/_/   \\_\\_| \\_|\\____|_|  |_/_/   \\_\\_| \\_|\r\n\r\n");
            }

            public static List<string> GetWordsList()
            {
                string filePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Words.csv"
                );

                if (!File.Exists(filePath))
                {
                    CreateWordsFile();
                }

                var wordsList = new List<string>();

                using (var reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var words = line?.Split(';') ?? [];

                        foreach (var word in words)
                        {   
                            if (word.Trim() != string.Empty) wordsList.Add(word.Trim());                          
                        }
                    }
                }

                return wordsList;
            }

            public static void CreateWordsFile()
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "Words.csv");

                StreamWriter writer = File.CreateText(filePath);

                // Some defaults words
                writer.WriteLine("apple;banana;cherry;date;elderberry;fig;grape;honeydew;kiwi;lemon;mango;nectarine;orange;papaya;quince;raspberry;strawberry;watermelon");

                writer.Close();
            }

            public static bool IsValidWord(string word)
            {
                if (word.Length > 20 || word == string.Empty) return false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (!char.IsLetter(word[i])) return false;
                }

                return true;
            }

            public static string GetRandomWord()
            {
                var words = GetWordsList();
                int randomIndex = new Random().Next(0, words.Count);

                return words[randomIndex];
            }
        }
    }
}
