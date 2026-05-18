using UnityEngine;

public class PlayerControles : MonoBehaviour
{
    private Animator animator;

    public Atacar atacar;

    public void SetAnimator(Animator nuevoAnimator)
    {
        animator = nuevoAnimator;
        Debug.Log("PlayerControls recibió Animator: " + animator.gameObject.name);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("HE PULSADO LA E");

            if (animator == null)
            {
                Debug.LogError("No hay Animator asignado en PlayerControles.");
                return;
            }

            Debug.Log("Ataque enviado al Animator: " + animator.gameObject.name);

            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");

            if (atacar != null)
            {
                atacar.Attack();
            }
            else
            {
                Debug.LogWarning("No hay script Atacar asignado en PlayerControles.");
            }
        }
    }
}