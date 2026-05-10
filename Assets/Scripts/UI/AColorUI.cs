using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AColorUI : MonoBehaviour
{
    public Image image;

    private float timer = 0f;
    public float duration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / duration;

        image.color = Color.Lerp(
            Color.black,
            Color.white,
            t
        );
    }
}
