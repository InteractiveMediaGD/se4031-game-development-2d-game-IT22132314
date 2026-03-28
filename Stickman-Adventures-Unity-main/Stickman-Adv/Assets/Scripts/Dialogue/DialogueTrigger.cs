using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogManager dialogManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
		    dialogManager.InitDialog(dialogue);
            Destroy(gameObject, 0.02f);
        }        
    }

}
