using System;
using System.Collections.Generic;

class Table
{
    public static void Scoresheet(
        int[] dice,
        List<int> held,
        Dictionary<string, int> preview,
        Dictionary<string, int> finalScores,
        score scorer,
        int roundNumber)   // <—— ADDED
    {
        Console.Clear();

        Console.WriteLine("===========================================");
        Console.WriteLine("            YAHTZEE ROUND: " + roundNumber);
        Console.WriteLine("===========================================");
        Console.WriteLine();

        Console.Write("Dice: ");
        for (int i = 0; i < dice.Length; i++)
        {
            if (held.Contains(i))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[" + dice[i] + "] ");
            }
            else
            {
                Console.ResetColor();
                Console.Write(dice[i] + " ");
            }
        }

        Console.ResetColor();
        Console.WriteLine("\n");

        Console.WriteLine("Category".PadRight(22) + "Score");
        Console.WriteLine("-------------------------------------------");

        int total = 0;

        string[] catOrder = {
            "Ones","Twos","Threes","Fours","Fives","Sixes",
            "3Kind","4Kind","FullHouse","SmallStraight",
            "LargeStraight","Yahtzee","Chance"
        };

        for (int i = 0; i < catOrder.Length; i++)
        {
            string name = catOrder[i];
            int number = i + 1;

            int lockedScore = finalScores[name];
            int prev = preview.ContainsKey(name) ? preview[name] : 0;

            bool used = IsUsed(name, scorer);

            if (used)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(($"[{number}] {name}").PadRight(22) + lockedScore);
                total += lockedScore;
            }
            else
            {
                if (prev > 0)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.WriteLine(($"[{number}] {name}").PadRight(22) + prev);
            }
        }

        Console.ResetColor();
        Console.WriteLine("-------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("TOTAL SCORE: ".PadRight(22) + total);
        Console.ResetColor();
        Console.WriteLine("===========================================\n");
    }

    private static bool IsUsed(string n, score s)
    {
        return n switch
        {
            "Ones" => s.ones,
            "Twos" => s.twos,
            "Threes" => s.threes,
            "Fours" => s.fours,
            "Fives" => s.fives,
            "Sixes" => s.sixs,
            "3Kind" => s.threeOfAKind,
            "4Kind" => s.fourOfAKind,
            "FullHouse" => s.fullHouse,
            "SmallStraight" => s.smlStright,
            "LargeStraight" => s.lrgStraight,
            "Yahtzee" => s.yahtzee,
            "Chance" => s.chance,
            _ => false
        };
    }
}