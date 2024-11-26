using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeScript : MonoBehaviour
{
    private CorkButtonScript geysers;
    private GameManager gameManager;
    public TMP_Text Timer;
    public int baseGameTime = 300;
    private int gameTime = 0;
    private float prevTime = 0;
    GameInformations gameInformations = GameInformations.GetGameInformations();

    public void Start()
    {
        geysers = GameObject.FindObjectsOfType<CorkButtonScript>()[0];
        gameManager = GameObject.FindObjectsOfType<GameManager>()[0];
        gameTime = baseGameTime;
    }
    public void FixedUpdate()
    {
        float calculatedTime = prevTime + Time.fixedDeltaTime;
        if (Mathf.Floor(calculatedTime) > Mathf.Floor(prevTime) && gameTime>0)
        {
            gameTime = gameTime - 1;
            setTimeText(gameTime);
            geysers.ChangeGeyserState(baseGameTime - gameTime);
            gameManager.ChangePeopleState(baseGameTime - gameTime);
            gameManager.RefreshScoreText();
        }

        prevTime = calculatedTime;

        if(!gameManager.endScreenShown && gameTime <= 0)
        {
            gameInformations.didPlayerWin = false;
            gameInformations.playerScore = gameManager.GetScore();
            gameInformations.loseReason = "Le temps est écoulé.";
            SceneManager.LoadSceneAsync("EndScreen", LoadSceneMode.Additive);
            gameManager.endScreenShown = true;
        }
    }

    private void setTimeText(int gameTime){
        int seconds = gameTime % 60;
        int minutes = gameTime / 60;

        Timer.text = $"{minutes}:{seconds:D2}";
    }
}