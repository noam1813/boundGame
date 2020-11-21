using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinPushInterval;
    [SerializeField] private bool isSpawing;

    [SerializeField] private float EndPos;
    [SerializeField] private float height;

    private IDisposable spawnObservable;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spawnObservable = Observable.Interval(TimeSpan.FromSeconds(coinPushInterval)).Subscribe(_ =>
        {
            Spawn();
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Spawn()
    {
        var obj = Instantiate(coinPrefab);
        obj.transform.position = new Vector2(Random.Range(-EndPos,EndPos*2f),height);
        
    }

    public void SpawnLoop(int loop)
    {
        for (int i = 0; i < loop; i++)
        {
            Spawn();
        }
    }
}
