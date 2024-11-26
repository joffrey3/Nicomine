using UnityEngine;

public class ItemBlock : Block
{
    public Items Item;
    public int ScoreValue;

    public override void OnBlockBreak()
    {
        base.OnBlockBreak();
        GameObject managerObject = GameObject.FindGameObjectWithTag("GameController");
        GameManager manager = managerObject.GetComponent<GameManager>();
        manager.AddToScore(ScoreValue);

        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.AddItem(Item);
    }
}
