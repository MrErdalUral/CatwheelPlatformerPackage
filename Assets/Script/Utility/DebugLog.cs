using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    public string DebugMessage;

    public void DebugLogMessage()
    {
        Debug.Log(DebugMessage);
    }
}
