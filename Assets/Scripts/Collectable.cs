using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float spinSpeed = 15;
    public float bobHeight = 0.25f;
    public float bobSpeedMultiplier = 2;
    public int scoreFromCollect = 500;

    private float initialYvalue;
    private bool collected = false;
    private float randomRotationOffset;

    private void Start()
    {
        randomRotationOffset = Random.value;
        initialYvalue = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotating the collectable
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

        // Making the collectable move up and down
        Vector3 tempPosition = transform.position;
        tempPosition.y = bobHeight * Mathf.Sin(Time.realtimeSinceStartup * bobSpeedMultiplier * randomRotationOffset) + initialYvalue;
        transform.position = tempPosition;
    }

    public void Collect()
    {
        collected = true;
    }

    public bool IsCollected()
    {
        return collected;
    }
}
