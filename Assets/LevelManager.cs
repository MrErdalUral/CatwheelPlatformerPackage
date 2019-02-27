using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<Level> _levels;

    private Level _currentLevel;

    public LevelUnityEvent OnCurrentLevelChanged;

    public List<Level> levels
    {
        get { return _levels; }
        set { _levels = value; }
    }

    void Start()
    {
        if (_levels != null)
            for (var i = 0; i < _levels.Count; i++)
            {
                if(_levels[i] == null) continue;
                _levels[i].OnLevelStarting.AddListener(UpdateCurrentLevel);
            }
        else
            _levels = new List<Level>();

        _currentLevel =
            _levels.FirstOrDefault(m => m.LevelState == LevelState.Starting || m.LevelState == LevelState.Playing);
        UpdateCurrentLevel(_currentLevel);
    }

    public void UpdateCurrentLevel(Level newLevel)
    {
        if (newLevel != _currentLevel)
            OnCurrentLevelChanged.Invoke(newLevel);
        _currentLevel = newLevel;
    }

}


