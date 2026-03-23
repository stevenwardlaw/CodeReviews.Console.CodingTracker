namespace codingTracker.stevenwardlaw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool appState = true;

            CodingController.CreateTable();

            while (appState)
            {
                UserInput.DisplayOptions();
            }
            
        }

    }
}
