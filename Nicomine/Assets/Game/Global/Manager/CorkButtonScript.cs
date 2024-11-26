using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CorkButtonScript : MonoBehaviour
{
    private Inventory stockage;
    private int geyserNumber = 0;
    private List<GameObject> allGeyser;
    private List<int> allGeyserState;
    private List<bool> allGeyserTrigger;
    public Sprite spriteClose;
    public Sprite spritePlug;
    private int nbGeysers = 0;
    public int timePeriod = 10;
    public TMP_Text closeGeysersText;
    public TMP_Text openGeysersText;
    public TMP_Text plugGeysersText;
    private GameManager gameManager;
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(OnClickButton);
        gameManager = GameObject.FindObjectsOfType<GameManager>()[0];
        stockage = GameObject.FindObjectsOfType<Inventory>()[0];
        SingleGeyserScript[] allGeyserScript = GameObject.FindObjectsOfType<SingleGeyserScript>();
        allGeyser = new();
        allGeyserState = new();
        allGeyserTrigger = new();
        for (int increment = 0; increment < allGeyserScript.Length; increment++)
        {
            allGeyser.Add(allGeyserScript[increment].gameObject);
            allGeyser[increment].GetComponent<SpriteRenderer>().sprite = spriteClose;
            allGeyserState.Add(0);
            allGeyserTrigger.Add(false);

        }
        SetGeyserNumber(allGeyser.Count);
        setGeysersText(allGeyser.Count, 0, 0);
    }
    private void setGeysersText(int close, int open, int plug)
    {
        closeGeysersText.text = close.ToString();
        openGeysersText.text = open.ToString();
        plugGeysersText.text = plug.ToString();
    }

    private int getCloseText()
    {
        return System.Int32.Parse(closeGeysersText.text);
    }

    private int getOpenText()
    {
        return System.Int32.Parse(openGeysersText.text);
    }

    public int getPlugText()
    {
        return System.Int32.Parse(plugGeysersText.text);
    }

    private void SetGeyserNumber(int number)
    {
        geyserNumber = number;
    }

    public void ChangeGeyserState(int time)
    {
        if (time % timePeriod == 0)
        {
            OpenGeyser();
        }
    }

    public void OpenGeyser()
    {
        bool flag = false;
        foreach (int isGeyserOpen in allGeyserState)
        {
            if (isGeyserOpen == 0)
            {
                flag = true;
                break;
            }
        }
        if (!flag)
        {
            return;
        }

        while (true)
        {
            int indexRandom = new System.Random().Next(0, geyserNumber);
            int elementRandom = allGeyserState[indexRandom];
            if (elementRandom == 0)
            {
                setGeysersText(getCloseText() - 1, getOpenText() + 1, getPlugText());
                allGeyserState[indexRandom] = 1;
                setOpenGeysers(getOpenGeysers() + 1);

                allGeyser[indexRandom].transform.GetChild(0).gameObject.SetActive(true);
                return;
            }
        }
    }

    public void CloseGeyser(int increment)
    {
        
        if(allGeyserState[increment] == 2)
        {
            return;
        }
        if (allGeyserState[increment] == 1)
        {
            setGeysersText(getCloseText(), getOpenText() - 1, getPlugText() + 1);
            setOpenGeysers(getOpenGeysers() - 1);
        }
        if (allGeyserState[increment] == 0)
        {
            setGeysersText(getCloseText() - 1, getOpenText(), getPlugText() + 1);
        }
        gameManager.AddToScore(ScoreValue.GEYSER_PLUGGED);
        allGeyserState[increment] = 2;
        allGeyser[increment].GetComponent<SpriteRenderer>().sprite = spritePlug;
        allGeyser[increment].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();
    }
    public int getOpenGeysers()
    {
        return nbGeysers;
    }
    public void setOpenGeysers(int geysers)
    {
        nbGeysers = geysers;
    }
    public void setTrigger(GameObject child, bool state)
    {
        allGeyserTrigger[allGeyser.IndexOf(child)] = state;
    }


    void OnClickButton()
    {
        if (stockage.Corks != 0)
        {
            stockage.Corks--;
            for (int increment = 0;increment < allGeyserTrigger.Count; increment++)
            {
                if (allGeyserTrigger[increment]==true)
                {
                    this.CloseGeyser(increment);
                }
            }
        }
    }
}
