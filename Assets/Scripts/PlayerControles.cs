using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControles : MonoBehaviour
{
    private Atacar attackController;

    // Start is called before the first frame update
    void Start()
    {
        attackController = GetComponent<Atacar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            attackController.Attack();
        }
    }
}
