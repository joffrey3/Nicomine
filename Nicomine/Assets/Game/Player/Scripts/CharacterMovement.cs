using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Direction None si joystick parfaitement au centre
    public enum JoystickFacingDirection { Up, Down, Left,  Right, None };

    public Lean.Gui.LeanJoystick leanJoystick;

    public float acceleration = 2000f;

    public float maxSpeed = 0.6f;

    private new Rigidbody2D rigidbody2D = null;

    private CharacterSpriteManager characterSpriteManager = null;

    private Vector3 movement;

    // Permet de savoir 
    private JoystickFacingDirection joystickFacingDirection = JoystickFacingDirection.None;

    private bool isPlayerFacingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        characterSpriteManager = GetComponent<CharacterSpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leanJoystick != null)
        {
            float joyX = Mathf.Clamp(leanJoystick.ScaledValue.x + Input.GetAxis("Horizontal"), -1, 1);
            float joyY = Mathf.Clamp(leanJoystick.ScaledValue.y + Input.GetAxis("Vertical"), -1, 1);

            // Le vecteur n'est pas normaliser pour que l'utilisateur puisse choisir la vitesse avec le joystick.
            movement = new Vector3(joyX, joyY, 0);

            // Si le joystick est centré, on ne change pas la direction du personnage.
            if (joyX != 0)
            {
                // Joystick.X < 0 Si le joystick est vers la gauche
                characterSpriteManager.ChooseFacingDirection(joyX < 0);
                isPlayerFacingLeft = joyX < 0;
            }

            UpdateJoystickDirection(joyX, joyY);
            //Debug.Log("x:" + joyX + " y:" + joyY + " dir:" + joystickFacingDirection.ToString());
            
        }
    }

    // FixedUpdate may be called multiple times per frame
    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    private void UpdateJoystickDirection(float joyX, float joyY)
    {
            if (joyX == 0 && joyY == 0)
                joystickFacingDirection = JoystickFacingDirection.None;
            else if (Mathf.Abs(joyY) > Mathf.Abs(joyX) && joyY > 0)
                joystickFacingDirection = JoystickFacingDirection.Up;
            else if (Mathf.Abs(joyY) > Mathf.Abs(joyX) && joyY < 0)
                joystickFacingDirection = JoystickFacingDirection.Down;
            else if (joyX < 0)
                joystickFacingDirection = JoystickFacingDirection.Left;
            else
                joystickFacingDirection = JoystickFacingDirection.Right;
    }

    public void MoveCharacter(Vector3 direction)
    {
        if(rigidbody2D != null)
        {
            float speed = acceleration * Time.fixedDeltaTime;
            Vector3 dir = direction * Mathf.Min(speed, maxSpeed);

            rigidbody2D.velocity += new Vector2(dir.x, dir.y);
            
            //Debug.Log("Speed: " + dir.magnitude.ToString());
            //Debug.Log(rigidbody2D.velocity);
        }
    }

    public bool IsPlayerFacingLeft()
    { 
        return isPlayerFacingLeft; 
    }

    public JoystickFacingDirection GetJoystickFacingDirection()
    {
        return joystickFacingDirection;
    }
}
