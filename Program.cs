using System;

namespace delegatesAndEvents
{
    public delegate void RaceCompletedEventHandler(int winner);

    public class Race
    {
        public event RaceCompletedEventHandler RaceCompleted;

        public void Racing(int contestants, int laps)
        {
            Console.WriteLine("Ready\nSet\nGo!");
            Random r = new Random();
            int[] participants = new int[contestants];
            bool done = false;
            int champ = -1;

            while (!done)
            {
                for (int i = 0; i < contestants; i++)
                {
                    participants[i] += r.Next(1, 5);
                    if (participants[i] >= laps)
                    {
                        champ = i;
                        done = true;
                        break; 
                    }
                }
            }

            TheWinner(champ + 1);
        }

        private void TheWinner(int champ)
        {
            Console.WriteLine("We have a winner!");
            RaceCompleted?.Invoke(champ);
        }
    }

    class Program
    {
        public static void Main()
        {
            Race round1 = new Race();

            round1.RaceCompleted += footRace;
            round1.Racing(5, 10);

            round1.RaceCompleted += carRace;
            round1.Racing(5, 20);

            round1.RaceCompleted += winner => Console.WriteLine($"Bike racer number {winner} is the winner.");
            round1.Racing(5, 15);
        }

        public static void carRace(int winner)
        {
            Console.WriteLine($"Car number {winner} is the winner.");
        }

        public static void footRace(int winner)
        {
            Console.WriteLine($"Racer number {winner} is the winner.");
        }
    }
}