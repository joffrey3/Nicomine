using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharacterFacingBlock : MonoBehaviour
{
    public GameObject Debug_CharacterFacingBlock = null;
    public GameObject Debug_CharacterPos = null;

    private CharacterMovement characterMovement = null;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x;
        float posY = transform.position.y;

        CharacterMovement.JoystickFacingDirection joystickFacingDirection = CharacterMovement.JoystickFacingDirection.None;
        bool isPlayerFacingLeft = false;
        if (characterMovement != null)
        {
            isPlayerFacingLeft = characterMovement.IsPlayerFacingLeft();
            joystickFacingDirection = characterMovement.GetJoystickFacingDirection();
        }
        int targetX = Mathf.RoundToInt(posX);
        int targetY = Mathf.FloorToInt(posY+0.5f); // +0.5f pour que ça soit en face de la tête du perso 😎

        switch (joystickFacingDirection)
        {
            case CharacterMovement.JoystickFacingDirection.Up:
                //targetX = Mathf.RoundToInt(posX);
                targetY++;
                break;
            case CharacterMovement.JoystickFacingDirection.Down:
                //targetX = Mathf.RoundToInt(posX);
                targetY--;
                break;
            case CharacterMovement.JoystickFacingDirection.Left:
            case CharacterMovement.JoystickFacingDirection.Right:
            case CharacterMovement.JoystickFacingDirection.None:
                int targetDir = isPlayerFacingLeft ? -1 : 1;
                targetX += targetDir;
                break;
        }


        if (Debug_CharacterFacingBlock != null)
            Debug_CharacterFacingBlock.transform.position = new Vector3(targetX, targetY, -7);

        if (Debug_CharacterPos != null)
            Debug_CharacterPos.transform.position = new Vector3(posX, posY, -2);
    }
}
