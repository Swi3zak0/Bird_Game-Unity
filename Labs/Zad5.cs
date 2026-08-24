using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int cubeCount = 10;
    public float planeSize = 10f;

    private List<Vector3> usedPositions = new List<Vector3>();

    void Start()
    {
        GenerateCubes();
    }

    void GenerateCubes()
    {
        int createdCubes = 0;

        while (createdCubes < cubeCount)
        {
            float x = Random.Range(-planeSize / 2 + 0.5f, planeSize / 2 - 0.5f);
            float z = Random.Range(-planeSize / 2 + 0.5f, planeSize / 2 - 0.5f);
            Vector3 randomPosition = new Vector3(x, 0.5f, z);

            if (!usedPositions.Contains(randomPosition))
            {
                Instantiate(cubePrefab, randomPosition, Quaternion.identity);
                usedPositions.Add(randomPosition);
                createdCubes++;
            }
        }
    }
}
