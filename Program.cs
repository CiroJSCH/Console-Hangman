namespace Hangman
{
    internal partial class Program
    {
        static void Main()
        {
            Console.Title = "Hangman - Created by Ciro Schapert";

            int option = Game.SelectOption();
            switch (option)
            {
                case 1:
                    Utils.GetWordsList();
                    break;
                case 2:
                    Game.AddWord();
                    break;
                case 3:
                    Game.ExitGame(); 
                    break;
            }
        }
    }
}
