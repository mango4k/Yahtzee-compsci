using System;
using System.Collections.Generic;

public class score
{
    public bool ones = false;
    public bool twos = false;
    public bool threes = false;
    public bool fours = false;
    public bool fives = false;
    public bool sixs = false;
    public bool threeOfAKind = false;
    public bool fourOfAKind = false;
    public bool fullHouse = false;
    public bool smlStright = false;
    public bool lrgStraight = false;
    public bool yahtzee = false;
    public bool chance = false;

    public Dictionary<string, int> CalculatePreview(int[] dice)
    {
        Dictionary<string, int> p = new Dictionary<string, int>();

        int[] counts = CountValues(dice);
        int sum = SumDice(dice);

        // Upper section
        p["Ones"] = ones ? 0 : counts[1] * 1;
        p["Twos"] = twos ? 0 : counts[2] * 2;
        p["Threes"] = threes ? 0 : counts[3] * 3;
        p["Fours"] = fours ? 0 : counts[4] * 4;
        p["Fives"] = fives ? 0 : counts[5] * 5;
        p["Sixes"] = sixs ? 0 : counts[6] * 6;

        // Lower section
        p["3Kind"] = threeOfAKind ? 0 : (HasKind(counts, 3) ? sum : 0);
        p["4Kind"] = fourOfAKind ? 0 : (HasKind(counts, 4) ? sum : 0);
        p["FullHouse"] = fullHouse ? 0 : (FullHouse(counts) ? 25 : 0);
        p["SmallStraight"] = smlStright ? 0 : (SmallStraight(counts) ? 30 : 0);
        p["LargeStraight"] = lrgStraight ? 0 : (LargeStraight(counts) ? 40 : 0);

        // Yahtzee only if all 5 held & identical
        if (yahtzee)
            p["Yahtzee"] = 0;
        else if (dice.Length == 5 && YahtzeeCheck(dice))
            p["Yahtzee"] = 50;
        else
            p["Yahtzee"] = 0;

        // Chance
        p["Chance"] = chance ? 0 : sum;

        return p;
    }

    public int ApplyScore(int[] dice, string category)
    {
        Dictionary<string, int> p = CalculatePreview(dice);

        switch (category)
        {
            case "Ones": ones = true; return p["Ones"];
            case "Twos": twos = true; return p["Twos"];
            case "Threes": threes = true; return p["Threes"];
            case "Fours": fours = true; return p["Fours"];
            case "Fives": fives = true; return p["Fives"];
            case "Sixes": sixs = true; return p["Sixes"];
            case "3Kind": threeOfAKind = true; return p["3Kind"];
            case "4Kind": fourOfAKind = true; return p["4Kind"];
            case "FullHouse": fullHouse = true; return p["FullHouse"];
            case "SmallStraight": smlStright = true; return p["SmallStraight"];
            case "LargeStraight": lrgStraight = true; return p["LargeStraight"];
            case "Yahtzee": yahtzee = true; return p["Yahtzee"];
            case "Chance": chance = true; return p["Chance"];
        }

        return 0;
    }

    private int[] CountValues(int[] dice)
    {
        int[] c = new int[7];
        for (int i = 0; i < dice.Length; i++)
            c[dice[i]]++;
        return c;
    }

    private int SumDice(int[] d)
    {
        int s = 0;
        for (int i = 0; i < d.Length; i++)
            s += d[i];
        return s;
    }

    private bool HasKind(int[] c, int need)
    {
        for (int i = 1; i <= 6; i++)
            if (c[i] >= need)
                return true;
        return false;
    }

    private bool FullHouse(int[] c)
    {
        bool three = false;
        bool two = false;

        for (int i = 1; i <= 6; i++)
        {
            if (c[i] == 3) three = true;
            if (c[i] == 2) two = true;
        }

        return three && two;
    }

    private bool SmallStraight(int[] c)
    {
        return (c[1] > 0 && c[2] > 0 && c[3] > 0 && c[4] > 0) ||
               (c[2] > 0 && c[3] > 0 && c[4] > 0 && c[5] > 0) ||
               (c[3] > 0 && c[4] > 0 && c[5] > 0 && c[6] > 0);
    }

    private bool LargeStraight(int[] c)
    {
        return (c[1] > 0 && c[2] > 0 && c[3] > 0 && c[4] > 0 && c[5] > 0) ||
               (c[2] > 0 && c[3] > 0 && c[4] > 0 && c[5] > 0 && c[6] > 0);
    }

    private bool YahtzeeCheck(int[] d)
    {
        for (int i = 1; i < d.Length; i++)
            if (d[i] != d[0])
                return false;
        return true;
    }
}