using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterLife : MonoBehaviour
{
    public int MaxlifePoints = 5;
    public int lifePoints;
    private CharacterSpriteManager characterSpriteManager = null;

    // Start is called before the first frame update
    void Start()
    {
        lifePoints = MaxlifePoints;
        characterSpriteManager = GetComponent<CharacterSpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLifePoints()
    {
        return lifePoints;
    }

    public int GetMaxLifePoints()
    {
        return MaxlifePoints;
    }

    public void SetLifePoints(int value)
    {
        lifePoints = Mathf.Max(0, value);

        if(lifePoints == 0)
        {
            if(characterSpriteManager != null)
                characterSpriteManager.playDeathAnimation();

            // TODO lose screen
        }
    }

    public void RemoveLifePoints(int value)
    {
        if(value > 0)
            SetLifePoints(lifePoints - value);
    }
}
