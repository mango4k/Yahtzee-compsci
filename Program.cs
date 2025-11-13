using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dice dice = new Dice();
        score scorer = new score();

        string[] categories = {
            "Ones","Twos","Threes","Fours","Fives","Sixes",
            "3Kind","4Kind","FullHouse","SmallStraight",
            "LargeStraight","Yahtzee","Chance"
        };

        Dictionary<string, int> finalScores = new Dictionary<string, int>();
        foreach (string c in categories)
            finalScores[c] = 0;

        for (int round = 1; round <= 13; round++)
        {
            List<int> held = new List<int>();
            dice.DiceRoll();
            int[] diceResult = dice.getDices();
            int rolls = 1;
            bool finished = false;

            while (!finished)
            {
                int[] previewDice = GetHeldDiceArray(diceResult, held);
                Dictionary<string, int> preview = scorer.CalculatePreview(previewDice);

                Table.Scoresheet(diceResult, held, preview, finalScores, scorer, round);
                
                Console.WriteLine("Roll " + rolls + "/3");
                Console.WriteLine("Type dice numbers to HOLD/UNHOLD (e.g. 1 3 5)");
                Console.WriteLine("Type 'reroll' to roll again");
                Console.WriteLine("Type 'score' to choose a category");
                Console.Write("> ");

                string input = Console.ReadLine().Trim().ToLower();

                if (input == "reroll")
                {
                    if (rolls < 3)
                    {
                        dice.RerollExcept(held);
                        diceResult = dice.getDices();
                        rolls++;
                    }
                    else
                    {
                        Console.WriteLine("No rerolls left.");
                    }
                }
                else if (input == "score")
                {
                    Console.Write("\nEnter category number: ");
                    int pick;

                    if (int.TryParse(Console.ReadLine(), out pick) &&
                        pick >= 1 && pick <= categories.Length)
                    {
                        string category = categories[pick - 1];

                        int[] scoringDice = (held.Count == 0)
                            ? new int[0]
                            : GetHeldDiceArray(diceResult, held);

                        int earned = scorer.ApplyScore(scoringDice, category);
                        finalScores[category] = earned;

                        finished = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                else
                {
                    // HOLD / UNHOLD TOGGLE
                    foreach (string part in input.Split(' '))
                    {
                        int n;
                        if (int.TryParse(part, out n) && n >= 1 && n <= 5)
                        {
                            int index = n - 1;

                            if (held.Contains(index))
                            {
                                // UNHOLD it
                                held.Remove(index);
                            }
                            else
                            {
                                // HOLD it
                                held.Add(index);
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nPress Enter for next round...");
            Console.ReadLine();
        }

        int total = 0;
        foreach (int val in finalScores.Values)
            total += val;

        Console.Clear();
        Console.WriteLine("=========== GAME OVER ============");
        Console.WriteLine("FINAL SCORE: " + total);
    }

    static int[] GetHeldDiceArray(int[] dice, List<int> held)
    {
        if (held.Count == 0)
            return new int[0];

        int[] arr = new int[held.Count];
        for (int i = 0; i < held.Count; i++)
            arr[i] = dice[held[i]];
        return arr;
    }
}