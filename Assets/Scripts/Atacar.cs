using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour
{
    public Collider attackCollider;

    public void Attack()
    {
        StartCoroutine(AttackRoutine());
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.2f);

        attackCollider.enabled = false;
    }
}
