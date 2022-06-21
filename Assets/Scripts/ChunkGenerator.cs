using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [Header("Chunk Parameters")]
    public GameObject player;
    public float chunkLength = 20f;
    public int maxGeneratedChunks = 5;
    [Range(0f, 1f)]
    public float chanceToGenerateObject = 0.5f;

    [Header("Chunk Objects")]
    public GameObject environment;
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> collectables = new List<GameObject>();

    private int nextChunkToLoad = 1;
    private List<GameObject> loadedChunks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxGeneratedChunks; i++)
            GenerateNewChunk();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.transform.position.x - chunkLength > loadedChunks[0].transform.position.x)
        {
            // Creating a new chunk
            GenerateNewChunk();

            // Destroying the first loaded chunk
            Destroy(loadedChunks[0]);
            loadedChunks.RemoveAt(0);
        }
    }

    void GenerateNewChunk()
    {
        // Creating the chunk parent and environment
        GameObject chunk = new GameObject($"Chunk #{nextChunkToLoad}");

        chunk.transform.position = new Vector3(chunkLength * nextChunkToLoad, 0, 0);
        GameObject environmentObject = Instantiate(environment, chunk.transform.position, Quaternion.identity);
        
        environmentObject.transform.SetParent(chunk.transform);

        // Loop for the amount of times an obstacle is to be created
        int amountOfLanes = GameManager.maximumLeftLanes + 1 + GameManager.maximumRightLanes;
        int maxObjectsInChunk = (int) (amountOfLanes * 5/*(chunkLength / GameManager.distanceBetweenObjects)*/);

        List<Vector2> generatedObjects = new List<Vector2>();

        for (int i = 0; i < maxObjectsInChunk; i++)
        {
            if (Random.value <= chanceToGenerateObject)
            {
                // Calculating the lane and row the obstacles are to be placed in
                float obstacleLane;
                float obstacleRow;
                while (true)
                {
                    obstacleLane = new float[] { -1.5f, 0, 1.5f }[Random.Range(0, 3)];
                    obstacleRow = new float[] { -10, -5, 0, 5, 10 }[Random.Range(0, 5)];

                    Vector2 tempPosition = new Vector2(obstacleLane, obstacleRow);
                    if (!generatedObjects.Contains(tempPosition))
                    {
                        generatedObjects.Add(tempPosition);
                        break;
                    }
                }
                Debug.Log($"{generatedObjects.Count} {maxObjectsInChunk}");
                
                if (Random.value <= 0.5f)
                {
                    // Generate collectable
                    GameObject collectableToGenerate = collectables[Random.Range(0, collectables.Count)];

                    collectableToGenerate = Instantiate(collectableToGenerate, Vector3.zero, Quaternion.identity);
                    collectableToGenerate.transform.SetParent(chunk.transform);

                    collectableToGenerate.transform.localPosition = new Vector3(obstacleRow, 0.75f, obstacleLane);
                }
                else
                {
                    // Generate obstacle
                    GameObject obstacleToGenerate = obstacles[Random.Range(0, obstacles.Count)];

                    obstacleToGenerate = Instantiate(obstacleToGenerate, Vector3.zero, Quaternion.identity);
                    obstacleToGenerate.transform.SetParent(chunk.transform);

                    obstacleToGenerate.transform.localPosition = new Vector3(obstacleRow, 0.75f, obstacleLane);
                }
            }
        }

        // Adding the final chunk to the loaded chunks list
        loadedChunks.Add(chunk);
        nextChunkToLoad++;
    }
}
