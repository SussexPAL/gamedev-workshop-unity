using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObject; // The object to spawn
    [SerializeField] float interSpawnTime = 1; // Secods between each spawn
    //[SerializeField] Vector3 spawnPosOffset = Vector3(0, 0, 0); // How far to spawn the object from the spawner's position
    float timeSinceLastSpawn = 0;
    Transform spawnerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > interSpawnTime)
        {
            timeSinceLastSpawn = 0;
            Instantiate(spawnObject, spawnerTransform.position, Quaternion.identity);
        }
    }
}
