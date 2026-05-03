using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armas : MonoBehaviour
{
    [SerializeField] public Collider Collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            VidaNpc health = other.GetComponent<VidaNpc>();
            if (health != null)
            {
                health.TakeDamage(20);
            }
        }
    }

}
