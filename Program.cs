namespace Hangman
{
    internal partial class Program
    {
        static void Main()
        {
            int option = Game.SelectOption();
            switch (option)
            {
                case 1:
                    Console.WriteLine("Start game");
                    break;
                case 2:
                    Console.WriteLine("Add word");
                    break;
                case 3:
                    Game.ExitGame(); 
                    break;
            }
        }
    }
}
