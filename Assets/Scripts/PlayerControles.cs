using UnityEngine;

public class PlayerControles : MonoBehaviour
{
    private Atacar attackController;

    public Animator animator;

    void Start()
    {
        attackController = GetComponent<Atacar>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (attackController != null)
            {
                attackController.Attack();
            }

            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }
        }
    }
}