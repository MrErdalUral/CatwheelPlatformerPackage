using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;
    public string InteractionButton = "Use";

    private bool _canActivate;

    public bool CanActivate
    {
        get { return _canActivate; }
        set { _canActivate = value; }
    }


    // Update is called once per frame
    void Update()
    {
        if(CanActivate && Input.GetButtonDown(InteractionButton))
            OnInteract.Invoke();
    }
}
