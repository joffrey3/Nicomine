using UnityEngine;
using UnityEngine.UI;

public class PlayerHeal : MonoBehaviour
{
    private Inventory stockage;
    private CharacterLife characterLife;
    // Start is called before the first frame update
    void Start()
    {
        stockage = GameObject.FindObjectsOfType<Inventory>()[0];
        characterLife = GameObject.FindObjectsOfType<CharacterLife>()[0];
        Button button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(HealPlayer);
    }

    void HealPlayer()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
        stockage.Antidotes--;
        characterLife.SetLifePoints(characterLife.GetMaxLifePoints());


    }
}
