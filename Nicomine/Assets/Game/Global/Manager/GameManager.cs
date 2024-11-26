using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public enum ScoreValue
{
    VILLAGER_HEALED = 500,
    GEYSER_PLUGGED = 1000
}

public class GameManager : MonoBehaviour
{
    private int _score = 0;
    public TMP_Text sainPeopleText;
    public TMP_Text sickPeopleText;
    public TMP_Text deadPeopleText;
    public TMP_Text scoreText;
    public int startSainPeople = 100;
    public int startSickPeople = 0;
    public int startDeadPeople = 0;
    private CorkButtonScript geyserScript;
    public int timePeriod = 10;
    GameInformations gameInformations = GameInformations.GetGameInformations();
    public bool endScreenShown = false;

    void Start()
    {
        geyserScript = GameObject.FindObjectsOfType<CorkButtonScript>()[0];
        setStatePeople(startSainPeople, startSickPeople, startDeadPeople);
    }

    public void RefreshScoreText()
    {
        scoreText.text = $"Score : {_score.ToString()}";
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddToScore(int value)
    {
        _score += (int)(value / Mathf.Log10(Time.timeSinceLevelLoad + 10));
    }

    public void AddToScore(ScoreValue value)
    {
        _score += (int)value;
    }

    public void SetScore(int score)
    {
        _score = score;
    }

    public void ResetScore()
    {
        _score = 0;
    }

    public void setStatePeople(int healthy, int sick, int dead)
    {
        sainPeopleText.text = healthy.ToString();
        sickPeopleText.text = sick.ToString();
        deadPeopleText.text = dead.ToString();
    }
    public int getSainPeople()
    {
        return int.Parse(sainPeopleText.text);
    }
    public int getSickPeople()
    {
        return int.Parse(sickPeopleText.text);
    }
    public int getDeadPeople()
    {
        return int.Parse(deadPeopleText.text);
    }

    public void ChangePeopleState(int time)
    {
        if (time % timePeriod == 0)
        {
            ChangeSickPeople(time);
            if (time > 150)
            {
                ChangeDeadPeople(time);
            }
        }
    }
    public void ChangeSickPeople(int time)
    {
        int nbGeysers = geyserScript.getOpenGeysers();
        int nbMalades = getSickPeople()+ getDeadPeople(); ;
        int settingSickPeople = nbGeysers * time*10 * (nbMalades + 1) * 100;
        int newSickPeople = 0;
        while (settingSickPeople>=1)
        {
            settingSickPeople = settingSickPeople / 10;
            newSickPeople++;
        }
        if (getSainPeople() - newSickPeople < 0){
            setStatePeople(0, getSickPeople() + getSainPeople() , getDeadPeople());
        }
        else {
            setStatePeople(getSainPeople() - newSickPeople, getSickPeople() + newSickPeople, getDeadPeople());
        }
    }
    void ChangeDeadPeople(int time)
    {
        int nbGeysers = geyserScript.getOpenGeysers();
        int nbDead = getSickPeople();
        int settingDeadPeople = nbGeysers * time*10 * (nbDead + 1) * 1000;
        int newDeadPeople = 0;
        while (settingDeadPeople >= 1)
        {
            settingDeadPeople = settingDeadPeople / 10;
            newDeadPeople++;
        }
        if (getSickPeople() - newDeadPeople < 0)
        {
            setStatePeople(getSainPeople(), 0, getDeadPeople() + getSickPeople());
        }
        else
        {
            setStatePeople(getSainPeople(), getSickPeople() - newDeadPeople, getDeadPeople() + newDeadPeople);
        }
    }

    private void Update()
    {
        if(!endScreenShown)
        {
            if (getDeadPeople() == startSainPeople)
            {
                gameInformations.didPlayerWin = false;
                gameInformations.playerScore = GetScore();
                gameInformations.loseReason = "Tous les villageois sont morts.";
                SceneManager.LoadSceneAsync("EndScreen", LoadSceneMode.Additive);
                endScreenShown = true;
            }
            Debug.Log(geyserScript.getPlugText());
            if (geyserScript.getPlugText() == 4 && getSainPeople() > 0 && getSickPeople() == 0)
            {
                gameInformations.didPlayerWin = true;
                gameInformations.playerScore = GetScore();
                SceneManager.LoadSceneAsync("EndScreen", LoadSceneMode.Additive);
                endScreenShown = true;
            }
        }
    }
}
