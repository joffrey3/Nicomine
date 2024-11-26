using UnityEngine;
using UnityEngine.UI;


public class VillagerHeal : MonoBehaviour
{
    private GameManager gameManager;
    private Inventory stockage;
    private AntidoteButtonScript antidoteButtonScript;
    // Start is called before the first frame update
    void Start()
    {
        stockage = GameObject.FindObjectsOfType<Inventory>()[0];
        gameManager = GameObject.FindObjectsOfType<GameManager>()[0];
        antidoteButtonScript = GameObject.FindObjectsOfType<AntidoteButtonScript>()[0];
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(HealAllVillager);
    }

    void HealAllVillager()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        int peopleHeal = antidoteButtonScript.getPeopleHeal();
        Debug.Log(peopleHeal);
        for (int increment = 0; increment < peopleHeal; increment++)
        {
            gameManager.AddToScore(ScoreValue.VILLAGER_HEALED);
        }
        int nbSick = gameManager.getSickPeople();
        if (nbSick >= stockage.Antidotes)
        {
            stockage.Antidotes = 0;
        }
        else
        {
            stockage.Antidotes = stockage.Antidotes - nbSick;
        }
        gameManager.setStatePeople(gameManager.getSainPeople() + peopleHeal, gameManager.getSickPeople() - peopleHeal, gameManager.getDeadPeople());
    }
}
