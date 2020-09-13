using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject PersistentGameObject;

    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;

        InstantiatePersistentGmobj();

        hasSpawned = true;
    }

    void InstantiatePersistentGmobj()
    {
        GameObject persistent = Instantiate(PersistentGameObject);
        DontDestroyOnLoad(persistent);
    }
}
