using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    public event Action OnPlayerInteract;
    public event Action<Vector2> OnPlayerMove;

    // Start is called before the first frame update
    void Start()
    {
        if(playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
        }
    }

    private void Update()
    {
        if (Input.GetButtonUp("Interact"))
        {
            OnPlayerInteract?.Invoke();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerMovement.Move(movementInput);
        OnPlayerMove?.Invoke(movementInput);
    }
}
