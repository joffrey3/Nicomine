using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteManager : MonoBehaviour
{
    private const int SPRITE1 = 0;
    private const int SPRITE2 = 1;
    private const int MASKEDSPRITE1 = 2;
    private const int MASKEDSPRITE2 = 3;

    public float turnSpeed = 5f;

    public float deathAnimationSpeed = 2f;

    public Sprite spriteCharacter = null;
    public Sprite spriteCharacter2 = null;
    
    public Sprite spriteMaskedCharacter = null;
    public Sprite spriteMaskedCharacter2 = null;

    private SpriteRenderer spriteRenderer = null;

    private CharacterInteractions characterInteractions = null;

    private Quaternion m_Rotation = Quaternion.Euler(0, 0, 0);

    private Quaternion m_DeadRotation = Quaternion.Euler(0, 0, 90);

    public bool isPlayerDead = false;

    private bool isPlayerInVllage;
    
    public float animationDelay = 0.1234f;

    private float animationTimer = 0.0f;

    private int spriteIndex = SPRITE1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterInteractions = GetComponent<CharacterInteractions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, m_Rotation * m_DeadRotation, Time.deltaTime * deathAnimationSpeed);
            return;
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, m_Rotation, Time.deltaTime * turnSpeed);

        animationTimer += Time.deltaTime;

        if(spriteRenderer != null)
        {
            float posY = transform.position.y;

            setIsPlayerInVillage(posY >= 0);

            if (posY >= 0 && (spriteIndex == MASKEDSPRITE1 || spriteIndex == MASKEDSPRITE2))
            {
                spriteRenderer.sprite = spriteCharacter;
                spriteIndex = SPRITE1;
            }
            else if (posY < 0 && (spriteIndex == SPRITE1 || spriteIndex == SPRITE2))
            {
                spriteRenderer.sprite = spriteMaskedCharacter;
                spriteIndex = MASKEDSPRITE1;
            }

            if(characterInteractions != null && characterInteractions.isPlayerMining)
            {
                if (animationTimer > animationDelay)
                {
                    animationTimer = 0.0f;
                    switch (spriteIndex)
                    {
                        case SPRITE1:
                            spriteRenderer.sprite = spriteCharacter2;
                            spriteIndex = SPRITE2;
                            break;
                        case SPRITE2:
                            spriteRenderer.sprite = spriteCharacter;
                            spriteIndex = SPRITE1;
                            break;
                        case MASKEDSPRITE1:
                            spriteRenderer.sprite = spriteMaskedCharacter2;
                            spriteIndex = MASKEDSPRITE2;
                            break;
                        case MASKEDSPRITE2:
                            spriteRenderer.sprite = spriteMaskedCharacter;
                            spriteIndex = MASKEDSPRITE1;
                            break;
                    }
                    //Debug.Log("SWITCH " + spriteIndex);
                }
            }
            else
            {
                switch (spriteIndex)
                {
                    case SPRITE1:
                    case SPRITE2:
                        spriteRenderer.sprite = spriteCharacter;
                        spriteIndex = SPRITE1;
                        break;
                    case MASKEDSPRITE1:
                    case MASKEDSPRITE2:
                        spriteRenderer.sprite = spriteMaskedCharacter;
                        spriteIndex = MASKEDSPRITE1;
                        break;
                }
            }
        }
    }

    public void ChooseFacingDirection(bool lookingLeft)
    {
        if (spriteRenderer != null)
        {
            if (lookingLeft)
            {
                m_Rotation = Quaternion.Euler(0, 180, 0);
            }else
            {
                m_Rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void setIsPlayerInVillage(bool isInVillage)
    {
        isPlayerInVllage = isInVillage;
    }
    public bool getIsPlayerInVillage()
    {
        return isPlayerInVllage;
    }

    public void playDeathAnimation()
    {
        isPlayerDead = true;
    }
}
