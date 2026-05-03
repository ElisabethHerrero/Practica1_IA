using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    public int vidaMax = 100;
    private int vidaActual;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMax;
    }

    public void CogerDaño(int amount)
    {
        vidaActual -= amount;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {

    }

}
