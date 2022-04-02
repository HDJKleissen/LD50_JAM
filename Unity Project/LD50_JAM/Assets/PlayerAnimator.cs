using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] PlayerInputHandler player;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (player == null)
        {
            player = GetComponent<PlayerInputHandler>();
        }
        player.OnPlayerMove += UpdateAnimator;
        player.OnPlayerInteract += TriggerInteract;
    }

    private void OnDestroy()
    {
        player.OnPlayerMove -= UpdateAnimator;
        player.OnPlayerInteract -= TriggerInteract;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void TriggerInteract()
    {
        animator.SetTrigger("Interact");
    }

    void UpdateAnimator(Vector2 movement)
    {
        if (movement.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        animator.SetInteger("HorizontalSpeed", (int)movement.x);
        animator.SetInteger("VerticalSpeed", (int)movement.y);
    }
}
