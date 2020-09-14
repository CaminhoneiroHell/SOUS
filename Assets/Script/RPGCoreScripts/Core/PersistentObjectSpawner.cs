using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] PersistentGameObject;

    static bool hasSpawned = false;

    private void Awake()
    {
        if (hasSpawned) return;

        InstantiatePersistentGmobj();

        hasSpawned = true;
    }

    void InstantiatePersistentGmobj()
    {
        for(int i = 0; i < PersistentGameObject.Length; i++)
        {

            GameObject persistent =  Instantiate(PersistentGameObject[i]);
            DontDestroyOnLoad(persistent);
        }
    }
}
