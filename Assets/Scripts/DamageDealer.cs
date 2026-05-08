using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;
    public GameObject owner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;

        Vida health = other.GetComponent<Vida>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
