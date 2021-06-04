using System;
using DeckOfCards.Services;
namespace DeckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Poker poker = new Poker();
            bool isPlay = true;
            Console.WriteLine("Deck Card Game,Press S to Shuffle, Press D to Draw a card,Press P to Print all Cards,Press R to restart game ,Press Q to quit");
            while (isPlay)
            {
                string input=Console.ReadLine();

                switch (input.ToLower())
                {
                    case "q":
                        isPlay = false;
                        break;
                    case "s":
                        Console.WriteLine(poker.shuffleCards());
                        break;
                    case "d":
                        Console.WriteLine(poker.dealOneCard());
                        break;
                    case "p":
                        Console.WriteLine(poker.printAllPlayCards());
                        break;
                    case "r":
                        poker.restGame();
                        Console.WriteLine("Game is restarted");
                        break;
                    default:
                        Console.WriteLine("Please enter a valid input");
                        break;
                }
            }

        }
    }
}
