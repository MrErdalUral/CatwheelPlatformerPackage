﻿using System.Collections;
using Assets.Input_Handlers;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Use this script to spawn prefabs
/// This will spawn a [SpawnCount] amount of objects per Random.Range(TimerRange.x,TimerRange.y)
/// DelayTime controls the time between SpawnStart and instantiation of the prefab.
/// If the new object has a rigidbody2d attached, its velocity will be set to SpawnDirection * SpawnVelocity
/// If the SpawnPosition is set the instantiation will be done at SpawnPosition else the new object will be create at this script's position
/// </summary>
public class Spawner : MonoBehaviour
{
    public GameObject SpawnPrefab;
    public Vector2 TimerRange;
    public float DelayTime = 0.5f;
    public UnityEvent OnSpawnStart;
    public GameObjectUnityEvent OutputSpawnedGameobject;
    public UnityEvent OnSpawnEnd;
    public UnityEvent OnOutOfSpawns;
    public UnityEvent OnSpawnCooldown;
    public int SpawnCount = 1;
    public Transform SpawnedObjectParent;

    public int BurstCount = 1;
    public float BurstTimeDelay = 0.05f;

    [SerializeField]
    private Vector2 _spawnDirection;

    public float SpawnVelocity = 10f;
    public Transform[] SpawnPosition;

    private float _cooldown;
    private WaitForSeconds _spawnDelay;
    private WaitForSeconds _burstDelay;

    public Vector2 SpawnDirection
    {
        get { return _spawnDirection; }
        set { _spawnDirection = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnDelay = new WaitForSeconds(DelayTime);
        _burstDelay = new WaitForSeconds(BurstTimeDelay);
        ResetCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        _cooldown -= Time.deltaTime;
    }

    public void StartSpawn()
    {
        if (SpawnCount <= 0)
        {
            OnOutOfSpawns.Invoke();
            return;
        }
        if (_cooldown > 0)
        {
            return;
        }
        StartCoroutine(Spawn());

    }
    private IEnumerator Spawn()
    {
        ResetCooldown();
        SpawnCount--;
        OnSpawnStart.Invoke();
        yield return _spawnDelay;

        var pos = transform.position;
        if (SpawnPosition != null && SpawnPosition.Length > 0)
            pos = SpawnPosition[Random.Range(0, SpawnPosition.Length)].position;
        for (int i = 0; i < BurstCount; i++)
        {
            var obj = (GameObject)Instantiate(SpawnPrefab, pos, Quaternion.identity, SpawnedObjectParent);
            var body = obj.GetComponent<Rigidbody2D>();
            if (body && !body.isKinematic)
                body.velocity = SpawnDirection * SpawnVelocity;
            else
                obj.transform.position = obj.transform.position + (Vector3)SpawnDirection * SpawnVelocity;
            OutputSpawnedGameobject.Invoke(obj);
            yield return _burstDelay;
        }
        OnSpawnEnd.Invoke();
    }

    private void ResetCooldown()
    {
        _cooldown = Random.Range(TimerRange.x, TimerRange.y);
    }

    public void SetHorizontalDirection(float dir)
    {
        _spawnDirection.x = dir;
    }

    public void SetVerticalDirection(float dir)
    {
        _spawnDirection.y = dir;
    }
}
