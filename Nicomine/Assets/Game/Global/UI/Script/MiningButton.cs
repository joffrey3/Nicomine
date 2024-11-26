using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiningButton : Button
{
    public bool isButtonPressed = false;

    public float timePressed = 0.0f;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        isButtonPressed = true;

        timePressed = 0.0f;

        //Debug.Log("Down");
        //show text
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        
        isButtonPressed = false;

        //Debug.Log("Up");
        //hide text
    }
}