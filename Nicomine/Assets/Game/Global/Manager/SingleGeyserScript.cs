using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleGeyserScript : MonoBehaviour
{
    private GameObject player;
    private CorkButtonScript corkButtonScript;
    private void Start()
    {
        corkButtonScript = GameObject.FindObjectsOfType<CorkButtonScript>()[0];
        player = GameObject.FindObjectsOfType<CharacterMovement>()[0].gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject==player)
        {
            corkButtonScript.setTrigger(this.gameObject,false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject==player)
        {
            corkButtonScript.setTrigger(this.gameObject,true);
        }
    }
}
