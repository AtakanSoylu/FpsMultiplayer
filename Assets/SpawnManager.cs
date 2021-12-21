using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    [SerializeField] private Transform[] _spawnPointArray;

    private void Awake()
    {
        Instance = this;
    }

    public Transform GetSpawnPoint()
    {
        return _spawnPointArray[Random.Range(0, _spawnPointArray.Length)];
    }
}
