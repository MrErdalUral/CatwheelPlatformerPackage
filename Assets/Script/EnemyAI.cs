using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Use this component to define multiple profiles for the update function calls of a game object
/// Create and add a Unity event to the EnemyStates array. On every update this script will invoke the UnityEvent
/// on the CurrentStateIndex. Modify CurrentStateIndex to switch between different profiles
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public IntUnityEvent OnBehaviourChange;
    public UnityEvent[] EnemyStates;
    public int _currentStateIndex;



    public int CurrentStateIndex
    {
        get { return _currentStateIndex; }
        set
        {

            OnBehaviourChange.Invoke(value);
            _currentStateIndex = value;
        }
    }

    public void SetEnemyState(int value)
    {
        CurrentStateIndex = value;
    }

    void Update()
    {
        if (CurrentStateIndex < EnemyStates.Length)
            EnemyStates[CurrentStateIndex].Invoke();
        else
            Debug.Log("CurrentState:" + CurrentStateIndex +" # of states:" + EnemyStates.Length);       
    }

}

