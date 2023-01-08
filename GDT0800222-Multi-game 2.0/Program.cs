using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GDT0800222_Multi_game_2._0
{
    internal class Program
    {
        static int WillBetWork(int Cash) 
        {
            int BetAmount = 0;
            int BackupCash = 0;

            Console.WriteLine("Write your bet amount");
            string Input = Console.ReadLine();

            if (int.TryParse(Input, out BetAmount))
            {
                Console.Clear();

                BackupCash = Cash;

                int CheckBet = Cash - BetAmount;

                if (CheckBet < 0)
                {
                    Console.WriteLine("Can not bet more then you have");
                    Cash = BackupCash;
                    Thread.Sleep(500);
                    WillBetWork(Cash);
                }

                else
                {
                    Cash = Cash - BetAmount;
                    Console.WriteLine($"Accepted\nYou betted {BetAmount}\nYou now have {Cash} if you do not win");
                }
            }

            else
            {
                Console.WriteLine("Use numbers");
                Thread.Sleep(500);
                WillBetWork(Cash);
            }
            return BetAmount; 
        }

        static string CardSuitsMetode(Random Rnd)
        {
            string[] CardSuits = { "Clubs", "hearts", "spades", "diamonds" };
            int RandomIndex = Rnd.Next(CardSuits.Length);
            string RandomElement = CardSuits[RandomIndex];
            return RandomElement;
        }

        static int cardNumberMetode(Random Rnd) 
        {
            int[] CardNumber = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11, };
            int RandomIndex = Rnd.Next(CardNumber.Length);
            int RandomElement = CardNumber[RandomIndex];
            return RandomElement;
        }

        static bool StopLoopMetode(bool StopLoop)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            char key = Char.ToUpper(keyInfo.KeyChar);

            if (key == 'N')
            {
                StopLoop = true;
            }

            return StopLoop;
        }

        static void Main(string[] args)
        {
            int Cash = 100;
            Random Rnd = new Random();

            do
            {
                string Suit = string.Empty;
                int BetAmount = 0;
                int UserCardValue = 0;
                int PCCardValue = 0;
                int UserCardNumberGet = 0;
                int PCCardNumberGet = 0;
                Console.WriteLine("Choose your game\n1. Blackjack");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:

                        bool StopLoop = false; 
                        bool StopGame = false;
                        
                        do
                        {
                            UserCardValue = 0;
                            UserCardNumberGet = 0;
                            PCCardValue = 0;
                            PCCardNumberGet = 0;

                            BetAmount = WillBetWork(Cash);

                            Console.WriteLine("Your cards\n");

                            for (int i = 0; i < 2; i++)
                            {
                                UserCardNumberGet = cardNumberMetode(Rnd);
                                Suit = CardSuitsMetode(Rnd);
                                if (UserCardValue >= 11)
                                {
                                    UserCardNumberGet = 1;
                                }
                                Console.WriteLine($"You get a {UserCardNumberGet} of {Suit}");
                                UserCardValue = UserCardNumberGet + UserCardValue;
                                UserCardNumberGet = 0;
                            }
                            
                            Console.WriteLine($"You have {UserCardValue = UserCardNumberGet + UserCardValue} in your hand\n");
                            Console.WriteLine("Dealers Turn\n");    

                            PCCardNumberGet = cardNumberMetode(Rnd);
                            Suit = CardSuitsMetode(Rnd);
                            PCCardValue = PCCardNumberGet + PCCardValue;
                            Console.WriteLine($"The Dealer get a {PCCardNumberGet} of {Suit}\nDealer Draw a hidden card");
                            PCCardNumberGet = 0;
                            PCCardNumberGet = cardNumberMetode(Rnd);
                            Suit = CardSuitsMetode(Rnd);
                            if (PCCardValue >= 11)
                            {
                                PCCardNumberGet = 1;
                            }
                            PCCardValue = PCCardNumberGet + PCCardValue;
                            PCCardNumberGet = 0;

                            Console.WriteLine("Do you want anoter card?\nY = Yes\nN = No");
                            StopLoop = StopLoopMetode(StopLoop);
                            Console.Clear();

                            while (!StopLoop)
                            {

                                UserCardNumberGet = cardNumberMetode(Rnd);
                                Suit = CardSuitsMetode(Rnd);

                                if (UserCardNumberGet == 11)
                                {
                                    UserCardNumberGet = 1;
                                }

                                Console.WriteLine($"You get {UserCardNumberGet} of {Suit}");
                                Console.WriteLine($"You have {UserCardValue = UserCardNumberGet + UserCardValue} in your hand\n");
                                UserCardNumberGet = 0;

                                if (UserCardValue >= 21)
                                {
                                    StopLoop = true;
                                    Console.Clear();
                                }

                                else if (UserCardValue < 21)
                                {
                                    Console.WriteLine("Do you want anoter card?\nY = Yes\nN = No");
                                    StopLoop = StopLoopMetode(StopLoop);
                                    Console.Clear();
                                }
                                

                            }

                            while (PCCardValue <= 17)
                            {
                                Console.WriteLine($"The Dealer had {PCCardValue} in it's hand");
                                PCCardNumberGet = cardNumberMetode(Rnd);
                                Suit = CardSuitsMetode(Rnd);
                                if (PCCardNumberGet == 11)
                                {
                                    PCCardNumberGet = 1;
                                }
                                PCCardValue = PCCardNumberGet + PCCardValue;
                                Console.WriteLine($"The Dealer get a {PCCardNumberGet} of {Suit}");
                            }

                            Console.WriteLine($"You have {UserCardValue} in your hand\nThe Dealer have {PCCardValue} in it's hand");

                            if (UserCardValue > 21)
                            {
                                Cash = Cash - BetAmount;
                                Console.WriteLine($"You lost this round, your bet was {BetAmount} and you now have {Cash}");
                            }
                            else if (PCCardValue > 21)
                            {
                                Cash = Cash + BetAmount;
                                Console.WriteLine($"You win this round, your bet was {BetAmount} and you now have {Cash}");
                            }

                            else if (PCCardValue == UserCardValue)
                            {
                                Cash = Cash - BetAmount;
                                Console.WriteLine($"You lost this round, your bet was {BetAmount} and you now have {Cash}");
                            }
                            else if (UserCardValue < 21 && PCCardValue < 21)
                            {
                                if (UserCardValue > PCCardValue)
                                {
                                    Cash = Cash + BetAmount;
                                    Console.WriteLine($"You win this round, your bet was {BetAmount} and you now have {Cash}");
                                }
                                else if (PCCardValue > UserCardValue)
                                {
                                    Cash = Cash - BetAmount;
                                    Console.WriteLine($"You lost this round, your bet was {BetAmount} and you now have {Cash}");
                                }
                            }
                            Console.WriteLine("Do you want to play again?\nY = Yes\nN = No");
                            StopGame = StopLoopMetode(StopGame);
                            Console.Clear();
                       
                        } while (!StopGame);

                        break;

                    case ConsoleKey.D2:

                        break;

                    default:
                        break;
                }
                Console.ReadKey(true);
            } while (true);
        }
    }
}
