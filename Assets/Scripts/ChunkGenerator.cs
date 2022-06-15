using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Chunk Parameters")]
    public int maxGeneratedChunks = 5;
    public GameObject environmentPrefab;

    [Header("Chunks")]
    public List<GameObject> chunkPrefabs = new List<GameObject>();

    private List<GameObject> generatedChunks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         TODO: THIS
         */
    }

    void GenerateNewChunk()
    {
        
    }
}
