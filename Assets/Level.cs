using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField]
    private LevelState _levelState;

    private LevelState _targetLevelState;

    public UnityEvent OnLevelReady;
    public UnityEvent OnLevelStarting;
    public UnityEvent OnLevelPlaying;
    public UnityEvent OnLevelEnded;


    public LevelState LevelState
    {
        get { return _levelState; }
        set
        {
            _targetLevelState = value;
            switch (value)
            {
                case LevelState.Ready:
                    OnLevelReady.Invoke();
                    break;
                case LevelState.Starting:
                    OnLevelStarting.Invoke();
                    break;
                case LevelState.Playing:
                    OnLevelPlaying.Invoke();
                    break;
                case LevelState.Ended:
                    OnLevelEnded.Invoke();
                    break;
            }
            _levelState = _targetLevelState;
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
        LevelState = LevelState.Ready;
    }

}

public enum LevelState
{
    Ready, Starting, Playing, Ended
}