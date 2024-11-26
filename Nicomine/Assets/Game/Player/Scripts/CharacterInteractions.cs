using UnityEngine;
using static CharacterMovement;

public class CharacterInteractions : MonoBehaviour
{
    public MapGenerator mapGenerator;

    private CharacterMovement characterMovement = null;

    private CharacterLife characterLife = null;

    public MiningButton miningButton = null;

    public bool isPlayerMining = false;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterLife = GetComponent<CharacterLife>();
    }

    public void Update()
    {
        if (miningButton.isButtonPressed)
        {
            mineBlock();
        }
    }

    public void mineBlock()
    {
        if(mapGenerator != null && characterMovement != null)
        {
            Vector2 coords = GetFacingBlock();
            mapGenerator.MineBlock((int)coords.x, (int)coords.y);
        }
    }

    private Vector2 GetFacingBlock()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;
        int targetX = Mathf.RoundToInt(posX);
        int targetY = Mathf.FloorToInt(posY + 0.5f); // +0.5f pour que ça soit en face de la tête du perso 😎

        JoystickFacingDirection joystickFacingDirection = JoystickFacingDirection.None;
        bool isPlayerFacingLeft = false;
        if (characterMovement != null)
        {
            isPlayerFacingLeft = characterMovement.IsPlayerFacingLeft();
            joystickFacingDirection = characterMovement.GetJoystickFacingDirection();
        }

        switch (joystickFacingDirection)
        {
            case JoystickFacingDirection.Up:
                targetY++;
                break;
            case JoystickFacingDirection.Down:
                targetY--;
                break;
            case JoystickFacingDirection.Left:
            case JoystickFacingDirection.Right:
            case JoystickFacingDirection.None:
                int targetDir = isPlayerFacingLeft ? -1 : 1;
                targetX += targetDir;
                break;
        }

        return new(targetX, targetY);
    }
}
