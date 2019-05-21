using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public float LevelCameraSize = 72;
    public Transform LevelCameraPosition;
    public float LevelSize;
    [SerializeField]
    private LevelState _levelState;

    private LevelState _targetLevelState;

    public LevelUnityEvent OnLevelReady;
    public LevelUnityEvent OnLevelStarting;
    public LevelUnityEvent OnLevelPlaying;
    public LevelUnityEvent OnLevelEnded;

    public Transform LeftBorder;
    public Transform RightBorder;
    public Transform LeftExit;
    public Transform RightExit;

    public LevelState LevelState
    {
        get { return _levelState; }
        set
        {
            if(_levelState == value) return;
            _targetLevelState = value;
            ApplyState(value);
            _levelState = _targetLevelState;
        }
    }

    private void ApplyState(LevelState value)
    {
        switch (value)
        {
            case LevelState.Ready:
                OnLevelReady.Invoke(this);
                break;
            case LevelState.Starting:
                OnLevelStarting.Invoke(this);
                break;
            case LevelState.Playing:
                OnLevelPlaying.Invoke(this);
                break;
            case LevelState.Ended:
                OnLevelEnded.Invoke(this);
                break;
        }
    }

    public void SetEnded()
    {
        LevelState = LevelState.Ended;
    }

    public void SetPlaying()
    {
        LevelState = LevelState.Playing;
    }
    public void SetStarting()
    {
        LevelState = LevelState.Starting;
    }

    public void SetReady()
    {
        LevelState = LevelState.Ready;
    }

    void Awake()
    {
        ApplyState(_levelState);
        SetBorderPositions();
        var levelManager = FindObjectOfType<LevelManager>();
        if (levelManager)
            levelManager.RegisterLevel(this);
    }

    private void SetBorderPositions()
    {
        LeftBorder.transform.localPosition = new Vector3(-LevelSize/2,0,0);
        LeftExit.transform.position = LeftBorder.transform.position;
        RightBorder.transform.localPosition = new Vector3(LevelSize/2,0,0);
        RightExit.transform.position = RightBorder.transform.position;
    }
}

public enum LevelState
{
    Ready, Starting, Playing, Ended
}