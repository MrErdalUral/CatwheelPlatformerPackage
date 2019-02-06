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

    [SerializeField]
    private Camera _mainCamera;

    private Level _currentLevel;

    public GameObjectUnityEvent OnCurrentLevelChanged;

    public List<Level> levels
    {
        get { return _levels; }
        set { _levels = value; }
    }

    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        if(_levels != null)
            for (var i = 0; i < _levels.Count; i++)
            {
                _levels[i].OnLevelStarting.AddListener(UpdateCurrentLevel);
            }
        else
            _levels = new List<Level>();
    }

    public void UpdateCurrentLevel()
    {
        var newCurrentLevel = levels.FirstOrDefault(m => m.LevelState == LevelState.Playing || m.LevelState == LevelState.Starting);
        if (newCurrentLevel == null)
        {
            Debug.Log("No level on playing state");
            return;
        }
        if(newCurrentLevel != _currentLevel)
            OnCurrentLevelChanged.Invoke(newCurrentLevel.gameObject);
        _currentLevel = newCurrentLevel;
    }

    public void SetCamera(GameObject levelGameObject)
    {
        var movement = _mainCamera.GetComponent<KinematicLerpMovement>();
        if (movement == null)
        {
            movement = _mainCamera.gameObject.AddComponent<KinematicLerpMovement>();
            movement.MoveTime = 4;
        }

        movement.SetTargetPosition2D(levelGameObject.transform.position);
    }
}
