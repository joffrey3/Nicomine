public class GameInformations
{
    private static GameInformations instance;

    public static GameInformations GetGameInformations()
    {
        if(instance == null)
            instance = new GameInformations();
        return instance;
    }

    public bool didPlayerWin = false;
    public int playerScore = 0;
    public string loseReason = "";
}