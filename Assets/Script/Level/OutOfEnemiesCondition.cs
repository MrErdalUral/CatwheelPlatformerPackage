using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OutOfEnemiesCondition : MonoBehaviour
{
    public UnityEvent OnOutOfEnemies;
    [SerializeField]
    private int _enemyCount;

    public Spawner[] EnemySpawners;
    public string EnemyTag = "Enemy";

    public int EnemyCount
    {
        get
        {
            return _enemyCount;
        }
        set
        {
            _enemyCount = value;
            if (value < 1) OnOutOfEnemies.Invoke();

        }
    }

    void Start()
    {
        _enemyCount = EnemySpawners.Sum(m => m.SpawnCount) + GameObject.FindGameObjectsWithTag(EnemyTag).Length;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyCount = EnemySpawners.Sum(m=>m.SpawnCount) + GameObject.FindGameObjectsWithTag(EnemyTag).Length;
    }

    public void SetSpawnersActive()
    {
        foreach (var enemySpawner in EnemySpawners)
        {
            enemySpawner.gameObject.SetActive(true);
        }
    }
}
