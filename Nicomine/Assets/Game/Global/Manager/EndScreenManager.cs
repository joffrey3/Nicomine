using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class EndScreenManager : MonoBehaviour
{
    public TMP_Text Result;
    
    public TMP_Text Score;
    
    public TMP_Text Reason;

    GameInformations gameInformations = null;

    private void Start()
    {
        gameInformations = GameInformations.GetGameInformations();

        if (gameInformations.didPlayerWin)
            GameWon(gameInformations.playerScore);
        else
            GameLost(gameInformations.playerScore, gameInformations.loseReason);
    }

    public void GameWon(int score)
    {
        if(Result != null)
        {
            Result.text = "Gagné !";
        }
        FillTextMeshPro(score,  null);
    }

    public void GameLost(int score, string reason)
    {
        if(Result != null) 
        {
            Result.text = "Perdu";
        }
        FillTextMeshPro(score, reason);
    }

    void FillTextMeshPro(int score, string reason)
    {
        if(Score != null)
        {
            Score.text = score.ToString();
        }
        if(Reason != null)
        {
            Reason.text = reason.ToString();
        }
    }

    public void OnMainMenuButtonClicked()
    {
        //SceneManager.LoadScene("", LoadSceneMode.Additive); // LOAD LA SCENE DACCUEIL
        Debug.Log("Accueil");
    }

    public void OnRestartButtonClicked()
    {
        Debug.Log("restart");
        try
        {
            SceneManager.UnloadSceneAsync("MainScene");
        }catch(Exception e)
        { 
            Debug.Log(e);
        }
        SceneManager.LoadSceneAsync("MainScene");
    }
}
