using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntidoteButtonScript : MonoBehaviour
{
    private CharacterSpriteManager characterSprite;
    private GameManager gameManager;
    private Inventory stockage;
    public GameObject Popup;
    private int peopleHeal;
    public TMP_Text VillagerHealCounter;
    void Start()
    {
        stockage = GameObject.FindObjectsOfType<Inventory>()[0];
        gameManager = GameObject.FindObjectsOfType<GameManager>()[0];
        characterSprite = GameObject.FindObjectsOfType<CharacterSpriteManager>()[0];
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(HealChoice);
    }
    private void HealChoice()
    {
        if (characterSprite.getIsPlayerInVillage() && gameManager.getSickPeople() > 0 && stockage.Antidotes != 0)
        {
            Popup.SetActive(true);
            int nbSick = gameManager.getSickPeople();
            if (nbSick >= stockage.Antidotes)
            {
                setPeopleHeal(stockage.Antidotes);
            }
            else
            {
                setPeopleHeal(nbSick);
            }
        }
    }

    public void setPeopleHeal(int people)
    {
        VillagerHealCounter.text = people.ToString();
        peopleHeal = people;
    }

    public int getPeopleHeal()
    {
        return peopleHeal;
    }
}
