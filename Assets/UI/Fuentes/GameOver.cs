using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * 3f) * 0.05f;

        transform.localScale =
            new Vector3(
                originalScale.x * scale,
                originalScale.y / scale,
                originalScale.z
            );
    }
}
