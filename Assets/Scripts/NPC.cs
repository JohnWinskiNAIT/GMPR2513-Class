using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    
    string dialog;
    // Start is called before the first frame update
    void Start()
    {
        dialog = "Hello, my name is Olag and this here is my treasure.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        MyEvents.StartDialog.Invoke();
        MyEvents.UpdateDialog.Invoke(dialog);
    }
}
