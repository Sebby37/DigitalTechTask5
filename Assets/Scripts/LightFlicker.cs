using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    static float chanceToFlicker = 0.001f;

    private float offTimer;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.intensity = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value <= chanceToFlicker && light.enabled)
        {
            offTimer = 0;
            light.enabled = false;
        }

        if (!light.enabled)
        {
            offTimer += Time.deltaTime;
        }

        if (offTimer > Random.value)
        {
            light.enabled = true;
        }
    }
}
