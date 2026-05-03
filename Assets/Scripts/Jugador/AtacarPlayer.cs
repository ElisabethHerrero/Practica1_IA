using UnityEngine;

public class AtacarPlayer : MonoBehaviour
{
    public Animator animator;

    [Header("Ataque")]
    public Transform attackPoint;
    public float attackRange = 1.5f;
    public int damage = 25;
    public LayerMask npcLayer;

    private bool canAttack = true;
    public float attackCooldown = 0.8f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canAttack)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        canAttack = false;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Collider[] hitNPCs = Physics.OverlapSphere(
            attackPoint.position,
            attackRange,
            npcLayer
        );

        foreach (Collider npc in hitNPCs)
        {
            VidaNpc vidaNpc = npc.GetComponent<VidaNpc>();

            if (vidaNpc != null)
            {
                vidaNpc.TakeDamage(damage);
            }
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}