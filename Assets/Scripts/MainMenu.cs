using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Transform targetCamera;
    [SerializeField] private float distance = 2f;
    [SerializeField] private Vector2 canvasSize = new Vector2(1920, 1080);

    SerializeField canva; 
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(true);
        AjustaCam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void AjustaCam()
    {
        if (targetCamera == null)
            targetCamera = Camera.main.transform;

        Camera cam = Camera.main;

        float height = 2f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * cam.aspect;

        Transform t = canvas.transform;

        t.position = targetCamera.position + targetCamera.forward * distance;
        t.rotation = targetCamera.rotation;

        t.localScale = new Vector3(width / canvasSize.x, height / canvasSize.y, 1f);
    }
}
