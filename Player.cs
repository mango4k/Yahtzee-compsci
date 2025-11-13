public class Player{
    private int[] dicePicked = new int[5];
    private int[] dices = new int[5];
    private int numOfDiceToRoll;
    private int diceChoice;


    public Player(int[] dicesTemp){
        for (int i = 0; i < dices.Length; i++) {dices[i] = dicesTemp[i];}
        diceChoice = 0;
    }
    public int GetToRoll() {return numOfDiceToRoll;}
    public int[] GetDicePicked() { return dicePicked; }
    public void SetDices(int[] dicesTemp)
    {
        for (int i = 0; i < dices.Length; i++) {dices[i] = dicesTemp[i];}
    }


    public void PickDices(){
        for (int i = 0; i < dices.Length; i++) { Console.WriteLine($"{i + 1}. {dices[i]}"); }
        Console.Write("Pick a Dice: ");
        int choice = int.Parse(Console.ReadLine()) - 1;


        if (dices[choice] > 0)
        {
            dicePicked[diceChoice] = dices[choice];
            dices[choice] = 0;
            diceChoice++;
            numOfDiceToRoll = dices.Length;
        }
        else{ Console.WriteLine("Already picked that dice"); }
        for (int i = 0; i < dicePicked.Length; i++) { Console.WriteLine($"{i + 1}. {dicePicked[i]}"); }
    }
}

