using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutopilotTarget : MonoBehaviour
{
    public string AutopilotObjectTag = "Player";

    public void SetAutopilotTarget()
    {
        var objects = GameObject.FindGameObjectsWithTag(AutopilotObjectTag);
        foreach (var o in objects)
        {
            var autopilotToTargetPosition = o.GetComponent<AutopilotToTargetPosition>();
            if (autopilotToTargetPosition == null) continue;
            autopilotToTargetPosition.SetTarget(transform.position);
        }
    }
}
