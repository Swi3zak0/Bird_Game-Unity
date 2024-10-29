using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Zad1 : MonoBehaviour
{
    public int numberOfBlocks = 10;
    public float delay = 2.0f;
    public GameObject block;
    public List<Material> materials;

    private List<Vector3> positions = new List<Vector3>();

    void Start()
    {
        Bounds bounds = GetComponent<Renderer>().bounds;
        for (int i = 0; i < numberOfBlocks; i++)
        {
            float randomX = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);
            positions.Add(new Vector3(randomX, -1f, randomZ));
        }

        foreach (Vector3 pos in positions)
        {
            Debug.Log(pos);
        }
        StartCoroutine(GenerujObiekt());
    }

    IEnumerator GenerujObiekt()
    {
        Debug.Log("Coroutine started");
        foreach (Vector3 pos in positions)
        {
            GameObject newBlock = Instantiate(this.block, pos, Quaternion.identity);
            if (materials.Count > 0)
            {
                Material randomMaterial = materials[UnityEngine.Random.Range(0, materials.Count)];
                newBlock.GetComponent<Renderer>().material = randomMaterial;
            }
            yield return new WaitForSeconds(this.delay);
        }
        Debug.Log("Coroutine finished");
    }
}
