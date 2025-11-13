using System;
using System.Collections.Generic;

class Dice
{
    private Random r = new Random();
    private int[] dice = new int[5];

    public void DiceRoll()
    {
        for (int i = 0; i < 5; i++)
            dice[i] = r.Next(1, 7);
    }

    public void RerollExcept(List<int> held)
    {
        for (int i = 0; i < 5; i++)
        {
            if (!held.Contains(i))
                dice[i] = r.Next(1, 7);
        }
    }

    public int[] getDices()
    {
        return dice;
    }
}