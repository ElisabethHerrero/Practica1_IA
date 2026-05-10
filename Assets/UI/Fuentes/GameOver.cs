using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    TMP_Text textMesh;

    public float waveSpeed = 2f;
    public float waveHeight = 5f;
    public float meltAmount = 8f;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();

        TMP_TextInfo textInfo = textMesh.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            float offsetY =
                Mathf.Sin(Time.time * waveSpeed + i * 0.5f)
                * waveHeight;

            float melt =
                Mathf.PerlinNoise(i * 0.2f, Time.time)
                * meltAmount;

            // vértices inferiores
            vertices[vertexIndex + 0].y -= melt;
            vertices[vertexIndex + 3].y -= melt;

            // vértices superiores
            vertices[vertexIndex + 1].y += offsetY;
            vertices[vertexIndex + 2].y += offsetY;
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices =
                textInfo.meshInfo[i].vertices;

            textMesh.UpdateGeometry(
                textInfo.meshInfo[i].mesh,
                i
            );
        }
    
    }
}
